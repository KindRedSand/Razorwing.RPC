using Razorwing.RPC.Extensions;
using Razorwing.RPC.Factory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Razorwing.RPC.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Razorwing.RPC
{
    public class RPCManager : Mod
    {
        public const string I_Type = "typ";
        public const string I_ParamCount = "prc";
        public const string I_Owner = "own";
        public const string I_MethodName = "mtd";
        public const string I_Target = "trg";
        internal const string I_ExecutingSide = "exs";

        private Dictionary<Type, IIdentityProvider> _identityProviders;
        private Dictionary<string, IIdentityProvider> _objectProviders;
        private Dictionary<string, MethodInfo> _methods;

        public static RPCManager Inst;

        public override void Load()
        {
            Inst = this;
            Initialize();
        }

        public override void Unload()
        {
            base.Unload();
            Inst = null;
            dev.instance = null;
        }

        public void Initialize()
        {
            _identityProviders = new Dictionary<Type, IIdentityProvider>(); 
            _objectProviders = new Dictionary<string, IIdentityProvider>();
            _methods = new Dictionary<string, MethodInfo>();
            dev.instance = new dev();
            Stopwatch clock = new Stopwatch();

            Logger.Info("Begin initialization of RPC Manager");
            clock.Start();

            foreach (var mod in ModLoader.Mods)
            {
                var types = mod.GetType().Assembly.DefinedTypes;
                foreach (var it in types)
                {
                    if (it.ImplementedInterfaces.Contains(typeof(IIdentityProvider)))
                    {
                        try
                        {
                            Logger.Info($"Registering {it.Name} provider from {mod.Name} mod");
                            var provider = (IIdentityProvider)Activator.CreateInstance(it);
                            var workingTypes = provider.workingTypes;
                            foreach (Type workingType in workingTypes)
                            {
                                if(!_identityProviders.ContainsKey(workingType))
                                    _identityProviders.Add(workingType, provider);
                                else
                                {
                                    Logger.Warn($"Provider for {workingType.Name} are already registered in" +
                                                $" {_identityProviders[workingType].GetType().Assembly.FullName}!");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Fatal($"Failed to load {it.Name} during initialization process!",e);
                        }
                    }
                    else
                    {
                        foreach (var mtd in it.DeclaredMethods)
                        {
                            if (mtd.CustomAttributes.Any(attr => attr.AttributeType == typeof(RPCCallableAttribute)))
                            {
                                loadMethod(mtd);
                            }
                        }
                    }
                }
            }

            foreach (var it in _identityProviders)
            {
                _objectProviders.Add(it.Key.Name, it.Value);
    
            }

            clock.Stop();
            Logger.Info($"Done loading providers in {clock.ElapsedMilliseconds}ms!");
        }

        public Identity GetIdentity(object target)
        {
            var baseType = target.GetType();
            while (baseType!= typeof(object) && baseType != null)
            {
                if (_identityProviders.ContainsKey(baseType))
                {
                    return _identityProviders[baseType].GetIdentity(target);
                }
                baseType = baseType.BaseType;
            }
            throw new NotImplementedException($"Identity provider for {target.GetType().Name} not registered!");
        }

        public object RestoreObject(Identity identity)
        {
            if (_objectProviders.ContainsKey(identity.Type))
            {
                return _objectProviders[identity.Type].GetObject(identity);
            }
            throw new NotImplementedException($"Identity provider for {identity.Type} not registered!");
        }

        internal static object ICall(object target, MethodInfo method, ExecutingSide side = ExecutingSide.Both, params object[] args)
        { 
            Inst.Call(target, method, side, args);
            return null;
        }
        
        internal static void ICall(object target, MethodInfo method, params object[] args)
            => Inst.Call(target, method, ExecutingSide.Both, args);

        internal void Call(object target, MethodInfo method, ExecutingSide side = ExecutingSide.Both, params object[] args)
        {
            if (Main.netMode == NetmodeID.SinglePlayer && !side.HasFlag(ExecutingSide.DenySender) )
            {
                method.Invoke(target, args);
                return;
            }

            var methodName = $"{method.ReflectedType.Name}.{method.Name}";
            if (!_methods.ContainsKey(methodName))
            {
                loadMethod(method);
            }
            Identity targetIdentity = GetIdentity(target);
            Identity tag = new Identity("target", "rpc")
            {
                [I_Target] = targetIdentity.ToTag(),
                [I_MethodName] = methodName,
                [I_ExecutingSide] = (byte)side,
            };
            
            if (args.Length > 0)
            {
                tag.Add(I_ParamCount, args.Length);
                for (int i = 0; i < args.Length; i++)
                {
                    var ind = GetIdentity(args[i]);
                    tag.Add($"pr{i}", ind.ToTag());
                }
            }
            else
            {
                tag.Add(I_ParamCount, 0);
            }

            //NetSend
            Logger.Info($"Sending RPC call to server. \nFor: {tag[I_Target]}, Method: {tag[I_MethodName]},\n With args: {args.Length}. Executing side: {side}");
            var packet = GetPacket();
            packet.Write(tag);
            packet.Send();
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            var data = reader.ReadIdentity();

            Logger.Info($"Received RPC packet. For: {data[I_Target]}, method: {data[I_MethodName]}, Executing side: {data.ExecutingSide.ToString()}");
            if (Main.netMode == NetmodeID.Server)
            {
                //Resend packet to clients
                if(data.ExecutingSide.HasFlag(ExecutingSide.Client))
                {
                    var packet = GetPacket();
                    packet.Write(data);
                    if(data.ExecutingSide.HasFlag(ExecutingSide.DenySender))
                        packet.Send(-1, whoAmI);
                    else
                        packet.Send();
                }

                if (data.ExecutingSide.HasFlag(ExecutingSide.Server))
                {
                    var target = data.GetCompound(I_Target).ToIdentity();
                    var method = data.GetString(I_MethodName);
                    var parc = data.GetInt(I_ParamCount);
                    if (parc > 0)
                    {
                        Identity[] arr = new Identity[parc];
                        for (int i = 0; i < parc; i++)
                        {
                            arr[i] = data.GetCompound($"pr{i}").ToIdentity();
                        }

                        RemoteCall(target, method, arr);
                    }
                    else
                    {
                        RemoteCall(target, method);
                    }
                }
            }
            else
            {
                if (data.ExecutingSide.HasFlag(ExecutingSide.Client))
                {
                    var target = data.GetCompound(I_Target).ToIdentity();
                    var method = data.GetString(I_MethodName);
                    var parc = data.GetInt(I_ParamCount);
                    if (parc > 0)
                    {
                        Identity[] arr = new Identity[parc];
                        for (int i = 0; i < parc; i++)
                        {
                            arr[i] = data.GetCompound($"pr{i}").ToIdentity();
                        }

                        RemoteCall(target, method, arr);
                    }
                    else
                    {
                        RemoteCall(target, method);
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Received packet for call what not intended to be called clientside!");
                }
            }
        }

        internal void RemoteCall(Identity targetIdentity, string methodName, params Identity[] args)
        {
            var target = RestoreObject(targetIdentity);
            if (!_methods.ContainsKey(methodName))
            {
                var method = target.GetType()
                    .GetMethod(methodName.Split('.')
                    .Last());
                loadMethod(method);
            }

            List<object> param = null;
            if (args.Length > 0)
            {
                param = new List<object>();

                for (int i = 0; i < args.Length; i++)
                {
                    param.Add(RestoreObject(args[i]));
                }
            }
            Logger.Info($"Performing call {methodName} with {args.Length} args...");
            _methods[methodName].Invoke(target, param?.ToArray());
        }

        private void loadMethod(MethodInfo method)
        {
            var methodName = $"{method.ReflectedType.Name}.{method.Name}";
            if (_methods.ContainsKey(methodName))
            {
                throw new InvalidOperationException($"Method {methodName}\nAlready loaded! RPC possibly corrupted!");
            }
            Logger.Info($"Method {methodName} loaded");
            _methods.Add(methodName, method);
        }
    }

    [Flags]
    public enum ExecutingSide : byte
    {
        Client = 1 << 0,
        Server = 1 << 1,
        DenySender = 1 << 2,
        Both = Client | Server,
    }
}

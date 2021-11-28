using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Razorwing.RPC.Factory;
using Terraria.ModLoader;

namespace Razorwing.RPC.Providers
{
    public class ModWorldProvider : IIdentityProvider
    {
        public Type[] workingTypes => new[] {typeof(ModWorld)};
        public Identity GetIdentity(object input)
        {
            if (input is ModWorld w)
            {
                return new Identity(nameof(ModWorld), w.mod.Name, w.Name);
            }

            throw new InvalidOperationException($"{input.GetType().Name} Cannot be handled in {nameof(ModWorldProvider)} (possibly {nameof(RPCManager)}) fault");
        }

        public object GetObject(Identity identity)
        {
            if (identity.Type == nameof(ModWorld))
            {
                var mod = ModLoader.GetMod(identity.Owner);
                return mod.GetModWorld(identity.GetString("val"));
            }

            throw new InvalidOperationException($"{identity.Type} Cannot be handled in {nameof(ModWorldProvider)} (possibly {nameof(RPCManager)}) fault");
        }

        public int Weight { get; }
    }
}

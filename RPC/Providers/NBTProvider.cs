using Razorwing.RPC.Factory;
using System;

namespace Razorwing.RPC.Providers
{
    public class NBTProvider : IIdentityProvider
    {
        public Type[] workingTypes => new[] {typeof(Identity)};
        public Identity GetIdentity(object input)
        {
            return new Identity()
            {
                [RPCManager.I_Owner] = "rpc",
                [RPCManager.I_Type] = "nbt",
                ["val"] = input,
            };
        }

        public object GetObject(Identity identity)
        {
            return identity.GetCompound("val");
        }

        public int Weight => 3;
    }
}

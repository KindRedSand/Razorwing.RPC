using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Razorwing.RPC.Factory;
using Terraria.ModLoader;

namespace Razorwing.RPC.Providers
{
    public class ModProvider : IIdentityProvider
    {
        public Type[] workingTypes => new []{typeof(Mod)};
        public Identity GetIdentity(object input)
        {
            if (input is Mod mod)
            {
                return new Identity(nameof(Mod), "rpc", mod.Name);
            }
            throw new InvalidOperationException($"{input.GetType().Name} Cannot be handled in {nameof(ModProvider)} (possibly {nameof(RPCManager)}) fault");
        }

        public object GetObject(Identity identity)
        {
            if (identity.Type == nameof(Mod))
            {
                ModLoader.GetMod(identity.GetString("val"));
            }
            throw new InvalidOperationException($"{identity.Type} Cannot be handled in {nameof(ModProvider)} (possibly {nameof(RPCManager)}) fault");

        }

        public int Weight => 3;
    }
}

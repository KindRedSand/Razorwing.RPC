using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Razorwing.RPC.Factory;
using Terraria;
using Terraria.ModLoader;

namespace Razorwing.RPC.Providers
{
    public class NPCProvider :IIdentityProvider

    {
        public Type[] workingTypes => new[] {typeof(NPC), typeof(ModNPC)};
        public Identity GetIdentity(object input)
        {
            switch (input)
            {
                case NPC npc:
                    return new Identity(nameof(NPC), "rpc", npc.whoAmI);
                case ModNPC mnpc:
                    return new Identity(nameof(ModNPC), mnpc.mod.Name, mnpc.npc.whoAmI);
                default:
                    throw new InvalidOperationException($"{input.GetType().Name} Cannot be handled in {nameof(NPCProvider)} (possibly {nameof(RPCManager)}) fault");
            }
        }

        public object GetObject(Identity identity)
        {
            if (identity.Type == nameof(NPC))
            {
                return Main.npc[identity.GetInt("val")];
            }
            else if(identity.Owner != "rpc")
            {
                return Main.npc[identity.GetInt("val")].modNPC;
            }
            throw new InvalidOperationException($"{identity.Type} Cannot be handled in {nameof(NPCProvider)} (possibly {nameof(RPCManager)}) fault");
        }

        public int Weight => 3;
    }
}

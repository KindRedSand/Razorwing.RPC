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
    public class PlayerProvider : IIdentityProvider
    {
        public Type[] workingTypes => new[] { typeof(Player), typeof(ModPlayer)};
        public Identity GetIdentity(object input)
        {
            switch (input)
            {
                case Player pl:
                    return new Identity(nameof(Player), "rpc", pl.whoAmI);
                case ModPlayer mpl:
                    return new Identity(nameof(ModPlayer), mpl.mod.Name, mpl.player.whoAmI)
                    {
                        ["stype"] = mpl.Name,
                    };
                default:
                    throw new InvalidOperationException($"{input.GetType().Name} Cannot be handled in {nameof(PlayerProvider)} (possibly {nameof(RPCManager)}) fault");
            }
        }

        public object GetObject(Identity identity)
        {
            switch (identity.Type)
            {
                case nameof(Player):
                    return Main.player[identity.GetInt("val")];
                case nameof(ModPlayer):
                {
                    var id = identity.GetInt("val");
                    var mp = identity.GetString("stype");
                    return Main.player[id].GetModPlayer(ModLoader.GetMod(identity.Owner), mp);
                }

                default:
                    throw new InvalidOperationException($"{identity.Type} Cannot be handled in {nameof(PlayerProvider)} (possibly {nameof(RPCManager)}) fault");
            }
        }

        public int Weight => 3;
    }
}

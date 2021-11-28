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
    public class ProjectileProvider : IIdentityProvider
    {
        public Type[] workingTypes => new[] { typeof(Projectile), typeof(ModProjectile) };
        public Identity GetIdentity(object input)
        {
            switch (input)
            {
                case Projectile proj:
                    return new Identity(nameof(Projectile), "rpc", proj.whoAmI);
                case ModProjectile mprj:
                    return new Identity(nameof(ModProjectile), mprj.mod.Name, mprj.projectile.whoAmI);
                default:
                    throw new InvalidOperationException($"{input.GetType().Name} Cannot be handled in {nameof(ProjectileProvider)} (possibly {nameof(RPCManager)}) fault");
            }
        }

        public object GetObject(Identity identity)
        {
            if (identity.Type == nameof(Projectile))
            {
                return Main.projectile[identity.GetInt("val")];
            }
            else if (identity.Owner != "rpc")
            {
                return Main.projectile[identity.GetInt("val")].modProjectile;
            }
            throw new InvalidOperationException($"{identity.Type} Cannot be handled in {nameof(NPCProvider)} (possibly {nameof(RPCManager)}) fault");
        }

        public int Weight => 3;
    }
}

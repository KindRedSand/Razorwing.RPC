using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Razorwing.RPC.Factory;

namespace Razorwing.RPC.Providers
{
    public class BaseStructProvider : IIdentityProvider
    {
        public Type[] workingTypes => new[] {typeof(Vector2), typeof(Rectangle), typeof(Color) };
        public Identity GetIdentity(object input)
        {
            switch (input)
            {
                case Vector2 vec:
                    return new Identity(nameof(Vector2), "rpc")
                    {
                        ["x"] = vec.X,
                        ["y"] = vec.Y,
                    };
                case Rectangle rec:
                    return new Identity(nameof(Rectangle), "rpc")
                    {
                        ["x"] = rec.X,
                        ["y"] = rec.Y,
                        ["w"] = rec.Width,
                        ["h"] = rec.Height,
                    };
                case Color c:
                    return new Identity(nameof(Color), "rpc")
                    {
                        ["r"] = c.R,
                        ["g"] = c.G,
                        ["b"] = c.B,
                        ["a"] = c.A,
                    };
                default:
                    throw new InvalidOperationException($"{input.GetType().Name} Cannot be handled in {nameof(BaseStructProvider)} (possibly {nameof(RPCManager)}) fault");
            }
        }

        public object GetObject(Identity identity)
        {
            if (identity.Type == nameof(Vector2))
            {
                var x = identity.GetFloat("x");
                var y = identity.GetFloat("y");
                return new Vector2(x,y);
            }
            else if (identity.Type == nameof(Rectangle))
            {
                var x = identity.GetInt("x");
                var y = identity.GetInt("y");
                var w = identity.GetInt("w");
                var h = identity.GetInt("h");
                return new Rectangle(x,y,w,h);
            }else if (identity.Type == nameof(Color))
            {
                var r = identity.GetInt("r");
                var g = identity.GetInt("g");
                var b = identity.GetInt("b");
                var a = identity.GetInt("a");
                return new Color(r,g,b,a);
            }

            throw new InvalidOperationException($"{identity.Type} Cannot be handled in {nameof(BaseStructProvider)} (possibly {nameof(RPCManager)}) fault");
        }
        public int Weight => 3;
    }
}

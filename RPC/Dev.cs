using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Razorwing.RPC.Factory;

namespace Razorwing.RPC
{
    public class dev
    {
        internal static dev instance;
        public static dev Null => instance;
    }

    public class NullProvider : IIdentityProvider
    {
        public Type[] workingTypes => new[] {typeof(dev)};
        public Identity GetIdentity(object input)
        {
            return new Identity(nameof(dev), "rpc");
        }

        public object GetObject(Identity identity)
        {
            return null;
        }
    }

}

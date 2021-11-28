using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Razorwing.RPC.Factory
{
    public interface IIdentityProvider
    {
        Type[] workingTypes { get; }

        Identity GetIdentity(object input);

        object GetObject(Identity identity);

        //int Weight { get; }
    }
}

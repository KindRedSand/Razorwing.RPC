using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Razorwing.RPC.Attributes
{

    /// <summary>
    /// Mark method for RPC autoloading.
    /// Instead compiling at runtime marked method will be compiled at loading state
    /// </summary>
    public class RPCCallableAttribute : Attribute
    {

    }
}

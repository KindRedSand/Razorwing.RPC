using Razorwing.RPC.Factory;
using System;

namespace Razorwing.RPC.Providers
{
    public class BaseTypeProvider : IIdentityProvider
    {
        public Type[] workingTypes => new[] { typeof(int) , typeof(float) , typeof(string) , typeof(double) , typeof(long) };
        public Identity GetIdentity(object input)
        {
            var name = input.GetType().Name;
            switch (input)
            {
                case int _:
                case float _:
                case string _:
                case double _:
                case long _:
                    return new Identity(name, "rpc", input);
                default:
                    throw new InvalidOperationException($"{input.GetType().Name} Cannot be handled in {nameof(BaseTypeProvider)} (possibly {nameof(RPCManager)}) fault");
            }
        }

        public object GetObject(Identity identity)
        {
            switch (identity.Type)
            {
                case "Int32":
                    return identity.GetInt("val");
                case "Float":
                    return identity.GetFloat("val");
                case "String":
                    return identity.GetString("val");
                case "Double":
                    return identity.GetDouble("val");
                case "Long":
                    return identity.GetLong("val");

                default:
                    throw new InvalidOperationException($"{identity.Type} Cannot be handled in {nameof(BaseTypeProvider)} (possibly {nameof(RPCManager)}) fault");
            }
        }

        public int Weight => 1;
    }
}

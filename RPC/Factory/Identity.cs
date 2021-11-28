using IL.Terraria.GameContent.UI.Chat;
using Terraria.ModLoader.IO;

namespace Razorwing.RPC.Factory
{

    public class Identity : TagCompound
    {

        public Identity()
        {
            
        }

        public Identity(string type, string owner)
        {
            Add(RPCManager.I_Owner, owner);
            Add(RPCManager.I_Type, type);
        }

        public Identity(string type, string owner, object value)
        {
            Add(RPCManager.I_Owner, owner);
            Add(RPCManager.I_Type, type);
            Add("val", value);
        }

        public string Owner => GetString(RPCManager.I_Owner);
        public string Type => GetString(RPCManager.I_Type);

        internal ExecutingSide ExecutingSide
        {
            get => (ExecutingSide) GetByte(RPCManager.I_ExecutingSide);
            set => this[RPCManager.I_ExecutingSide] = value;
        }



    }

    public static class IdentityExtension
    {
        internal static TagCompound ToTag(this Identity tag)
        {
            var output = new TagCompound();
            foreach (var it in tag)
            {
                output.Add(it.Key, it.Value);
            }

            return output;
        }

        internal static Identity ToIdentity(this TagCompound tag)
        {
            var output = new Identity();
            foreach (var it in tag)
            {
                output.Add(it.Key, it.Value);
            }

            return output;
        }
    }
}

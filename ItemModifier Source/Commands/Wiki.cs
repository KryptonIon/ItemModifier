using System.Diagnostics;
using System.Net;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Wiki : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "wiki";

        public override string Description => "Opens up the wiki";

        public override string Usage => "/wiki (Optional)[Search]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var replyColor = Config.replyColor;

            if (args.Length == 0)
            {
                caller.Reply("Opening wiki... expect minor lag", replyColor);
                Process.Start("https://github.com/KryptonIon/ItemModifier/wiki");
            }
            else
            {
                string temp = "";

                for (int i = 0; i < args.Length; i++)
                {
                    temp += args[i];
                    if (i < args.Length - 1)
                    {
                        temp += " ";
                    }
                }
                caller.Reply("Opening wiki... expect minor lag", replyColor);
                Process.Start($"https://github.com/KryptonIon/ItemModifier/search?q={WebUtility.UrlEncode(temp)}&type=Wikis");
            }
        }
    }
}
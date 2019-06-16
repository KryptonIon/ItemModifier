using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class AutoReuse : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "autoreuse";

        public override string Description => "Gets the data of an Item(item.autoReuse) or modifies it";

        public override string Usage => "/au (Optional)[AutoReuse]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.autoReuse == true)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is AutoReuse", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not AutoReuse", replyColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyAutoReuse(caller, MouseItem, args[0]);
                    return;
                }
            }
            else
            {
                caller.Reply("No Item Selected", errorColor);
                return;
            }
        }
    }

    public class AutoReuseA1 : AutoReuse
    {
        public override string Command => "au";

        public override string Description => "Command Alias";
    }
}
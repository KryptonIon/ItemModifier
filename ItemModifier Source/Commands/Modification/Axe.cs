using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Axe : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "axe";

        public override string Description => "Gets the data of an Item(item.axe) or modifies it";

        public override string Usage => "/a (Optional)[Axe Power]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.axe != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Axe Power is {MouseItem.axe}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not an Axe", replyColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyAxe(caller, MouseItem, args[0]);
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

    public class AxeA1 : Axe
    {
        public override string Command => "a";

        public override string Description => "Command Alias";
    }
}
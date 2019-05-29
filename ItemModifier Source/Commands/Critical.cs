using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Critical : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "c";

        public override string Description => "Gets the data of an Item(item.crit) or modifies it";

        public override string Usage => "/c [Optional]<Critical Strike Chance>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.crit != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Critical Strike Chance is {MouseItem.crit}", replyColor);
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't have any critical strike chance", replyColor);
                    }
                }
                else
                {
                    int c;
                    if (!int.TryParse(args[0], out c))
                    {
                        caller.Reply($"Error, Critical Strike Chance({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        MouseItem.crit = c;
                        caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s Critical Strike Chance to {args[0]}", replyColor);
                        return;
                    }
                }
            }
            else
            {
                caller.Reply("No Item Selected", errorColor);
                return;
            }
        }
    }
}
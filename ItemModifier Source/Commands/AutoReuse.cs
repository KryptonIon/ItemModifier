using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class AutoReuse : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "au";

        public override string Description => "Gets the data of an Item(item.autoReuse) or modifies it";

        public override string Usage => "/au [Optional]<AutoReuse>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    caller.Reply($"{Modifier.GetItem2(MouseItem)}'s AutoReuse is {MouseItem.autoReuse}", replyColor);
                }
                else
                {
                    bool au;
                    if (!bool.TryParse(args[0], out au))
                    {
                        caller.Reply($"Error, AutoReuse({args[0]}) must be a bool(true/false)", errorColor);
                    }
                    else
                    {
                        MouseItem.autoReuse = au;
                        caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s AutoReuse to {args[0]}", replyColor);
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
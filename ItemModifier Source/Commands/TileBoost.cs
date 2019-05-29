using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class TileBoost : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "tb";

        public override string Description => "Gets the data of an Item(item.tileBoost) or modifies it";

        public override string Usage => "/tb [Optional]<TileBoost>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.tileBoost != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s TileBoost is {MouseItem.tileBoost}", replyColor);
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't have additional range", replyColor);
                    }
                }
                else
                {
                    int tb;
                    if (!int.TryParse(args[0], out tb))
                    {
                        caller.Reply($"Error, TileBoost({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        MouseItem.tileBoost = tb;
                        caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s TileBoost to {args[0]}", replyColor);
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
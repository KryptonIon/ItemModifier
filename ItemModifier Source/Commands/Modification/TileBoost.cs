using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class TileBoost : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "tileboost";

        public override string Description => "Gets the data of an Item(item.tileBoost) or modifies it";

        public override string Usage => "/tb (Optional)[TileBoost]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.tileBoost != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s TileBoost is {MouseItem.tileBoost}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't have additional range", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyTileBoost(caller, MouseItem, args[0]);
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

    public class TileBoostA1 : TileBoost
    {
        public override string Command => "tb";

        public override string Description => "Command Alias";
    }
}
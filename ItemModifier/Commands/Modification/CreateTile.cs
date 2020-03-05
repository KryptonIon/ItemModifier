using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class CreateTile : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "createtile";

        public override string Description => "Gets the data of an Item(item.createTile) or modifies it";

        public override string Usage => "/ct (Optional)[TileID]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.createTile > -1)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} creates Tile {MouseItem.createTile}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't create any tiles", errorColor);
                        return;
                    }
                }
                else
                {
                    Utilities.Modifier.ModifyCreateTile(caller, MouseItem, args[0]);
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

    public class CreateTileA1 : CreateTile
    {
        public override string Command => "ct";

        public override string Description => "Command Alias";
    }
}
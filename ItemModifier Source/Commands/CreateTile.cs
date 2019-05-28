using Terraria.ModLoader;
using Terraria.ID;

namespace ItemModifier.Commands
{
    public class CreateTile : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "ct";

        public override string Description => "Gets the data of an Item(item.createTile) or modifies it";

        public override string Usage => "/ct [Optional]<TileID>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.createTile > -1)
                    {
                        caller.Reply($"{MouseItem.Name}([i/s{MouseItem.stack}:{MouseItem.type}]) creates Tile {MouseItem.createTile}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{MouseItem.Name}([i/s{MouseItem.stack}:{MouseItem.type}]) doesn't create any tiles", errorColor);
                        return;
                    }
                }
                else
                {
                    int t;
                    if (!int.TryParse(args[0], out t))
                    {
                        caller.Reply($"Error, TileID({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (t > TileLoader.TileCount - 1)
                        {
                            caller.Reply($"TileID({args[0]}) is bigger than tile count", errorColor);
                            return;
                        }
                        else if (t < -1)
                        {
                            caller.Reply($"TileID({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else if (t == TileID.Count)
                        {
                            caller.Reply($"Sorry, TileID: {TileID.Count} is prohibited from use due to issues with crashing", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.createTile = t;
                            caller.Reply($"Set [i/s{MouseItem.stack}p{MouseItem.prefix}:{MouseItem.type}]'s CreateTile property to {args[0]}", replyColor);
                            return;
                        }
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
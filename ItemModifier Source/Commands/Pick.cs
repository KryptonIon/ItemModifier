using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Pickaxe : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "p";

        public override string Description => "Gets the data of an Item(item.pick) or modifies it";

        public override string Usage => "/p [Optional]<Pickaxe Power>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.pick != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Pickaxe Power is {MouseItem.pick}", replyColor);
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not a pickaxe", replyColor);
                    }
                }
                else
                {
                    int p;
                    if (!int.TryParse(args[0], out p))
                    {
                        caller.Reply($"Error, Pickaxe Power({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (p < -1)
                        {
                            caller.Reply($"Pickaxe Power({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.pick = p;
                            caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s Pickaxe Power to {args[0]}", replyColor);
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
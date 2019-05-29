using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Axe : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "a";

        public override string Description => "Gets the data of an Item(item.axe) or modifies it";

        public override string Usage => "/a [Optional]<Axe Power>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.axe != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Axe Power is {MouseItem.axe}", replyColor);
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not an Axe", replyColor);
                    }
                }
                else
                {
                    int a;
                    if (!int.TryParse(args[0], out a))
                    {
                        caller.Reply($"Error, Axe Power({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (a < -1)
                        {
                            caller.Reply($"Axe Power({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.axe = a;
                            caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s Axe Power to {args[0]}", replyColor);
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
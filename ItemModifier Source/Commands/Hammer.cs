using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Hammer : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "h";

        public override string Description => "Gets the data of an Item(item.hammer) or modifies it";

        public override string Usage => "/h [Optional]<Hammer Power>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.hammer != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Hammer Power is {MouseItem.hammer}", replyColor);
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not a hammer", replyColor);
                    }
                }
                else
                {
                    int h;
                    if (!int.TryParse(args[0], out h))
                    {
                        caller.Reply($"Error, Hammer Power({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (h < -1)
                        {
                            caller.Reply($"Hammer Power({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.hammer = h;
                            caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s Hammer Power to {args[0]}", replyColor);
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
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class UseTime : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "ut";

        public override string Description => "Gets the data of an Item(item.useTime) or modifies it";

        public override string Usage => "/ut [Optional]<UseTime>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    caller.Reply($"{MouseItem.Name}([i/s{MouseItem.stack}:{MouseItem.type}])'s UseTime is {MouseItem.useTime}", replyColor);
                }
                else
                {
                    int ut;
                    if (!int.TryParse(args[0], out ut))
                    {
                        caller.Reply($"Error, UseTime({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (ut < -1)
                        {
                            caller.Reply($"UseTime({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.useTime = ut;
                            caller.Reply($"Set [i/s{MouseItem.stack}p{MouseItem.prefix}:{MouseItem.type}]'s UseTime to {args[0]}", replyColor);
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
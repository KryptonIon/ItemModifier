using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class UseAnimation : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "ua";

        public override string Description => "Gets the data of an Item(item.useAnimation) or modifies it";

        public override string Usage => "/ua [Optional]<UseAnimation>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    caller.Reply($"{MouseItem.Name}([i/s{MouseItem.stack}:{MouseItem.type}])'s UseAnimation is {MouseItem.useAnimation}", replyColor);
                }
                else
                {
                    int ua;
                    if (!int.TryParse(args[0], out ua))
                    {
                        caller.Reply($"Error, UseAnimation({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (ua < -1)
                        {
                            caller.Reply($"UseAnimation({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.useAnimation = ua;
                            caller.Reply($"Set [i/s{MouseItem.stack}p{MouseItem.prefix}:{MouseItem.type}]'s UseAnimation to {args[0]}", replyColor);
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
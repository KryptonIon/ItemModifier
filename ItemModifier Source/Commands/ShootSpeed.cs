using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class ShootSpeed : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "ss";

        public override string Description => "Gets the data of an Item(item.shootSpeed) or modifies it";

        public override string Usage => "/ss [Optional]<ShootSpeed>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    caller.Reply($"{MouseItem.Name}([i/s{MouseItem.stack}:{MouseItem.type}])'s ShootSpeed is {MouseItem.shootSpeed}", replyColor);
                }
                else
                {
                    int ss;
                    if (!int.TryParse(args[0], out ss))
                    {
                        caller.Reply($"Error, ShootSpeed({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        MouseItem.shootSpeed = ss;
                        caller.Reply($"Set [i/s{MouseItem.stack}p{MouseItem.prefix}:{MouseItem.type}]'s ShootSpeed to {args[0]}", replyColor);
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
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class HealLife : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "hl";

        public override string Description => "Gets the data of an Item(item.healLife) or modifies it";

        public override string Usage => "/hl [Optional]<HP>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.healLife > 0)
                    {
                        caller.Reply($"{MouseItem.Name}([i/s{MouseItem.stack}:{MouseItem.type}]) heals {MouseItem.healLife} HP", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{MouseItem.Name}([i/s{MouseItem.stack}:{MouseItem.type}]) doesn't heal", errorColor);
                        return;
                    }
                }
                else
                {
                    int hl;
                    if (!int.TryParse(args[0], out hl))
                    {
                        caller.Reply($"Error, HP({args[0]}) must be a number", errorColor);
                        return;
                    }
                    else
                    {
                        MouseItem.healLife = hl;
                        caller.Reply($"Set [i/s{MouseItem.stack}p{MouseItem.prefix}:{MouseItem.type}]'s HealLife property to {args[0]}", replyColor);
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
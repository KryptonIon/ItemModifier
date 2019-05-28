using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class HealMana : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "hm";

        public override string Description => "Gets the data of an Item(item.healMana) or modifies it";

        public override string Usage => "/hm [Optional]<Mana>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.healMana > 0)
                    {
                        caller.Reply($"{MouseItem.Name}([i/s{MouseItem.stack}:{MouseItem.type}]) heals {MouseItem.healMana} mana", replyColor);
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
                    int hm;
                    if (!int.TryParse(args[0], out hm))
                    {
                        caller.Reply($"Error, Mana({args[0]}) must be a number", errorColor);
                        return;
                    }
                    else
                    {
                        MouseItem.healMana = hm;
                        caller.Reply($"Set [i/s{MouseItem.stack}p{MouseItem.prefix}:{MouseItem.type}]'s HealMana property to {args[0]}", replyColor);
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
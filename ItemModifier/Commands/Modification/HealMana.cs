using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class HealMana : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "healmana";

        public override string Description => "Gets the data of an Item(item.healMana) or modifies it";

        public override string Usage => "/hm (Optional)[Mana]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.healMana > 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} heals {MouseItem.healMana} mana", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't heal mana", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyHealMana(caller, MouseItem, args[0]);
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

    public class HealManaA1 : HealMana
    {
        public override string Command => "hm";

        public override string Description => "Command Alias";
    }
}
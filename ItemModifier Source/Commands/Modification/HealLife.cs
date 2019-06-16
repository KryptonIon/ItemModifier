using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class HealLife : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "heallife";

        public override string Description => "Gets the data of an Item(item.healLife) or modifies it";

        public override string Usage => "/hl (Optional)[HP]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.healLife > 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} heals {MouseItem.healLife} HP", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't heal", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyHealLife(caller, MouseItem, args[0]);
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

    public class HealLifeA1 : HealLife
    {
        public override string Command => "hl";

        public override string Description => "Command Alias";
    }
}
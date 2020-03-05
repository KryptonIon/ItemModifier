using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Critical : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "critical";

        public override string Description => "Gets the data of an Item(item.crit) or modifies it";

        public override string Usage => "/crit (Optional)[Critical Strike Chance]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.crit != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Critical Strike Chance is {MouseItem.crit}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't have any critical strike chance", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyCritical(caller, MouseItem, args[0]);
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

    public class CriticalA1 : Critical
    {
        public override string Command => "crit";

        public override string Description => "Command Alias";
    }
}
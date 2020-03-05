using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class BuffTime : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "bufftime";

        public override string Description => "Gets the data of an Item(item.buffTime) or modifies it";

        public override string Usage => "/bt (Optional)[BuffTime]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    caller.Reply($"{Modifier.GetItem2(MouseItem)}'s BuffTime is {MouseItem.buffTime}", replyColor);
                    return;
                }
                else
                {
                    Modifier.ModifyBuffTime(caller, MouseItem, args[0]);
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

    public class BuffTimeA1 : BuffTime
    {
        public override string Command => "bt";

        public override string Description => "Command Alias";
    }
}
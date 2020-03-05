using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class UseTime : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "usetime";

        public override string Description => "Gets the data of an Item(item.useTime) or modifies it";

        public override string Usage => "/ut (Optional)[UseTime]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    caller.Reply($"{Modifier.GetItem2(MouseItem)}'s UseTime is {MouseItem.useTime}", replyColor);
                    return;
                }
                else
                {
                    Modifier.ModifyUseTime(caller, MouseItem, args[0]);
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

    public class UseTimeA1 : UseTime
    {
        public override string Command => "ut";

        public override string Description => "Command Alias";
    }
}
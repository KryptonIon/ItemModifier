using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class MaxStack : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "maxstack";

        public override string Description => "Gets the data of an Item(item.maxStack) or modifies it";

        public override string Usage => "/ms (Optional)[MaxStack]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    caller.Reply($"{Modifier.GetItem2(MouseItem)}'s MaxStack is {MouseItem.maxStack}", replyColor);
                    return;
                }
                else
                {
                    Modifier.ModifyMaxStack(caller, MouseItem, args[0]);
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

    public class MaxStackA1 : MaxStack
    {
        public override string Command => "ms";

        public override string Description => "Command Alias";
    }
}
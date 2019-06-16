using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Stack : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "stack";

        public override string Description => "Gets the data of an Item(item.stack) or modifies it";

        public override string Usage => "/st (Optional)[Stack]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Stack is {MouseItem.stack}", replyColor);
                    return;
                }
                else
                {
                    Modifier.ModifyStack(caller, MouseItem, args[0]);
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

    public class StackA1 : Stack
    {
        public override string Command => "st";

        public override string Description => "Command Alias";
    }
}
using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class UseAnimation : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "useanimation";

        public override string Description => "Gets the data of an Item(item.useAnimation) or modifies it";

        public override string Usage => "/ua (Optional)[UseAnimation]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    caller.Reply($"{Modifier.GetItem2(MouseItem)}'s UseAnimation is {MouseItem.useAnimation}", replyColor);
                    return;
                }
                else
                {
                    Modifier.ModifyUseAnimation(caller, MouseItem, args[0]);
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

    public class UseAnimationA1 : UseAnimation
    {
        public override string Command => "ua";

        public override string Description => "Command Alias";
    }
}
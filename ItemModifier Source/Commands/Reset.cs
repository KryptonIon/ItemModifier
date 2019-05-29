using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Reset : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "reset";

        public override string Description => "Resets an item's properties";

        public override string Usage => "/reset";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var MouseItem = caller.Player.HeldItem;
            int stack = MouseItem.stack;
            var errorColor = ItemModifier.errorColor;

            if (MouseItem.type != 0)
            {
                MouseItem.SetDefaults(MouseItem.type);
                MouseItem.stack = stack;
            }
            else
            {
                caller.Reply("No Item Selected", errorColor);
            }
        }
    }
}

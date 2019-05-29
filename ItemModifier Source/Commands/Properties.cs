using ItemModifier.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Properties : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "properties";

        public override string Description => "Gets the data of an Item";

        public override string Usage => "/properties (Optional Parameters)<Property>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;
            Item umitem = new Item();
            umitem.SetDefaults(MouseItem.type);

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    string Reply = Modifier.GetProperties(MouseItem);
                    caller.Reply(Reply, replyColor);
                }
                else
                {
                    string Reply = "Requested Properties are:";

                    Reply += Modifier.GetProperties2(MouseItem, caller, args);

                    caller.Reply(Reply, replyColor);
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
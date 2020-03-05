using ItemModifier.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Set : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "set";

        public override string Description => "Modifies the data of an item";

        public override string Usage => "/set [Parameters]<Property> <Value>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var helpColor = Config.helpColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            string help = Description +
                $"\nUsage: {Usage}, examples" +
                "\n/set ct 26, Modifies the CreateTile property of the selected item(yellow box) to 26" +
                "\n/set ct 26 s 5, Modifies CreateTile to 26 and Shoot to 5";
            string parameters = ItemModifier.PropParams;

            if (args.Length <= 0)
            {
                caller.Reply(help, helpColor);
                return;
            }
            else if (args[0].ToLower() == "help")
            {
                caller.Reply(help, helpColor);
                return;
            }
            else if (args[0].ToLower() == "params")
            {
                caller.Reply(parameters, helpColor);
                return;
            }

            if (MouseItem.type > 0)
            {
                Item umitem = new Item();
                umitem.SetDefaults(MouseItem.type);
                if (args.Length % 2 == 0)
                {
                    Modifier.ModifyItem(MouseItem, caller, args, 0);

                    string Reply = "Modified " + Modifier.GetItem(MouseItem);

                    if (Config.ShowProperties)
                    {
                        Reply += Modifier.GetProperties(MouseItem);
                    }

                    caller.Reply(Reply, replyColor);
                }
                else
                {
                    caller.Reply($"Invalid Syntax: {input}", errorColor);
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
}
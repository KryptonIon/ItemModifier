using ItemModifier.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class GenerateItem : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "gi";

        public override string Description => "Generates an item, and/or with custom properties, do /gi params to see a list of parameters";

        public override string Usage => "/gi <ItemID> [Optional Parameters]<Property> <Value>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var helpColor = ItemModifier.helpColor;
            var replyColor = ItemModifier.replyColor;

            string help = Description +
                    $" \nUsage: {Usage}, examples:" +
                    " \n/gi 757, Spawns a Terra Blade" +
                    " \n/gi 757 s 5, Spawns a Terra Blade that shoots Jester's Arrows" +
                    " \n/gi 757 ct 26 s 5, Spawns a Terra Blade that shoots Jester's Arrows whilst placing Demon Altars like a block";
            string parameters = ItemModifier.parameters;

            if (args.Length <= 0)
            {
                caller.Reply(help, helpColor);
                return;
            }
            else if (args.Length < 1)
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

            if (int.Parse(args[0]) > ItemLoader.ItemCount || int.Parse(args[0]) <= 0)
            {
                caller.Reply($"ItemID must not be less than 1 or exceed current amount of items: {ItemLoader.ItemCount}", errorColor);
                return;
            }

            if (ItemModifier.FindSlot(caller.Player.inventory) == -1)
            {
                caller.Reply("Inventory Full", errorColor);
                return;
            }
            else
            {
                Item item = new Item();
                item.SetDefaults(int.Parse(args[0]));

                if (args.Length > 1)
                {
                    if ((args.Length - 1) % 2 == 0)
                    {
                        Modifier.ModifyItem(item, caller, args, 1);
                    }
                    else
                    {
                        caller.Reply($"Invalid Syntax: {input}", errorColor);
                        return;
                    }
                }

                caller.Player.inventory[ItemModifier.FindSlot(caller.Player.inventory)] = item;

                string Reply = "Spawned " + Modifier.GetItem(item);

                if (Config.ShowProperties)
                {
                    Reply += Modifier.GetProperties(item);
                }

                caller.Reply(Reply, replyColor);
            }
        }
    }
}
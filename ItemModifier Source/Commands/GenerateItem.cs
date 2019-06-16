using ItemModifier.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class GenerateItem : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "generateitem";

        public override string Description => "Generates an item, and/or with custom properties, do /gi params to see a list of parameters";

        public override string Usage => "/gi [ItemID] (Optional Parameters)[Property] [Value]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var helpColor = Config.helpColor;
            var replyColor = Config.replyColor;

            string help = Description +
                    $" \nUsage: {Usage}, examples:" +
                    " \n/gi 757, Spawns a Terra Blade" +
                    " \n/gi 757 s 5, Spawns a Terra Blade that shoots Jester's Arrows" +
                    " \n/gi 757 ct 26 s 5, Spawns a Terra Blade that shoots Jester's Arrows whilst placing Demon Altars like a block";
            string parameters = ItemModifier.PropParams;

            if (args.Length == 0)
            {
                if (ItemModifier.ItemId != 0)
                {

                }
                else
                {
                    caller.Reply(help, helpColor);
                    return;
                }
            }
            else
            {
                if (args[0].ToLower() == "help")
                {
                    caller.Reply(help, helpColor);
                    return;
                }
                else if (args[0].ToLower() == "params")
                {
                    caller.Reply(parameters, helpColor);
                    return;
                }
            }

            int slot;
            if (!ItemModifier.FindSlot(caller.Player.inventory, out slot))
            {
                caller.Reply("Inventory Full", errorColor);
                return;
            }
            else
            {
                Item item = new Item();

                if (args.Length != 0)
                {
                    int id;

                    if (int.TryParse(args[0], out id))
                    {
                        if (ItemModifier.ItemId != 0 && !Config.AlwaysUseID)
                        {
                            item.SetDefaults(ItemModifier.ItemId);
                        }
                        else
                        {
                            if (id < ItemLoader.ItemCount)
                            {
                                item.SetDefaults(id);
                            }
                            else
                            {
                                caller.Reply(ErrorHandler.BiggerThanError("ItemID", id, "Item Count", ItemLoader.ItemCount), errorColor);
                                if (ItemModifier.ItemId != 0)
                                {
                                    caller.Reply("Using SetItem instead...", errorColor);
                                    item.SetDefaults(ItemModifier.ItemId);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else if (ItemModifier.ItemId != 0)
                    {
                        item.SetDefaults(ItemModifier.ItemId);
                    }
                    else
                    {
                        caller.Reply(ErrorHandler.ParsingError("int", "ItemID", args[0]), errorColor);
                        return;
                    }

                    int n;
                    if (int.TryParse(args[0], out n))
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
                    else
                    {
                        if (args.Length % 2 == 0)
                        {
                            Modifier.ModifyItem(item, caller, args, 0);
                        }
                        else
                        {
                            caller.Reply($"Invalid Syntax: {input}", errorColor);
                            return;
                        }
                    }
                }
                else
                {
                    item.SetDefaults(ItemModifier.ItemId);
                }

                caller.Player.inventory[slot] = item;

                string Reply = "Spawned " + Modifier.GetItem(item);

                if (Config.ShowProperties)
                {
                    Reply += Modifier.GetProperties(item);
                }

                caller.Reply(Reply, replyColor);
            }
        }
    }

    public class GenerateItemA1 : GenerateItem
    {
        public override string Command => "gi";

        public override string Description => "Command Alias";
    }
}
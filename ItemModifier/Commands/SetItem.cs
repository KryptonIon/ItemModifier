using ItemModifier.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class SetItem : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "setitem";

        public override string Description => "Sets the item for GenerateItem(/gi)";

        public override string Usage => "/setitem (Optional)[ItemID or ItemName]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var replyColor = Config.replyColor;
            var helpColor = Config.helpColor;
            var errorColor = Config.errorColor;
            string help = Description +
                $"\nUsage: {Usage}. examples:" +
                "\n/setitem Shows the currently set item" +
                "\n/setitem 50 Sets the item to Magic Mirror";

            if (args.Length == 0)
            {
                if (ItemModifier.ItemId == 0)
                {
                    caller.Reply("No ItemID is currently set", replyColor);
                    return;
                }
                else if (ItemModifier.ItemId < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("SetItemID", ItemModifier.ItemId), errorColor);
                    return;
                }
                else
                {
                    Item item = new Item();
                    item.SetDefaults(ItemModifier.ItemId);
                    caller.Reply($"ItemID is set to {Modifier.GetItem2(item)}({item.type})", replyColor);
                    return;
                }
            }
            else if (args.Length == 1)
            {
                if (args[0] == "help")
                {
                    caller.Reply(help, helpColor);
                    return;
                }
                else if (args[0] == "$^")
                {
                    if (ItemModifier.ItemId != 0)
                    {
                        ItemModifier.ItemId = caller.Player.HeldItem.type;
                    }
                    else
                    {
                        caller.Reply("No Item Selected", errorColor);
                        return;
                    }
                }
                else
                {
                    int id;
                    if (!int.TryParse(args[0], out id))
                    {
                        int r = ItemName.GetID(args[0], caller);
                        if (r == -1)
                        {
                            caller.Reply(ErrorHandler.NoMatchError(args[0]), errorColor);
                            return;
                        }
                        ItemModifier.ItemId = r;
                        Item it3 = new Item();
                        it3.SetDefaults(r);
                        caller.Reply($"ItemID set to {it3.type} ({it3.AffixName()})", replyColor);
                    }
                    else
                    {
                        if (id > ItemLoader.ItemCount)
                        {
                            caller.Reply(ErrorHandler.BiggerThanError("ItemID", id, "Item Count", ItemLoader.ItemCount), errorColor);
                        }
                        else if (id < 0)
                        {
                            caller.Reply(ErrorHandler.NegativeError("ItemID", id), errorColor);
                        }
                        else
                        {
                            ItemModifier.ItemId = id;
                            Item it2 = new Item();
                            it2.SetDefaults(id);
                            caller.Reply($"ItemID set to {it2.type} ({it2.AffixName()})", replyColor);
                        }
                    }
                }
            }
            else
            {
                int r = ItemName.GetID(ItemName.CombineString(args), caller);
                if (r == -1)
                {
                    caller.Reply(ErrorHandler.NoMatchError(ItemName.CombineString(args)), errorColor);
                    return;
                }
                ItemModifier.ItemId = r;
                Item it4 = new Item();
                it4.SetDefaults(r);
                caller.Reply($"ItemID set to {it4.type} ({it4.AffixName()})", replyColor);
            }
        }
    }
}
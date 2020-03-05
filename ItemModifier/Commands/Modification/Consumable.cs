using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Consumable : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "consumable";

        public override string Description => "Gets the data of an Item(item.consumable) or modifies it";

        public override string Usage => "/cons (Optional)[True/False]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.consumable)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is consumable", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not consumable", replyColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyConsumable(caller, MouseItem, args[0]);
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

    public class ConsumableA1 : Consumable
    {
        public override string Command => "cons";

        public override string Description => "Command Alias";
    }
}
using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Potion : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "potion";

        public override string Description => "Gets the data of an Item(item.potion) or modifies it";

        public override string Usage => "/pot (Optional)[True/False]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.potion)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is a potion", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not a potion", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyPotion(caller, MouseItem, args[0]);
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

    public class PotionA1 : Potion
    {
        public override string Command => "pot";

        public override string Description => "Command Alias";
    }
}
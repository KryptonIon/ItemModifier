using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Pickaxe : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "pickaxe";

        public override string Description => "Gets the data of an Item(item.pick) or modifies it";

        public override string Usage => "/p (Optional)[Pickaxe Power]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.pick != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Pickaxe Power is {MouseItem.pick}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not a pickaxe", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyPickaxe(caller, MouseItem, args[0]);
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

    public class PickaxeA1 : Pickaxe
    {
        public override string Command => "p";

        public override string Description => "Command Alias";
    }
}
using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Knockback : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "knockback";

        public override string Description => "Gets the data of an Item(item.knockBack) or modifies it";

        public override string Usage => "/kb (Optional)[Knockback]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.knockBack != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Knockback is {MouseItem.knockBack}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't deal knockback", replyColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyKnockBack(caller, MouseItem, args[0]);
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

    public class KnockbackA1 : Knockback
    {
        public override string Command => "kb";

        public override string Description => "Command Alias";
    }
}
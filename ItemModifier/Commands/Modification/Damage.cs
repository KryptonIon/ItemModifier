using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Damage : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "damage";

        public override string Description => "Gets the data of an Item(item.damage) or modifies it";

        public override string Usage => "/d (Optional)[Damage]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.damage != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Damage is {MouseItem.damage}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't deal damage", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyDamage(caller, MouseItem, args[0]);
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

    public class DamageA1 : Damage
    {
        public override string Command => "d";

        public override string Description => "Command Alias";
    }
}
using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Buff : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "buff";

        public override string Description => "Gets the data of an Item(item.buffType) or modifies it";

        public override string Usage => "/bid (Optional)[BuffID]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.buffType != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s BuffID is {MouseItem.buffType}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't inflict buffs", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyBuffID(caller, MouseItem, args[0]);
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

    public class BuffA1 : Buff
    {
        public override string Command => "bid";

        public override string Description => "Command Alias";
    }
}
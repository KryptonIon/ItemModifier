using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Hammer : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "hammer";

        public override string Description => "Gets the data of an Item(item.hammer) or modifies it";

        public override string Usage => "/h (Optional)[Hammer Power]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.hammer != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Hammer Power is {MouseItem.hammer}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} is not a hammer", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyHammer(caller, MouseItem, args[0]);
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

    public class HammerA1 : Hammer
    {
        public override string Command => "h";

        public override string Description => "Command Alias";
    }
}
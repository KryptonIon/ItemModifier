using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class ShootSpeed : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "shootspeed";

        public override string Description => "Gets the data of an Item(item.shootSpeed) or modifies it";

        public override string Usage => "/ss (Optional)[ShootSpeed]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.shootSpeed != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s ShootSpeed is {MouseItem.shootSpeed}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't have shooting speed", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyShootSpeed(caller, MouseItem, args[0]);
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

    public class ShootSpeedA1 : ShootSpeed
    {
        public override string Command => "ss";

        public override string Description => "Command Alias";
    }
}
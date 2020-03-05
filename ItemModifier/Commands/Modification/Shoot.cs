using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands.Modification
{
    public class Shoot : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "shoot";

        public override string Description => "Gets the data of an Item(item.shoot) or modifies it";

        public override string Usage => "/s (Optional)[ProjectileID]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length == 0)
                {
                    if (MouseItem.shoot > -1)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} shoots Projectile {MouseItem.shoot}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't shoot any projectiles", errorColor);
                        return;
                    }
                }
                else
                {
                    Modifier.ModifyShoot(caller, MouseItem, args[0]);
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

    public class ShootA1 : Shoot
    {
        public override string Command => "s";

        public override string Description => "Command Alias";
    }
}
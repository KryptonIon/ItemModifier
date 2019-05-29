using ItemModifier.Utilities;
using Terraria.ID;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Shoot : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "s";

        public override string Description => "Gets the data of an Item(item.shoot) or modifies it";

        public override string Usage => "/s [Optional]<ProjectileID>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
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
                    int s;
                    if (!int.TryParse(args[0], out s))
                    {
                        caller.Reply($"Error, ProjectileID({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (s > ProjectileLoader.ProjectileCount - 1)
                        {
                            caller.Reply($"ProjectileID({args[0]}) is bigger than tile count", errorColor);
                            return;
                        }
                        else if (s < -1)
                        {
                            caller.Reply($"ProjectileID({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else if (s == ProjectileID.Count)
                        {
                            caller.Reply($"Sorry, ProjectileID: {ProjectileID.Count} is prohibited from use due to issues with crashing", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.shoot = s;
                            caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s Shoot property to {args[0]}", replyColor);
                            return;
                        }
                    }
                }
            }
            else
            {
                caller.Reply("No Item Selected", errorColor);
                return;
            }
        }
    }
}
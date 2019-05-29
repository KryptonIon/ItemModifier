using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Damage : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "d";

        public override string Description => "Gets the data of an Item(item.damage) or modifies it";

        public override string Usage => "/d [Optional]<Damage>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.damage != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Damage is {MouseItem.damage}", replyColor);
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't deal damage", replyColor);
                    }
                }
                else
                {
                    int d;
                    if (!int.TryParse(args[0], out d))
                    {
                        caller.Reply($"Error, Damage({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (d < -1)
                        {
                            caller.Reply($"Damage({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.damage = d;
                            caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s Damage to {args[0]}", replyColor);
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
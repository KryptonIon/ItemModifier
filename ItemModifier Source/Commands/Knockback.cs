using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Knockback : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "kb";

        public override string Description => "Gets the data of an Item(item.knockBack) or modifies it";

        public override string Usage => "/kb [Optional]<Knockback>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    if (MouseItem.knockBack != 0)
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)}'s Knockback is {MouseItem.knockBack}", replyColor);
                    }
                    else
                    {
                        caller.Reply($"{Modifier.GetItem2(MouseItem)} doesn't deal knockback", replyColor);
                    }
                }
                else
                {
                    int kb;
                    if (!int.TryParse(args[0], out kb))
                    {
                        caller.Reply($"Error, Knockback({args[0]}) must be a number", errorColor);
                    }
                    else
                    {
                        if (kb < -1)
                        {
                            caller.Reply($"Knockback({args[0]}) can't be negative", errorColor);
                            return;
                        }
                        else
                        {
                            MouseItem.knockBack = kb;
                            caller.Reply($"Set {Modifier.GetItem2(MouseItem)}'s Knockback to {args[0]}", replyColor);
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
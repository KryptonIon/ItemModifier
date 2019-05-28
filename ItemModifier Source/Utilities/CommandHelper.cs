using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ItemModifier.Utilities
{
    public class CommandHelper
    {
        public static bool ValidifySyntax(CommandCaller caller, string[] args, int RequiredArgs, string Help, params Identifier[] Identifier)
        {
            Color helpColor = ItemModifier.helpColor;
            if (args.Length <= 0)
            {
                caller.Reply(Help, helpColor);
                return false;
            }
            else if(args.Length < RequiredArgs)
            {
                caller.Reply(Help, helpColor);
                return false;
            }
            else
            {
                if(args[0].ToLower() == "help")
                {
                    caller.Reply(Help, helpColor);
                    return false;
                }
                for(int i = 0; i < Identifier.Length; i++)
                {
                    if(args[0].ToLower() == Identifier[i].name)
                    {
                        caller.Reply(Identifier[i].value, Identifier[i].color);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
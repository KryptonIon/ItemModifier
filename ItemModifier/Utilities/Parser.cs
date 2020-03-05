using Terraria.ModLoader;

namespace ItemModifier.Utilities
{
    public class Parser
    {
        public static bool ParseBool(CommandCaller caller, string value, out bool Parsed, string varName = "Bool")
        {
            var errorColor = Config.errorColor;

            if (value.StartsWith("t"))
            {
                Parsed = true;
                return true;
            }
            else if (value.StartsWith("f"))
            {
                Parsed = false;
                return true;
            }
            else
            {
                Parsed = false;
                caller.Reply(ErrorHandler.ParsingError("bool", varName, value), errorColor);
                return false;
            }
        }

        public static bool ParseInt(CommandCaller caller, string value, out int Parsed)
        {
            var errorColor = Config.errorColor;
            int v;

            if (!int.TryParse(value, out v))
            {
                Parsed = -1;
                caller.Reply(ErrorHandler.ParsingError("int", "Integer", value));
                return false;
            }
            else
            {
                Parsed = v;
                return true;
            }
        }
    }
}

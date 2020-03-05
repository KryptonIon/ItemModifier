namespace ItemModifier.Utilities
{
    static class ErrorHandler
    {
        public static string ParsingError(string type, string varName, object value) => $"Error 0. {varName}(UserSpecified:\"{value}\") must be {type}";

        public static string NegativeError(string varName, float value) => $"Error 1. {varName}(UserSpecified:\"{value}\") can't be negative";

        public static string BiggerThanError(string varName, int value, string maxName, int max) => $"Error 2. {varName}(UserSpecified:\"{value}\") must be smaller than {maxName}({max})";

        //public static string NullError(string varName, string isName = "null") => $"Error 3. {varName} is {isName}";

        public static string NoMatchError(string name) => $"Error 4. \"{name}\" doesn't match anything";

        public static string InvalidSettingError(string settingName) => $"Error 4. \"{settingName}\" is an invalid setting";

        public static string InvalidArguementError(string arguementName) => $"Error 4. \"{arguementName}\" is an invalid arguement";
    }
}
using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Setting : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "settings";

        public override string Description => "Modify internal settings/preferences. Do /settings list to see a list of settings";

        public override string Usage => "/settings <SettingName> (Optional)[Value]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var helpColor = Config.helpColor;
            var replyColor = Config.replyColor;

            string help = Description +
                $"\nUsage: {Usage}, examples:" +
                "\n/settings ShowUnnecessary, Shows the current value of ShowUnnecessary setting" +
                "\n/settings ShowUnnecessary true, changes ShowUnnecessary to true" +
                "\n/settings sHuN false, changes ShowUnnecessary to true. Setting names are caps insensitive as demonstrated.";
            string settings = ItemModifier.settings;

            if (args.Length == 0)
            {
                caller.Reply(help, helpColor);
                return;
            }
            else if (args[0].ToLower() == "help")
            {
                caller.Reply(help, helpColor);
                return;
            }
            else if (args[0].ToLower() == "list")
            {
                caller.Reply(settings, helpColor);
                return;
            }

            if (args.Length >= 2)
            {
                bool v;

                if (!Parser.ParseBool(caller, args[1], out v, "Settings"))
                {
                    return;
                }
                else
                {
                    SettingInfo si = Config.ModifyConfig(args[0], v);

                    if (si != null)
                    {
                        caller.Reply($"{si.name} set to {si.value}", replyColor);
                        return;
                    }
                    else
                    {
                        caller.Reply(ErrorHandler.InvalidSettingError(args[0]), errorColor);
                        return;
                    }
                }
            }
            else
            {
                if (args[0] == "reset")
                {
                    Config.Reset();
                    caller.Reply("Settings Reseted", replyColor);
                    return;
                }
                else
                {
                    SettingInfo si = new SettingInfo();
                    if (!Config.GetSettingInfo(args[0], out si))
                    {
                        caller.Reply(ErrorHandler.InvalidSettingError(args[0]), errorColor);
                        return;
                    }
                    else
                    {
                        caller.Reply($"{si.name} is {si.value}", replyColor);
                        return;
                    }
                }
            }
        }
    }
}
using ItemModifier.Utilities;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class Setting : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "setting";

        public override string Description => "Modify internal settings/preferences. Do /setting settings to see a list of settings";

        public override string Usage => "/setting <SettingName> [Optional]<Value>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var helpColor = ItemModifier.helpColor;
            var replyColor = ItemModifier.replyColor;

            string help = Description +
                $"\nUsage: {Usage}, examples:" +
                "\n/setting ShowUnnecessary, Shows the current value of ShowUnnecessary setting" +
                "\n/setting ShowUnnecessary true, changes ShowUnnecessary to true" +
                "\n/setting sHuN false, changes ShowUnnecessary to true. Setting names are caps insensitive as demonstrated.";
            string settings = ItemModifier.settings;

            if (args.Length <= 0)
            {
                caller.Reply(help, helpColor);
                return;
            }
            else if (args.Length < 1)
            {
                caller.Reply(help, helpColor);
                return;
            }
            else if (args[0].ToLower() == "help")
            {
                caller.Reply(help, helpColor);
                return;
            }
            else if (args[0].ToLower() == "settings")
            {
                caller.Reply(settings, helpColor);
                return;
            }

            if (args.Length >= 2)
            {
                if (!Config.ModifyConfig(args[0], args[1]))
                {
                    caller.Reply($"Error Invalid Setting Name or Value", errorColor);
                    return;
                }
                else
                {
                    SettingInfo setting = new SettingInfo();
                    Config.GetSettingInfo(args[0], out setting);
                    caller.Reply($"Success! {setting.name} is now {setting.value}", replyColor);
                    return;
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
                        caller.Reply($"{args[0]} is an invalid setting", errorColor);
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
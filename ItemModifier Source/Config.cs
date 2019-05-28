using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using System.IO;
using System.Collections.Generic;
using ItemModifier.Utilities;

namespace ItemModifier
{
    public static class Config
    {
        public static bool ShowProperties = true;
        public static bool ShowUnnecessary = false;
        public static bool ShowPID = true;

        static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", "ItemModifier.json");
        static Preferences Configuration = new Preferences(ConfigPath);

        public static void Load()
        {
            if (!ReadConfig())
            {
                ErrorLogger.Log("Failed to read ItemModifier's Config");
                CreateConfig();
            }
        }
        
        static bool ReadConfig()
        {
            if (Configuration.Load())
            {
                Configuration.Get("ShowProperties", ref ShowProperties);
                Configuration.Get("ShowUnnecessary", ref ShowUnnecessary);
                Configuration.Get("ShowPID", ref ShowPID);
                return true;
            }
            return false;
        }
        
        static void CreateConfig()
        {
            Configuration.Clear();
            Configuration.Put("ShowProperties", ShowProperties);
            Configuration.Put("ShowUnnecessary", ShowUnnecessary);
            Configuration.Put("ShowPID", ShowPID);
            Configuration.Save();
        }

        public static bool ModifyConfig(string SettingName, string value)
        {
            var sn = SettingName.ToLower();
            bool v;
            if ("showunnecessary".Contains(sn) || sn == "shun")
            {
                if(!bool.TryParse(value, out v)) return false;
                else ShowUnnecessary = v;
            }
            else if ("showproperties".Contains(sn) || sn == "shpr")
            {
                if(!bool.TryParse(value, out v)) return false;
                else ShowProperties = v;
            }
            else if ("showpid".Contains(sn) || sn == "shpid")
            {
                if (!bool.TryParse(value, out v)) return false;
                else ShowPID = v;
            }
            else return false;
            CreateConfig();
            return true;
        }

        public static bool GetSettingInfo(string SettingName, out SettingInfo result)
        {
            var sn = SettingName.ToLower();
            if ("showunnecessary".Contains(sn) || sn == "shun") result = new SettingInfo("ShowUnnecessary", ShowUnnecessary);
            else if ("showproperties".Contains(sn) || sn == "shpr") result = new SettingInfo("ShowProperties", ShowProperties);
            else if ("showpid".Contains(sn) || sn == "shpid") result = new SettingInfo("ShowPID", ShowPID);
            else
            {
                result = new SettingInfo("Error", null);
                return false;
            }
            return true;
        }

        public static void Reset()
        {
            ShowProperties = true;
            ShowUnnecessary = false;
            ShowPID = true;
            CreateConfig();
        }
    }
}
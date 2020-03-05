using ItemModifier.Utilities;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace ItemModifier
{
    public static class Config
    {
        public static bool ShowProperties = true;
        public static bool ShowUnnecessary = false;
        public static bool ShowEWMessage = true;
        public static bool AlwaysUseID = false;
        public static bool ShowResultList = true;
        public static bool GetRandomItem = false;
        public static bool ShowMaxStack = true;
        public static Color errorColor = new Color(255, 0, 0);
        public static Color helpColor = new Color(255, 255, 0);
        public static Color replyColor = new Color(0, 100, 255);

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
                Configuration.Get("ShowEWMessage", ref ShowEWMessage);
                Configuration.Get("AlwaysUseID", ref AlwaysUseID);
                Configuration.Get("ShowResultList", ref ShowResultList);
                Configuration.Get("GetRandomItem", ref GetRandomItem);
                Configuration.Get("ShowMaxStack", ref ShowMaxStack);
                return true;
            }
            return false;
        }

        static void CreateConfig()
        {
            Configuration.Clear();
            Configuration.Put("ShowProperties", ShowProperties);
            Configuration.Put("ShowUnnecessary", ShowUnnecessary);
            Configuration.Put("ShowEWMessage", ShowEWMessage);
            Configuration.Put("AlwaysUseID", AlwaysUseID);
            Configuration.Put("ShowResultList", ShowResultList);
            Configuration.Put("GetRandomItem", GetRandomItem);
            Configuration.Put("ShowMaxStack", ShowMaxStack);
            Configuration.Save();
        }

        public static void ModifyConfig(ref bool Setting, bool value)
        {
            Setting = value;
            Save();
        }

        public static SettingInfo ModifyConfig(string SettingName, bool value)
        {
            string sn = SettingName.ToLower();

            if (sn.StartsWith("s"))
            {
                if ("showunnecessary".StartsWith(sn) || sn == "shun")
                {
                    ModifyConfig(ref ShowUnnecessary, value);
                    return new SettingInfo("ShowUnnecessary", ShowUnnecessary);
                }
                else if ("showproperties".StartsWith(sn) || sn == "shpr")
                {
                    ModifyConfig(ref ShowProperties, value);
                    return new SettingInfo("ShowProperties", ShowProperties);
                }
                else if ("showewmessage".StartsWith(sn) || sn == "shewmsg")
                {
                    ModifyConfig(ref ShowEWMessage, value);
                    return new SettingInfo("ShowEWMessage", ShowEWMessage);
                }
                else if ("showresultlist".StartsWith(sn) || sn == "shrl")
                {
                    ModifyConfig(ref ShowResultList, value);
                    return new SettingInfo("ShowResultList", ShowResultList);
                }
                else if ("showmaxstack".StartsWith(sn) || sn == "shms")
                {
                    ModifyConfig(ref ShowUnnecessary, value);
                    return new SettingInfo("ShowMaxStack", ShowMaxStack);
                }
                else
                {
                    return null;
                }
            }

            if (sn.StartsWith("a"))
            {
                if ("alwaysuseid".StartsWith(sn) || sn == "auid")
                {
                    ModifyConfig(ref AlwaysUseID, value);
                    return new SettingInfo("AlwaysUseID", AlwaysUseID);
                }
                else
                {
                    return null;
                }
            }

            if (sn.StartsWith("g"))
            {
                if ("getrandomitem".StartsWith(sn) || sn == "gri")
                {
                    ModifyConfig(ref GetRandomItem, value);
                    return new SettingInfo("GetRandomItem", GetRandomItem);
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public static bool GetSettingInfo(string SettingName, out SettingInfo result)
        {
            var sn = SettingName.ToLower();
            if (sn.StartsWith("s"))
            {
                if ("showunnecessary".StartsWith(sn) || sn == "shun")
                {
                    result = new SettingInfo("ShowUnnecessary", ShowUnnecessary);
                }
                else if ("showproperties".StartsWith(sn) || sn == "shpr")
                {
                    result = new SettingInfo("ShowProperties", ShowProperties);
                }
                else if ("showewmessage".StartsWith(sn) || sn == "shewmsg")
                {
                    result = new SettingInfo("ShowEWMessage", ShowEWMessage);
                }
                else if ("showresultlist".StartsWith(sn) || sn == "srl")
                {
                    result = new SettingInfo("ShowResultList", ShowResultList);
                }
                else if ("showmaxstack".StartsWith(sn) || sn == "sms")
                {
                    result = new SettingInfo("ShowMaxStack", ShowMaxStack);
                }
                else
                {
                    goto Error;
                }

                return true;
            }

            {
                if ("alwaysuseid".StartsWith(sn) || sn == "auid")
                {
                    result = new SettingInfo("AlwaysUseID", AlwaysUseID);
                }
                else if ("getrandomitem".StartsWith(sn) || sn == "gri")
                {
                    result = new SettingInfo("getrandomitem", GetRandomItem);
                }
                else
                {
                    goto Error;
                }

                return true;
            }

        Error:
            result = new SettingInfo("Error", null);
            return false;
        }

        public static void Reset()
        {
            ShowProperties = true;

            ShowUnnecessary = false;

            ShowEWMessage = true;

            AlwaysUseID = false;

            ShowResultList = true;

            GetRandomItem = false;

            ShowMaxStack = true;

            errorColor = new Color(255, 0, 0);

            helpColor = new Color(255, 255, 0);

            replyColor = new Color(0, 100, 255);

            Save();
        }

        public static void Save()
        {
            CreateConfig();
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier
{
    public class ItemModifier : Mod
    {
        public ItemModifier()
        {

        }

        public static Color errorColor = new Color(255, 0, 0);
        public static Color helpColor = new Color(255, 255, 0);
        public static Color replyColor = new Color(0, 100, 255);
        public static string parameters = "Parameters: shoot or s, shootspeed or ss, createtile or ct, usetime or ut, useanimation or ua, healmana or hm, heallife or hl" +
                    "\nParameters are caps insensitive meaning, you can do /gi 757 ShOoT 5 and it will still shoot a jester's arrow" +
                    "\nParameters have shortcuts, eg /gi 757 s 5, they're listed above";
        public static string settings = "Current Settings are: ShowUnnecessary or shun, ShowProperties or shpr, ShowPID or shpid, ShowEWMessage or shewmsg" +
                "\nSettings are caps insensitive. shun, shpr, and etc are the Setting Name shortcuts.";

        public static int FindSlot(Item[] Inventory)
        {
            for (int i = 0; i < 49; i++)
            {
                if (Inventory[i].IsAir && Inventory[i].type == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        public override void Load()
        {
            Config.Load();
        }
    }
}
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier
{
    public class ItemModifier : Mod
    {
        public static string PropParams = "Parameters: shoot or s, shootspeed or ss, createtile or ct, usetime or ut, useanimation or ua, healmana or hm, heallife or hl, axe or a, pick or p, hammer or h, autoreuse or au, critical or c, damage or d, knockback or kb, tileboost or tb, buff or b, bufftime or bt, stack or st(amount or amt)" +
                    "\nParameters are caps insensitive meaning, you can do /gi 757 ShOoT 5 and it will still shoot a Jester's Arrow" +
                    "\nParameters have shortcuts, eg /gi 757 s 5, they're listed above";
        public static string settings = "Current Settings are: ShowUnnecessary or shun, ShowProperties or shpr, ShowEWMessage or shewmsg" +
                "\nSettings are caps insensitive. shun, shpr, and etc are the Setting Name shortcuts.";
        public static string TypeIDParams = "Parameters: item or i, tile or t, projectile or p, buff or b";
        public static int ItemId = 0;

        public static bool FindSlot(Item[] Inventory, out int slot)
        {
            for (int i = 0; i < 49; i++)
            {
                if (Inventory[i].IsAir && Inventory[i].type == 0)
                {
                    slot = i;
                    return true;
                }
            }

            slot = -1;
            return false;
        }

        public override void Load()
        {
            Config.Load();
        }
    }
}
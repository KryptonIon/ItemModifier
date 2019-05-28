using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ItemModifier.Utilities;
using Terraria;

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

        public static Identifier ConstructIdentifier(string Name, string Value) => ConstructIdentifier(Name, Value, helpColor);

        public static Identifier ConstructIdentifier(string Name, string Value, Color Color) => new Identifier(Name, Value, Color);

        public static int FindSlot(Item[] Inventory)
        {
            for (int i = 0; i < 49; i++)
            {
                if(Inventory[i].IsAir && Inventory[i].type == 0)
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
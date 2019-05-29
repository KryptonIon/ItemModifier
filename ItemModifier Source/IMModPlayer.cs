using Terraria;
using Terraria.ModLoader;

namespace ItemModifier
{
    public class IMModPlayer : ModPlayer
    {
        public override void OnEnterWorld(Player player)
        {
            var helpColor = ItemModifier.helpColor;
            if (Config.ShowEWMessage)
            {
                Main.NewText("ItemModifier: Remember to check the documentation for a more complicated explanation of this mod. The documentation can be found at Mods > Item Modifier > Visit Mod's Homepage.", helpColor);
                Main.NewText("Do /gi to get started.");
            }
        }
    }
}
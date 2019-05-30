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
                Main.NewText("ItemModifier: Remember to check the wiki, do /wiki to open the wiki", helpColor);
            }
        }
    }
}
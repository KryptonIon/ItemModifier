using Terraria.GameInput;
using Terraria.ModLoader;

namespace ItemModifier
{
    internal class ItemModifierPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();
            if (instance.MainUI != null)
            {
                if (instance.ToggleItemModifierUIHotKey.JustPressed)
                    instance.MainUI.ToggleItemModifierUI();
                if (instance.ToggleNewItemUIHotKey.JustPressed)
                    instance.MainUI.ToggleNewItemUI();
                if (instance.OpenWikiHotKey.JustPressed)
                    ItemModifier.OpenWiki();
            }
        }
    }
}

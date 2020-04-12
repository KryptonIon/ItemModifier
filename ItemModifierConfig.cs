using ItemModifier.UI;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ItemModifier
{
    public class ItemModifierConfig : ModConfig
    {
        public override ConfigScope Mode
        {
            get
            {
                return ConfigScope.ClientSide;
            }
        }

        public static ItemModifierConfig Instance;

        [Label("Limited")]
        [Tooltip("If true, Item Modifier will set boundaries for some properties.\nChanging this setting will cause ItemModiferUI to reload")]
        [DefaultValue(true)]
        public bool Limited { get; set; }

        public override void OnChanged()
        {
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();
            ItemModifyUIW window = instance.MainUI?.ModifyWindow;
            if (window == null)
            {
                return;
            }
            window.RemoveAllChildren();
            window.Initialize();
            window.CategoryIndex = window.CategoryIndex;
            window.Visible = window.Visible;
            window.LiveSync = window.LiveSync;
        }
    }
}

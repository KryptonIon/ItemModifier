using ItemModifier.UI;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ItemModifier
{
    public class UIConfig : ModConfig
    {
        public override ConfigScope Mode
        {
            get
            {
                return ConfigScope.ClientSide;
            }
        }

        public static UIConfig Instance;

        [Label("Limited")]
        [Tooltip("If true, Item Modifier will set boundaries for some properties.\nChanging this setting will cause ItemModiferUI to reload")]
        [DefaultValue(true)]
        public bool Limited { get; set; }

        public override void OnChanged()
        {
            if (!Main.gameMenu)
            {
                ItemModifier instance = ModContent.GetInstance<ItemModifier>();
                ItemModifyUIW window = instance.MainUI?.ItemModifierWindow;
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
}

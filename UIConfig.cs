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
        [Tooltip("If true, Item Modifier will set boundaries for some properties")]
        [DefaultValue(true)]
        public bool Limited { get; set; }

        [Label("DebugLogs")]
        [Tooltip("If true, the current state of the UI will be logged every time the mod loads")]
        [ReloadRequired]
        [DefaultValue(false)]
        public bool DebugLogs { get; set; }

        public override void OnChanged()
        {
            base.OnChanged();

            ItemModifier instance = ModContent.GetInstance<ItemModifier>();
            instance.MainUI?.ItemModifierWindow.SetLimits();
        }
    }
}

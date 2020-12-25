using ItemModifier.UI;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.Collections.Generic;

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

        [Label("Show Property Icons")]
        [Tooltip("If false, property icons will be hidden")]
        [DefaultValue(true)]
        public bool ShowPropertyIcons { get; set; }

        public override void OnChanged()
        {
            base.OnChanged();

            ItemModifier instance = ModContent.GetInstance<ItemModifier>();
            MainInterface mainUI = instance.MainUI;
            if (mainUI != null)
            {
                mainUI.ItemModifierWindow.SetLimits();

                List<UICategory.UIProperty> properties = mainUI.ItemModifierWindow.AllCategory.Properties;
                for (int i = 0; i < properties.Count; i++)
                    properties[i].imageLabel.Visible = ShowPropertyIcons;
            }
        }
    }
}

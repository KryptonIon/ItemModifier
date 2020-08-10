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
        [ReloadRequired]
        [DefaultValue(true)]
        public bool Limited { get; set; }
    }
}

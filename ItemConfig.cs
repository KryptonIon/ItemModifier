using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ItemModifier
{
    public class ItemConfig : ModConfig
    {
        public override ConfigScope Mode
        {
            get
            {
                return ConfigScope.ClientSide;
            }
        }

        public static ItemConfig Instance;

        [Label("Global Modifications")]
        [Tooltip("These modifications apply to all items")]
        [DefaultValue(typeof(List<ItemProperties>), "")]
        [ReloadRequired]
        public Dictionary<int, ItemProperties> GlobalModifications { get; set; }
    }
}

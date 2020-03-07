using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ItemModifier
{
    public class KRConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static KRConfig Instance;

        [Label("HelpMessage")]
        [Tooltip("Defines whether there will be a message box with basic instructions.")]
        [DefaultValue(true)]
        public bool HelpMessage { get; set; } = true;

        [Label("Error Color")]
        [Tooltip("Color of Error Messages")]
        [DefaultValue(typeof(Color), "255, 0, 0, 255")]
        public Color ErrorColor { get; set; } = new Color(255, 0, 0);
    }
}

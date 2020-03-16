using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ItemModifier.UIKit
{
    public class UIText : UIElement
    {
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
                Recalculate();
            }
        }

        private string text;

        public Color TextColor { get; set; } = Color.White;

        public bool SkipDescenderCheck { get; set; } = false;

        public UIText(string text, Vector4 padding = default, Vector4 margin = default) : base(padding, margin)
        {
            Text = text;
        }

        public UIText(string text, Color textColor, Vector4 padding = default, Vector4 margin = default) : this(text, padding, margin)
        {
            TextColor = textColor;
        }

        public override void Recalculate()
        {
            RecalculateSize();
            base.Recalculate();
        }

        private void RecalculateSize()
        {
            Vector2 size = KRUtils.MeasureTextAccurate(Text, SkipDescenderCheck);
            Width = new StyleDimension(size.X);
            Height = new StyleDimension(size.Y);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            Utils.DrawBorderString(sb, Text, Dimensions.Position, TextColor);
        }
    }
}

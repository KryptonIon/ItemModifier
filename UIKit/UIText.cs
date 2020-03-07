using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ItemModifier.UIKit
{
    public class UIText : UIElement
    {
        public string Text
        {
            get => _text;

            set
            {
                _text = value;
                RecalculateSize();
            }
        }

        private string _text;

        public Color TextColor { get; set; } = Color.White;

        public bool SkipDescenderCheck { get; set; } = false;

        public UIText(string Text, Vector4 Padding = default, Vector4 Margin = default) : base(Padding, Margin) => (this.Text, Height) = (Text, new StyleDimension(21));

        public UIText(string Text, Color TextColor, Vector4 Padding = default, Vector4 Margin = default) : this(Text, Padding, Margin) => this.TextColor = TextColor;
        
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

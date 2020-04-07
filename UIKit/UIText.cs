using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ItemModifier.UIKit.Utils;
using static Terraria.Utils;

namespace ItemModifier.UIKit
{
    public class UIText : UIElement
    {
        private string text;

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

        public Color TextColor { get; set; } = Color.White;

        public bool SkipDescenderCheck { get; set; }

        public float Scale { get; set; } = 1f;

        public Vector2 Anchor { get; set; }

        public UIText(string text)
        {
            Text = text;
        }

        public UIText(string text, Color textColor) : this(text)
        {
            TextColor = textColor;
        }

        public UIText(string text, Color textColor, float scale, Vector2 anchor) : this(text, textColor)
        {
            Scale = scale;
            Anchor = anchor;
        }

        protected internal override void RecalculateSelf()
        {
            RecalculateTextSize();
            base.RecalculateSelf();
        }

        protected virtual void RecalculateTextSize()
        {
            Vector2 size = MeasureString2(Text, SkipDescenderCheck);
            Width = new SizeDimension(size.X);
            Height = new SizeDimension(size.Y);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            DrawBorderString(sb, Text, InnerPosition, TextColor, Scale, Anchor.X, Anchor.Y);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    public class UILine : UIElement
    {
        public Color LineColor { get; set; } = Color.Black;

        public bool Horizontal { get; set; }

        public UILine(float length, float thickness, bool horizontal = true)
        {
            Horizontal = horizontal;
            Width = new SizeDimension(length);
            Height = new SizeDimension(thickness);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (Horizontal)
            {
                sb.Draw(ItemModifier.Textures.HorizontalLine, InnerRect, LineColor);
            }
            else
            {
                sb.Draw(ItemModifier.Textures.HorizontalLine, InnerRect, null, LineColor, 1.571f, Vector2.Zero, SpriteEffects.None, 0);
            }
        }
    }
}

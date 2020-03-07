using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    public class UILine : UIElement
    {
        public Color LineColor { get; set; } = Color.Black;

        public bool Horizontal { get; set; } = true;

        public UILine(float Length, float Thickness, bool Horizontal = true, Vector4 Padding = default, Vector4 Margin = default) : base(Padding, Margin)
        {
            if (Horizontal)
            {
                Width = new StyleDimension(Length);
                Height = new StyleDimension(Thickness);
            }
            else
            {
                Width = new StyleDimension(Thickness);
                Height = new StyleDimension(Length);
            }
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(Horizontal ? ItemModifier.Textures.HorizontalLine : ItemModifier.Textures.VerticalLine, Dimensions.Rectangle, LineColor);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    public class UILine : UIElement
    {
        public Color LineColor { get; set; } = Color.Black;

        public bool Horizontal { get; set; } = true;

        public UILine(float length, float thickness, bool horizontal = true, Vector4 padding = default, Vector4 margin = default) : base(padding, margin)
        {
            if (horizontal)
            {
                Width = new StyleDimension(length);
                Height = new StyleDimension(thickness);
            }
            else
            {
                Width = new StyleDimension(thickness);
                Height = new StyleDimension(length);
            }
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(Horizontal ? ItemModifier.Textures.HorizontalLine : ItemModifier.Textures.VerticalLine, Dimensions.Rectangle, LineColor);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    public class UIContainer : UIElement
    {
        public Color BackgroundColor { get; set; } = Color.Transparent;

        public UIContainer(Vector2 Size, Vector4 Padding = default, Vector4 Margin = default) : base(Padding, Margin) => (Width, Height) = (new StyleDimension(Size.X), new StyleDimension(Size.Y));

        public UIContainer(Color BackgroundColor, Vector2 Size, Vector4 Padding = default, Vector4 Margin = default) : this(Size, Padding, Margin) => this.BackgroundColor = BackgroundColor;

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (BackgroundColor.A < 255) sb.Draw(ItemModifier.Textures.WindowBackground, Dimensions.Rectangle, BackgroundColor);
        }
    }
}

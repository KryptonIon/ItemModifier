using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    public class UIImage : UIElement
    {
        private Texture2D _image;

        public Texture2D Image
        {
            get => _image;
            set
            {
                _image = value;
                Width = new StyleDimension(_image.Width);
                Height = new StyleDimension(_image.Height);
            }
        }

        public Color ColorTint;

        public UIImage(Texture2D Image, Color? ColorTint = null, Vector4 Margin = default) : base(Margin: Margin) => (this.Image, this.ColorTint, Width, Height) = (Image, ColorTint ?? Color.White, new StyleDimension(Image.Width), new StyleDimension(Image.Height));

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(Image, Dimensions.Rectangle, ColorTint);
        }
    }
}

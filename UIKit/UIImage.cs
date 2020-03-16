using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    public class UIImage : UIElement
    {
        private Texture2D image;

        public Texture2D Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                Width = new StyleDimension(image.Width);
                Height = new StyleDimension(image.Height);
            }
        }

        public Color ColorTint;

        public UIImage(Texture2D image, Color? colorTint = null, Vector4 margin = default) : base(margin: margin)
        {
            Image = image;
            ColorTint = colorTint;
            Width = new StyleDimension(image.Width);
            Height = new StyleDimension(image.Height);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(Image, Dimensions.Rectangle, ColorTint);
        }
    }
}

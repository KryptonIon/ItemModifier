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
                if (AutoScale)
                {
                    Width = new SizeDimension(Image.Width);
                    Height = new SizeDimension(Image.Height);
                    Recalculate();
                }
            }
        }

        public bool AutoScale { get; set; }

        public Color ColorTint { get; set; }

        public UIImage(Texture2D image, bool autoScale = true, Color? colorTint = null)
        {
            AutoScale = autoScale;
            Image = image;
            ColorTint = colorTint ?? Color.White;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            sb.Draw(Image, InnerRect, ColorTint);
        }
    }
}

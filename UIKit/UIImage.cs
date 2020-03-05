using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for image display.
    /// </summary>
    public class UIImage : UIElement
    {
        private Texture2D _image;

        /// <summary>
        /// Image to be displayed.
        /// </summary>
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

        /// <summary>
        /// Color the texture; White if no tinting is applied.
        /// </summary>
        public Color ColorTint;

        /// <summary>
        /// Initializes a new <see cref="UIImage"/> Element.
        /// </summary>
        /// <param name="Image">Image to be displayed.</param>
        /// <param name="ColorTint">Color of texture, white if no tinting applied.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIImage(Texture2D Image, Color? ColorTint = null, Vector4 Margin = default) : base(Margin: Margin) => (this.Image, this.ColorTint, Width, Height) = (Image, ColorTint ?? Color.White, new StyleDimension(Image.Width), new StyleDimension(Image.Height));

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(Image, Dimensions.Rectangle, ColorTint);
        }
    }
}

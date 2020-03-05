using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element to contain other elements.
    /// </summary>
    public class UIContainer : UIElement
    {
        /// <summary>
        /// Color of the background.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.Transparent;

        /// <summary>
        /// Initializes a new <see cref="UIContainer"/> Element.
        /// </summary>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIContainer(Vector2 Size, Vector4 Padding = default, Vector4 Margin = default) : base(Padding, Margin) => (Width, Height) = (new StyleDimension(Size.X), new StyleDimension(Size.Y));

        /// <summary>
        /// Initializes a new <see cref="UIContainer"/> Element.
        /// </summary>
        /// <param name="BackgroundColor">Color of the background.</param>
        /// <param name="Size">Size of the element.</param>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIContainer(Color BackgroundColor, Vector2 Size, Vector4 Padding = default, Vector4 Margin = default) : this(Size, Padding, Margin) => this.BackgroundColor = BackgroundColor;
        
        protected override void DrawSelf(SpriteBatch sb)
        {
            if (BackgroundColor.A < 255) sb.Draw(ItemModifier.Textures.WindowBackground, Dimensions.Rectangle, BackgroundColor);
        }
    }
}

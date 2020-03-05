using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for line display.
    /// </summary>
    public class UILine : UIElement
    {
        /// <summary>
        /// Color of the line.
        /// </summary>
        public Color LineColor { get; set; } = Color.Black;

        /// <summary>
        /// If true, line will be horizontal, vertical otherwise.
        /// </summary>
        public bool Horizontal { get; set; } = true;

        /// <summary>
        /// Initializes a new <see cref="UILine"/> Element.
        /// </summary>
        /// <param name="Length">Length of line.</param>
        /// <param name="Thickness">Thickness of line.</param>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
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

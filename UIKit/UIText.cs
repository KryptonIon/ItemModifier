using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for text display.
    /// </summary>
    public class UIText : UIElement
    {
        /// <summary>
        /// Text to display.
        /// </summary>
        public string Text
        {
            get => _text;

            set
            {
                _text = value;
                RecalculateSize();
            }
        }

        private string _text;

        /// <summary>
        /// Color of text.
        /// </summary>
        public Color TextColor { get; set; } = Color.White;

        /// <summary>
        /// Wheter the presence of the letters g, j, p, q, Q or y will affect the height
        /// </summary>
        public bool SkipDescenderCheck { get; set; } = false;

        /// <summary>
        /// Initializes a new <see cref="UIText"/> Element.
        /// </summary>
        /// <param name="Text">Text to display.</param>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIText(string Text, Vector4 Padding = default, Vector4 Margin = default) : base(Padding, Margin) => (this.Text, Height) = (Text, new StyleDimension(21));

        /// <summary>
        /// Initializes a new <see cref="UIText"/> Element.
        /// </summary>
        /// <param name="Text">Text to display.</param>
        /// <param name="TextColor">Color of text.</param>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIText(string Text, Color TextColor, Vector4 Padding = default, Vector4 Margin = default) : this(Text, Padding, Margin)
        {
            this.TextColor = TextColor;
        }

        public override void Recalculate()
        {
            RecalculateSize();
            base.Recalculate();
        }

        private void RecalculateSize()
        {
            Vector2 size = KRUtils.MeasureTextAccurate(Text, SkipDescenderCheck);
            Width = new StyleDimension(size.X);
            Height = new StyleDimension(size.Y);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            Utils.DrawBorderString(sb, Text, Dimensions.Position, TextColor);
        }
    }
}

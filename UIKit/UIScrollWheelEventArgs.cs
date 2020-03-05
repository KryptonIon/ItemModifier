using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Event args for scroll wheel events.
    /// </summary>
    public class UIScrollWheelEventArgs : UIMouseEventArgs
    {
        /// <summary>
        /// Amount of scrolling.
        /// </summary>
        public int ScrollWheelValue { get; }

        /// <summary>
        /// Initializes a new <see cref="UIScrollWheelEventArgs"/>.
        /// </summary>
        /// <param name="Target">Target of the cursor.</param>
        /// <param name="MousePosition">Position of the mouse.</param>
        /// <param name="ScrollWheelValue">Amount of scrolling.</param>
        public UIScrollWheelEventArgs(UIElement Target, Vector2 MousePosition, int ScrollWheelValue) : base(Target, MousePosition) => this.ScrollWheelValue = ScrollWheelValue;
    }
}

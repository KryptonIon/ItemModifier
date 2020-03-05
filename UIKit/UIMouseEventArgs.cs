using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Event args for mouse events.
    /// </summary>
    public class UIMouseEventArgs : UIEventArgs
    {
        /// <summary>
        /// Position of the mouse.
        /// </summary>
        public Vector2 MousePosition { get; }

        /// <summary>
        /// Initializes a new <see cref="UIMouseEventArgs"/>
        /// </summary>
        /// <param name="Target">Target of the cursor.</param>
        /// <param name="MousePosition">Position of the mouse.</param>
        public UIMouseEventArgs(UIElement Target, Vector2 MousePosition) : base(Target) => this.MousePosition = MousePosition;
    }
}

using System;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Event args for UI Mouse Events.
    /// </summary>
    public class UIEventArgs : EventArgs
    {
        /// <summary>
        /// Target of the cursor.
        /// </summary>
        public UIElement Target { get; }

        /// <summary>
        /// Initializes a new <see cref="UIEventArgs"/>.
        /// </summary>
        /// <param name="Target"></param>
        public UIEventArgs(UIElement Target) => this.Target = Target;
    }
}

using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    public class UIMouseEventArgs : UIEventArgs
    {
        public Vector2 MousePosition { get; }

        public UIMouseEventArgs(UIElement Target, Vector2 MousePosition) : base(Target) => this.MousePosition = MousePosition;
    }
}

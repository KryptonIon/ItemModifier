using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    public class UIScrollWheelEventArgs : UIMouseEventArgs
    {
        public int ScrollWheelValue { get; }

        public UIScrollWheelEventArgs(UIElement Target, Vector2 MousePosition, int ScrollWheelValue) : base(Target, MousePosition) => this.ScrollWheelValue = ScrollWheelValue;
    }
}

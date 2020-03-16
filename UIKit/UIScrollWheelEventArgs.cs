using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    public class UIScrollWheelEventArgs : UIMouseEventArgs
    {
        public int ScrollWheelValue { get; }

        public UIScrollWheelEventArgs(UIElement target, Vector2 mousePosition, int scrollWheelValue) : base(target, mousePosition)
        {
            ScrollWheelValue = scrollWheelValue;
        }
    }
}

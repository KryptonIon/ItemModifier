using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    public class UIMouseEventArgs : UIEventArgs
    {
        public Vector2 MousePosition { get; }

        public UIMouseEventArgs(UIElement target, Vector2 mousePosition) : base(target)
        {
            MousePosition = mousePosition;
        }
    }
}

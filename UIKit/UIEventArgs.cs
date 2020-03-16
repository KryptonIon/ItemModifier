using System;

namespace ItemModifier.UIKit
{
    public class UIEventArgs : EventArgs
    {
        public UIElement Target { get; }

        public UIEventArgs(UIElement target)
        {
            Target = target;
        }
    }
}

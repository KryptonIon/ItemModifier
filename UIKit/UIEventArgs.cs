using System;

namespace ItemModifier.UIKit
{
    public class UIEventArgs : EventArgs
    {
        public UIElement Target { get; }

        public UIEventArgs(UIElement Target) => this.Target = Target;
    }
}

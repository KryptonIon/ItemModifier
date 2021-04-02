using System;

namespace ItemModifier.UIKit.Inputs
{
    public class UIRadioButtonEventArgs : EventArgs
    {
        public UIRadioButton Target { get; }

        public UIRadioButtonEventArgs(UIRadioButton target)
        {
            Target = target;
        }
    }
}
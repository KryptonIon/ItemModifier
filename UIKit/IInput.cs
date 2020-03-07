namespace ItemModifier.UIKit
{
    internal interface IInput<T>
    {
        event UIElement.UIEventHandler<T> OnValueChanged;

        T Value { get; set; }
    }
}

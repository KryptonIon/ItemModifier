namespace ItemModifier.UIKit.Inputs
{
    public delegate void UIValueChangedEventHandler<T>(UIElement sender, EventArgs<T> e);

    public interface IInput<T>
    {
        event UIValueChangedEventHandler<T> OnValueChanged;

        T Value { get; set; }
    }
}

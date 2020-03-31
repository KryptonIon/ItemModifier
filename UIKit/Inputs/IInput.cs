using static ItemModifier.UIKit.UIElement;

namespace ItemModifier.UIKit.Inputs
{
    public interface IInput<T>
    {
        event UIEventHandler<T> OnValueChanged;

        T Value { get; set; }
    }
}

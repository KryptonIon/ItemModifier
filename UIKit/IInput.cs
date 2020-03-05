namespace ItemModifier.UIKit
{
    /// <summary>
    /// Defines a value and an event that a UI Element can implement to determine what type of input it is.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IInput<T>
    {
        /// <summary>
        /// Fired when <see cref="Value"/> is changed.
        /// </summary>
        event UIElement.UIEventHandler<T> OnValueChanged;

        /// <summary>
        /// Value of input.
        /// </summary>
        T Value { get; set; }
    }
}

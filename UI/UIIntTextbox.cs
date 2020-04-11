using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;

namespace ItemModifier.UI
{
    public class UIIntTextbox : UITextbox, IInput<int>
    {
        public event UIEventHandler<EventArgs<int>> OnValueChanged;

        private int _value;

        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (_value != value)
                {
                    _value = value > MaxValue ? MaxValue : value < MinValue ? MinValue : value;
                    DrawText = Value.ToString();
                    OnValueChanged?.Invoke(this, new EventArgs<int>(Value));
                }
            }
        }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public UIIntTextbox(int minValue = int.MinValue, int maxValue = int.MaxValue) : base(11)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            _value = 0 > MaxValue ? MaxValue : 0 < MinValue ? MinValue : 0;
            DrawText = Value.ToString();
            OnUnfocused += (source) =>
            {
                if (string.IsNullOrEmpty(Text))
                {
                    Value = 0;
                }
                else if (Text.Length == 1 && Text[0] == '-')
                {
                    Value = 0;
                }
                else
                {
                    if (int.TryParse(Text, out int val))
                    {
                        Value = val;
                    }
                    else
                    {
                        Value = Text.StartsWith("-") ? MinValue : MaxValue;
                    }
                }
            };
            OnTextChanged += (source, e) =>
            {
                string value = e.Value;
                string newText = "";
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                if (char.IsDigit(value[0]) || value[0] == '-')
                {
                    newText += value[0];
                }
                int i = 1;
                while (i < CaretPosition)
                {
                    if (char.IsDigit(value[i]))
                    {
                        newText += value[i];
                    }

                    i++;
                }
                CaretPosition = newText.Length;
                while (i < value.Length)
                {
                    if (char.IsDigit(value[i]))
                    {
                        newText += value[i];
                    }

                    i++;
                }
                DrawText = newText;
            };
        }
    }
}

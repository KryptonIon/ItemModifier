using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;

namespace ItemModifier.UI
{
    public class UIFloatTextbox : UITextbox, IInput<float>
    {
        public event UIEventHandler<EventArgs<float>> OnValueChanged;

        private float _value;

        public float Value
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
                    CaretPosition = DrawText.Length;
                    OnValueChanged?.Invoke(this, new EventArgs<float>(Value));
                }
            }
        }

        private float maxValue;

        public float MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                maxValue = value;
                Value = Value;
            }
        }

        private float minValue;

        public float MinValue
        {
            get
            {
                return minValue;
            }

            set
            {
                minValue = value;
                Value = Value;
            }
        }

        public UIFloatTextbox(float maxValue = float.MaxValue, float minValue = float.MinValue) : base(11)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            _value = 0f > MaxValue ? MaxValue : 0f < MinValue ? MinValue : 0f;
            DrawText = Value.ToString();
            CaretPosition = DrawText.Length;
            OnUnfocused += (source) =>
            {
                if (string.IsNullOrEmpty(Text))
                {
                    Value = 0f;
                }
                else if (Text.Length == 1 && Text[0] == '-')
                {
                    Value = 0f;
                }
                else
                {
                    if (float.TryParse(Text, out float val))
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
                if (string.IsNullOrEmpty(e.Value))
                {
                    return;
                }
                if (char.IsDigit(value[0]) || value[0] == '-')
                {
                    newText += value[0];
                }
                int i = 1;
                bool dot = false;
                while (i < CaretPosition)
                {
                    if (char.IsDigit(value[i]))
                    {
                        newText += value[i];
                    }
                    else if (!dot && value[i] == '.')
                    {
                        newText += value[i];
                        dot = true;
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
                    else if (!dot && value[i] == '.')
                    {
                        newText += value[i];
                        dot = true;
                    }
                    i++;
                }
                DrawText = newText;
            };
        }
    }
}

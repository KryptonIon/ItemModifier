using ItemModifier.UIKit.Inputs;

namespace ItemModifier.UI
{
    public class UIFloatTextbox : UITextbox, IInput<float>
    {
        public event UIEventHandler<float> OnValueChanged;

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
                    OnValueChanged?.Invoke(this, Value);
                }
            }
        }

        public float MaxValue { get; set; }

        public float MinValue { get; set; }

        public UIFloatTextbox(float maxValue = float.MaxValue, float minValue = float.MinValue) : base(11)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            _value = 0f > MaxValue ? MaxValue : 0f < MinValue ? MinValue : 0f;
            DrawText = Value.ToString();
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
                string newText = "";
                if (string.IsNullOrEmpty(e))
                {
                    return;
                }
                if (char.IsDigit(e[0]) || e[0] == '-')
                {
                    newText += e[0];
                }
                int i = 1;
                bool dot = false;
                while (i < CaretPosition)
                {
                    if (char.IsDigit(e[i]))
                    {
                        newText += e[i];
                    }
                    else if (!dot && e[i] == '.')
                    {
                        newText += e[i];
                        dot = true;
                    }
                    i++;
                }
                CaretPosition = newText.Length;
                while (i < e.Length)
                {
                    if (char.IsDigit(e[i]))
                    {
                        newText += e[i];
                    }
                    else if (!dot && e[i] == '.')
                    {
                        newText += e[i];
                        dot = true;
                    }
                    i++;
                }
                DrawText = newText;
            };
        }
    }
}

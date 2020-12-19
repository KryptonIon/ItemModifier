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
                    Text = _value.ToString();
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
                if (Value > maxValue)
                {
                    Value = maxValue;
                }
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
                if (Value < minValue)
                {
                    Value = minValue;
                }
            }
        }

        public UIFloatTextbox(float maxValue = float.MaxValue, float minValue = float.MinValue) : base(11)
        {
            _value = 0f > MaxValue ? MaxValue : 0f < MinValue ? MinValue : 0f;
            this.maxValue = maxValue;
            this.minValue = minValue;
            Text = _value.ToString();
            OnUnfocused += (source) => ParseText();
            OnTextChanged += (source, e) =>
            {
                if (!Focused)
                {
                    ParseText();
                }
            };
        }

        private void ParseText()
        {
            if (float.TryParse(Text, out float val))
            {
                Value = val;
            }
            else
            {
                Text = Value.ToString();
            }
        }

        protected override void ProcessInput(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string newText = "";
                if (input[0] == '-')
                {
                    newText += input[0];
                }
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i].IsHADigit() || input[i] == '.')
                    {
                        newText += input[i];
                    }
                }
                input = newText;
            }
            base.ProcessInput(input);
        }
    }
}

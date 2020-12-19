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
                    Text = _value.ToString();
                    OnValueChanged?.Invoke(this, new EventArgs<int>(Value));
                }
            }
        }

        private int maxValue;

        public int MaxValue
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

        private int minValue;

        public int MinValue
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

        public UIIntTextbox(int minValue = int.MinValue, int maxValue = int.MaxValue) : base(11)
        {
            _value = 0 > MaxValue ? MaxValue : 0 < MinValue ? MinValue : 0;
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
            if (int.TryParse(Text, out int val))
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
                    if (input[i].IsHADigit())
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

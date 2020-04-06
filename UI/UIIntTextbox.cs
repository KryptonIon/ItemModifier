using ItemModifier.UIKit.Inputs;

namespace ItemModifier.UI
{
    public class UIIntTextbox : UITextbox, IInput<int>
    {
        public event UIEventHandler<int> OnValueChanged;

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
                    OnValueChanged?.Invoke(this, Value);
                }
            }
        }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public bool Negateable { get; set; } = true;

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
                while (i < CaretPosition)
                {
                    if (char.IsDigit(e[i]))
                    {
                        newText += e[i];
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

                    i++;
                }
                DrawText = newText;
            };
        }
    }
}

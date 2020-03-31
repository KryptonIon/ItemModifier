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
                    _value = value > MaxValue ? int.MaxValue : value < MinValue ? MinValue : value;
                    Text = Value.ToString();
                    OnValueChanged?.Invoke(this, Value);
                }
            }
        }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public bool Negateable { get; set; } = true;

        public UIIntTextbox(int maxValue = int.MaxValue, int minValue = int.MinValue) : base(10)
        {
            text = "0";
            MaxValue = maxValue;
            MinValue = minValue;
            OnFocusChanged += (source, e) =>
            {
                if (!e) _value = int.Parse(Text);
            };
            OnTextChanged += (source, e) =>
            {
                string newText = "";
                if(string.IsNullOrEmpty(e))
                {
                    Text = "0";
                    return;
                }
                if (Negateable && e[0] == '-') newText += e[0];
                for (int i = 1; i < e.Length; i++)
                {
                    if (char.IsDigit(e[i])) newText += e[i];
                }
                newText.Trim('0');
                if (string.IsNullOrEmpty(newText)) Text = "0";
                else Text = newText;
            };
        }
    }
}

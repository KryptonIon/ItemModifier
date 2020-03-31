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
                    _value = value;
                    OnValueChanged?.Invoke(this, Value);
                }
            }
        }

        public float MaxValue { get; set; }

        public float MinValue { get; set; }

        public bool Negateable { get; set; } = true; 

        public UIFloatTextbox(float maxValue = float.MaxValue, float minValue = float.MinValue) : base(38)
        {
            MaxValue = maxValue;
            MinValue = minValue;
            OnFocusChanged += (source, e) =>
            {
                if (!e)
                {
                    _value = float.Parse(Text);
                }
            };
            OnTextChanged += (source, e) =>
            {
                string newText = "";
                bool hasDot = false;
                if (string.IsNullOrEmpty(e))
                {
                    Text = "0";
                    return;
                }
                if (Negateable && e[0] == '-') newText += e[0];
                for (int i = 1; i < e.Length; i++)
                {
                    if (!hasDot && e[i] == '.') hasDot = true;
                    if (!char.IsDigit(e[i])) continue;
                    newText += e[i];
                }
                newText.Trim('0');
                if (string.IsNullOrEmpty(newText)) Text = "0";
                else Text = newText;
            };
        }
    }
}

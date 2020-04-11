using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit.Inputs
{
    public class UIBool : UIText, IInput<bool>
    {
        public event UIEventHandler<EventArgs<bool>> OnValueChanged;

        private bool _value;

        public bool Value
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
                    Text = Value ? TrueText : FalseText;
                    OnValueChanged?.Invoke(this, new EventArgs<bool>(Value));
                }
            }
        }

        public string TrueText { get; set; } = "True";

        public string FalseText { get; set; } = "False";

        public UIBool(bool value = false) : base(value.ToString())
        {
            Value = value;
        }

        public UIBool(string trueText, string falseText, bool value = false) : this(value)
        {
            TrueText = trueText;
            FalseText = falseText;
        }

        public override void MouseOver(UIMouseEventArgs e)
        {
            Main.PlaySound(SoundID.MenuTick);
            base.MouseOver(e);
        }

        public override void LeftClick(UIMouseEventArgs e)
        {
            Value = !Value;
            Main.PlaySound(SoundID.MenuTick);
            base.LeftClick(e);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    public class UIBool : UIElement, IInput<bool>
    {
        public event UIEventHandler<bool> OnValueChanged;

        private bool PrivateValue;

        public bool Value
        {
            get
            {
                return PrivateValue;
            }

            set
            {
                if (PrivateValue != value)
                {
                    PrivateValue = value;
                    OnValueChanged?.Invoke(this, PrivateValue);
                }
            }
        }

        public string TrueText { get; set; } = "True";

        public string FalseText { get; set; } = "False";

        public UIBool(bool value = false, Vector4 margin = default) : base(margin: margin)
        {
            Width = new StyleDimension(41f);
            Height = new StyleDimension(21f);
            Value = value;
        }

        public UIBool(bool value, string trueText, string falseText, Vector4 margin = default) : this(value, margin)
        {
            TrueText = trueText;
            FalseText = falseText;
        }

        public override void LeftClick(UIMouseEventArgs e)
        {
            Value = !Value;
            Main.PlaySound(SoundID.MenuTick);
            base.LeftClick(e);
        }

        public override void MouseOver(UIMouseEventArgs e)
        {
            Main.PlaySound(SoundID.MenuTick);
            base.MouseOver(e);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            Utils.DrawBorderString(sb, Value ? (TrueText ?? "") : (FalseText ?? ""), Dimensions.Position, Color.White);
        }
    }
}

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
            get => PrivateValue;

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

        public UIBool(bool Value = false, Vector4 Margin = default) : base(Margin: Margin) => (Width, Height, this.Value) = (new StyleDimension(41), new StyleDimension(21), Value);

        public UIBool(bool Value, string TrueText, string FalseText, Vector4 Margin = default) : this(Value, Margin) => (this.TrueText, this.FalseText) = (TrueText, FalseText);

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

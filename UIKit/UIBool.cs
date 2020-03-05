using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for true/false input.
    /// </summary>
    public class UIBool : UIElement, IInput<bool>
    {
        /// <summary>
        /// Fired when <see cref="Value"/> is changed.
        /// </summary>
        public event UIEventHandler<bool> OnValueChanged;

        private bool PrivateValue;

        /// <summary>
        /// Value of this input.
        /// </summary>
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

        /// <summary>
        /// Displayed if <see cref="Value"/> is true.
        /// </summary>
        public string TrueText { get; set; } = "True";

        /// <summary>
        /// Dispalyed if <see cref="Value"/> is false.
        /// </summary>
        public string FalseText { get; set; } = "False";

        /// <summary>
        /// Creates a new <see cref="UIBool"/> object.
        /// </summary>
        /// <param name="Value">Default value of <see cref="Value"/>.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIBool(bool Value = false, Vector4 Margin = default) : base(Margin: Margin) => (Width, Height, this.Value) = (new StyleDimension(41), new StyleDimension(21), Value);

        /// <summary>
        /// Initializes a new <see cref="UIBool"/> Element.
        /// </summary>
        /// <param name="Value">Default value of <see cref="Value"/>.</param>
        /// <param name="TrueText">Text that will be displayed if <see cref="Value"/> is true.</param>
        /// <param name="FalseText">Text that will be displayed if <see cref="Value"/> is false.</param>
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for check/x input.
    /// </summary>
    public class UICheckbox : UIElement, IInput<bool>
    {
        /// <summary>
        /// Fired when <see cref="Value"/> is changed.
        /// </summary>
        public event UIEventHandler<bool> OnValueChanged;

        private bool PrivateValue;

        /// <summary>
        /// True if the checked, false otherwise.
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
        /// Initializes a new <see cref="UICheckbox"/> Element.
        /// </summary>
        /// <param name="Margin">Add space around the element.</param>
        public UICheckbox(Vector4 Margin = default) : base(Margin) => (Width, Height) = (new StyleDimension(18f), new StyleDimension(18f));

        public UICheckbox(bool Value, Vector4 Margin = default) : this(Margin) => this.Value = Value;

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
            sb.Draw(ItemModifier.Textures.Checkbox, Dimensions.Position, new Rectangle(Value ? 20 : 0, 0, 18, 18), Color.White);
        }
    }
}

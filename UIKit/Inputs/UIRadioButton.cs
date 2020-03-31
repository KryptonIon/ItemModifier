using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit.Inputs
{
    public class UIRadioButton : UIText, IInput<bool>
    {
        public event UIEventHandler<bool> OnValueChanged;

        private bool selected;

        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnValueChanged?.Invoke(this, Selected);
                }
            }
        }

        bool IInput<bool>.Value
        {
            get
            {
                return Selected;
            }

            set
            {
                Selected = value;
            }
        }

        public UIRadioButton(string label) : base(label)
        {

        }

        public override void LeftClick(UIMouseEventArgs e)
        {
            Parent?.SelectRadio(this);
            Selected = !Selected;
            Main.PlaySound(SoundID.MenuTick);
            base.LeftClick(e);
        }

        public override void MouseOver(UIMouseEventArgs e)
        {
            Main.PlaySound(SoundID.MenuTick);
            base.MouseOver(e);
        }

        protected override void RecalculateTextSize()
        {
            base.RecalculateTextSize();
            Width = new SizeDimension(Width.Pixels + 14f);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(ItemModifier.Textures.SquareSelect, new Vector2(PadY, PadY + 5), new Rectangle(Selected ? 12 : 0, 0, 10, 10), Color.White);
        }
    }
}

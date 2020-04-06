using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit.Inputs
{
    public class UICheckbox : UIElement, IInput<bool>
    {
        public event UIEventHandler<bool> OnValueChanged;

        private bool check;

        public bool Check
        {
            get
            {
                return check;
            }

            set
            {
                if (check != value)
                {
                    check = value;
                    OnValueChanged?.Invoke(this, Check);
                }
            }
        }

        bool IInput<bool>.Value
        {
            get
            {
                return Check;
            }

            set
            {
                Check = value;
            }
        }

        public UICheckbox() : base()
        {
            Width = new SizeDimension(18f);
            Height = Width;
        }

        public override void LeftClick(UIMouseEventArgs e)
        {
            Check = !Check;
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
            sb.Draw(ItemModifier.Textures.Checkbox, InnerRect, new Rectangle(Check ? 20 : 0, 0, 18, 18), Color.White);
        }
    }
}

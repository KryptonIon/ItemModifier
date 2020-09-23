using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit.Inputs
{
    public class UIImageButton : UIImage
    {
        private float activeTransparency = 1f;

        public float ActiveTransparency
        {
            get
            {
                return activeTransparency;
            }

            set
            {
                activeTransparency = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        private float inactiveTransparency = 0.4f;

        public float InactiveTransparency
        {
            get
            {
                return inactiveTransparency;
            }

            set
            {
                inactiveTransparency = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        public UIImageButton(Texture2D image, bool autoScale = true, Color? colorTint = null) : base(image, autoScale, colorTint)
        {
        }

        public UIImageButton(Texture2D image, float activeTransparency, float inactiveTransparency, bool autoScale = true, Color? colorTint = null) : base(image, autoScale, colorTint)
        {
            ActiveTransparency = activeTransparency;
            InactiveTransparency = inactiveTransparency;
        }

        public override void MouseOver(UIMouseEventArgs e)
        {
            Main.PlaySound(SoundID.MenuTick);
            base.MouseOver(e);
        }

        public override void LeftClick(UIMouseEventArgs e)
        {
            Main.PlaySound(SoundID.MenuTick);
            base.LeftClick(e);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            var colorTint = ColorTint;
            ColorTint *= MouseHovering
                    ? ActiveTransparency
                    : InactiveTransparency;
            base.DrawSelf(sb);
            ColorTint = colorTint;
        }
    }
}

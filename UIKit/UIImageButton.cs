using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    public class UIImageButton : UIImage
    {
        private float _activeTransparency;

        public float ActiveTransparency
        {
            get
            {
                return _activeTransparency;
            }

            set
            {
                _activeTransparency = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        private float _inactiveTransparency;

        public float InactiveTransparency
        {
            get
            {
                return _inactiveTransparency;
            }

            set
            {
                _inactiveTransparency = MathHelper.Clamp(value, 0f, 1f);
            }
        }

        public UIImageButton(Texture2D image, Color? colorTint = null, float activeTransparency = 1f, float inactiveTransparency = 0.4f, Vector4 margin = default) : base(image, colorTint, margin)
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
            OnLeftClick += (source, e) => Main.PlaySound(SoundID.MenuTick);
            base.LeftClick(e);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(Image, Dimensions.Rectangle, ColorTint * (MouseHovering ? ActiveTransparency : InactiveTransparency));
        }
    }
}

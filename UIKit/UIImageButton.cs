using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    public class UIImageButton : UIImage
    {
        private float _activeTransparency;

        public float ActiveTransparency { get => _activeTransparency; set => _activeTransparency = MathHelper.Clamp(value, 0f, 1f); }

        private float _inactiveTransparency;

        public float InactiveTransparency { get => _inactiveTransparency; set => _inactiveTransparency = MathHelper.Clamp(value, 0f, 1f); }

        public UIImageButton(Texture2D Image, Color? ColorTint = null, float ActiveTransparency = 1f, float InactiveTransparency = 0.4f, Vector4 Margin = default) : base(Image, ColorTint, Margin)
        {
            this.ActiveTransparency = ActiveTransparency;
            this.InactiveTransparency = InactiveTransparency;
            OnMouseOver += (source, e) => Main.PlaySound(SoundID.MenuTick);
            OnLeftClick += (source, e) => Main.PlaySound(SoundID.MenuTick);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(Image, Dimensions.Rectangle, ColorTint * (MouseHovering ? ActiveTransparency : InactiveTransparency));
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for image as button.
    /// </summary>
    public class UIImageButton : UIImage
    {
        private float _activeTransparency;

        /// <summary>
        /// Transparency of image when mouse is hovering.
        /// </summary>
        public float ActiveTransparency { get => _activeTransparency; set => _activeTransparency = MathHelper.Clamp(value, 0f, 1f); }

        private float _inactiveTransparency;

        /// <summary>
        /// Transparency of image when mouse is not hovering.
        /// </summary>
        public float InactiveTransparency { get => _inactiveTransparency; set => _inactiveTransparency = MathHelper.Clamp(value, 0f, 1f); }

        /// <summary>
        /// Initializes a new <see cref="UIImageButton"/> Element.
        /// </summary>
        /// <param name="Image">Image to be displayed.</param>
        /// <param name="ColorTint">Color of texture, white if no tinting applied.</param>
        /// <param name="ActiveTransparency">Transparency of image when mouse is hovering.</param>
        /// <param name="InactiveTransparency">Transparency of image when mouse is not hovering.</param>
        /// <param name="Margin">Add space around the element.</param>
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

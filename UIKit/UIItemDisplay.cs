using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for item display.
    /// </summary>
    public class UIItemDisplay : UIElement
    {
        /// <summary>
        /// ID of item to display.
        /// </summary>
        public Item DisplayItem = new Item();

        /// <summary>
        /// Color the texture; White if no tinting is applied.
        /// </summary>
        public Color ColorTint = Color.White;

        /// <summary>
        /// If true, the texture will be scaled down if texture is bigger than <see cref="UIItemDisplay"/>.
        /// </summary>
        public bool ScaleDown { get; set; } = false;

        /// <summary>
        /// If true, the texture will be scaled up if texture is smaller than <see cref="UIItemDisplay"/>.
        /// </summary>
        public bool ScaleUp { get; set; } = false;

        /// <summary>
        /// If true, the texture will be centered.
        /// </summary>
        public bool Center { get; set; } = true;

        /// <summary>
        /// Initializes a new <see cref="UIItemDisplay"/> Element.
        /// </summary>
        /// <param name="Size">Size of <see cref="UIItemDisplay"/>.</param>
        /// <param name="ItemID">ID of item to display.</param>
        /// <param name="ScaleDown">If true, the texture will be scaled down if texture is bigger than <see cref="UIItemDisplay"/>.</param>
        /// <param name="ScaleUp">If true, the texture will be scaled up if texture is smaller than <see cref="UIItemDisplay"/>.</param>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIItemDisplay(Vector2 Size, int ItemID = 0, bool ScaleDown = true, bool ScaleUp = false, Vector4 Padding = default, Vector4 Margin = default) : base(Padding, Margin)
        {
            Width = new StyleDimension(Size.X);
            Height = new StyleDimension(Size.Y);
            DisplayItem.SetDefaults(ItemID);
            this.ScaleDown = ScaleDown;
            this.ScaleUp = ScaleUp;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            Texture2D texture = DisplayItem.type == 0 ? ItemModifier.Textures.Air : Main.itemTexture[DisplayItem.type];
            Vector2 textureSize = texture.Size();
            Rectangle rect = new Rectangle((int)Dimensions.X, (int)Dimensions.Y, (int)textureSize.X, (int)textureSize.Y);
            if (ScaleDown && (textureSize.X > Width.Pixels || textureSize.Y > Height.Pixels) || ScaleUp && (textureSize.X < Width.Pixels || textureSize.Y < Height.Pixels))
            {
                if (textureSize.X == textureSize.Y)
                {
                    rect.Width = (int)Width.Pixels;
                    rect.Height = (int)Height.Pixels;
                }
                else if (textureSize.X > textureSize.Y)
                {
                    rect.Width = (int)Width.Pixels;
                    rect.Height = (int)(Width.Pixels / textureSize.X * textureSize.Y);
                }
                else
                {
                    rect.Width = (int)(Height.Pixels / textureSize.Y * textureSize.X);
                    rect.Height = (int)Height.Pixels;
                }
            }
            if (Center)
            {
                rect.X += (int)((Width.Pixels - rect.Width) * 0.5f);
                rect.Y += (int)((Height.Pixels - rect.Height) * 0.5f);
            }
            sb.Draw(texture, rect, ColorTint);
        }
    }
}

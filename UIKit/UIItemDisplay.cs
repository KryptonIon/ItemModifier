using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ItemModifier.UIKit
{
    public class UIItemDisplay : UIElement
    {
        public Item DisplayItem = new Item();

        public Color ColorTint = Color.White;

        public bool ScaleDown { get; set; } = false;

        public bool ScaleUp { get; set; } = false;

        public bool Center { get; set; } = true;

        public UIItemDisplay(Vector2 size, int itemID = 0, bool scaleDown = true, bool scaleUp = false, Vector4 padding = default, Vector4 margin = default) : base(padding, margin)
        {
            Width = new StyleDimension(size.X);
            Height = new StyleDimension(size.Y);
            DisplayItem.SetDefaults(itemID);
            ScaleDown = scaleDown;
            ScaleUp = scaleUp;
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

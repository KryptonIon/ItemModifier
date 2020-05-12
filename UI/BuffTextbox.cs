using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.UI
{
    public class UIBuffTextbox : UIIntTextbox
    {
        protected override Rectangle ScissorRectangle
        {
            get
            {
                return new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
            }
        }

        protected override Vector2 TextPosition
        {
            get
            {
                Vector2 textSize = Font.MeasureString(Text);
                return new Vector2(InnerX + (InnerWidth - textSize.X) * 0.5f, InnerY + InnerWidth * 0.5f - 10.5f);
            }
        }

        public static Texture2D BlankBuffTexture { get; } = ModContent.GetTexture("Terraria/Buff");

        public UIBuffTextbox() : base(0, BuffLoader.BuffCount - 1)
        {
            CharacterLimit = 3;
            BorderSize = 0;
            TextColor = Color.White;
            BackgroundColor = Color.White;
            Width = new SizeDimension(32f);
            Height = Width;
            TextboxTexture = BlankBuffTexture;
            OnValueChanged += (source, e) => TextboxTexture = Main.buffTexture[e.Value] ?? BlankBuffTexture;
        }

        protected override void DrawText(SpriteBatch sb)
        {
            if (Focused)
            {
                base.DrawText(sb);
            }
        }
    }
}

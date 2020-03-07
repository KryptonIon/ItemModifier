using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    public class UIButton : UIElement
    {
        public bool Flat { get; set; } = true;

        private bool SmallButton { get; set; } = false;

        public bool Small
        {
            get => SmallButton;

            set
            {
                SmallButton = value;
                if (Small)
                {
                    Width = new StyleDimension(60f, Width.Percent);
                    Height = new StyleDimension(28f, Height.Percent);
                }
                else
                {
                    Width = new StyleDimension(80f, Width.Percent);
                    Height = new StyleDimension(32f, Height.Percent);
                }
            }
        }

        public Color ButtonColor { get; set; } = new Color(0, 0, 161);

        public Color TextColor { get; set; } = Color.White;

        public string Text { get; set; }

        public UIButton(bool Flat = true, bool Small = false, Vector4 Padding = default, Vector4 Margin = default) : base(Padding, Margin) => (this.Flat, this.Small, Width, Height) = (Flat, Small, new StyleDimension(80f), new StyleDimension(32f));

        public override void LeftClick(UIMouseEventArgs e)
        {
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
            sb.Draw(Flat ? Small ? ItemModifier.Textures.FlatButtonSmall : ItemModifier.Textures.FlatButton : Small ? ItemModifier.Textures.RoundButtonSmall : ItemModifier.Textures.RoundButton, Dimensions.Position, new Rectangle(IsLeftDown ? ((Small ? 60 : 80) + 2) : 0, 0, Small ? 60 : 80, Small ? 28 : 32), ButtonColor);
            Utils.DrawBorderString(sb, Text, new Vector2(Dimensions.X + 4, Dimensions.Y + 5 + (IsLeftDown ? 1 : 0)), TextColor);
        }
    }
}

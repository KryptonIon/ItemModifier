using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static ItemModifier.ItemModifier;

namespace ItemModifier.UIKit
{
    public class UIPanel : UIContainer
    {
        public bool HasBorder { get; set; } = true;

        public const int BorderSize = 2;

        public Color BorderColor { get; set; } = Color.Black;

        public UIPanel() : base()
        {
            BackgroundColor = UIBackgroundColor;
        }

        public UIPanel(Color backgroundColor) : base(backgroundColor)
        {
        }

        public UIPanel(Color backgroundColor, Color borderColor) : this(backgroundColor)
        {
            BorderColor = borderColor;
        }

        protected internal override void RecalculateSelf()
        {
            base.RecalculateSelf();
            if (HasBorder)
            {
                float leftRight = BorderSize + BorderSize;
                float topBottom = BorderSize + BorderSize;
                OuterWidth += leftRight;
                OuterHeight += topBottom;
                PadWidth += leftRight;
                PadHeight += topBottom;
                InnerX += BorderSize;
                InnerY += BorderSize;
            }
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            int padX = (int)PadX;
            int padY = (int)PadY;
            int padWidth = (int)PadWidth;
            int padHeight = (int)PadHeight;
            // Y pos left and right border will start at
            int lrYPos = padY + BorderSize;
            // Y length left and right border will be
            int lrYLength = padHeight - BorderSize - BorderSize;
            // Draw background
            sb.Draw(Textures.WhiteDot, new Rectangle(padX, padY, padWidth, padHeight), BackgroundColor);
            // Draw borders
            if (HasBorder)
            {
                sb.Draw(Textures.WhiteDot, new Rectangle(padX, padY, padWidth, BorderSize), BorderColor);
                sb.Draw(Textures.WhiteDot, new Rectangle(padX, lrYPos, BorderSize, lrYLength), BorderColor);
                sb.Draw(Textures.WhiteDot, new Rectangle(padX + padWidth - BorderSize, lrYPos, BorderSize, lrYLength), BorderColor);
                sb.Draw(Textures.WhiteDot, new Rectangle(padX, padY + padHeight - BorderSize, padWidth, BorderSize), BorderColor);
            }
        }
    }
}

using ItemModifier.UIKit.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static ItemModifier.ItemModifier;
using static Terraria.Utils;

namespace ItemModifier.UIKit
{
    public class UIWindow : UIElement
    {
        public string Title { get; set; } = string.Empty;

        public Color BorderColor { get; set; } = Color.Black;

        public Color BackgroundColor { get; set; } = Utils.UIBackgroundColor;

        public int BorderSize { get; protected set; } = 2;

        private bool dragging;

        public bool Draggable { get; set; }

        private Vector2 localDragPosition;

        protected UIImageButton CloseButton { get; private set; }

        public float TitleX { get; protected set; }

        public float TitleY { get; protected set; }

        private float titleHeight;

        public float TitleHeight
        {
            get
            {
                return titleHeight;
            }

            protected set
            {
                if (titleHeight != value)
                {
                    titleHeight = value;
                    Recalculate();
                }
            }
        }

        public UIWindow(bool draggable = true)
        {
            Draggable = draggable;
            Width = new SizeDimension(300f);
            Height = new SizeDimension(200f);
        }

        public UIWindow(string title, bool hasCloseButton = true, bool draggable = true) : this(draggable)
        {
            titleHeight = 21f;
            Title = title;
            if (hasCloseButton)
            {
                CloseButton = new UIImageButton(Textures.X);
            }
        }

        protected internal override void RecalculateSelf()
        {
            base.RecalculateSelf();
            if (BorderSize > 0)
            {
                OuterWidth += BorderSize + BorderSize;
                OuterHeight += BorderSize + BorderSize;
                PadWidth += BorderSize + BorderSize;
                PadHeight += BorderSize + BorderSize;
                InnerX += BorderSize;
                InnerY += BorderSize;
            }
            if (TitleHeight != 0)
            {
                TitleX = InnerX;
                TitleY = InnerY;
                OuterHeight += TitleHeight;
                PadHeight += TitleHeight;
                InnerY += TitleHeight;
            }
            else
            {
                TitleX = -1;
                TitleY = -1;
            }
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (dragging)
            {
                XOffset = new SizeDimension(Main.mouseX - localDragPosition.X, XOffset.Percent);
                YOffset = new SizeDimension(Main.mouseY - localDragPosition.Y, YOffset.Percent);
                Recalculate();
            }
            int padX = (int)PadX;
            int padY = (int)PadY;
            int padWidth = (int)PadWidth;
            int padHeight = (int)PadHeight;
            int backgroundX = padX + BorderSize;
            int backgroundY = padY + BorderSize;
            int backgroundWidth = padWidth - BorderSize - BorderSize;
            if (BorderSize > 0)
            {
                int verticalPosition = padY + BorderSize;
                int verticalLength = padHeight - BorderSize - BorderSize;
                sb.Draw(Textures.BlackDot, new Rectangle(padX, padY, padWidth, BorderSize), BorderColor);
                sb.Draw(Textures.BlackDot, new Rectangle(padX, verticalPosition, BorderSize, verticalLength), BorderColor);
                sb.Draw(Textures.BlackDot, new Rectangle((int)(PadX + PadWidth) - BorderSize, verticalPosition, BorderSize, verticalLength), BorderColor);
                sb.Draw(Textures.BlackDot, new Rectangle(padX, (int)(PadY + PadHeight) - BorderSize, padWidth, BorderSize), BorderColor);
            }
            sb.Draw(Textures.WhiteDot, new Rectangle(backgroundX, backgroundY, backgroundWidth, padHeight - BorderSize - BorderSize), BackgroundColor);
            if (TitleHeight != 0)
            {
                sb.Draw(Textures.WhiteDot, new Rectangle(backgroundX, backgroundY, backgroundWidth, (int)TitleHeight), Utils.UIBackgroundColor);
                DrawBorderString(sb, Title, new Vector2(PadX + BorderSize + 2f, PadY + BorderSize + 1f), Color.White);
            }
        }

        public override void LeftMouseDown(UIMouseEventArgs e)
        {
            if (Draggable && (TitleHeight != 0 && e.MousePosition.X >= TitleX && e.MousePosition.Y >= TitleY && e.MousePosition.X <= TitleX + PadWidth - BorderSize - BorderSize && e.MousePosition.Y <= TitleY + TitleHeight || e.MousePosition.X >= PadX && e.MousePosition.Y >= PadY && e.MousePosition.X <= PadX + PadWidth - BorderSize && e.MousePosition.Y <= PadY + BorderSize))
            {
                localDragPosition = new Vector2(e.MousePosition.X - XOffset.Pixels, e.MousePosition.Y - YOffset.Pixels);
                dragging = true;
            }
            base.LeftMouseDown(e);
        }

        public override void LeftMouseUp(UIMouseEventArgs e)
        {
            if (dragging)
            {
                dragging = false;
                Recalculate();
            }
            base.LeftMouseUp(e);
        }

        public override void OnInitialize()
        {
            if (CloseButton != null)
            {
                CloseButton.XOffset = new SizeDimension(-CloseButton.Width.Pixels - 3f, 1f);
                CloseButton.YOffset = new SizeDimension(TitleHeight != 0 ? -19f : 2f);
                CloseButton.OnLeftClick += (source, e) => Visible = false;
                CloseButton.WhileMouseHover += (source, e) => ModContent.GetInstance<ItemModifier>().Tooltip = "Close";
                CloseButton.Parent = this;
            }
        }
    }
}

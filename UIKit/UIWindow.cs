using ItemModifier.UIKit.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static ItemModifier.ItemModifier;
using static Terraria.Utils;

namespace ItemModifier.UIKit
{
    public class UIWindow : UIPanel
    {
        public string Title { get; set; } = string.Empty;

        private bool dragging;

        public bool Draggable { get; set; }

        private Vector2 localDragPosition;

        protected UIImageButton CloseButton { get; private set; }

        public bool HasTitle { get; set; }

        public float TitleX { get; protected set; }

        public float TitleY { get; protected set; }

        public float TitleWidth { get; protected set; }

        public const int TitleHeight = 21;

        public Rectangle TitleRect => new Rectangle((int)TitleX, (int)TitleY, (int)TitleWidth, (int)TitleHeight);

        public UIWindow() : base(UIBackgroundColor, Color.Black)
        {
            Width = new SizeDimension(300f);
            Height = new SizeDimension(200f);
        }

        public UIWindow(string title, bool hasCloseButton = true, bool draggable = true) : this()
        {
            HasTitle = true;
            Draggable = draggable;
            Title = title;
            if (hasCloseButton)
            {
                CloseButton = new UIImageButton(Textures.X);
            }
        }

        protected internal override void RecalculateSelf()
        {
            base.RecalculateSelf();
            if (HasTitle)
            {
                // Make sure title doesn't overlap borders
                // Not derived from InnerX/Y so that padding doesn't affect title
                TitleX = PadX + BorderSize;
                TitleY = PadY + BorderSize;
                TitleWidth = PadWidth - BorderSize - BorderSize;
                OuterHeight += TitleHeight;
                PadHeight += TitleHeight;
                // Shift InnerRect down
                InnerY += TitleHeight;
            }
            else
            {
                TitleX = -1;
                TitleY = -1;
                TitleWidth = -1;
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
            base.DrawSelf(sb);
            if (HasTitle)
            {
                sb.Draw(Textures.WhiteDot, TitleRect, UIBackgroundColor);
                DrawBorderString(sb, Title, new Vector2(TitleX + 2f, TitleY + 1f), Color.White);
            }
        }

        public override void LeftMouseDown(UIMouseEventArgs e)
        {
            if (Draggable && HasTitle && TitleRect.Contains(e.MousePosition.ToPoint()))
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
            base.OnInitialize();
            if (CloseButton != null)
            {
                CloseButton.XOffset = new SizeDimension(-CloseButton.Width.Pixels - 3f, 1f);
                CloseButton.YOffset = new SizeDimension(HasTitle ? -19f : 2f);
                CloseButton.OnLeftClick += (source, e) => Visible = false;
                CloseButton.WhileMouseHover += (source, e) => ModContent.GetInstance<ItemModifier>().Tooltip = "Close";
                CloseButton.Parent = this;
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace ItemModifier.UIKit
{
    public class UIContainer : UIElement
    {
        public Color BackgroundColor { get; set; } = Color.Transparent;

        private float scrollValue;

        private float tempScrollValue;

        public float ScrollValue
        {
            get
            {
                return scrollValue;
            }

            set
            {
                scrollValue = value < 0 ? 0 : value > MaxScrollValue ? MaxScrollValue : value;
            }
        }

        public float MaxScrollValue { get; private set; }

        private float scrollPerPixel;

        private int scrollInnerSize;

        private Rectangle scrollInnerPos;

        private Point DragOrigin;

        private bool DraggingScrollInner;

        public UIContainer(Vector2 size, Vector4 padding = default, Vector4 margin = default) : base(padding, margin)
        {
            Width = new StyleDimension(size.X);
            Height = new StyleDimension(size.Y);
        }

        public UIContainer(Color backgroundColor, Vector2 size, Vector4 padding = default, Vector4 margin = default) : this(size, padding, margin)
        {
            BackgroundColor = backgroundColor;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (BackgroundColor.A < 255)
            {
                sb.Draw(ItemModifier.Textures.WindowBackground, Dimensions.Rectangle, BackgroundColor);
            }
            if (OverflowHidden)
            {
                Point scrollBarPos = new Point((int)(Dimensions.X + Width.Pixels - 2f), (int)(Dimensions.Y + 2f));
                sb.Draw(ItemModifier.Textures.ScrollBorder, new Rectangle(scrollBarPos.X, scrollBarPos.Y, 4, (int)(Height.Pixels - 4f)), Color.White);
                if (DraggingScrollInner)
                {
                    ScrollValue = tempScrollValue + (Main.mouseY - DragOrigin.Y) * scrollPerPixel;
                    RecalculateChildren();
                }
                scrollInnerPos = new Rectangle(scrollBarPos.X, scrollBarPos.Y + (int)(ScrollValue / scrollPerPixel), 4, scrollInnerSize);
                sb.Draw(ItemModifier.Textures.ScrollInside, scrollInnerPos, Color.White);
            }
        }

        public override void Recalculate()
        {
            base.Recalculate();
            if (OverflowHidden)
            {
                float lowestPoint = 0f;
                for (int i = 0; i < Count; i++)
                {
                    float ySize = this[i].Top.CalculateValue(Height.Pixels) + this[i].OuterDimensions.Height;
                    if (lowestPoint < ySize)
                    {
                        lowestPoint = ySize;
                    }
                }
                MaxScrollValue = Math.Max(0, lowestPoint - Height.Pixels);
                scrollPerPixel = lowestPoint / (Height.Pixels - 4f);
                scrollInnerSize = (int)((Height.Pixels - 4f) * (Height.Pixels / (Height.Pixels + MaxScrollValue)));
            }
        }

        public override void ScrollWheel(UIScrollWheelEventArgs e)
        {
            ScrollValue -= e.ScrollWheelValue;
            RecalculateChildren();
            base.ScrollWheel(e);
        }

        public override void LeftMouseDown(UIMouseEventArgs e)
        {
            Point mousePos = new Point((int)e.MousePosition.X, (int)e.MousePosition.Y);
            if (scrollInnerPos.Contains(mousePos))
            {
                DraggingScrollInner = true;
                DragOrigin = mousePos;
                tempScrollValue = scrollValue;
            }
            base.LeftMouseDown(e);
        }

        public override void LeftMouseUp(UIMouseEventArgs e)
        {
            DraggingScrollInner = false;
            base.LeftMouseDown(e);
        }
    }
}

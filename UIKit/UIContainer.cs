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
                RecalculateChildren();
            }
        }

        public float MaxScrollValue { get; private set; }

        private float scrollPerPixel;

        private int scrollInnerSize;

        private Rectangle scrollInnerPos;

        private Point DragOrigin;

        private bool DraggingScrollInner;

        public UIContainer()
        {

        }

        public UIContainer(Color backgroundColor)
        {
            BackgroundColor = backgroundColor;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (BackgroundColor.A > 0)
            {
                sb.Draw(ItemModifier.Textures.WhiteDot, PadRect, new Rectangle(2, 2, 24, 24), BackgroundColor);
            }
            if (OverflowHidden)
            {
                Point scrollBarPos = new Point((int)(InnerX + InnerWidth - 4f), (int)(InnerY + 2f));
                sb.Draw(ItemModifier.Textures.OpaqueWindowBackground, new Rectangle(scrollBarPos.X, scrollBarPos.Y, 4, (int)(InnerHeight - 4f)), Color.White);
                if (DraggingScrollInner)
                {
                    ScrollValue = tempScrollValue + (Main.mouseY - DragOrigin.Y) * scrollPerPixel;
                    RecalculateChildren();
                }
                scrollInnerPos = new Rectangle(scrollBarPos.X, scrollBarPos.Y + Math.Max((int)(ScrollValue / scrollPerPixel), 0), 4, scrollInnerSize);
                sb.Draw(ItemModifier.Textures.WhiteDot, scrollInnerPos, Color.White);
            }
        }

        public override void Recalculate()
        {
            base.Recalculate();
            if (OverflowHidden)
            {
                float lowestPoint = 0f;
                for (int i = 0; i < Children.Count; i++)
                {
                    UIElement child = Children[i];
                    if (child.Visible)
                    {
                        float ySize = child.OuterY + child.OuterHeight + ScrollValue - InnerY;
                        if (lowestPoint < ySize)
                        {
                            lowestPoint = ySize;
                        }
                    }
                }
                MaxScrollValue = Math.Max(0, lowestPoint - InnerHeight);
                scrollPerPixel = lowestPoint / (InnerHeight - 4f);
                scrollInnerSize = Math.Max((int)((InnerHeight - 4f) * (InnerHeight / (InnerHeight + MaxScrollValue))), 1);
                ScrollValue = ScrollValue;
            }
        }

        public override void ScrollWheel(UIScrollWheelEventArgs e)
        {
            UIElement target = e.Target;
            while(!(target is UIContainer))
            {
                target = target.Parent;
            }
            if (target == this)
            {
                ScrollValue -= e.ScrollWheelValue;
            }
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

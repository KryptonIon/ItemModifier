using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace ItemModifier.UIKit
{
    public class UIContainer : UIElement
    {
        public static Color UIBackgroundColor => new Color(44, 57, 105, 190);

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
                if (MaxScrollValue > 0)
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
            OnChildAdded += (source, e) => RecalculateSelf();
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            
            Texture2D whiteDot = new Texture2D(Main.spriteBatch.GraphicsDevice, 1, 1);
            whiteDot.SetData(new[] { new Color(255, 255, 255) });

            if (BackgroundColor.A > 0)
            {
                sb.Draw(whiteDot, PadRect, new Rectangle(2, 2, 24, 24), BackgroundColor);
            }
            if (OverflowHidden)
            {
                Point scrollBarPos = new Point((int)(InnerX + InnerWidth - 4f), (int)(InnerY + 2f));
                Color scrollBarColor = UIBackgroundColor;
                scrollBarColor.A = 255;
                sb.Draw(whiteDot, new Rectangle(scrollBarPos.X, scrollBarPos.Y, 4, (int)(InnerHeight - 4f)), scrollBarColor);
                if (DraggingScrollInner)
                {
                    ScrollValue = tempScrollValue + (Main.mouseY - DragOrigin.Y) * scrollPerPixel;
                    RecalculateChildren();
                }
                scrollInnerPos = new Rectangle(scrollBarPos.X, scrollBarPos.Y + Math.Max((int)(ScrollValue / scrollPerPixel), 0), 4, scrollInnerSize);
                sb.Draw(whiteDot, scrollInnerPos, Color.White);
            }
        }

        protected internal override void RecalculateSelf()
        {
            base.RecalculateSelf();
            if (OverflowHidden)
            {
                float lowestPoint = 0f;
                for (int i = 0; i < Children.Count; i++)
                {
                    UIElement child = Children[i];
                    if (child.Visible)
                    {
                        float bottom = child.OuterY + child.OuterHeight;
                        if (lowestPoint < bottom)
                        {
                            lowestPoint = bottom;
                        }
                    }
                }

                // Only calculate values if lowestPoint is past viewable area
                if (lowestPoint > InnerHeight)
                {
                    MaxScrollValue = lowestPoint - InnerHeight;
                    if (MaxScrollValue > 0)
                    {
                        // scroll value per scroll bar pixel, used to determine where scroll interior begins
                        scrollPerPixel = lowestPoint / (InnerHeight - 4f);

                        // Scroll bar size * Viewable:Entire Area(ratio) = Size of scroll interior
                        // Minimum 1 pixel in size
                        scrollInnerSize = Math.Max((int)((InnerHeight - 4f) * (InnerHeight / lowestPoint)), 1);
                    }
                }
                else
                {
                    MaxScrollValue = 0;
                }

                // Property's setter stops it being <0. <0 check is skipped
                if (ScrollValue > MaxScrollValue)
                {
                    // RecalculateChildren is called after, no need to call it again
                    scrollValue = MaxScrollValue;
                }
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
            base.LeftMouseUp(e);
        }
    }
}

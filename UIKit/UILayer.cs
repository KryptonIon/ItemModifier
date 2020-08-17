using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace ItemModifier.UIKit
{
    public class UILayer : UIElement
    {
        public const int DOUBLE_CLICK_MAX_GAP = 500;

        private UIElement lastHover;

        private UIElement lastLeftClick;

        private UIElement lastLeftDown;

        private UIElement lastRightClick;

        private UIElement lastRightDown;

        private UIElement lastMiddleClick;

        private UIElement lastMiddleDown;

        private UIElement lastBackClick;

        private UIElement lastBackDown;

        private UIElement lastForwardClick;

        private UIElement lastForwardDown;

        private UIElement lastFocused;

        private double lastLeftClickTime;

        private double lastRightClickTime;

        private double lastMiddleClickTime;

        private double lastBackClickTime;

        private double lastForwardClickTime;

        public UILayer()
        {
            Width = new SizeDimension(Main.screenWidth);
            Height = new SizeDimension(Main.screenHeight);
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            Vector2 mousePosition = Main.MouseScreen;
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();
            TriggersSet oldTriggersSet = PlayerInput.Triggers.Old;
            bool leftDown;
            bool wasLeftDown = oldTriggersSet.MouseLeft;
            bool rightDown;
            bool wasRightDown = oldTriggersSet.MouseRight;
            bool middleDown;
            bool wasMiddleDown = oldTriggersSet.MouseMiddle;
            bool backDown;
            bool wasBackDown = oldTriggersSet.MouseXButton1;
            bool forwardDown;
            bool wasForwardDown = oldTriggersSet.MouseXButton2;
            UIElement target;
            if (Main.hasFocus)
            {
                leftDown = Main.mouseLeft;
                rightDown = Main.mouseRight;
                middleDown = Main.mouseMiddle;
                backDown = Main.mouseXButton1;
                forwardDown = Main.mouseXButton2;
                target = GetElementAt(mousePosition);
                // Prevent UILayer from always blocking mouse input
                if (target == this)
                    target = null;
            }
            else
            {
                leftDown = false;
                rightDown = false;
                middleDown = false;
                backDown = false;
                forwardDown = false;
                target = null;
                if (lastFocused != null)
                {
                    lastFocused.Unfocus();
                    lastFocused = null;
                }
            }
            if (target == null)
            {
                instance.MouseWheelDisabled = false;
                instance.ItemAtCursorDisabled = false;
            }
            else
            {
                Main.LocalPlayer.mouseInterface = true;
                instance.MouseWheelDisabled = true;
                instance.ItemAtCursorDisabled = true;
            }
            if (target != lastHover)
            {
                lastHover?.MouseOut(new UIMouseEventArgs(lastHover, mousePosition));
                target?.MouseOver(new UIMouseEventArgs(target, mousePosition));
                lastHover = target;
            }
            target?.MouseHover(new UIMouseEventArgs(target, mousePosition));
            if (leftDown && !wasLeftDown || rightDown && !wasRightDown || middleDown && !wasMiddleDown || backDown && !wasBackDown || forwardDown && !wasForwardDown)
            {
                if (lastFocused != target)
                {
                    lastFocused?.Unfocus();
                    lastFocused = target;
                    target?.Focus();
                }
            }
            if (leftDown && !wasLeftDown && target != null)
            {
                lastLeftDown = target;
                target.LeftMouseDown(new UIMouseEventArgs(target, mousePosition));
                if (lastLeftClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastLeftClickTime < DOUBLE_CLICK_MAX_GAP)
                {
                    target.LeftDoubleClick(new UIMouseEventArgs(target, mousePosition));
                    lastLeftClick = null;
                }
                lastLeftClickTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
            else if (!leftDown && wasLeftDown && lastLeftDown != null)
            {
                if (lastLeftDown.ContainsPoint(mousePosition))
                {
                    lastLeftDown.LeftClick(new UIMouseEventArgs(lastLeftDown, mousePosition));
                    lastLeftClick = lastLeftDown;
                }
                lastLeftDown.LeftMouseUp(new UIMouseEventArgs(lastLeftDown, mousePosition));
                lastLeftDown = null;
            }
            if (rightDown && !wasRightDown && target != null)
            {
                lastRightDown = target;
                target.RightMouseDown(new UIMouseEventArgs(target, mousePosition));
                if (lastRightClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastRightClickTime < DOUBLE_CLICK_MAX_GAP)
                {
                    target.RightDoubleClick(new UIMouseEventArgs(target, mousePosition));
                    lastRightClick = null;
                }
                lastRightClickTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
            else if (!rightDown && wasRightDown && lastRightDown != null)
            {
                if (lastRightDown.ContainsPoint(mousePosition))
                {
                    lastRightDown.RightClick(new UIMouseEventArgs(lastRightDown, mousePosition));
                    lastRightClick = lastRightDown;
                }
                lastRightDown.RightMouseUp(new UIMouseEventArgs(lastRightDown, mousePosition));
                lastRightDown = null;
            }
            if (middleDown && !wasMiddleDown && target != null)
            {
                lastMiddleDown = target;
                target.MiddleMouseDown(new UIMouseEventArgs(target, mousePosition));
                if (lastMiddleClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastMiddleClickTime < DOUBLE_CLICK_MAX_GAP)
                {
                    target.MiddleDoubleClick(new UIMouseEventArgs(target, mousePosition));
                    lastMiddleClick = null;
                }
                lastMiddleClickTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
            else if (!middleDown && wasMiddleDown && lastMiddleDown != null)
            {
                if (lastMiddleDown.ContainsPoint(mousePosition))
                {
                    lastMiddleDown.MiddleClick(new UIMouseEventArgs(lastMiddleDown, mousePosition));
                    lastMiddleClick = lastMiddleDown;
                }
                lastMiddleDown.MiddleMouseUp(new UIMouseEventArgs(lastMiddleDown, mousePosition));
                lastMiddleDown = null;
            }
            if (backDown && !wasBackDown && target != null)
            {
                lastBackDown = target;
                target.BackDown(new UIMouseEventArgs(target, mousePosition));
                if (lastBackClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastBackClickTime < DOUBLE_CLICK_MAX_GAP)
                {
                    target.BackDoubleClick(new UIMouseEventArgs(target, mousePosition));
                    lastBackClick = null;
                }
                lastBackClickTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
            else if (!backDown && wasBackDown && lastBackDown != null)
            {
                if (lastBackDown.ContainsPoint(mousePosition))
                {
                    lastBackDown.BackClick(new UIMouseEventArgs(lastBackDown, mousePosition));
                    lastBackClick = lastBackDown;
                }
                lastBackDown.BackUp(new UIMouseEventArgs(lastBackDown, mousePosition));
                lastBackDown = null;
            }
            if (forwardDown && !wasForwardDown && target != null)
            {
                lastForwardDown = target;
                target.ForwardDown(new UIMouseEventArgs(target, mousePosition));
                if (lastForwardClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastForwardClickTime < DOUBLE_CLICK_MAX_GAP)
                {
                    target.ForwardDoubleClick(new UIMouseEventArgs(target, mousePosition));
                    lastForwardClick = null;
                }
                lastForwardClickTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
            else if (!forwardDown && wasForwardDown && lastForwardDown != null)
            {
                if (lastForwardDown.ContainsPoint(mousePosition))
                {
                    lastForwardDown.ForwardClick(new UIMouseEventArgs(lastForwardDown, mousePosition));
                    lastForwardClick = lastForwardDown;
                }
                lastForwardDown.ForwardUp(new UIMouseEventArgs(lastForwardDown, mousePosition));
                lastForwardDown = null;
            }
            if (PlayerInput.ScrollWheelDeltaForUI != 0)
            {
                target?.ScrollWheel(new UIScrollWheelEventArgs(target, mousePosition, PlayerInput.ScrollWheelDeltaForUI));
            }

            base.UpdateSelf(gameTime);
        }
    }
}

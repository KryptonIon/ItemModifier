using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace ItemModifier.UIKit
{
    public class UserInterface
    {
        private const double DOUBLE_CLICK_MAX_GAP = 500.0;

        private const double STATE_CHANGE_CLICK_DISABLE_TIME = 200.0;

        public Vector2 MousePosition { get; set; }

        private bool wasLeftdown;

        private bool wasRightDown;

        private bool wasMiddleDown;

        private bool wasBackDown;

        private bool wasForwardDown;

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

        private double clickDisabledTime;

        internal List<UIElement> Children { get; } = new List<UIElement>();

        public bool Visible { get; set; } = true;

        public bool Initialized { get; set; }

        public UserInterface()
        {
        }

        public void ResetLasts()
        {
            lastHover?.MouseOut(new UIMouseEventArgs(lastHover, MousePosition));
            lastHover = null;
            lastLeftClick = null;
            lastLeftDown = null;
            lastRightClick = null;
            lastRightDown = null;
            lastMiddleClick = null;
            lastMiddleDown = null;
            lastBackClick = null;
            lastBackDown = null;
            lastForwardClick = null;
            lastForwardDown = null;
            lastLeftClickTime = 0d;
            lastRightClickTime = 0d;
            lastMiddleClickTime = 0d;
            lastBackClickTime = 0d;
            lastForwardClickTime = 0d;
        }

        private void ResetState()
        {
            MousePosition = new Vector2(Main.mouseX, Main.mouseY);
            wasLeftdown = Main.mouseLeft;
            wasRightDown = Main.mouseRight;
            wasMiddleDown = Main.mouseMiddle;
            wasBackDown = Main.mouseXButton1;
            wasForwardDown = Main.mouseXButton2;
            ResetLasts();
            clickDisabledTime = Math.Max(clickDisabledTime, STATE_CHANGE_CLICK_DISABLE_TIME);
        }

        public void Update(GameTime gameTime)
        {
            MousePosition = new Vector2(Main.mouseX, Main.mouseY);
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();
            bool leftDown = Main.mouseLeft && Main.hasFocus;
            bool rightDown = Main.mouseRight && Main.hasFocus;
            bool middleDown = Main.mouseMiddle && Main.hasFocus;
            bool backDown = Main.mouseXButton1 && Main.hasFocus;
            bool forwardDown = Main.mouseXButton2 && Main.hasFocus;
            UIElement target = Main.hasFocus ? GetElementAt(MousePosition) : null;
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
            clickDisabledTime = Math.Max(0.0, clickDisabledTime - gameTime.ElapsedGameTime.TotalMilliseconds);
            bool clickDisabled = clickDisabledTime > 0.0;
            if (target != lastHover)
            {
                lastHover?.MouseOut(new UIMouseEventArgs(lastHover, MousePosition));
                target?.MouseOver(new UIMouseEventArgs(target, MousePosition));
                lastHover = target;
            }
            target?.MouseHover(new UIMouseEventArgs(target, MousePosition));
            if (!clickDisabled)
            {
                if (leftDown && !wasLeftdown || rightDown && !wasRightDown || middleDown && !wasMiddleDown || backDown && !wasBackDown || forwardDown && !wasForwardDown)
                {
                    if (lastFocused != null && lastFocused != target) lastFocused.Unfocus();
                    lastFocused = target;
                    if (target != null) target.Focus();
                }
                if (leftDown && !wasLeftdown && target != null)
                {
                    lastLeftDown = target;
                    target.LeftMouseDown(new UIMouseEventArgs(target, MousePosition));
                    if (lastLeftClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastLeftClickTime < DOUBLE_CLICK_MAX_GAP)
                    {
                        target.LeftDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        lastLeftClick = null;
                    }
                    lastLeftClickTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!leftDown && wasLeftdown && lastLeftDown != null)
                {
                    if (lastLeftDown.ContainsPoint(MousePosition))
                    {
                        lastLeftDown.LeftClick(new UIMouseEventArgs(lastLeftDown, MousePosition));
                        lastLeftClick = lastLeftDown;
                    }
                    lastLeftDown.LeftMouseUp(new UIMouseEventArgs(lastLeftDown, MousePosition));
                    lastLeftDown = null;
                }
                if (rightDown && !wasRightDown && target != null)
                {
                    lastRightDown = target;
                    target.RightMouseDown(new UIMouseEventArgs(target, MousePosition));
                    if (lastRightClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastRightClickTime < DOUBLE_CLICK_MAX_GAP)
                    {
                        target.RightDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        lastRightClick = null;
                    }
                    lastRightClickTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!rightDown && wasRightDown && lastRightDown != null)
                {
                    if (lastRightDown.ContainsPoint(MousePosition))
                    {
                        lastRightDown.RightClick(new UIMouseEventArgs(lastRightDown, MousePosition));
                        lastRightClick = lastRightDown;
                    }
                    lastRightDown.RightMouseUp(new UIMouseEventArgs(lastRightDown, MousePosition));
                    lastRightDown = null;
                }
                if (middleDown && !wasMiddleDown && target != null)
                {
                    lastMiddleDown = target;
                    target.MiddleMouseDown(new UIMouseEventArgs(target, MousePosition));
                    if (lastMiddleClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastMiddleClickTime < DOUBLE_CLICK_MAX_GAP)
                    {
                        target.MiddleDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        lastMiddleClick = null;
                    }
                    lastMiddleClickTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!middleDown && wasMiddleDown && lastMiddleDown != null)
                {
                    if (lastMiddleDown.ContainsPoint(MousePosition))
                    {
                        lastMiddleDown.MiddleClick(new UIMouseEventArgs(lastMiddleDown, MousePosition));
                        lastMiddleClick = lastMiddleDown;
                    }
                    lastMiddleDown.MiddleMouseUp(new UIMouseEventArgs(lastMiddleDown, MousePosition));
                    lastMiddleDown = null;
                }
                if (backDown && !wasBackDown && target != null)
                {
                    lastBackDown = target;
                    target.BackDown(new UIMouseEventArgs(target, MousePosition));
                    if (lastBackClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastBackClickTime < DOUBLE_CLICK_MAX_GAP)
                    {
                        target.BackDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        lastBackClick = null;
                    }
                    lastBackClickTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!backDown && wasBackDown && lastBackDown != null)
                {
                    if (lastBackDown.ContainsPoint(MousePosition))
                    {
                        lastBackDown.BackClick(new UIMouseEventArgs(lastBackDown, MousePosition));
                        lastBackClick = lastBackDown;
                    }
                    lastBackDown.BackUp(new UIMouseEventArgs(lastBackDown, MousePosition));
                    lastBackDown = null;
                }
                if (forwardDown && !wasForwardDown && target != null)
                {
                    lastForwardDown = target;
                    target.ForwardDown(new UIMouseEventArgs(target, MousePosition));
                    if (lastForwardClick == target && gameTime.TotalGameTime.TotalMilliseconds - lastForwardClickTime < DOUBLE_CLICK_MAX_GAP)
                    {
                        target.ForwardDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        lastForwardClick = null;
                    }
                    lastForwardClickTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!forwardDown && wasForwardDown && lastForwardDown != null)
                {
                    if (lastForwardDown.ContainsPoint(MousePosition))
                    {
                        lastForwardDown.ForwardClick(new UIMouseEventArgs(lastForwardDown, MousePosition));
                        lastForwardClick = lastForwardDown;
                    }
                    lastForwardDown.ForwardUp(new UIMouseEventArgs(lastForwardDown, MousePosition));
                    lastForwardDown = null;
                }
            }
            if (PlayerInput.ScrollWheelDeltaForUI != 0) target?.ScrollWheel(new UIScrollWheelEventArgs(target, MousePosition, PlayerInput.ScrollWheelDeltaForUI));
            wasLeftdown = leftDown;
            wasRightDown = rightDown;
            wasMiddleDown = middleDown;
            wasBackDown = backDown;
            wasForwardDown = forwardDown;
            for (int i = 0; i < Children.Count; i++) Children[i].Update(gameTime);
        }

        public void PostUpdateInput()
        {
            for (int i = 0; i < Children.Count; i++) Children[i].PostUpdateInput();
        }

        public void Draw(SpriteBatch sb)
        {
            if (Visible)
            {
                for (int i = 0; i < Children.Count; i++) Children[i].Draw(sb);
            }
        }

        internal void RefreshState()
        {
            for (int i = 0; i < Children.Count; i++) Children[i].Deactivate();
            ResetState();
            for (int i = 0; i < Children.Count; i++) Children[i].Activate();
        }

        public void Recalculate()
        {
            for (int i = 0; i < Children.Count; i++) Children[i].Recalculate();
        }

        public void RemoveAllChildren()
        {
            for (int i = 0; i < Children.Count; i++) Children[i].ParentUI = null;
            Children.Clear();
        }

        public void Activate()
        {
            Recalculate();
            if (!Initialized) Initialize();
            OnActivate();
            for (int i = 0; i < Children.Count; i++) Children[i].Activate();
        }

        public virtual void OnActivate()
        {

        }

        public void Deactivate()
        {
            OnDeactivate();
            for (int i = 0; i < Children.Count; i++) Children[i].Deactivate();
        }

        public virtual void OnDeactivate()
        {

        }

        public void Initialize()
        {
            OnInitialize();
            Initialized = true;
        }

        public virtual void OnInitialize()
        {

        }

        public UIElement GetElementAt(Vector2 point)
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                UIElement element = Children[i];
                if (element.Visible && element.ContainsPoint(point)) return element.GetElementAt(point);
            }
            return null;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;

namespace ItemModifier.UIKit
{
    public class UserInterface
    {
        private const double DOUBLE_CLICK_TIME = 500.0;

        private const double STATE_CHANGE_CLICK_DISABLE_TIME = 200.0;

        public static UserInterface ActiveInstance { get; set; } = new UserInterface();

        public Vector2 MousePosition;

        private bool _wasMouseDown;

        private bool _wasRightMouseDown;

        private bool _wasMiddleMouseDown;

        private bool _wasBackMouseDown;

        private bool _wasForwardMouseDown;

        private UIElement _lastElementHover;

        private UIElement _lastElementDown;

        private UIElement _lastElementRightDown;

        private UIElement _lastElementMiddleDown;

        private UIElement _lastElementBackDown;

        private UIElement _lastElementForwardDown;

        private UIElement _lastElementClicked;

        private UIElement _lastElementRightClicked;

        private UIElement _lastElementMiddleClicked;

        private UIElement _lastElementBackClicked;

        private UIElement _lastElementForwardClicked;

        private double _lastMouseDownTime;

        private double _lastMouseRightDownTime;

        private double _lastMouseMiddleDownTime;

        private double _lastMouseBackDownTime;

        private double _lastMouseForwardDownTime;

        private double _clickDisabledTimeRemaining;

        public bool Initialized { get; private set; }

        internal List<UIElement> Children = new List<UIElement>();

        public Dimensions Dimensions { get; } = new Dimensions(0f, 0f, Main.screenWidth, Main.screenHeight);

        public bool Visible { get; set; } = true;

        public UserInterface()
        {
            ActiveInstance = this;
        }

        public void Use()
        {
            if (ActiveInstance != this)
            {
                ActiveInstance = this;
                Recalculate();
                return;
            }
            ActiveInstance = this;
        }

        public void ResetLasts()
        {
            _lastElementHover?.MouseOut(new UIMouseEventArgs(_lastElementHover, MousePosition));
            _lastElementHover = null;
            _lastElementDown = null;
            _lastElementClicked = null;
            _lastElementRightDown = null;
            _lastElementRightClicked = null;
            _lastElementMiddleDown = null;
            _lastElementMiddleClicked = null;
            _lastElementBackDown = null;
            _lastElementBackClicked = null;
            _lastElementForwardDown = null;
            _lastElementForwardClicked = null;
            _lastMouseDownTime = 0.0;
            _lastMouseRightDownTime = 0.0;
            _lastMouseMiddleDownTime = 0.0;
            _lastMouseBackDownTime = 0.0;
            _lastMouseForwardDownTime = 0.0;
        }

        private void ResetState()
        {
            MousePosition = new Vector2(Main.mouseX, Main.mouseY);
            _wasMouseDown = Main.mouseLeft;
            _wasRightMouseDown = Main.mouseRight;
            _wasMiddleMouseDown = Main.mouseMiddle;
            _wasBackMouseDown = Main.mouseXButton1;
            _wasForwardMouseDown = Main.mouseXButton2;
            ResetLasts();
            _clickDisabledTimeRemaining = Math.Max(_clickDisabledTimeRemaining, STATE_CHANGE_CLICK_DISABLE_TIME);
        }

        public void Update(GameTime gameTime)
        {
            MousePosition = new Vector2(Main.mouseX, Main.mouseY);
            bool leftDown = Main.mouseLeft && Main.hasFocus;
            bool rightDown = Main.mouseRight && Main.hasFocus;
            bool middleDown = Main.mouseMiddle && Main.hasFocus;
            bool backDown = Main.mouseXButton1 && Main.hasFocus;
            bool forwardDown = Main.mouseXButton2 && Main.hasFocus;
            UIElement target = Main.hasFocus ? GetElementAt(MousePosition) : null;
            if (target != null)
            {
                Main.LocalPlayer.mouseInterface = true;
                ItemModifier.Instance.MouseWheelDisabled = true;
                ItemModifier.Instance.ItemAtCursorDisabled = true;
            }
            else
            {
                ItemModifier.Instance.MouseWheelDisabled = false;
                ItemModifier.Instance.ItemAtCursorDisabled = false;
            }
            _clickDisabledTimeRemaining = Math.Max(0.0, _clickDisabledTimeRemaining - gameTime.ElapsedGameTime.TotalMilliseconds);
            bool clickDisabled = _clickDisabledTimeRemaining > 0.0;
            if (target != _lastElementHover)
            {
                _lastElementHover?.MouseOut(new UIMouseEventArgs(_lastElementHover, MousePosition));
                target?.MouseOver(new UIMouseEventArgs(target, MousePosition));
                _lastElementHover = target;
            }
            target?.MouseHover(new UIMouseEventArgs(target, MousePosition));
            if (!clickDisabled)
            {
                if (leftDown && !_wasMouseDown && target != null)
                {
                    _lastElementDown = target;
                    target.LeftMouseDown(new UIMouseEventArgs(target, MousePosition));
                    if (_lastElementClicked == target && gameTime.TotalGameTime.TotalMilliseconds - _lastMouseDownTime < DOUBLE_CLICK_TIME)
                    {
                        target.LeftDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        _lastElementClicked = null;
                    }
                    _lastMouseDownTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!leftDown && _wasMouseDown && _lastElementDown != null)
                {
                    UIElement lastElementDown = _lastElementDown;
                    if (lastElementDown.ContainsPoint(MousePosition))
                    {
                        lastElementDown.LeftClick(new UIMouseEventArgs(lastElementDown, MousePosition));
                        _lastElementClicked = _lastElementDown;
                    }
                    lastElementDown.LeftMouseUp(new UIMouseEventArgs(lastElementDown, MousePosition));
                    _lastElementDown = null;
                }
                if (rightDown && !_wasRightMouseDown && target != null)
                {
                    _lastElementRightDown = target;
                    target.RightMouseDown(new UIMouseEventArgs(target, MousePosition));
                    if (_lastElementRightClicked == target && gameTime.TotalGameTime.TotalMilliseconds - _lastMouseRightDownTime < DOUBLE_CLICK_TIME)
                    {
                        target.RightDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        _lastElementRightClicked = null;
                    }
                    _lastMouseRightDownTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!rightDown && _wasRightMouseDown && _lastElementRightDown != null)
                {
                    UIElement lastElementRightDown = _lastElementRightDown;
                    if (lastElementRightDown.ContainsPoint(MousePosition))
                    {
                        lastElementRightDown.RightClick(new UIMouseEventArgs(lastElementRightDown, MousePosition));
                        _lastElementRightClicked = _lastElementRightDown;
                    }
                    lastElementRightDown.RightMouseUp(new UIMouseEventArgs(lastElementRightDown, MousePosition));
                    _lastElementRightDown = null;
                }
                if (middleDown && !_wasMiddleMouseDown && target != null)
                {
                    _lastElementMiddleDown = target;
                    target.MiddleMouseDown(new UIMouseEventArgs(target, MousePosition));
                    if (_lastElementMiddleClicked == target && gameTime.TotalGameTime.TotalMilliseconds - _lastMouseMiddleDownTime < DOUBLE_CLICK_TIME)
                    {
                        target.MiddleDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        _lastElementMiddleClicked = null;
                    }
                    _lastMouseMiddleDownTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!middleDown && _wasMiddleMouseDown && _lastElementMiddleDown != null)
                {
                    UIElement lastElementMiddleDown = _lastElementMiddleDown;
                    if (lastElementMiddleDown.ContainsPoint(MousePosition))
                    {
                        lastElementMiddleDown.MiddleClick(new UIMouseEventArgs(lastElementMiddleDown, MousePosition));
                        _lastElementMiddleClicked = _lastElementMiddleDown;
                    }
                    lastElementMiddleDown.MiddleMouseUp(new UIMouseEventArgs(lastElementMiddleDown, MousePosition));
                    _lastElementMiddleDown = null;
                }
                if (backDown && !_wasBackMouseDown && target != null)
                {
                    _lastElementBackDown = target;
                    target.BackDown(new UIMouseEventArgs(target, MousePosition));
                    if (_lastElementBackClicked == target && gameTime.TotalGameTime.TotalMilliseconds - _lastMouseBackDownTime < DOUBLE_CLICK_TIME)
                    {
                        target.BackDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        _lastElementBackClicked = null;
                    }
                    _lastMouseBackDownTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!backDown && _wasBackMouseDown && _lastElementBackDown != null)
                {
                    UIElement lastElementXButton1Down = _lastElementBackDown;
                    if (lastElementXButton1Down.ContainsPoint(MousePosition))
                    {
                        lastElementXButton1Down.BackClick(new UIMouseEventArgs(lastElementXButton1Down, MousePosition));
                        _lastElementBackClicked = _lastElementBackDown;
                    }
                    lastElementXButton1Down.BackUp(new UIMouseEventArgs(lastElementXButton1Down, MousePosition));
                    _lastElementBackDown = null;
                }
                if (forwardDown && !_wasForwardMouseDown && target != null)
                {
                    _lastElementForwardDown = target;
                    target.ForwardDown(new UIMouseEventArgs(target, MousePosition));
                    if (_lastElementForwardClicked == target && gameTime.TotalGameTime.TotalMilliseconds - _lastMouseForwardDownTime < DOUBLE_CLICK_TIME)
                    {
                        target.ForwardDoubleClick(new UIMouseEventArgs(target, MousePosition));
                        _lastElementForwardClicked = null;
                    }
                    _lastMouseForwardDownTime = gameTime.TotalGameTime.TotalMilliseconds;
                }
                else if (!forwardDown && _wasForwardMouseDown && _lastElementForwardDown != null)
                {
                    UIElement lastElementXButton2Down = _lastElementForwardDown;
                    if (lastElementXButton2Down.ContainsPoint(MousePosition))
                    {
                        lastElementXButton2Down.ForwardClick(new UIMouseEventArgs(lastElementXButton2Down, MousePosition));
                        _lastElementForwardClicked = _lastElementForwardDown;
                    }
                    lastElementXButton2Down.ForwardUp(new UIMouseEventArgs(lastElementXButton2Down, MousePosition));
                    _lastElementForwardDown = null;
                }
            }
            if (PlayerInput.ScrollWheelDeltaForUI != 0) target?.ScrollWheel(new UIScrollWheelEventArgs(target, MousePosition, PlayerInput.ScrollWheelDeltaForUI));
            _wasMouseDown = leftDown;
            _wasRightMouseDown = rightDown;
            _wasMiddleMouseDown = middleDown;
            _wasBackMouseDown = backDown;
            _wasForwardMouseDown = forwardDown;
            Children.ForEach(child => child.Update(gameTime));
        }

        public void Draw(SpriteBatch sb)
        {
            if (Visible)
            {
                Use();
                Children.ForEach(child => child.Draw(sb));
                if (ItemModifier.Instance.DimensionsView && _lastElementHover != null) sb.Draw(ItemModifier.Textures.WindowBackground, (ItemModifier.Instance.DimensionsType == 1 ? _lastElementHover.OuterDimensions : ItemModifier.Instance.DimensionsType == 2 ? _lastElementHover.Dimensions : _lastElementHover.InnerDimensions).Rectangle, new Color(255, 255, 255, 50));
            }
        }

        internal void RefreshState()
        {
            Children.ForEach(child => child.Deactivate());
            ResetState();
            Children.ForEach(child =>
            {
                child.Activate();
                child.Recalculate();
            });
        }

        public void Recalculate()
        {
            Children.ForEach(child => child.Recalculate());
        }

        public bool RemoveChild(UIElement Child)
        {
            Child.ParentInterface = null;
            return Children.Remove(Child);
        }

        public void RemoveAllChildren()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].ParentInterface = null;
            }
            Children.Clear();
        }

        public UIElement GetElementAt(Vector2 point)
        {
            for (int i = Children.Count - 1; i > -1; i--)
            {
                UIElement element = Children[i];
                if (element.Visible && element.ContainsPoint(point))
                {
                    return element.GetElementAt(point);
                }
            }
            return null;
        }

        public void Activate()
        {
            if (!Initialized) Initialize();
            OnActivate();
            Children.ForEach(child => child.Activate());
            Recalculate();
        }

        public virtual void OnActivate()
        {

        }

        public void Deactivate()
        {
            OnDeactivate();
            Children.ForEach(child => child.Deactivate());
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
    }
}

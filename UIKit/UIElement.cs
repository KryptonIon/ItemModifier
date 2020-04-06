using ItemModifier.UIKit.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;
using Terraria;
using static ItemModifier.UIKit.Utils;

namespace ItemModifier.UIKit
{
    public class UIElement : IEnumerable<UIElement>
    {
        public delegate void UIEventHandler(UIElement sender);

        public delegate void UIEventHandler<T>(UIElement sender, T e);

        public event UIEventHandler<UIMouseEventArgs> OnMouseOver;

        public event UIEventHandler<UIMouseEventArgs> WhileMouseHover;

        public event UIEventHandler<UIMouseEventArgs> OnMouseOut;

        public event UIEventHandler<UIMouseEventArgs> OnLeftDown;

        public event UIEventHandler<UIMouseEventArgs> OnLeftUp;

        public event UIEventHandler<UIMouseEventArgs> OnLeftClick;

        public event UIEventHandler<UIMouseEventArgs> OnLeftDoubleClick;

        public event UIEventHandler<UIMouseEventArgs> OnRightDown;

        public event UIEventHandler<UIMouseEventArgs> OnRightUp;

        public event UIEventHandler<UIMouseEventArgs> OnRightClick;

        public event UIEventHandler<UIMouseEventArgs> OnRightDoubleClick;

        public event UIEventHandler<UIMouseEventArgs> OnMiddleDown;

        public event UIEventHandler<UIMouseEventArgs> OnMiddleUp;

        public event UIEventHandler<UIMouseEventArgs> OnMiddleClick;

        public event UIEventHandler<UIMouseEventArgs> OnMiddleDoubleClick;

        public event UIEventHandler<UIMouseEventArgs> OnBackDown;

        public event UIEventHandler<UIMouseEventArgs> OnBackUp;

        public event UIEventHandler<UIMouseEventArgs> OnBackClick;

        public event UIEventHandler<UIMouseEventArgs> OnBackDoubleClick;

        public event UIEventHandler<UIMouseEventArgs> OnForwardDown;

        public event UIEventHandler<UIMouseEventArgs> OnForwardUp;

        public event UIEventHandler<UIMouseEventArgs> OnForwardClick;

        public event UIEventHandler<UIMouseEventArgs> OnForwardDoubleClick;

        public event UIEventHandler<UIScrollWheelEventArgs> OnScrollWheel;

        public event UIEventHandler<bool> OnVisibilityChanged;

        public event UIEventHandler OnFocused;

        public event UIEventHandler OnUnfocused;

        public event UIEventHandler<UIElement> OnChildAdded;

        protected List<UIElement> Children { get; } = new List<UIElement>();

        private UIElement parent;

        public UIElement Parent
        {
            get
            {
                return parent;
            }

            set
            {
                Parent?.Children.Remove(this);
                parent = value;
                if (Parent != null)
                {
                    Parent.Children.Add(this);
                    parentUI = Parent.ParentUI;
                    Parent.OnChildAdded?.Invoke(Parent, this);
                }
                Recalculate();
            }
        }

        private UserInterface parentUI;

        public UserInterface ParentUI
        {
            get
            {
                return parentUI;
            }

            set
            {
                ParentUI?.Children.Remove(this);
                parentUI = value;
                ParentUI?.Children.Add(this);
                for (int i = 0; i < Children.Count; i++) Children[i].parentUI = ParentUI;
                Recalculate();
            }
        }

        public SizeDimension Width { get; set; }

        public SizeDimension Height { get; set; }

        public SizeDimension XOffset { get; set; }

        public SizeDimension YOffset { get; set; }

        public Thickness Margin { get; set; }

        public Thickness Padding { get; set; }

        public float VerticalAlign { get; set; }

        public float HorizontalAlign { get; set; }

        // Calculated X

        public float OuterX { get; protected set; }

        // Calculated Y

        public float OuterY { get; protected set; }

        // Marginated Width

        public float OuterWidth { get; protected set; }

        // Marginated Height

        public float OuterHeight { get; protected set; }

        // Marginated X

        public float PadX { get; protected set; }

        // Marginated Y

        public float PadY { get; protected set; }

        // Padded Width

        public float PadWidth { get; protected set; }

        // Padded Height

        public float PadHeight { get; protected set; }

        // Padded X

        public float InnerX { get; protected set; }

        // Padded Y

        public float InnerY { get; protected set; }

        // Calculated Width

        public float InnerWidth { get; protected set; }

        // Calculated Height

        public float InnerHeight { get; protected set; }

        // Calculated X Offset

        public float CalculatedXOffset { get; protected set; }

        // Calculated Y Offset

        public float CalculatedYOffset { get; protected set; }

        // Position

        public Vector2 OuterPosition
        {
            get
            {
                return new Vector2(OuterX, OuterY);
            }
        }

        // PosPoint

        public Point OuterPoint
        {
            get
            {
                return new Point((int)OuterX, (int)OuterY);
            }
        }

        // Marginated Position

        public Vector2 PadPosition
        {
            get
            {
                return new Vector2(PadX, PadY);
            }
        }

        // Marginated Point

        public Point PadPoint
        {
            get
            {
                return new Point((int)PadX, (int)PadY);
            }
        }

        // Padded Position

        public Vector2 InnerPosition
        {
            get
            {
                return new Vector2(InnerX, InnerY);
            }
        }

        // Padded Point

        public Point InnerPoint
        {
            get
            {
                return new Point((int)InnerX, (int)InnerY);
            }
        }

        public Rectangle OuterRect
        {
            get
            {
                return new Rectangle((int)OuterX, (int)OuterY, (int)OuterWidth, (int)OuterHeight);
            }
        }

        public Rectangle PadRect
        {
            get
            {
                return new Rectangle((int)PadX, (int)PadY, (int)PadWidth, (int)PadHeight);
            }
        }

        public Rectangle InnerRect
        {
            get
            {
                return new Rectangle((int)InnerX, (int)InnerY, (int)InnerWidth, (int)InnerHeight);
            }
        }

        public bool OverflowHidden { get; set; }

        public static RasterizerState OverflowHiddenRasterizerState { get; } = new RasterizerState
        {
            CullMode = CullMode.None,
            ScissorTestEnable = true
        };

        protected bool UseImmediateMode { get; set; }

        public bool Initialized { get; private set; }

        public bool MouseHovering { get; protected set; }

        public bool IsLeftDown { get; protected set; }

        public bool IsRightDown { get; protected set; }

        public bool IsMiddleDown { get; protected set; }

        public bool IsBackDown { get; protected set; }

        public bool IsForwardDown { get; protected set; }

        private bool visible = true;

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                if (Visible != value)
                {
                    visible = value;
                    OnVisibilityChanged?.Invoke(this, Visible);
                }
            }
        }

        public bool Focused { get; protected set; }

        public UIElement this[int index]
        {
            get
            {
                return Children[index];
            }
        }

        public int ChildrenCount
        {
            get
            {
                return Children.Count;
            }
        }

        public UIElement()
        {

        }

        public bool HasChild(UIElement child)
        {
            return Children.Contains(child);
        }

        public virtual bool ContainsPoint(Vector2 point)
        {
            return point.X >= PadX && point.Y >= PadY && point.X <= PadX + PadWidth && point.Y <= PadY + PadHeight;
        }

        public virtual bool ContainsPoint(Point point)
        {
            return point.X >= PadX && point.Y >= PadY && point.X <= PadX + PadWidth && point.Y <= PadY + PadHeight;
        }

        public UIElement GetElementAt(Vector2 point)
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                UIElement element = Children[i];
                if (element.Visible && element.ContainsPoint(point)) return element.GetElementAt(point);
            }
            return this;
        }

        public UIElement GetElementAt(Point point)
        {
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                UIElement element = Children[i];
                if (element.Visible && element.ContainsPoint(point)) return element.GetElementAt(point);
            }
            return this;
        }

        public void Update(GameTime gameTime)
        {
            UpdateSelf(gameTime);
            for (int i = 0; i < Children.Count; i++) Children[i].Update(gameTime);
        }

        protected virtual void UpdateSelf(GameTime gameTime)
        {

        }

        public void PostUpdateInput()
        {
            PostUpdateInputSelf();
            for (int i = 0; i < Children.Count; i++) Children[i].PostUpdateInput();
        }

        protected virtual void PostUpdateInputSelf()
        {

        }

        public virtual void Draw(SpriteBatch sb)
        {
            if (Visible)
            {
                if (UseImmediateMode)
                {
                    sb.End();
                    sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
                    DrawSelf(sb);
                    sb.End();
                    sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
                }
                else
                {
                    DrawSelf(sb);
                }

                if (OverflowHidden)
                {
                    Rectangle scissorRectangle = sb.GraphicsDevice.ScissorRectangle;
                    sb.End();
                    sb.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(GetClippingRectangle(sb, InnerRect), sb.GraphicsDevice.ScissorRectangle);
                    sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
                    DrawChildren(sb);
                    sb.End();
                    sb.GraphicsDevice.ScissorRectangle = scissorRectangle;
                    sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, sb.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
                }
                else
                {
                    DrawChildren(sb);
                }
            }
        }

        protected virtual void DrawSelf(SpriteBatch sb)
        {

        }

        protected virtual void DrawChildren(SpriteBatch sb)
        {
            for (int i = 0; i < Children.Count; i++) Children[i].Draw(sb);
        }

        public void RemoveAllChildren()
        {
            while (Children.Count > 0) Children[0].Parent = null;
        }

        public virtual void Recalculate()
        {
            RecalculateSelf();
            RecalculateChildren();
        }

        protected internal virtual void RecalculateSelf()
        {
            float baseX;
            float baseY;
            float baseWidth;
            float baseHeight;
            if (Parent == null)
            {
                baseX = 0;
                baseY = 0;
                baseWidth = Main.screenWidth;
                baseHeight = Main.screenHeight;
            }
            else
            {
                baseX = Parent.InnerX;
                baseY = Parent.InnerY;
                baseWidth = Parent.InnerWidth;
                baseHeight = Parent.InnerHeight;
            }
            InnerWidth = Width.CalculateValue(baseWidth);
            InnerHeight = Height.CalculateValue(baseHeight);
            PadWidth = InnerWidth + Padding.Left + Padding.Right;
            PadHeight = InnerHeight + Padding.Top + Padding.Bottom;
            OuterWidth = PadWidth + Margin.Left + Margin.Right;
            OuterHeight = PadHeight + Margin.Top + Margin.Bottom;
            CalculatedXOffset = XOffset.CalculateValue(baseWidth);
            OuterX = baseX + CalculatedXOffset;
            if (HorizontalAlign != 0f) OuterX += (baseWidth - OuterWidth) * HorizontalAlign;
            CalculatedYOffset = YOffset.CalculateValue(baseHeight);
            OuterY = baseY + CalculatedYOffset;
            if (VerticalAlign != 0f) OuterY += (baseHeight - OuterHeight) * VerticalAlign;
            if (Parent is UIContainer container) OuterY -= container.ScrollValue;
            PadX = OuterX + Margin.Left;
            PadY = OuterY + Margin.Top;
            InnerX = PadX + Padding.Left;
            InnerY = PadY + Padding.Top;
        }

        public virtual void RecalculateChildren()
        {
            for (int i = 0; i < Children.Count; i++) Children[i].Recalculate();
        }

        #region MouseEventsInvokers

        public virtual void ScrollWheel(UIScrollWheelEventArgs e)
        {
            OnScrollWheel?.Invoke(this, e);
            Parent?.ScrollWheel(e);
        }

        public virtual void MouseOver(UIMouseEventArgs e)
        {
            MouseHovering = true;
            OnMouseOver?.Invoke(this, e);
            Parent?.MouseOver(e);
        }

        public virtual void MouseHover(UIMouseEventArgs e)
        {
            WhileMouseHover?.Invoke(this, e);
            Parent?.MouseHover(e);
        }

        public virtual void MouseOut(UIMouseEventArgs e)
        {
            MouseHovering = false;
            OnMouseOut?.Invoke(this, e);
            Parent?.MouseOut(e);
        }

        public virtual void LeftMouseDown(UIMouseEventArgs e)
        {
            IsLeftDown = true;
            OnLeftDown?.Invoke(this, e);
            Parent?.LeftMouseDown(e);
        }

        public virtual void LeftMouseUp(UIMouseEventArgs e)
        {
            IsLeftDown = false;
            OnLeftUp?.Invoke(this, e);
            Parent?.LeftMouseUp(e);
        }

        public virtual void LeftClick(UIMouseEventArgs e)
        {
            OnLeftClick?.Invoke(this, e);
            Parent?.LeftClick(e);
        }

        public virtual void LeftDoubleClick(UIMouseEventArgs e)
        {
            OnLeftDoubleClick?.Invoke(this, e);
            Parent?.LeftDoubleClick(e);
        }

        public virtual void RightMouseDown(UIMouseEventArgs e)
        {
            IsRightDown = true;
            OnRightDown?.Invoke(this, e);
            Parent?.RightMouseDown(e);
        }

        public virtual void RightMouseUp(UIMouseEventArgs e)
        {
            IsRightDown = false;
            OnRightUp?.Invoke(this, e);
            Parent?.RightMouseUp(e);
        }

        public virtual void RightClick(UIMouseEventArgs e)
        {
            OnRightClick?.Invoke(this, e);
            Parent?.RightClick(e);
        }

        public virtual void RightDoubleClick(UIMouseEventArgs e)
        {
            OnRightDoubleClick?.Invoke(this, e);
            Parent?.RightDoubleClick(e);
        }

        public virtual void MiddleMouseDown(UIMouseEventArgs e)
        {
            IsMiddleDown = true;
            OnMiddleDown?.Invoke(this, e);
            Parent?.MiddleMouseDown(e);
        }

        public virtual void MiddleMouseUp(UIMouseEventArgs e)
        {
            IsMiddleDown = true;
            OnMiddleUp?.Invoke(this, e);
            Parent?.MiddleMouseUp(e);
        }

        public virtual void MiddleClick(UIMouseEventArgs e)
        {
            OnMiddleClick?.Invoke(this, e);
            Parent?.MiddleClick(e);
        }

        public virtual void MiddleDoubleClick(UIMouseEventArgs e)
        {
            OnMiddleDoubleClick?.Invoke(this, e);
            Parent?.MiddleDoubleClick(e);
        }

        public virtual void BackDown(UIMouseEventArgs e)
        {
            IsBackDown = true;
            OnBackDown?.Invoke(this, e);
            Parent?.BackDown(e);
        }

        public virtual void BackUp(UIMouseEventArgs e)
        {
            IsBackDown = false;
            OnBackUp?.Invoke(this, e);
            Parent?.BackUp(e);
        }

        public virtual void BackClick(UIMouseEventArgs e)
        {
            OnBackClick?.Invoke(this, e);
            Parent?.BackClick(e);
        }

        public virtual void BackDoubleClick(UIMouseEventArgs e)
        {
            OnBackDoubleClick?.Invoke(this, e);
            Parent?.BackDoubleClick(e);
        }

        public virtual void ForwardDown(UIMouseEventArgs e)
        {
            IsForwardDown = true;
            OnForwardDown?.Invoke(this, e);
            Parent?.ForwardDown(e);
        }

        public virtual void ForwardUp(UIMouseEventArgs e)
        {
            IsForwardDown = false;
            OnForwardUp?.Invoke(this, e);
            Parent?.ForwardUp(e);
        }

        public virtual void ForwardClick(UIMouseEventArgs e)
        {
            OnForwardClick?.Invoke(this, e);
            Parent?.ForwardClick(e);
        }

        public virtual void ForwardDoubleClick(UIMouseEventArgs e)
        {
            OnForwardDoubleClick?.Invoke(this, e);
            Parent?.ForwardDoubleClick(e);
        }

        #endregion

        public virtual void Focus()
        {
            Focused = true;
            OnFocused?.Invoke(this);
            Parent?.Focus();
        }

        public virtual void Unfocus()
        {
            Focused = false;
            OnUnfocused?.Invoke(this);
            Parent?.Unfocus();
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

        public virtual void SelectRadio(UIRadioButton radio)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] is UIRadioButton child && child != radio) child.Selected = false;
            }
        }

        public virtual void DeselectAllRadio()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] is UIRadioButton child) child.Selected = false;
            }
        }

        public IEnumerator<UIElement> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Children.GetEnumerator();
        }
    }
}

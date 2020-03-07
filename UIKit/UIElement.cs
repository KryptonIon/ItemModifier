using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace ItemModifier.UIKit
{
    public abstract class UIElement : IComparable<UIElement>
    {
        public delegate void UIEventHandler<T>(UIElement source, T args);

        public string ID { get; private set; }

        private UIElement parent;

        public UIElement Parent
        {
            get => parent;

            set
            {
                if (value == null)
                {
                    parent?.RemoveChild(this);
                }
                else
                {
                    parent?.Children.Remove(this);
                    parent = value;
                    parent.Children.Add(this);
                    if (InheritVisibility) Visible = parent.Visible;
                    parentInterface = parent.parentInterface;
                }
                Recalculate();
            }
        }

        private UserInterface parentInterface;

        public UserInterface ParentInterface
        {
            get => parentInterface;

            set
            {
                if (value == null)
                {
                    parentInterface?.RemoveChild(this);
                }
                else
                {
                    parentInterface?.Children.Remove(this);
                    parentInterface = value;
                    parentInterface.Children.Add(this);
                }
                Children.ForEach(child => child.parentInterface = parentInterface);
                Recalculate();
            }
        }

        protected List<UIElement> Children = new List<UIElement>();

        public StyleDimension Top { get; set; }

        public StyleDimension Left { get; set; }

        public StyleDimension Width { get; set; }

        public StyleDimension Height { get; set; }

        public StyleDimension MaxWidth { get; set; } = StyleDimension.Fill;

        public StyleDimension MaxHeight { get; set; } = StyleDimension.Fill;

        public StyleDimension MinWidth { get; set; } = StyleDimension.Empty;

        public StyleDimension MinHeight { get; set; } = StyleDimension.Empty;

        public Vector4 Padding;

        public Vector4 Margin;

        public bool OverflowHidden { get; set; }

        public float HorizontalAlign { get; set; }

        public float VerticalAlign { get; set; }

        public Dimensions InnerDimensions { get; protected set; }

        public Dimensions Dimensions { get; protected set; }

        public Dimensions OuterDimensions { get; protected set; }

        public static RasterizerState OverflowHiddenRasterizerState { get; protected set; }

        private SnapPoint _snapPoint;

        public virtual List<SnapPoint> SnapPoints
        {
            get
            {
                List<SnapPoint> list = new List<SnapPoint>();

                if (GetSnapPoint(out SnapPoint item))
                {
                    list.Add(item);
                }

                Children.ForEach(element => list.AddRange(element.SnapPoints));

                return list;
            }
        }

        public bool UseImmediateMode { get; protected set; }

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
            get => visible;

            set
            {
                if (visible != value)
                {
                    visible = value;
                    Children.ForEach(child => { if (child.InheritVisibility) child.Visible = visible; });
                    OnVisibilityChanged?.Invoke(this, visible);
                }
            }
        }

        public bool InheritVisibility { get; set; } = true;

        #region MouseEvents

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

        #endregion

        public int Count { get => Children.Count; }

        public UIElement(Vector4 Padding = default, Vector4 Margin = default)
        {
            ID = Guid.NewGuid().ToString();
            this.Padding = Padding;
            this.Margin = Margin;
            if (OverflowHiddenRasterizerState == null)
            {
                OverflowHiddenRasterizerState = new RasterizerState
                {
                    CullMode = CullMode.None,
                    ScissorTestEnable = true
                };
            }
        }

        public int CompareTo(UIElement element)
        {
            return ID.CompareTo(element.ID);
        }

        public UIElement this[int index]
        {
            get => Children[index];
        }

        public Rectangle GetClippingRectangle(SpriteBatch sb)
        {
            Vector2 vector = Vector2.Transform(new Vector2(InnerDimensions.X, InnerDimensions.Y), Main.UIScaleMatrix);
            Vector2 position = Vector2.Transform(new Vector2(InnerDimensions.Width, InnerDimensions.Height) + vector, Main.UIScaleMatrix);
            int width = sb.GraphicsDevice.Viewport.Width;
            int height = sb.GraphicsDevice.Viewport.Height;
            Rectangle result = new Rectangle(Utils.Clamp((int)vector.X, 0, width), Utils.Clamp((int)vector.Y, 0, height), (int)(position.X - vector.X), (int)(position.Y - vector.Y));
            result.Width = Utils.Clamp(result.Width, 0, width - result.X);
            result.Height = Utils.Clamp(result.Height, 0, height - result.Y);
            return result;
        }

        public virtual bool ContainsPoint(Vector2 point)
        {
            return point.X >= Dimensions.X && point.Y >= Dimensions.Y && point.X <= Dimensions.X + Dimensions.Width && point.Y <= Dimensions.Y + Dimensions.Height;
        }

        public UIElement GetElementAt(Vector2 point)
        {
            for (int i = Children.Count - 1; i > -1; i--)
            {
                UIElement element = Children[i];
                if (element.Visible && element.ContainsPoint(point)) return element.GetElementAt(point);
            }
            return this;
        }

        public bool GetSnapPoint(out SnapPoint point)
        {
            point = _snapPoint;
            _snapPoint?.Calculate(this);
            return _snapPoint != null;
        }

        public void SetSnapPoint(string name, int id, Vector2? anchor = null, Vector2? offset = null)
        {
            if (!anchor.HasValue)
            {
                anchor = new Vector2(0.5f);
            }
            if (!offset.HasValue)
            {
                offset = Vector2.Zero;
            }
            _snapPoint = new SnapPoint(name, id, anchor.Value, offset.Value);
        }

        public void SetPadding(float pixels) => (Padding.X, Padding.Y, Padding.Z, Padding.W) = (pixels, pixels, pixels, pixels);

        public virtual void Update(GameTime gameTime)
        {
            Children.ForEach(element => element.Update(gameTime));
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
                    sb.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(GetClippingRectangle(sb), sb.GraphicsDevice.ScissorRectangle);
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
            Children.ForEach(element => element.Draw(sb));
        }

        public bool RemoveChild(UIElement Child)
        {
            Child.parent = null;
            return Children.Remove(Child);
        }

        public void RemoveAllChildren()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Parent = null;
            }
            Children.Clear();
        }

        public bool Contains(UIElement child)
        {
            return Children.Contains(child);
        }

        public virtual void Recalculate()
        {
            Dimensions ParentInnerDimension = Parent?.InnerDimensions ?? ParentInterface?.Dimensions ?? UserInterface.ActiveInstance.Dimensions;
            var width = MathHelper.Clamp(Width.CalculateValue(ParentInnerDimension.Width), MinWidth.CalculateValue(ParentInnerDimension.Width), MaxWidth.CalculateValue(ParentInnerDimension.Width)) + Padding.X + Padding.Z + Margin.X + Margin.Z;
            var height = MathHelper.Clamp(Height.CalculateValue(ParentInnerDimension.Height), MinHeight.CalculateValue(ParentInnerDimension.Height), MaxHeight.CalculateValue(ParentInnerDimension.Height)) + Padding.Y + Padding.W + Margin.Y + Margin.W;
            OuterDimensions = new Dimensions(Left.CalculateValue(ParentInnerDimension.Width) + ParentInnerDimension.X + ParentInnerDimension.Width * HorizontalAlign - width * HorizontalAlign, Top.CalculateValue(ParentInnerDimension.Height) + ParentInnerDimension.Y + ParentInnerDimension.Height * VerticalAlign - height * VerticalAlign, width, height);
            Dimensions = new Dimensions(OuterDimensions.X + Margin.X, OuterDimensions.Y + Margin.Y, OuterDimensions.Width - Margin.X - Margin.Z, OuterDimensions.Height - Margin.Y - Margin.W);
            InnerDimensions = new Dimensions(Dimensions.X + Padding.X, Dimensions.Y + Padding.Y, Dimensions.Width - Padding.X - Padding.Z, Dimensions.Height - Padding.Y - Padding.W);
            RecalculateChildren();
        }

        public virtual void RecalculateChildren()
        {
            Children.ForEach(element => element.Recalculate());
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

        public void Activate()
        {
            if (!Initialized) Initialize();
            OnActivate();
            Children.ForEach(child => child.Activate());
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

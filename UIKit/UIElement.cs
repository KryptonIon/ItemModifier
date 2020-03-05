﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Represents an user interface element.
    /// </summary>
    public abstract class UIElement : IComparable<UIElement>
    {
        /// <summary>
        /// Event Handler for UI Events.
        /// </summary>
        /// <typeparam name="T">Arguments or value.</typeparam>
        /// <param name="source"><see cref="UIElement"/> that raised the event.</param>
        /// <param name="args">Arguments or value specified by event.</param>
        public delegate void UIEventHandler<T>(UIElement source, T args);

        /// <summary>
        /// Unique ID for the element.
        /// </summary>
        public string ID { get; private set; }

        private UIElement parent;

        /// <summary>
        /// Parent element of this element.
        /// </summary>
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

        /// <summary>
        /// Child elements of this element.
        /// </summary>
        protected List<UIElement> Children = new List<UIElement>();

        /// <summary>
        /// Defines the space between the very top of it's parent and this element.
        /// </summary>
        public StyleDimension Top { get; set; }

        /// <summary>
        /// Defines the space between the very left of it's parent and this element.
        /// </summary>
        public StyleDimension Left { get; set; }

        /// <summary>
        /// Width of element.
        /// </summary>
        public StyleDimension Width { get; set; }

        /// <summary>
        /// Height of element.
        /// </summary>
        public StyleDimension Height { get; set; }

        /// <summary>
        /// Maximum value of <see cref="Width"/>.
        /// </summary>
        public StyleDimension MaxWidth { get; set; } = StyleDimension.Fill;

        /// <summary>
        /// Maximum value of <see cref="Height"/>.
        /// </summary>
        public StyleDimension MaxHeight { get; set; } = StyleDimension.Fill;

        /// <summary>
        /// Mininum value of <see cref="Width"/>.
        /// </summary>
        public StyleDimension MinWidth { get; set; } = StyleDimension.Empty;

        /// <summary>
        /// Minimum value of <see cref="Height"/>.
        /// </summary>
        public StyleDimension MinHeight { get; set; } = StyleDimension.Empty;

        /// <summary>
        /// Add space inside the element.
        /// <see cref="Vector4.X"/> = Left.
        /// <see cref="Vector4.Z"/> = Right.
        /// <see cref="Vector4.Y"/> = Up.
        /// <see cref="Vector4.W"/> = Down.
        /// </summary>
        public Vector4 Padding;

        /// <summary>>
        /// Add space around the element.
        /// <see cref="Vector4.X"/> = Left.
        /// <see cref="Vector4.Z"/> = Right.
        /// <see cref="Vector4.Y"/> = Up.
        /// <see cref="Vector4.W"/> = Down.
        /// </summary>
        public Vector4 Margin;

        /// <summary>
        /// If true, stuff exceeding the boundaries of this element will be clipped.
        /// </summary>
        public bool OverflowHidden { get; set; }

        public float HorizontalAlign { get; set; }

        public float VerticalAlign { get; set; }

        /// <summary>
        /// Base dimensions of this element.
        /// </summary>
        public Dimensions InnerDimensions { get; protected set; }

        /// <summary>
        /// Base dimensions with padding included.
        /// </summary>
        public Dimensions Dimensions { get; protected set; }

        /// <summary>
        /// Base dimensions with padding and margin included.
        /// </summary>
        public Dimensions OuterDimensions { get; protected set; }

        /// <summary>
        /// <see cref="RasterizerState"/> that defines how to handle overflow.
        /// </summary>
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

        /// <summary>
        /// If true, immediate sprite sorting mode will be used over deferred sprite sorting mode.
        /// </summary>
        public bool UseImmediateMode { get; protected set; }

        /// <summary>
        /// True if <see cref="OnInitialize"/> has been invoked.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// True if mouse is hovering over element, false otherwise.
        /// </summary>
        public bool MouseHovering { get; protected set; }

        /// <summary>
        /// True if left mouse is being held down over the element.
        /// </summary>
        public bool IsLeftDown { get; protected set; }

        /// <summary>
        /// True if right mouse is being held down over the element.
        /// </summary>
        public bool IsRightDown { get; protected set; }

        /// <summary>
        /// True if middle mouse is being held down over the element.
        /// </summary>
        public bool IsMiddleDown { get; protected set; }

        /// <summary>
        /// True if back mouse is being held down over the element.
        /// </summary>
        public bool IsBackDown { get; protected set; }

        /// <summary>
        /// True if forward mouse is being held down over the element.
        /// </summary>
        public bool IsForwardDown { get; protected set; }

        private bool visible = true;

        /// <summary>
        /// True if element is visible, false otherwise.
        /// </summary>
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

        /// <summary>
        /// If true, the element will inherit the parent's visibility changes.
        /// </summary>
        public bool InheritVisibility { get; set; } = true;

        #region MouseEvents

        /// <summary>
        /// Fired when mouse initially hovers over element.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnMouseOver;

        /// <summary>
        /// Fired while mouse is hovering.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> WhileMouseHover;

        /// <summary>
        /// Fired when mouse stops hovering over element.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnMouseOut;

        /// <summary>
        /// Fired when left mouse is held down.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnLeftDown;

        /// <summary>
        /// Fired when left mouse is released.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnLeftUp;

        /// <summary>
        /// Fired when left mouse is clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnLeftClick;

        /// <summary>
        /// Fired when left mouse is double clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnLeftDoubleClick;

        /// <summary>
        /// Fired when right mouse is held down.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnRightDown;

        /// <summary>
        /// Fired when right mouse is released.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnRightUp;

        /// <summary>
        /// Fired when left mouse is clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnRightClick;

        /// <summary>
        /// Fired when left mouse is double clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnRightDoubleClick;

        /// <summary>
        /// Fired when middle mouse is held down.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnMiddleDown;

        /// <summary>
        /// Fired when middle mouse is released.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnMiddleUp;

        /// <summary>
        /// Fired when middle mouse is clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnMiddleClick;

        /// <summary>
        /// Fired when middle mouse is double clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnMiddleDoubleClick;

        /// <summary>
        /// Fired when back mouse is held down.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnBackDown;

        /// <summary>
        /// Fired when back mouse is released.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnBackUp;

        /// <summary>
        /// Fired when back mouse is clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnBackClick;

        /// <summary>
        /// Fired when back mouse is double clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnBackDoubleClick;

        /// <summary>
        /// Fired when forward mouse is held down.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnForwardDown;

        /// <summary>
        /// Fired when forward mouse is released.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnForwardUp;

        /// <summary>
        /// Fired when forward mouse is clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnForwardClick;

        /// <summary>
        /// Fired when forward mouse is double clicked.
        /// </summary>
        public event UIEventHandler<UIMouseEventArgs> OnForwardDoubleClick;

        /// <summary>
        /// Fired when scrolling over the element.
        /// </summary>
        public event UIEventHandler<UIScrollWheelEventArgs> OnScrollWheel;

        /// <summary>
        /// Fired when <see cref="Visible"/> is changed.
        /// </summary>
        public event UIEventHandler<bool> OnVisibilityChanged;

        #endregion

        /// <summary>
        /// Count of children this element has.
        /// </summary>
        public int Count { get => Children.Count; }

        /// <summary>
        /// Initializes a new <see cref="UIElement"/>.
        /// </summary>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
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

        /// <summary>
        /// Compares this element's ID with another element's ID.
        /// </summary>
        /// <param name="element">Element to compare to.</param>
        /// <returns>An integer that marks whether this element precedes, succeeds or is in the same position as the other element.</returns>
        public int CompareTo(UIElement element)
        {
            return ID.CompareTo(element.ID);
        }

        /// <summary>
        /// Returns a UIElement.
        /// </summary>
        /// <param name="index">Index of UIElement.</param>
        /// <returns>An UIElement in the corresponding index.</returns>
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

        /// <summary>
        /// Used to determine if the specified point is inside the element.
        /// </summary>
        /// <param name="point">Point in the screen.</param>
        /// <returns>True if the point is located inside the element, false otherwise.</returns>
        public virtual bool ContainsPoint(Vector2 point)
        {
            return point.X >= Dimensions.X && point.Y >= Dimensions.Y && point.X <= Dimensions.X + Dimensions.Width && point.Y <= Dimensions.Y + Dimensions.Height;
        }

        /// <summary>
        /// Gets the element at the specified point.
        /// </summary>
        /// <param name="point">Point in the screen.</param>
        /// <returns>The element at the specified point, null is non is found.</returns>
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

        /// <summary>
        /// Sets the padding of the element(on all sides).
        /// </summary>
        /// <param name="pixels">Padding in pixels.</param>
        public void SetPadding(float pixels) => (Padding.X, Padding.Y, Padding.Z, Padding.W) = (pixels, pixels, pixels, pixels);

        /// <summary>
        /// Triggered even if element is not visible.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            Children.ForEach(element => element.Update(gameTime));
        }

        /// <summary>
        /// Used to draw the element and it's children. Triggered only when element is visible.
        /// </summary>
        /// <param name="sb">Spritebatch to draw in.</param>
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

        /// <summary>
        /// Draw the element.
        /// </summary>
        /// <param name="sb">Spritebatch to draw in.</param>
        protected virtual void DrawSelf(SpriteBatch sb)
        {

        }

        /// <summary>
        /// Draw the element's children.
        /// </summary>
        /// <param name="sb">Spritebatch to draw in.</param>
        protected virtual void DrawChildren(SpriteBatch sb)
        {
            Children.ForEach(element => element.Draw(sb));
        }

        /// <summary>
        /// Remove a child element.
        /// </summary>
        /// <param name="Child">Child object.</param>
        public bool RemoveChild(UIElement Child)
        {
            Child.parent = null;
            return Children.Remove(Child);
        }

        /// <summary>
        /// Removes all children.
        /// </summary>
        public void RemoveAllChildren()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Parent = null;
            }
            Children.Clear();
        }

        /// <summary>
        /// Used to determine if this element has child.
        /// </summary>
        /// <param name="child">Child Object.</param>
        /// <returns>True if the child is a child of this element, false otherwise.</returns>
        public bool Contains(UIElement child)
        {
            return Children.Contains(child);
        }

        /// <summary>
        /// Recalculate element values.
        /// </summary>
        public virtual void Recalculate()
        {
            Dimensions ParentInnerDimension = Parent?.InnerDimensions ?? ParentInterface?.Dimensions ?? UserInterface.ActiveInstance.Dimensions;
            var width = MathHelper.Clamp(Width.CalculateValue(ParentInnerDimension.Width), MinWidth.CalculateValue(ParentInnerDimension.Width), MaxWidth.CalculateValue(ParentInnerDimension.Width)) + Padding.X + Padding.Z + Margin.X + Margin.Z;
            var height = MathHelper.Clamp(Height.CalculateValue(ParentInnerDimension.Height), MinHeight.CalculateValue(ParentInnerDimension.Height), MaxHeight.CalculateValue(ParentInnerDimension.Height)) + Padding.Y + Padding.W + Margin.Y + Margin.W;
            OuterDimensions = new Dimensions(width, height, Left.CalculateValue(ParentInnerDimension.Width) + ParentInnerDimension.X + ParentInnerDimension.Width * HorizontalAlign - width * HorizontalAlign, Top.CalculateValue(ParentInnerDimension.Height) + ParentInnerDimension.Y + ParentInnerDimension.Height * VerticalAlign - height * VerticalAlign);
            Dimensions = new Dimensions(OuterDimensions.X + Margin.X, OuterDimensions.Y + Margin.Y, OuterDimensions.Width - Margin.X - Margin.Z, OuterDimensions.Height - Margin.Y - Margin.W);
            InnerDimensions = new Dimensions(Dimensions.X + Padding.X, Dimensions.Y + Padding.Y, Dimensions.Width - Padding.X - Padding.Z, Dimensions.Height - Padding.Y - Padding.W);
            RecalculateChildren();
        }

        /// <summary>
        /// Triggers <see cref="Recalculate"/> for the element's children.
        /// </summary>
        public virtual void RecalculateChildren()
        {
            Children.ForEach(element => element.Recalculate());
        }

        #region MouseEventsInvokers

        /// <summary>
        /// Used to fire <see cref="OnScrollWheel"/>.
        /// </summary>
        /// <param name="e">Scroll Wheel Event Args.</param>
        public virtual void ScrollWheel(UIScrollWheelEventArgs e)
        {
            OnScrollWheel?.Invoke(this, e);
            Parent?.ScrollWheel(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnMouseOver"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void MouseOver(UIMouseEventArgs e)
        {
            MouseHovering = true;
            OnMouseOver?.Invoke(this, e);
            Parent?.MouseOver(e);
        }

        /// <summary>
        /// Used to fire <see cref="WhileMouseHover"/>
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void MouseHover(UIMouseEventArgs e)
        {
            WhileMouseHover?.Invoke(this, e);
            Parent?.MouseHover(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnMouseOut"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void MouseOut(UIMouseEventArgs e)
        {
            MouseHovering = false;
            OnMouseOut?.Invoke(this, e);
            Parent?.MouseOut(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnLeftDown"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void LeftMouseDown(UIMouseEventArgs e)
        {
            IsLeftDown = true;
            OnLeftDown?.Invoke(this, e);
            Parent?.LeftMouseDown(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnLeftUp"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void LeftMouseUp(UIMouseEventArgs e)
        {
            IsLeftDown = false;
            OnLeftUp?.Invoke(this, e);
            Parent?.LeftMouseUp(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnLeftClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void LeftClick(UIMouseEventArgs e)
        {
            OnLeftClick?.Invoke(this, e);
            Parent?.LeftClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnLeftDoubleClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void LeftDoubleClick(UIMouseEventArgs e)
        {
            OnLeftDoubleClick?.Invoke(this, e);
            Parent?.LeftDoubleClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnRightDown"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void RightMouseDown(UIMouseEventArgs e)
        {
            IsRightDown = true;
            OnRightDown?.Invoke(this, e);
            Parent?.RightMouseDown(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnRightUp"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void RightMouseUp(UIMouseEventArgs e)
        {
            IsRightDown = false;
            OnRightUp?.Invoke(this, e);
            Parent?.RightMouseUp(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnRightClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void RightClick(UIMouseEventArgs e)
        {
            OnRightClick?.Invoke(this, e);
            Parent?.RightClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnRightDoubleClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void RightDoubleClick(UIMouseEventArgs e)
        {
            OnRightDoubleClick?.Invoke(this, e);
            Parent?.RightDoubleClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnMiddleDown"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void MiddleMouseDown(UIMouseEventArgs e)
        {
            IsMiddleDown = true;
            OnMiddleDown?.Invoke(this, e);
            Parent?.MiddleMouseDown(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnMiddleUp"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void MiddleMouseUp(UIMouseEventArgs e)
        {
            IsMiddleDown = true;
            OnMiddleUp?.Invoke(this, e);
            Parent?.MiddleMouseUp(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnMiddleClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void MiddleClick(UIMouseEventArgs e)
        {
            OnMiddleClick?.Invoke(this, e);
            Parent?.MiddleClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnMiddleDoubleClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void MiddleDoubleClick(UIMouseEventArgs e)
        {
            OnMiddleDoubleClick?.Invoke(this, e);
            Parent?.MiddleDoubleClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnBackDown"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void BackDown(UIMouseEventArgs e)
        {
            IsBackDown = true;
            OnBackDown?.Invoke(this, e);
            Parent?.BackDown(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnBackUp"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void BackUp(UIMouseEventArgs e)
        {
            IsBackDown = false;
            OnBackUp?.Invoke(this, e);
            Parent?.BackUp(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnBackClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void BackClick(UIMouseEventArgs e)
        {
            OnBackClick?.Invoke(this, e);
            Parent?.BackClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnBackDoubleClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void BackDoubleClick(UIMouseEventArgs e)
        {
            OnBackDoubleClick?.Invoke(this, e);
            Parent?.BackDoubleClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnForwardDown"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void ForwardDown(UIMouseEventArgs e)
        {
            IsForwardDown = true;
            OnForwardDown?.Invoke(this, e);
            Parent?.ForwardDown(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnForwardUp"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void ForwardUp(UIMouseEventArgs e)
        {
            IsForwardDown = false;
            OnForwardUp?.Invoke(this, e);
            Parent?.ForwardUp(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnForwardClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void ForwardClick(UIMouseEventArgs e)
        {
            OnForwardClick?.Invoke(this, e);
            Parent?.ForwardClick(e);
        }

        /// <summary>
        /// Used to fire <see cref="OnForwardDoubleClick"/>.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        public virtual void ForwardDoubleClick(UIMouseEventArgs e)
        {
            OnForwardDoubleClick?.Invoke(this, e);
            Parent?.ForwardDoubleClick(e);
        }

        #endregion

        /// <summary>
        /// Activate Element, triggers <see cref="OnActivate"/> and <see cref="Initialize"/>.
        /// </summary>
        public void Activate()
        {
            if (!Initialized) Initialize();
            OnActivate();
            Children.ForEach(child => child.Activate());
        }

        /// <summary>
        /// Triggered by <see cref="Activate"/>.
        /// </summary>
        public virtual void OnActivate()
        {

        }

        /// <summary>
        /// Deactivate Element, triggers <see cref="OnDeactivate"/>
        /// </summary>
        public void Deactivate()
        {
            OnDeactivate();
            Children.ForEach(child => child.Deactivate());
        }

        /// <summary>
        /// Triggered by <see cref="Deactivate"/>.
        /// </summary>
        public virtual void OnDeactivate()
        {

        }

        /// <summary>
        /// Initialize element, triggers <see cref="OnInitialize"/>.
        /// </summary>
        public void Initialize()
        {
            OnInitialize();
            Initialized = true;
        }

        /// <summary>
        /// Triggered by <see cref="Initialize"/>.
        /// </summary>
        public virtual void OnInitialize()
        {

        }
    }
}
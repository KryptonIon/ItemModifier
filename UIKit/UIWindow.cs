using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    public class UIWindow : UIElement
    {
        /// <summary>
        /// Title of the window.
        /// </summary>
        public string Title { get; protected set; } = "";

        /// <summary>
        /// True if window is being dragged, false otherwise.
        /// </summary>
        public bool Dragging { get; private set; }

        /// <summary>
        /// True if window can be dragged, false otherwise.
        /// </summary>
        public bool Draggable { get; set; } = true;

        private Vector2 DragPos;

        /// <summary>
        /// Texture of the border.
        /// </summary>
        public Texture2D BorderTexture { get; set; } = ItemModifier.Textures.WindowBorder;

        /// <summary>
        /// Color of the border.
        /// </summary>
        public Color BorderColor { get; set; } = Color.Black;

        /// <summary>
        /// Texture of the background.
        /// </summary>
        public Texture2D BackgroundTexture { get; set; } = ItemModifier.Textures.WindowBackground;

        /// <summary>
        /// Color of the background.
        /// </summary>
        public Color BackgroundColor = KRUtils.UIBackgroundColor;

        /// <summary>
        /// Height of the title bar.
        /// </summary>
        public const int TitleBarHeight = 21;

        private bool useTitle = false;

        /// <summary>
        /// True if the window has a title bar, false otherwise.
        /// </summary>
        public bool UseTitle
        {
            get => useTitle;

            set
            {
                useTitle = value;
                CloseButton.Visible = useTitle;
                Recalculate();
            }
        }

        private bool hasBorder = true;

        /// <summary>
        /// True if the window has a border, false otherwise.
        /// </summary>
        public bool HasBorder
        {
            get => hasBorder;

            set
            {
                hasBorder = value;
                Recalculate();
            }
        }

        internal UIImageButton CloseButton = new UIImageButton(ItemModifier.Textures.X);

        /// <summary>
        /// The dimensions of the title bar. Null if there's none.
        /// </summary>
        public Dimensions? TitleBarDimensions { get; protected set; }

        private readonly bool hasCloseButton = false;

        /// <summary>
        /// Initializes a new <see cref="UIWindow"/> Element.
        /// </summary>
        /// <param name="Title">Title of the window.</param>
        /// <param name="UseTitle">If true, the title bar will be drawn.</param>
        /// <param name="HasCloseButton">If true, an X button to hide the window will be at the top right, false otherwise.</param>
        /// <param name="HasBorder">If true, a black border will surround the window.</param>
        /// <param name="Draggable">If true, the window can be dragged.</param>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIWindow(string Title, bool UseTitle = true, bool HasCloseButton = true, bool HasBorder = true, bool Draggable = true, Vector4 Padding = default, Vector4 Margin = default) : base(Padding, Margin)
        {
            this.UseTitle = UseTitle;
            this.Title = Title;
            hasCloseButton = HasCloseButton;
            hasBorder = HasBorder;
            this.Draggable = Draggable;
            Width = new StyleDimension(300f);
            Height = new StyleDimension(200f);
            OnVisibilityChanged += (source, value) => Main.PlaySound(value ? SoundID.MenuOpen : SoundID.MenuClose);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (Dragging)
            {
                Left = new StyleDimension(Main.mouseX - DragPos.X, Left.Pixels);
                Top = new StyleDimension(Main.mouseY - DragPos.Y, Top.Pixels);
                Recalculate();
            }
            Point dimensionsPoint = new Point((int)Dimensions.X, (int)Dimensions.Y);
            sb.Draw(BackgroundTexture, new Rectangle(dimensionsPoint.X, dimensionsPoint.Y, (int)Dimensions.Width, (int)Dimensions.Height), BackgroundColor);
            if (HasBorder)
            {
                Point borderpointTL = new Point(dimensionsPoint.X - 2, dimensionsPoint.Y - 2);
                int BarSizeTop = (int)Dimensions.Width - 8 - 8 + 4;
                int BarSizeBottom = (int)Dimensions.Width - 8 - 8 + 4;
                int BarSizeLeft = (int)Dimensions.Height - 8 - 8 + 4;
                int BarSizeRight = (int)Dimensions.Height - 8 - 8 + 4;
                sb.Draw(BorderTexture, new Rectangle(borderpointTL.X, borderpointTL.Y, 8, 8), new Rectangle(0, 0, 8, 8), BorderColor);
                sb.Draw(BorderTexture, new Rectangle(borderpointTL.X + 8, borderpointTL.Y, BarSizeTop, 8), new Rectangle(8, 0, 12, 8), BorderColor);
                sb.Draw(BorderTexture, new Rectangle(borderpointTL.X + 8 + BarSizeTop, borderpointTL.Y, 8, 8), new Rectangle(20, 0, 8, 8), BorderColor);
                sb.Draw(BorderTexture, new Rectangle(borderpointTL.X, borderpointTL.Y + 8, 8, BarSizeLeft), new Rectangle(0, 8, 8, 12), BorderColor);
                sb.Draw(BorderTexture, new Rectangle(borderpointTL.X + 8 + BarSizeTop, borderpointTL.Y + 8, 8, BarSizeRight), new Rectangle(20, 8, 8, 12), BorderColor);
                sb.Draw(BorderTexture, new Rectangle(borderpointTL.X, borderpointTL.Y + 8 + BarSizeLeft, 8, 8), new Rectangle(0, 20, 8, 8), BorderColor);
                sb.Draw(BorderTexture, new Rectangle(borderpointTL.X + 8, borderpointTL.Y + 8 + BarSizeLeft, BarSizeBottom, 8), new Rectangle(8, 20, 8, 8), BorderColor);
                sb.Draw(BorderTexture, new Rectangle(borderpointTL.X + 8 + BarSizeBottom, borderpointTL.Y + 8 + BarSizeRight, 8, 8), new Rectangle(20, 20, 8, 8), BorderColor);
            }
            if (UseTitle)
            {
                sb.Draw(ItemModifier.Textures.WindowBackground, new Rectangle(dimensionsPoint.X, dimensionsPoint.Y, (int)Dimensions.Width, TitleBarHeight), KRUtils.UIBackgroundColor);
                Utils.DrawBorderString(sb, Title, new Vector2(Dimensions.X + 2, Dimensions.Y + 1), Color.White);
            }
        }

        public override void LeftMouseDown(UIMouseEventArgs e)
        {
            if (Draggable && e.MousePosition.X == 2 + TitleBarHeight)
            {
                DragPos = new Vector2(e.MousePosition.X - Left.Pixels, e.MousePosition.Y - Top.Pixels);
                Dragging = true;
            }
            base.LeftMouseDown(e);
        }

        public override void LeftMouseUp(UIMouseEventArgs e)
        {
            Dragging = false;
            Recalculate();
            base.LeftMouseUp(e);
        }

        public override void OnInitialize()
        {
            if (hasCloseButton)
            {
                CloseButton.Left = new StyleDimension(-CloseButton.Width.Pixels - 3f, 1f);
                CloseButton.Top = new StyleDimension(UseTitle ? -19f : 2f);
                CloseButton.OnLeftClick += (source, e) => Visible = false;
                CloseButton.OnMouseOver += (source, e) => ItemModifier.Instance.Tooltip = "Close";
                CloseButton.OnMouseOut += (source, e) => ItemModifier.Instance.Tooltip = "";
                CloseButton.Parent = this;
            }
        }

        public override void Recalculate()
        {
            Dimensions ParentInnerDimension = Parent?.InnerDimensions ?? ParentInterface?.Dimensions ?? UserInterface.ActiveInstance.Dimensions;
            var width = MathHelper.Clamp(Width.CalculateValue(ParentInnerDimension.Width), MinWidth.CalculateValue(ParentInnerDimension.Width), MaxWidth.CalculateValue(ParentInnerDimension.Width)) + Padding.X + Padding.Z + Margin.X + Margin.Z + 4; // +4 is border, 2 sides
            var height = MathHelper.Clamp(Height.CalculateValue(ParentInnerDimension.Height), MinHeight.CalculateValue(ParentInnerDimension.Height), MaxHeight.CalculateValue(ParentInnerDimension.Height)) + Padding.Y + Padding.W + Margin.Y + Margin.W + (UseTitle ? 25 : 4); // +4 is border, 2 sides
            OuterDimensions = new Dimensions(Left.CalculateValue(ParentInnerDimension.Width) + ParentInnerDimension.X + ParentInnerDimension.Width * HorizontalAlign - width * HorizontalAlign, Top.CalculateValue(ParentInnerDimension.Height) + ParentInnerDimension.Y + ParentInnerDimension.Height * VerticalAlign - height * VerticalAlign, width, height);
            Dimensions = new Dimensions(OuterDimensions.X + Margin.X + 2, OuterDimensions.Y + Margin.Y + 2, OuterDimensions.Width - Margin.X - Margin.Z - 4, OuterDimensions.Height - Margin.Y - Margin.W - 4);
            if (UseTitle)
            {
                InnerDimensions = new Dimensions(Dimensions.X + Padding.X, Dimensions.Y + Padding.Y + TitleBarHeight, Dimensions.Width - Padding.X - Padding.Z, Dimensions.Height - Padding.Y - Padding.W - TitleBarHeight);
                TitleBarDimensions = new Dimensions(Dimensions.X + Padding.X, Dimensions.Y + Padding.Y, Dimensions.Width - Padding.X - Padding.Z, TitleBarHeight);
            }
            else
            {
                InnerDimensions = new Dimensions(Dimensions.X + Padding.X, Dimensions.Y + Padding.Y, Dimensions.Width - Padding.X - Padding.Z, Dimensions.Height - Padding.Y - Padding.W);
                TitleBarDimensions = null;
            }
            RecalculateChildren();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    public class UIWindow : UIElement
    {
        public string Title { get; protected set; } = "";

        public bool Dragging { get; private set; }

        public bool Draggable { get; set; } = true;

        private Vector2 DragPos;

        public Texture2D BorderTexture { get; set; } = ItemModifier.Textures.WindowBorder;

        public Color BorderColor { get; set; } = Color.Black;

        public Texture2D BackgroundTexture { get; set; } = ItemModifier.Textures.WindowBackground;

        public Color BackgroundColor = KRUtils.UIBackgroundColor;

        public const int TitleBarHeight = 21;

        private bool useTitle = false;

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

        public Dimensions? TitleBarDimensions { get; protected set; }

        private readonly bool hasCloseButton = false;

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
                Left = new StyleDimension(Main.mouseX - DragPos.X, Left.Percent);
                Top = new StyleDimension(Main.mouseY - DragPos.Y, Top.Percent);
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
            if (Draggable && TitleBarDimensions.Value.Rectangle.Contains(e.MousePosition.ToPoint()))
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
                CloseButton.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Close";
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

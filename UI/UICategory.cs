using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static Terraria.Utils;

namespace ItemModifier.UI
{
    public class UICategory
    {
        public class UIProperty : UIElement
        {
            private string label;

            public string Label
            {
                get
                {
                    return label;
                }

                set
                {
                    label = value;
                    RecalculateSelf();
                }
            }

            private UIImage imageLabel;

            public Texture2D ImageLabel
            {
                get
                {
                    return imageLabel.Image;
                }

                set
                {
                    imageLabel.Image = value;
                }
            }

            private UIElement inputElement;

            internal UIElement InputElement
            {
                get
                {
                    return inputElement;
                }

                set
                {
                    inputElement = value;
                    InputElement.XOffset = new SizeDimension(24f + Utils.MeasureString2(Label, true).X);
                    inputElement.Parent = this;
                    RecalculateSelf();
                }
            }

            public Color TextColor { get; set; } = Color.White;

            public UIProperty(Texture2D imageLabel, string label, UIElement inputElement)
            {
                this.imageLabel = new UIImage(imageLabel)
                {
                    Parent = this
                };
                Label = label;
                InputElement = inputElement;
            }

            protected override void DrawSelf(SpriteBatch sb)
            {
                DrawBorderString(sb, Label, new Vector2(InnerX + 20, InnerY), TextColor);
            }

            protected internal override void RecalculateSelf()
            {
                Vector2 labelSize = Utils.MeasureString2(Label, true);
                float titleSize = 20 + labelSize.X;
                if (InputElement == null)
                {
                    Width = new SizeDimension(titleSize);
                    Height = new SizeDimension(labelSize.Y);
                }
                else
                {
                    Width = new SizeDimension(titleSize + 4f + InputElement.OuterWidth);
                    Height = new SizeDimension(labelSize.Y > InputElement.OuterWidth ? labelSize.Y : InputElement.OuterHeight);
                    InputElement.XOffset = new SizeDimension(24f + labelSize.X);
                }
                base.RecalculateSelf();
            }
        }

        public string Name { get; set; }

        public List<UIProperty> Properties { get; }

        public UICategory(string name, List<UIProperty> properties)
        {
            Name = name;
            Properties = properties ?? new List<UIProperty>();
        }

        public void AppendProperties(UIContainer container)
        {
            for (int i = 0; i < Properties.Count; i++)
            {
                Properties[i].Parent = container;
            }
        }
    }
}

using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

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
                    Recalculate();
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
                    InputElement.Left = new StyleDimension(20 + KRUtils.MeasureTextAccurate(Label, true).X + 4f);
                    inputElement.Parent = this;
                    Recalculate();
                }
            }

            public Color TextColor { get; set; } = Color.White;

            public UIProperty(Texture2D imageLabel, string label, UIElement inputElement)
            {
                this.imageLabel = new UIImage(imageLabel);
                Label = label;
                InputElement = inputElement;
            }

            private void RecalculateSize()
            {
                Vector2 labelSize = KRUtils.MeasureTextAccurate(Label, true);
                float titleSize = 20 + labelSize.X;
                if (InputElement == null)
                {
                    Width = new StyleDimension(titleSize);
                    Height = new StyleDimension(labelSize.Y);
                }
                else
                {
                    Width = new StyleDimension(titleSize + 4f + InputElement.Width.Pixels);
                    Height = new StyleDimension(labelSize.Y > InputElement.Height.Pixels ? labelSize.Y : InputElement.Height.Pixels);
                }
            }

            protected override void DrawSelf(SpriteBatch sb)
            {
                Utils.DrawBorderString(sb, Label, new Vector2(Dimensions.Position.X + 20, Dimensions.Position.Y), TextColor);
            }

            public override void Recalculate()
            {
                RecalculateSize();
                base.Recalculate();
            }
        }

        public string Name { get; set; }

        public List<UIProperty> Properties { get; }

        public UICategory(string name, List<UIProperty> properties = new List<UIProperty>)
        {
            Name = name;
            Properties = properties;
        }

        public UIProperty this[int index]
        {
            get
            {
                return Properties[index];
            }
        }

        public int Count
        {
            get
            {
                return Properties.Count;
            }
        }

        public void AppendProperties(UIContainer container)
        {
            float SpaceOccupiedY = 0f;
            for (int i = 0; i < Count; i++)
            {
                UIProperty property = this[i];
                property.Top = new StyleDimension(SpaceOccupiedY);
                property.Parent = container;
                SpaceOccupiedY += property.Height.Pixels + 4f;
            }
        }
    }
}

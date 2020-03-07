using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;

namespace ItemModifier.UI
{
    public class UICategory : UIElement
    {
        public class UIProperty : UIElement
        {
            public string Label
            {
                get => label;

                set
                {
                    label = value;
                    RecalculateSize();
                }
            }

            private string label;

            private UIElement inputElement;

            internal UIElement InputElement
            {
                get => inputElement;

                set
                {
                    inputElement = null;
                    inputElement = value;
                    inputElement.Parent = this;
                    Recalculate();
                }
            }

            public Vector2 NameSize => KRUtils.MeasureTextAccurate(Label, true);

            public Color TextColor { get; set; } = Color.White;

            public UIProperty(string Label)
            {
                this.Label = Label;
            }

            public float RecalculateSize()
            {
                if (InputElement != null)
                {
                    Width = new StyleDimension(NameSize.X + 5f + InputElement.OuterDimensions.Width);
                    InputElement.Left = new StyleDimension(NameSize.X + 5);
                    Height = new StyleDimension(NameSize.Y > InputElement.Height.Pixels ? NameSize.Y : InputElement.Height.Pixels);
                }
                else
                {
                    Width = new StyleDimension(NameSize.X + 5f);
                    Height = new StyleDimension(NameSize.Y);
                }
                return Width.Pixels;
            }

            protected override void DrawSelf(SpriteBatch sb)
            {
                Utils.DrawBorderString(sb, Label, Dimensions.Position, TextColor);
            }

            public override void Recalculate()
            {
                base.Recalculate();
                RecalculateSize();
            }
        }

        public List<UIProperty> Properties = new List<UIProperty>();

        public string Name { get; set; }
        
        public float RowSpacing { get; set; } = 0;
       
        public float ColumnSpacing { get; set; } = 8;

        public UICategory(string Name) => this.Name = Name;

        public void AddProperty(UIProperty Property)
        {
            Properties.Add(Property);
        }

        public void GatherProperties()
        {
            float SpaceOccupiedY = 0f;
            float SpaceOccupiedX = 0f;
            float BiggestInColumnXAxis = 0f;

            for (int i = 0; i < Properties.Count; i++)
            {
                UIProperty property = Properties[i];
                if (SpaceOccupiedY != 0) SpaceOccupiedY += RowSpacing;
                if (SpaceOccupiedY + property.Height.Pixels > Height.Pixels)
                {
                    SpaceOccupiedY = 0;
                    SpaceOccupiedX += BiggestInColumnXAxis + ColumnSpacing;
                    BiggestInColumnXAxis = property.Width.Pixels;
                }
                else if (property.Width.Pixels > BiggestInColumnXAxis)
                {
                    BiggestInColumnXAxis = property.Width.Pixels;
                }
                property.Top = new StyleDimension(SpaceOccupiedY);
                SpaceOccupiedY += property.Height.Pixels < 24 ? 24 : property.Height.Pixels;
                property.Left = new StyleDimension(SpaceOccupiedX);
                property.Visible = true;
                property.Parent = this;
            }
        }

        public void RemoveAllProperties()
        {
            Properties.Clear();
        }
    }
}

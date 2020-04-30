using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

            public SizeDimension ChildXOffset { get; private set; }

            public Color TextColor { get; set; } = Color.White;

            public UIProperty(Texture2D imageLabel, string label, params UIElement[] children)
            {
                this.imageLabel = new UIImage(imageLabel)
                {
                    Parent = this
                };
                Label = label;
                OnChildAdded += (source, child) => child.Target.XOffset = ChildXOffset;
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Parent = this;
                }
            }

            protected override void DrawSelf(SpriteBatch sb)
            {
                DrawBorderString(sb, Label, new Vector2(InnerX + 20, InnerY), TextColor);
            }

            protected internal override void RecalculateSelf()
            {
                Vector2 labelSize = Utils.MeasureString2(Label, true);
                float width = labelSize.X + 20f;
                float height = labelSize.Y;
                ChildXOffset = new SizeDimension(width + 4f);
                float furthest = 0f;
                float deepest = 0f;
                for (int i = 0; i < ChildrenCount; i++)
                {
                    UIElement child = Children[i];
                    float dX = child.XOffset.Pixels + child.OuterWidth;
                    float dY = child.YOffset.Pixels + child.OuterHeight;
                    if (dX > furthest) furthest = dX;
                    if (dY > deepest) deepest = dY;
                }
                width += furthest;
                Width = new SizeDimension(width);
                Height = new SizeDimension(Math.Max(height, deepest));
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

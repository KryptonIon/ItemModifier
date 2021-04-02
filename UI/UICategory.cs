using ItemModifier.UIKit;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ItemModifier.UI
{
    public class UICategory
    {
        public class UIProperty
        {
            internal UIText label;

            public string Label
            {
                get
                {
                    return label.Text;
                }

                set
                {
                    label.Text = value;
                }
            }

            internal UIImage imageLabel;

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

            internal readonly List<UIElement> children = new List<UIElement>();

            public IEnumerable<UIElement> Children
            {
                get
                {
                    for (int i = 0; i < children.Count; i++)
                    {
                        yield return children[i];
                    }
                }
            }

            public UIProperty(Texture2D imageLabel, string label, params UIElement[] children)
            {
                this.imageLabel = new UIImage(imageLabel)
                {
                    AutoScale = false,
                    Width = new SizeDimension(16),
                    Height = new SizeDimension(16)
                };
                this.label = new UIText(label);
                this.children.AddRange(children);
            }

            public void Append(params UIElement[] elements)
            {
                children.AddRange(elements);
            }

            public void AppendTo(UIElement parent)
            {
                label.Parent = parent;
                imageLabel.Parent = parent;
                AppendChildren(parent);
            }

            public void AppendChildren(UIElement parent)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Parent = parent;
                }
            }
        }

        public string Name { get; set; }

        public List<UIProperty> Properties { get; }

        public UICategory(string name, List<UIProperty> properties)
        {
            Name = name;
            Properties = properties ?? new List<UIProperty>();
        }

        public int PropertyDistance { get; set; } = 5;

        public int InputElementDistance { get; set; } = 3;

        public void AppendProperties(UIContainer container)
        {
            // Make sure there isnt distance between
            // top and first element
            float yOffset = PropertyDistance * -1;

            for (int i = 0; i < Properties.Count; i++)
            {
                UIProperty property = Properties[i];
                // Give XY Positioning to image and label
                property.imageLabel.XOffset = SizeDimension.Empty;
                property.imageLabel.YOffset = new SizeDimension(yOffset + PropertyDistance);
                // Force recalculate
                property.imageLabel.Parent = container;
                property.label.XOffset = new SizeDimension(property.imageLabel.OuterWidth + 2);
                property.label.YOffset = property.imageLabel.YOffset;
                // Force recalculate
                property.label.Parent = container;

                yOffset = property.imageLabel.YOffset.Pixels + property.label.OuterHeight;

                // Remove spacing between label and first element
                if (property.children.Count > 0)
                    yOffset -= InputElementDistance;

                // Give XY positioning to children
                for (int j = 0; j < property.children.Count; j++)
                {
                    UIElement child = property.children[j];
                    child.XOffset = property.label.XOffset;
                    child.YOffset = new SizeDimension(yOffset + InputElementDistance);
                    // Force recalculate
                    child.Parent = container;
                    yOffset = child.YOffset.Pixels + child.OuterHeight;
                }
            }
        }
    }
}

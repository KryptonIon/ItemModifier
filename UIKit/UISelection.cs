using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    public class UISelection : UIElement
    {
        public class UISelectionChoice : UIElement, IInput<bool>
        {
            public event UIEventHandler<bool> OnValueChanged;

            public string Label
            {
                get => label;

                set
                {
                    label = value;
                    Recalculate();
                }
            }
            private string label;

            private bool PrivateValue;

            public bool Value
            {
                get => PrivateValue;

                set
                {
                    PrivateValue = value;
                    OnValueChanged?.Invoke(this, PrivateValue);
                }
            }

            internal UISelectionChoice(string Label, Vector4 Margin = default) : base(Margin: Margin) => this.Label = Label;

            public override void LeftClick(UIMouseEventArgs e)
            {
                Main.PlaySound(SoundID.MenuTick);
                base.LeftClick(e);
            }

            public override void MouseOver(UIMouseEventArgs e)
            {
                Main.PlaySound(SoundID.MenuTick);
                base.MouseOver(e);
            }

            public void RecalculateSize()
            {
                Vector2 labelSize = KRUtils.MeasureTextAccurate(Label, false);
                Width = new StyleDimension(14f + labelSize.X);
                Height = new StyleDimension(labelSize.Y);
            }

            public override void Recalculate()
            {
                RecalculateSize();
                base.Recalculate();
            }

            protected override void DrawSelf(SpriteBatch sb)
            {
                sb.Draw(ItemModifier.Textures.CircleSelect, new Vector2(Dimensions.Position.X, Dimensions.Position.Y + 5), new Rectangle(Value ? 12 : 0, 0, 10, 10), Color.White);
                Utils.DrawBorderString(sb, Label, new Vector2(Dimensions.X + 14, Dimensions.Y), Color.White);
            }
        }

        public event UIEventHandler<UISelectionChoice> OnSelectedChanged;

        public List<UISelectionChoice> Choices { get; private set; } = new List<UISelectionChoice>();

        public bool AllowMultipleSelection { get; set; }

        public UISelectionChoice Selected
        {
            get
            {
                for (int i = 0; i < Choices.Count; i++)
                {
                    if (Choices[i].Value)
                    {
                        return Choices[i];
                    }
                }
                return null;
            }
        }

        public List<UISelectionChoice> AllSelected
        {
            get
            {
                List<UISelectionChoice> list = new List<UISelectionChoice>();
                for (int i = 0; i < Choices.Count; i++)
                {
                    if (Choices[i].Value)
                    {
                        list.Add(Choices[i]);
                    }
                }
                return list;
            }
        }

        public UISelection(Vector4 Margin = default, params string[] Labels) : base(Margin: Margin)
        {
            AddChoices(Labels);
        }

        public UISelectionChoice AddChoice(string Label)
        {
            UISelectionChoice newChoice = new UISelectionChoice(Label);
            newChoice.OnLeftClick += (source, e) => Select(newChoice);
            newChoice.Top = new StyleDimension(21 * Choices.Count);
            Height = new StyleDimension(Height.Pixels + newChoice.Height.Pixels, Height.Percent);
            if (newChoice.Width.Pixels > Width.Pixels) Width = new StyleDimension(newChoice.Width.Pixels, Width.Percent);
            Choices.Add(newChoice);
            newChoice.Parent = this;
            return newChoice;
        }

        public List<UISelectionChoice> AddChoices(params string[] Labels)
        {
            List<UISelectionChoice> list = new List<UISelectionChoice>();
            for (int i = 0; i < Labels.Length; i++)
            {
                list.Add(AddChoice(Labels[i]));
            }
            return list;
        }

        public void Select(UISelectionChoice choice)
        {
            if (!AllowMultipleSelection)
            {
                List<UISelectionChoice> allSelected = AllSelected;
                for (int i = 0; i < allSelected.Count; i++)
                {
                    if (allSelected[i] != choice)
                    {
                        allSelected[i].Value = false;
                    }
                }
            }
            choice.Value = true;
            OnSelectedChanged?.Invoke(this, choice);
        }

        public void Select(int index)
        {
            Select(Choices[index]);
        }

        public void DeselectAll()
        {
            for (int i = 0; i < Choices.Count; i++)
            {
                Choices[i].Value = false;
            }
        }
    }
}

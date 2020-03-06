using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for multiple choice selection.
    /// </summary>
    public class UISelection : UIElement
    {
        /// <summary>
        /// Choices for <see cref="UISelection"/>.
        /// </summary>
        public class UISelectionChoice : UIElement, IInput<bool>
        {
            /// <summary>
            /// Fired when <see cref="Value"/> is changed.
            /// </summary>
            public event UIEventHandler<bool> OnValueChanged;

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

            private bool PrivateValue;

            /// <summary>
            /// True if selected, false otherwise.
            /// </summary>
            public bool Value
            {
                get => PrivateValue;

                set
                {
                    PrivateValue = value;
                    OnValueChanged?.Invoke(this, PrivateValue);
                }
            }

            /// <summary>
            /// Initializes a new <see cref="UISelectionChoice"/> Element.
            /// </summary>
            /// <param name="Label">Label of the choice.</param>
            /// <param name="Padding">Add space inside the element.</param>
            /// <param name="Margin">Add space around the element.</param>
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

            /// <summary>
            /// Recalculates Width and Height.
            /// </summary>
            public void RecalculateSize()
            {
                Vector2 labelSize = KRUtils.MeasureTextAccurate(Label, true);
                Width = new StyleDimension(14f + labelSize.X);
                Height = new StyleDimension(labelSize.Y);
            }

            public override void Recalculate()
            {
                base.Recalculate();
                RecalculateSize();
            }

            protected override void DrawSelf(SpriteBatch sb)
            {
                sb.Draw(ItemModifier.Textures.CircleSelect, new Vector2(Dimensions.Position.X, Dimensions.Position.Y + 5), new Rectangle(Value ? 12 : 0, 0, 10, 10), Color.White);
                Utils.DrawBorderString(sb, Label, new Vector2(Dimensions.X + 14, Dimensions.Y), Color.White);
            }
        }

        /// <summary>
        /// Fired when selected choice(s) is/are changed.
        /// </summary>
        public event UIEventHandler<UISelectionChoice> OnSelectedChanged;

        /// <summary>
        /// Choices of <see cref="UISelection"/>.
        /// </summary>
        public List<UISelectionChoice> Choices { get; private set; } = new List<UISelectionChoice>();

        /// <summary>
        /// If true, multiple choices can be selected, if false, only one may be selected.
        /// </summary>
        public bool AllowMultipleSelection { get; set; }

        /// <summary>
        /// Gets selected choice; First selected choice if <see cref="AllowMultipleSelection"/> is enabled.
        /// </summary>
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

        /// <summary>
        /// Gets a list of selected choices.
        /// </summary>
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

        /// <summary>
        /// Initializes a new <see cref="UISelection"/> Element.
        /// </summary>
        /// <param name="Margin">Add space around the element.</param>
        /// <param name="Labels">Labels for choices.</param>
        public UISelection(Vector4 Margin = default, params string[] Labels) : base(Margin: Margin)
        {
            AddChoices(Labels);
        }

        /// <summary>
        /// Adds a new choice.
        /// </summary>
        /// <param name="Label">Label for new choice.</param>
        /// <returns></returns>
        public UISelectionChoice AddChoice(string Label)
        {
            UISelectionChoice newChoice = new UISelectionChoice(Label);
            newChoice.OnLeftClick += (source, e) => Select(newChoice);
            newChoice.Top = new StyleDimension(21 * Choices.Count);
            Height = new StyleDimension(newChoice.Height.Pixels, Height.Percent);
            if (newChoice.Width.Pixels > Width.Pixels) Width = new StyleDimension(newChoice.Width.Pixels, Width.Percent);
            Choices.Add(newChoice);
            newChoice.Parent = this;
            return newChoice;
        }

        /// <summary>
        /// Adds new choices.
        /// </summary>
        /// <param name="Labels">Labels for new choices.</param>
        /// <returns></returns>
        public List<UISelectionChoice> AddChoices(params string[] Labels)
        {
            List<UISelectionChoice> list = new List<UISelectionChoice>();
            for (int i = 0; i < Labels.Length; i++)
            {
                list.Add(AddChoice(Labels[i]));
            }
            return list;
        }

        /// <summary>
        /// Selects a choice.
        /// </summary>
        /// <param name="choice">A <see cref="UISelectionChoice"/> Element</param>
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

        /// <summary>
        /// Selects a choice based on their index.
        /// </summary>
        /// <param name="index">Starts at 0. Index of choices(top to bottom).</param>
        public void Select(int index)
        {
            Select(Choices[index]);
        }

        /// <summary>
        /// Deselects all choices.
        /// </summary>
        public void DeselectAll()
        {
            for (int i = 0; i < Choices.Count; i++)
            {
                Choices[i].Value = false;
            }
        }
    }
}

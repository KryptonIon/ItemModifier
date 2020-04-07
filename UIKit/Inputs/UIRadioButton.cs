﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using static Terraria.Utils;
using static ItemModifier.UIKit.Utils;

namespace ItemModifier.UIKit.Inputs
{
    public class UIRadioButton : UIElement, IInput<bool>
    {
        public event UIEventHandler<bool> OnValueChanged;

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

        public Color LabelColor { get; set; } = Color.White;

        private bool selected;

        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnValueChanged?.Invoke(this, Selected);
                }
            }
        }

        bool IInput<bool>.Value
        {
            get
            {
                return Selected;
            }

            set
            {
                Selected = value;
            }
        }

        public bool SkipDescenderCheck { get; set; }

        public UIRadioButton(string label) : base()
        {
            Label = label;
        }

        public override void LeftClick(UIMouseEventArgs e)
        {
            Parent?.SelectRadio(this);
            Selected = !Selected;
            Main.PlaySound(SoundID.MenuTick);
            base.LeftClick(e);
        }

        public override void MouseOver(UIMouseEventArgs e)
        {
            Main.PlaySound(SoundID.MenuTick);
            base.MouseOver(e);
        }

        protected internal override void RecalculateSelf()
        {
            RecalculateButtonSize();
            base.RecalculateSelf();
        }

        protected virtual void RecalculateButtonSize()
        {
            Vector2 size = MeasureString2(Label, SkipDescenderCheck);
            Width = new SizeDimension(size.X + 14f);
            Height = new SizeDimension(size.Y);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            sb.Draw(ItemModifier.Textures.SquareSelect, new Vector2(InnerX, InnerY + 5), new Rectangle(Selected ? 12 : 0, 0, 10, 10), Color.White);
            DrawBorderString(sb, Label, new Vector2(InnerX + 14f, InnerY), LabelColor);
        }
    }
}
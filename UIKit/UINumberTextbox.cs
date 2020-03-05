using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element that is the backbone of number textboxes.
    /// </summary>
    public abstract class UINumberTextbox<T> : UITextbox, IInput<T> where T : struct
    {
        /// <summary>
        /// Fired when <see cref="Value"/> is changed.
        /// </summary>
        public event UIEventHandler<T> OnValueChanged;

        /// <summary>
        /// Value of this input.
        /// </summary>
        public abstract T Value { get; set; }

        /// <summary>
        /// If true, + or - will display before the value.
        /// </summary>
        public bool Sign { get; set; } = true;

        /// <summary>
        /// If true, value can be negated else value will stay positive.
        /// </summary>
        public bool Negatable { get; set; } = true;

        /// <summary>
        /// If true, + will be displayed, - otherwise.
        /// </summary>
        public bool Positive { get; set; } = true;

        /// <summary>
        /// Color of the + sign.
        /// </summary>
        public Color PositiveColor { get; set; } = new Color(0, 255, 0);

        /// <summary>
        /// Color of the - sign.
        /// </summary>
        public Color NegativeColor { get; set; } = Color.Red;

        /// <summary>
        /// Initializes a new <see cref="UINumberTextbox"/> Element.
        /// </summary>
        /// <param name="Margin">Add space around the element.</param>
        public UINumberTextbox(Vector4 Margin = default) : base(Margin)
        {
            Text = "0";
            OnFocusChanged += (source, value) => { if (!value) RecalculateValue(); };
        }

        /// <summary>
        /// Initializes a new <see cref="UINumberTextbox"/> Element.
        /// </summary>
        /// <param name="CharacterLimit">Maximum characters textbox can hold.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UINumberTextbox(uint CharacterLimit, Vector4 Margin = default) : base(CharacterLimit, Margin)
        {
            Text = "0";
            OnFocusChanged += (source, value) => { if (!value) RecalculateValue(); };
        }

        /// <summary>
        /// Time till value increases.
        /// </summary>
        protected int ValIncHoldDelta { get; set; }

        private int valIncHoldDeltaThres;

        /// <summary>
        /// Maximum time till value increases.
        /// </summary>
        protected int ValIncHoldDeltaThres
        {
            get => valIncHoldDeltaThres;
            set
            {
                valIncHoldDeltaThres = value < 5 ? 5 : value;
            }
        }

        public override void Recalculate()
        {
            base.Recalculate();
            RecalculateValue();
            UpdateText();
        }

        /// <summary>
        /// Recalculate value of input.
        /// </summary>
        public abstract void RecalculateValue();

        /// <summary>
        /// Sync value with text.
        /// </summary>
        public virtual void UpdateText()
        {
            if (Sign) Text = Value.ToString().Replace("-", "");
            else Text = Value.ToString();
        }

        protected override void TextChanged(Tuple<string, int> newText)
        {
            int newCaretPos = newText.Item2;
            Text = "";
            if (string.IsNullOrEmpty(newText.Item1))
            {
                Text = "0";
                return;
            }
            if (newText.Item1[0] != '0') Text += newText.Item1[0];
            for (int i = 1; i < newText.Item1.Length; i++)
            {
                if (char.IsDigit(newText.Item1[i])) Text += newText.Item1[i];
                else newCaretPos--;
            }
            if (string.IsNullOrEmpty(Text)) Text = "0";
            CaretPosition = newCaretPos;
        }

        protected override Vector3 DrawText(SpriteBatch sb)
        {
            Vector2 pos = Dimensions.Position;
            float maxWidth = Width.Pixels;
            if (Box)
            {
                pos.X += 4;
                maxWidth -= 8;
            }
            if (Sign)
            {
                float width = Utils.DrawBorderString(sb, Positive ? "+" : "-", pos, Positive ? PositiveColor : NegativeColor).X;
                pos.X += width;
                maxWidth -= width;
            }
            return new Vector3(pos.X, pos.Y, Utils.DrawBorderString(sb, KRUtils.TrimTextReverse(Text, maxWidth, Main.fontMouseText), pos, TextColor).X);
        }

        protected override void CheckKeys()
        {
            base.CheckKeys();
            if (Sign)
            {
                if (KRUtils.IsAnyKeyPressed(Main.oldKeyState, Main.keyState, Keys.OemMinus, Keys.Subtract)) Positive = false;
                if (KRUtils.IsAnyKeyPressed(Main.oldKeyState, Main.keyState, Keys.OemPlus, Keys.Add)) Positive = true;
            }
        }

        protected void ValueChanged(T value)
        {
            OnValueChanged?.Invoke(this, value);
        }
    }
}

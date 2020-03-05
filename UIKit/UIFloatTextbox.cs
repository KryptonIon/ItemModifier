using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for decimal input.
    /// </summary>
    public class UIFloatTextbox : UINumberTextbox<float>
    {
        private float PrivateValue;

        /// <summary>
        /// Value of this input.
        /// </summary>
        public override float Value
        {
            get => PrivateValue;

            set
            {
                if (PrivateValue != value)
                {
                    PrivateValue = MaxThresholdEnabled && value > MaxThreshold ? MaxThreshold : MinThresholdEnabled && value < MinThreshold ? MinThreshold : value;
                    Positive = Value > -1;
                    UpdateText();
                    ValueChanged(PrivateValue);
                }
            }
        }

        /// <summary>
        /// Maximum <see cref="Value"/> can be.
        /// </summary>
        public float MaxThreshold { get; set; }

        /// <summary>
        /// If true, <see cref="Value"/> will be limited by <see cref="MaxThreshold"/>.
        /// </summary>
        public bool MaxThresholdEnabled { get; set; }

        /// <summary>
        /// Minimum <see cref="Value"/> can be.
        /// </summary>
        public float MinThreshold { get; set; }

        /// <summary>
        /// If true, <see cref="Value"/> will be limited by <see cref="MinThreshold"/>.
        /// </summary>
        public bool MinThresholdEnabled { get; set; }

        /// <summary>
        /// Initializes a new <see cref="UIFloatTextbox"/> Element.
        /// </summary>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIFloatTextbox(Vector4 Margin = default) : base(39, Margin)
        {

        }

        /// <summary>
        /// Initializes a new <see cref="UIFloatTextbox"/> Element.
        /// </summary>
        /// <param name="Min">Minimum value <see cref="Value"/> should be.</param>
        /// <param name="Max">Maximum Value <see cref="Value"/> should be.</param>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIFloatTextbox(float Min, float Max, Vector4 Margin = default) : this(Margin) => (MaxThresholdEnabled, MaxThreshold, MinThresholdEnabled, MinThreshold) = (true, Max, true, Min);

        public override void RecalculateValue()
        {
            try
            {
                float numberparsed = float.Parse(Text);
                if (numberparsed > int.MaxValue)
                {
                    Value = int.MaxValue;
                }
                else if (numberparsed < int.MinValue)
                {
                    Value = int.MinValue;
                }
                else
                {
                    if (Negatable)
                    {
                        Value = Positive ? Math.Abs(numberparsed) : -Math.Abs(numberparsed);
                    }
                    else
                    {
                        Value = Math.Abs(numberparsed);
                    }
                }
            }
            catch (Exception)
            {
                Value = Positive ? int.MaxValue : int.MinValue;
            }
        }

        public override void UpdateText()
        {
            Text = Value.ToStringLong().Replace("-", "");
        }

        protected override void TextChanged(Tuple<string, int> newText)
        {
            bool hasDot = false;
            int newCaretPos = newText.Item2;
            Text = "";
            if (string.IsNullOrEmpty(newText.Item1))
            {
                Text = "0";
                return;
            }
            if (newText.Item1[0] != '0' || newText.Item1.Length > 1 && newText.Item1[1] == '.') Text += newText.Item1[0];
            for (int i = 1; i < newText.Item1.Length; i++)
            {
                if (char.IsDigit(newText.Item1[i]))
                {
                    Text += newText.Item1[i];
                }
                else if (newText.Item1[i] == '.' && !hasDot)
                {
                    Text += ".";
                    hasDot = true;
                }
                else
                {
                    newCaretPos--;
                }
            }
            if (string.IsNullOrEmpty(Text)) Text = "0";
            CaretPosition = newCaretPos;
        }

        protected override void CheckKeys()
        {
            base.CheckKeys();
            if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.Up)) Value++;
            if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.Down)) Value--;
            if (Main.keyState.IsKeyDown(Keys.Up))
            {
                if (ValIncHoldDelta > ValIncHoldDeltaThres)
                {
                    ValIncHoldDelta = 0;
                    Value++;
                    ValIncHoldDeltaThres -= 3;
                }
                else
                {
                    ValIncHoldDelta++;
                }
            }
            else if (Main.keyState.IsKeyDown(Keys.Down))
            {
                if (ValIncHoldDelta > ValIncHoldDeltaThres)
                {
                    ValIncHoldDelta = 0;
                    Value--;
                    ValIncHoldDeltaThres -= 3;
                }
                else
                {
                    ValIncHoldDelta++;
                }
            }
            else
            {
                ValIncHoldDeltaThres = 19;
            }
        }
    }
}

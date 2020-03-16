using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;

namespace ItemModifier.UIKit
{
    public class UIFloatTextbox : UINumberTextbox<float>
    {
        private float PrivateValue;

        public override float Value
        {
            get
            {
                return PrivateValue;
            }

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

        public float MaxThreshold { get; set; }

        public bool MaxThresholdEnabled { get; set; }

        public float MinThreshold { get; set; }

        public bool MinThresholdEnabled { get; set; }

        public UIFloatTextbox(Vector4 margin = default) : base(39, margin)
        {

        }

        public UIFloatTextbox(float min, float max, Vector4 margin = default) : this(margin)
        {
            MinThresholdEnabled = true;
            MinThreshold = min;
            MaxThresholdEnabled = true;
            MaxThreshold = max;
        }

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
                    if (Negatable) Value = Positive ? Math.Abs(numberparsed) : -Math.Abs(numberparsed);
                    else Value = Math.Abs(numberparsed);
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

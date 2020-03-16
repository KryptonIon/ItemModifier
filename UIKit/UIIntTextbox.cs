using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;

namespace ItemModifier.UIKit
{
    public class UIIntTextbox : UINumberTextbox<int>
    {
        private int PrivateValue;

        public override int Value
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

        public int MaxThreshold { get; set; }

        public bool MaxThresholdEnabled { get; set; }

        public int MinThreshold { get; set; }

        public bool MinThresholdEnabled { get; set; }

        public UIIntTextbox(Vector4 Margin = default) : base(10, Margin)
        {

        }

        public UIIntTextbox(int Min, int Max, Vector4 Margin = default) : this(Margin)
        {
            MinThresholdEnabled = true;
            MinThreshold = Min;
            MaxThresholdEnabled = true;
            MaxThreshold = Max;
        }

        public override void RecalculateValue()
        {
            try
            {
                int numberparsed = int.Parse(Text);
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
            catch (OverflowException)
            {
                Value = Positive ? int.MaxValue : int.MinValue;
            }
            catch (FormatException)
            {
                Value = 0;
            }
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
                ValIncHoldDelta = 0;
                ValIncHoldDeltaThres = 19;
            }
        }
    }
}

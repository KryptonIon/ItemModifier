using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using Terraria;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Element for integer input.
    /// </summary>
    public class UIIntTextbox : UINumberTextbox<int>
    {
        private int PrivateValue;

        /// <summary>
        /// Value of this input.
        /// </summary>
        public override int Value
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
        public int MaxThreshold { get; set; }

        /// <summary>
        /// If true, <see cref="Value"/> will be limited by <see cref="MaxThreshold"/>.
        /// </summary>
        public bool MaxThresholdEnabled { get; set; }

        /// <summary>
        /// Minimum <see cref="Value"/> can be.
        /// </summary>
        public int MinThreshold { get; set; }

        /// <summary>
        /// If true, <see cref="Value"/> will be limited by <see cref="MinThreshold"/>.
        /// </summary>
        public bool MinThresholdEnabled { get; set; }

        /// <summary>
        /// Initializes a new <see cref="UIIntTextbox"/> Element.
        /// </summary>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIIntTextbox(Vector4 Margin = default) : base(10, Margin)
        {

        }

        /// <summary>
        /// Initializes a new <see cref="UIIntTextbox"/> Element.
        /// </summary>
        /// <param name="Min">Minimum value <see cref="Value"/> should be.</param>
        /// <param name="Max">Maximum Value <see cref="Value"/> should be.</param>
        /// <param name="Padding">Add space inside the element.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UIIntTextbox(int Min, int Max, Vector4 Margin = default) : this(Margin) => (MaxThresholdEnabled, MaxThreshold, MinThresholdEnabled, MinThreshold) = (true, Max, true, Min);

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

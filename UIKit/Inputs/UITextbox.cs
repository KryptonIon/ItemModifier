using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using static ItemModifier.ItemModifier;
using static ItemModifier.UIKit.Utils;

namespace ItemModifier.UIKit.Inputs
{
    public class UITextbox : UIElement, IInput<string>
    {
        public event UIEventHandler<EventArgs<string>> OnTextChanged;

        event UIEventHandler<EventArgs<string>> IInput<string>.OnValueChanged
        {
            add
            {
                OnTextChanged += value;
            }

            remove
            {
                OnTextChanged -= value;
            }
        }

        protected Texture2D TextboxTexture { get; set; } = Textures.WhiteDot;

        public Color TextColor { get; set; } = Color.Black;

        public Color BackgroundColor { get; set; } = UIContainer.UIBackgroundColor;

        public Color BorderColor { get; set; } = Color.Black;

        public int BorderSize { get; set; } = 2;

        public Color CaretColor { get; set; } = Color.Black;

        public DynamicSpriteFont Font { get; set; } = Main.fontMouseText;

        public int CharacterLimit { get; set; } = int.MaxValue;

        protected virtual Rectangle ScissorRectangle
        {
            get
            {
                Vector2 textPosition = TextPosition;
                return new Rectangle((int)textPosition.X, (int)textPosition.Y, (int)InnerWidth - 2, (int)InnerHeight);
            }
        }

        protected virtual Vector2 TextPosition
        {
            get
            {
                return new Vector2(InnerX + 2f, InnerY);
            }
        }

        private string text = string.Empty;

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                if (text != value)
                {
                    if (value.Length > CharacterLimit)
                    {
                        text = value.Substring(0, CharacterLimit);
                    }
                    else
                    {
                        text = value;
                    }

                    if (CaretPosition > text.Length)
                    {
                        CaretPosition = text.Length;
                    }

                    OnTextChanged?.Invoke(this, new EventArgs<string>(Text));
                }
            }
        }

        string IInput<string>.Value
        {
            get
            {
                return Text;
            }

            set
            {
                Text = value;
            }
        }

        public int CaretPosition { get; set; }

        private int caretDelta;

        private int caretMoveDelta;

        public UITextbox() : base()
        {
            Width = new SizeDimension(100f);
            Height = new SizeDimension(22f);
            OnUnfocused += (source) =>
            {
                CaretPosition = Text.Length;
                caretDelta = 0;
                caretMoveDelta = 0;
            };
        }

        public UITextbox(int characterLimit) : this()
        {
            CharacterLimit = characterLimit;
        }

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

        protected internal override void RecalculateSelf()
        {
            base.RecalculateSelf();
            if (BorderSize > 0)
            {
                OuterWidth += BorderSize + BorderSize;
                OuterHeight += BorderSize + BorderSize;
                PadWidth += BorderSize + BorderSize;
                PadHeight += BorderSize + BorderSize;
                InnerX += BorderSize;
                InnerY += BorderSize;
            }
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (Focused)
            {
                PlayerInput.WritingText = true;
                Main.instance.HandleIME();
                /*
                 * Title: Magic Storage
                 * Author: blushiemagic | Kaylee Minsuh Kim
                 * Date: October 25 2019
                 * Code version: v0.4.3.5
                 * Availability: https://github.com/blushiemagic/MagicStorage
                 * Based on: https://uark.libguides.com/CSCE/CitingCode
                 */
                string frontHalf = Text.Substring(0, CaretPosition);
                string newText = Main.GetInputText(frontHalf);
                // Handle if there is input
                if (frontHalf != newText)
                {
                    newText = ProcessInput(newText);
                    
                    CaretPosition = newText.Length;
                    
                    // Append back half if Caret isn't at the end
                    if (frontHalf.Length != Text.Length)
                    {
                        newText += Text.Substring(frontHalf.Length);
                    }

                    Text = newText;
                }
                if (Main.keyState.IsKeyDown(Keys.Left))
                {
                    if (Main.oldKeyState.IsKeyUp(Keys.Left))
                    {
                        if (--CaretPosition < 0)
                        {
                            CaretPosition = 0;
                        }

                        caretMoveDelta = 20;
                    }
                    if (--caretMoveDelta == 0)
                    {
                        caretMoveDelta = 3;
                        if (--CaretPosition < 0)
                        {
                            CaretPosition = 0;
                        }
                    }
                    caretDelta = 0;
                }
                else if (Main.keyState.IsKeyDown(Keys.Right))
                {
                    if (Main.oldKeyState.IsKeyUp(Keys.Right))
                    {
                        if (++CaretPosition > Text.Length)
                        {
                            CaretPosition = Text.Length;
                        }

                        caretMoveDelta = 20;
                    }
                    if (--caretMoveDelta == 0)
                    {
                        caretMoveDelta = 3;
                        if (++CaretPosition > Text.Length)
                        {
                            CaretPosition = Text.Length;
                        }
                    }
                    caretDelta = 0;
                }
            }

            base.DrawSelf(sb);
            int padX = (int)PadX;
            int padY = (int)PadY;
            int padWidth = (int)PadWidth;
            int padHeight = (int)PadHeight;
            if (BorderSize > 0)
            {
                int verticalPosition = padY + BorderSize;
                int verticalLength = padHeight - BorderSize - BorderSize;
                sb.Draw(Textures.WhiteDot, new Rectangle(padX, padY, padWidth, BorderSize), BorderColor);
                sb.Draw(Textures.WhiteDot, new Rectangle(padX, verticalPosition, BorderSize, verticalLength), BorderColor);
                sb.Draw(Textures.WhiteDot, new Rectangle((int)(PadX + PadWidth) - BorderSize, verticalPosition, BorderSize, verticalLength), BorderColor);
                sb.Draw(Textures.WhiteDot, new Rectangle(padX, (int)(PadY + PadHeight) - BorderSize, padWidth, BorderSize), BorderColor);
            }
            sb.Draw(TextboxTexture, InnerRect, BackgroundColor);
            DrawText(sb);
        }

        protected virtual void DrawText(SpriteBatch sb)
        {
            Rectangle originalScissorRect = sb.GraphicsDevice.ScissorRectangle;
            sb.End();
            sb.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(GetClippingRectangle(sb, ScissorRectangle), originalScissorRect);
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
            Vector2 textPos = TextPosition;
            sb.DrawString(Font, Text, textPos, TextColor);
            sb.End();
            sb.GraphicsDevice.ScissorRectangle = originalScissorRect;
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, sb.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
            if (Focused)
            {
                if (caretDelta < 20)
                {
                    sb.Draw(Textures.WhiteDot, new Rectangle((int)(textPos.X + Font.MeasureString(Text.Substring(0, CaretPosition)).X), (int)textPos.Y, 2, 20), CaretColor);
                }
                if (++caretDelta > 39)
                {
                    caretDelta = 0;
                }
            }
        }

        protected virtual string ProcessInput(string input)
        {
            return input;
        }

        public override void MiddleClick(UIMouseEventArgs e)
        {
            base.MiddleClick(e);
            Focused = false;
        }

        public override void RightClick(UIMouseEventArgs e)
        {
            base.RightClick(e);
            Focused = false;
        }

        public override void BackClick(UIMouseEventArgs e)
        {
            base.BackClick(e);
            Focused = false;
        }

        public override void ForwardClick(UIMouseEventArgs e)
        {
            base.ForwardClick(e);
            Focused = false;
        }
    }
}

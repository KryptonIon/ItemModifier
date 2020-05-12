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

        public event UIEventHandler<EventArgs<string>> OnTextChangedByUser;

        protected Texture2D TextboxTexture { get; set; } = Textures.WhiteDot;

        public Color TextColor { get; set; } = Color.Black;

        public Color BackgroundColor { get; set; } = UIBackgroundColor;

        public Color BorderColor { get; set; } = Color.Black;

        public int BorderSize { get; set; } = 2;

        public Color CaretColor { get; set; } = Color.Black;

        public DynamicSpriteFont Font { get; set; } = Main.fontMouseText;

        public int CharacterLimit { get; set; }

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

        protected string RawText
        {
            get
            {
                return text;
            }

            set
            {
                if (CharacterLimit > 0 && value.Length > CharacterLimit)
                {
                    text = value.Substring(0, CharacterLimit);
                }
                else
                {
                    text = value;
                }
            }
        }

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
                    RawText = value;
                    CaretPosition = RawText.Length;
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
            base.MouseHover(e);
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
            int padX = (int)PadX;
            int padY = (int)PadY;
            int padWidth = (int)PadWidth;
            int padHeight = (int)PadHeight;
            if (BorderSize > 0)
            {
                int verticalPosition = padY + BorderSize;
                int verticalLength = padHeight - BorderSize - BorderSize;
                sb.Draw(Textures.BlackDot, new Rectangle(padX, padY, padWidth, BorderSize), BorderColor);
                sb.Draw(Textures.BlackDot, new Rectangle(padX, verticalPosition, BorderSize, verticalLength), BorderColor);
                sb.Draw(Textures.BlackDot, new Rectangle((int)(PadX + PadWidth) - BorderSize, verticalPosition, BorderSize, verticalLength), BorderColor);
                sb.Draw(Textures.BlackDot, new Rectangle(padX, (int)(PadY + PadHeight) - BorderSize, padWidth, BorderSize), BorderColor);
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
                    sb.Draw(Textures.Caret, new Vector2(textPos.X + Font.MeasureString(Text.Substring(0, CaretPosition)).X, textPos.Y), CaretColor);
                }
                if (++caretDelta > 39)
                {
                    caretDelta = 0;
                }
            }
        }

        protected override void PostUpdateInputSelf()
        {
            if (Focused)
            {
                PlayerInput.WritingText = true;
                Main.instance.HandleIME();
                // Thanks to Magic Storage
                string frontHalf = Text.Substring(0, CaretPosition);
                string newText = Main.GetInputText(frontHalf);
                if (frontHalf != newText)
                {
                    if (frontHalf.Length != Text.Length)
                    {
                        string backHalf = Text.Substring(CaretPosition);
                        CaretPosition = newText.Length;
                        RawText = newText + backHalf;
                    }
                    else
                    {
                        CaretPosition = newText.Length;
                        Text = newText;
                    }
                    OnTextChangedByUser?.Invoke(this, new EventArgs<string>(Text));
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

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
        public event UIEventHandler<string> OnTextChanged;

        event UIEventHandler<string> IInput<string>.OnValueChanged
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

        public event UIEventHandler<string> OnTextChangedByUser;

        public Color TextColor { get; set; } = Color.Black;

        public Color BackgroundColor { get; set; } = UIBackgroundColor;

        public Color BorderColor { get; set; } = Color.Black;

        public int BorderSize { get; set; } = 2;

        public Color CaretColor { get; set; } = Color.Black;

        public DynamicSpriteFont Font { get; set; } = Main.fontMouseText;

        public int CharacterLimit { get; set; }

        private string text = string.Empty;

        protected string DrawText
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
                    CaretPosition = CharacterLimit;
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
                    DrawText = value;
                    CaretPosition = DrawText.Length;
                    OnTextChanged?.Invoke(this, Text);
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
            OnUnfocused += (source) => caretDelta = 0;
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
            sb.Draw(Textures.WhiteDot, new Rectangle(padX + BorderSize, padY + BorderSize, padWidth - BorderSize - BorderSize, padHeight - BorderSize - BorderSize), BackgroundColor);
            Rectangle scissorRect = sb.GraphicsDevice.ScissorRectangle;
            Rectangle innerRect = new Rectangle((int)InnerX + 2, (int)InnerY, (int)InnerWidth - 2, (int)InnerHeight);
            sb.End();
            sb.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(GetClippingRectangle(sb, innerRect), scissorRect);
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
            sb.DrawString(Font, Text, new Vector2(innerRect.X, innerRect.Y), TextColor);
            sb.End();
            sb.GraphicsDevice.ScissorRectangle = scissorRect;
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, sb.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
            if (Focused)
            {
                if (caretDelta < 20)
                {
                    float textSizeX = Font.MeasureString(Text.Substring(0, CaretPosition)).X;
                    sb.Draw(Textures.Caret, new Vector2(innerRect.X + textSizeX, InnerY + 1), CaretColor);
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
                    if (frontHalf != Text)
                    {
                        int caretPos = CaretPosition;
                        CaretPosition = newText.Length;
                        Text = newText + Text.Substring(caretPos);
                    }
                    else
                    {
                        CaretPosition = newText.Length;
                        Text = newText;
                    }
                    OnTextChangedByUser?.Invoke(this, Text);
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
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using static ItemModifier.ItemModifier;
using static Terraria.Utils;

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

        public Color TextColor { get; set; } = Color.White;

        public Color BackgroundColor { get; set; } = Utils.UIBackgroundColor;

        public Color BorderColor { get; set; } = Color.Black;

        public int BorderSize { get; set; } = 2;

        public Color CaretColor { get; set; } = Color.Black;

        public int CharacterLimit { get; set; } = 10;

        protected string text = string.Empty;

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
                    text = value ?? string.Empty;
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

        protected int CaretDelta { get; set; }

        public UITextbox() : base()
        {
            Width = new SizeDimension(100f);
            Height = new SizeDimension(22f);
            OnFocusChanged += (source, e) => { if (!e) CaretDelta = 0; };
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
            OuterWidth += BorderSize + BorderSize;
            OuterHeight += BorderSize + BorderSize;
            PadWidth += BorderSize + BorderSize;
            PadHeight += BorderSize + BorderSize;
            InnerX += BorderSize;
            InnerY += BorderSize;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (BorderSize > 0)
            {
                int padX = (int)PadX;
                int padY = (int)PadY;
                int padWidth = (int)PadWidth;
                int padHeight = (int)PadHeight;
                int verticalPosition = padY + BorderSize;
                int verticalLength = padHeight - BorderSize - BorderSize;
                sb.Draw(Textures.Textbox, new Rectangle(padX, padY, padWidth, BorderSize), new Rectangle(0, 0, 100, 2), BorderColor);
                sb.Draw(Textures.Textbox, new Rectangle(padX, verticalPosition, BorderSize, verticalLength), new Rectangle(0, 2, 2, 18), BorderColor);
                sb.Draw(Textures.Textbox, new Rectangle((int)(PadX + PadWidth) - BorderSize, verticalPosition, BorderSize, verticalLength), new Rectangle(98, 2, 2, 18), BorderColor);
                sb.Draw(Textures.Textbox, new Rectangle(padX, (int)(PadY + PadHeight) - BorderSize, padWidth, BorderSize), new Rectangle(0, 20, 100, 2), BorderColor);
                sb.Draw(Textures.Textbox, new Rectangle(padX + 2, padY + 2, padWidth - 4, padHeight - 4), new Rectangle(2, 2, 96, 18), BackgroundColor);
            }
            Rectangle scissorRect = sb.GraphicsDevice.ScissorRectangle;
            Rectangle innerRect = new Rectangle((int)InnerX + 2, (int)InnerY, (int)InnerWidth - 2, (int)InnerHeight);
            sb.End();
            sb.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(Utils.GetClippingRectangle(sb, innerRect), scissorRect);
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
            DrawBorderString(sb, Text, new Vector2(innerRect.X, innerRect.Y), TextColor, maxCharactersDisplayed: CharacterLimit);
            sb.End();
            sb.GraphicsDevice.ScissorRectangle = scissorRect;
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, sb.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
            if (Focused)
            {
                if (CaretDelta < 20) sb.Draw(Textures.Caret, new Vector2(innerRect.X + 2 + Main.fontMouseText.MeasureString(Text).X, InnerY + 1), CaretColor);
                else if (CaretDelta >= 39) CaretDelta = 0;
                CaretDelta++;
            }
        }

        public override void UpdateSelf(GameTime gameTime)
        {
            if (Focused)
            {
                PlayerInput.WritingText = true;
                Main.instance.HandleIME();
                // Thanks to Magic Storage
                string frontHalf = text.Substring(0, CaretPosition);
                string newText = Main.GetInputText(frontHalf);
                CaretPosition = newText.Length;
                newText += text.Substring(CaretPosition);
                if (Text != newText)
                {
                    Text = newText;
                    OnTextChangedByUser?.Invoke(this, Text);
                }
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.OS;
using System;
using System.Reflection;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;

namespace ItemModifier.UIKit
{
    public class UITextbox : UIElement
    {
        public enum CharacterLimitType
        {
            DynamicLimit,
            StaticLimit,
            NoLimit
        }

        public event UIEventHandler<string> OnTextChanged;

        public event UIEventHandler<string> OnTextChangedByUser;

        public event UIEventHandler<bool> OnFocusChanged;

        public Color TextColor { get; set; } = Color.White;

        public bool Box { get; set; } = true;

        public uint CharacterLimit
        {
            get
            {
                return characterLimit;
            }

            set
            {
                characterLimit = value < 1 ? 1 : value;
            }
        }

        private uint characterLimit = 10;

        private string text = "";

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
                    text = value;
                    if (CaretAnchoredAtEnd) CaretPosition = Text.Length;
                    if (CaretPosition > text.Length) CaretPosition = text.Length;
                    OnTextChanged?.Invoke(this, text);
                }
            }
        }

        private bool focused;

        public bool Focused
        {
            get
            {
                return focused;
            }

            protected set
            {
                if (focused != value)
                {
                    focused = value;
                    OnFocusChanged?.Invoke(this, value);
                }
            }
        }

        protected string[] TextHistory = new string[10];

        protected int[] CaretHistory = new int[10];

        private int latestIndex;

        protected int LatestIndex
        {
            get
            {
                return latestIndex;
            }

            set
            {
                latestIndex = value > 9 ? 0 : value;
            }
        }

        protected int CaretDelta { get; set; }

        private int caretPosition;

        public int CaretPosition
        {
            get
            {
                return caretPosition;
            }

            set
            {
                caretPosition = value < 0 ? 0 : value > Text.Length ? Text.Length : value;
                if (caretPosition < Text.Length) CaretAnchoredAtEnd = false;
                if (CaretPosition == Text.Length) CaretAnchoredAtEnd = true;
            }
        }

        public bool CaretAnchoredAtEnd { get; set; } = true;

        public CharacterLimitType LimitType { get; set; } = CharacterLimitType.NoLimit;

        public UITextbox(Vector4 Margin = default) : base(margin: Margin)
        {
            Width = new StyleDimension(100f);
            Height = new StyleDimension(22f);
        }

        public UITextbox(uint characterLimit, Vector4 margin = default) : this(margin)
        {
            CharacterLimit = characterLimit;
            LimitType = CharacterLimitType.StaticLimit;
        }

        public override void LeftClick(UIMouseEventArgs e)
        {
            Focused = true;
            Main.PlaySound(SoundID.MenuTick);
            base.LeftClick(e);
        }

        public override void MouseOver(UIMouseEventArgs e)
        {
            Main.PlaySound(SoundID.MenuTick);
            base.MouseOver(e);
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            if (Box)
            {
                sb.Draw(ItemModifier.Textures.Textbox, new Rectangle((int)Dimensions.X, (int)Dimensions.Y, 2, 22), new Rectangle(0, 0, 2, 22), Color.White);
                sb.Draw(ItemModifier.Textures.Textbox, new Rectangle((int)Dimensions.X + 2, (int)Dimensions.Y, (int)Dimensions.Width - 4, 22), new Rectangle(2, 0, 96, 22), Color.White);
                sb.Draw(ItemModifier.Textures.Textbox, new Rectangle((int)Dimensions.X + (int)Dimensions.Width - 2, (int)Dimensions.Y, 2, 22), new Rectangle(98, 0, 2, 22), Color.White);
            }
            else
            {
                sb.Draw(ItemModifier.Textures.LineTextbox, Dimensions.Rectangle, Color.White);
            }
            if (Focused)
            {
                if (!Main.hasFocus || (Main.mouseLeft || Main.mouseRight) && !ContainsPoint(new Vector2(Main.mouseX, Main.mouseY)))
                {
                    DrawText(sb);
                    Focused = false;
                    CaretDelta = 0;
                    return;
                }
                CheckKeys();
                HandleText();
                Vector3 textsize = DrawText(sb);
                if (CaretDelta < 20) DrawCaret(sb, textsize);
                else if (CaretDelta > 39) CaretDelta = 0;
                CaretDelta++;
            }
            else
            {
                DrawText(sb);
            }
        }

        protected virtual Vector3 DrawText(SpriteBatch sb)
        {
            Vector2 pos = new Vector2(Dimensions.X, Dimensions.Y);
            float maxWidth = Width.Pixels;
            if (Box)
            {
                pos.X += 4;
                maxWidth -= 8;
            }
            return new Vector3(pos.X, pos.Y, Utils.DrawBorderString(sb, KRUtils.TrimText(Text, maxWidth, Main.fontMouseText), pos, TextColor).X);
            /*Rectangle scissorRect = sb.GraphicsDevice.ScissorRectangle;
            sb.End();
            sb.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(KRUtils.GetClippingRectangle(sb, Dimensions.Rectangle), sb.GraphicsDevice.ScissorRectangle);
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
            float size = KRUtils.DrawString(sb, Main.fontMouseText, text, pos, TextColor).X;
            sb.End();
            sb.GraphicsDevice.ScissorRectangle = scissorRect;
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, sb.GraphicsDevice.RasterizerState, null, Main.UIScaleMatrix);
            return new Vector3(pos.X, pos.Y, size);*/
        }

        protected virtual void DrawCaret(SpriteBatch sb, Vector3 TextPosition)
        {
            sb.Draw(ItemModifier.Textures.Caret, new Vector2(TextPosition.X + TextPosition.Z, Dimensions.Y), Color.Black);
        }

        protected virtual void CheckKeys()
        {
            if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.Left)) CaretPosition--;
            if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.Right)) CaretPosition++;
            if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.Delete))
            {
                Text = "";
                CaretPosition = 0;
            }
            if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.Insert))
            {
                string clipboardText = Platform.Current.Clipboard;
                for (int i = 0; i < clipboardText.Length; i++)
                {
                    if (clipboardText[i] < ' ' || clipboardText[i] == '\u007f') clipboardText = clipboardText.Replace(clipboardText[i--].ToString() ?? "", "");
                }
                InsertText(clipboardText);
            }
            if (KRUtils.IsAnyKeyDown(Main.keyState, Keys.LeftControl, Keys.RightControl))
            {
                /*if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.Z))
                {
                    Undo();
                }
                else if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.Y))
                {
                    Redo();
                }
                else*/
                if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.X))
                {
                    Platform.Current.Clipboard = Text;
                    Text = "";
                    CaretPosition = 0;
                    LatestIndex++;
                    TextHistory[LatestIndex] = Text;
                    CaretHistory[LatestIndex] = CaretPosition;
                }
                else if (KRUtils.IsAnyKeyPressed(Main.oldKeyState, Main.keyState, Keys.C))
                {
                    Platform.Current.Clipboard = Text;
                }
                else if (KRUtils.IsKeyPressed(Main.oldKeyState, Main.keyState, Keys.V))
                {
                    InsertText(Platform.Current.Clipboard);
                }
            }
        }

        protected virtual void HandleText()
        {
            PlayerInput.WritingText = true;
            Main.instance.HandleIME();
            Tuple<string, int> newText = GetInputText();
            if (Text != newText.Item1)
            {
                switch (LimitType)
                {
                    case CharacterLimitType.DynamicLimit:
                        float MaxWidth = Width.Pixels;
                        if (Box) MaxWidth -= 8;
                        string trimmedText = KRUtils.TrimText(newText.Item1, MaxWidth, Main.fontMouseText);
                        newText = Tuple.Create(trimmedText, trimmedText.Length);
                        break;
                    case CharacterLimitType.StaticLimit:
                        if (newText.Item1.Length > CharacterLimit) newText = Tuple.Create(newText.Item1.Remove((int)CharacterLimit), (int)CharacterLimit);
                        break;
                }
                TextChanged(newText);
                OnTextChangedByUser?.Invoke(this, Text);
                if (CaretAnchoredAtEnd) CaretPosition = Text.Length;
                LatestIndex++;
                TextHistory[LatestIndex] = Text;
                CaretHistory[LatestIndex] = CaretPosition;
            }
        }

        protected virtual void TextChanged(Tuple<string, int> newText)
        {
            Text = newText.Item1;
            CaretPosition = newText.Item2;
        }

        protected virtual Tuple<string, int> GetInputText()
        {
            if (!Main.hasFocus) return Tuple.Create(Text, CaretPosition);
            Main.inputTextEnter = false;
            Main.inputTextEscape = false;
            string newText = Text ?? "";
            string inputText = "";
            int newCaretPos = CaretPosition;
            FieldInfo backSpaceCountInfo = typeof(Main).GetField("backSpaceCount", BindingFlags.NonPublic | BindingFlags.Static);
            int backSpaceCount = (int)backSpaceCountInfo.GetValue(null);
            bool flag = false;
            for (int j = 0; j < Main.keyCount; j++)
            {
                int num = Main.keyInt[j];
                string str = Main.keyString[j];
                if (num == 13) Main.inputTextEnter = true;
                else if (num == 27) Main.inputTextEscape = true;
                else if (num >= 32 && num != 127) inputText += str;
            }
            Main.keyCount = 0;
            newText = newText.Insert(newCaretPos, inputText);
            newCaretPos += inputText.Length;
            Main.oldInputText = Main.inputText;
            Main.inputText = Keyboard.GetState();
            Keys[] pressedKeys = Main.inputText.GetPressedKeys();
            Keys[] oldPressedKeys = Main.oldInputText.GetPressedKeys();
            if (Main.keyState.IsKeyDown(Keys.Back))
            {
                if (backSpaceCount == 0)
                {
                    backSpaceCount = 7;
                    flag = true;
                }
                backSpaceCount--;
            }
            else
            {
                backSpaceCount = 15;
            }
            backSpaceCountInfo.SetValue(null, backSpaceCount);
            for (int k = 0; k < pressedKeys.Length; k++)
            {
                bool isKeyDown = true;
                for (int l = 0; l < oldPressedKeys.Length; l++)
                {
                    if (pressedKeys[k] == oldPressedKeys[l]) isKeyDown = false;
                }
                if (string.Concat(pressedKeys[k]) == "Back" && (isKeyDown || flag) && newText.Length > 0 && newCaretPos > 0)
                {
                    newCaretPos--;
                    newText = newText.Remove(newCaretPos, 1);
                }
            }
            return Tuple.Create(newText, newCaretPos);
        }

        public virtual void InsertText(string Text)
        {
            Text.Insert(CaretPosition, Text);
        }

        public virtual void InsertText(int InsertPos, string Text)
        {
            Text.Insert(InsertPos, Text);
        }

        public void Undo()
        {
            LatestIndex--;
            Text = TextHistory[LatestIndex];
            CaretPosition = CaretHistory[LatestIndex];
        }

        public void Redo()
        {
            LatestIndex++;
            Text = TextHistory[LatestIndex];
            CaretPosition = CaretHistory[LatestIndex];
        }
    }
}

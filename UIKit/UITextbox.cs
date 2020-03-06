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
    /// <summary>
    /// Element for text input.
    /// </summary>
    public class UITextbox : UIElement
    {
        public enum CharacterLimitType
        {
            DynamicLimit,
            StaticLimit,
            NoLimit
        }

        /// <summary>
        /// Fired when <see cref="Text"/> is changed.
        /// </summary>
        public event UIEventHandler<string> OnTextChanged;

        /// <summary>
        /// Fired when <see cref="Text"/> is changed by the user.
        /// </summary>
        public event UIEventHandler<string> OnTextChangedByUser;

        /// <summary>
        /// Fired when textbox is focused/unfocused.
        /// </summary>
        public event UIEventHandler<bool> OnFocusChanged;

        /// <summary>
        /// Color of text.
        /// </summary>
        public Color TextColor { get; set; } = Color.White;

        /// <summary>
        /// If true, a box-like textbox will be used, otherwise a line-like textbox will be used.
        /// </summary>
        public bool Box { get; set; } = true;

        /// <summary>
        /// Maximum characters <see cref="Text"/> can hold.
        /// </summary>
        public uint CharacterLimit
        {
            get => characterLimit;

            set
            {
                characterLimit = value < 1 ? 1 : value;
            }
        }

        private uint characterLimit = 10;

        private string text = "";

        /// <summary>
        /// Value of this input.
        /// </summary>
        public string Text
        {
            get => text;

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

        /// <summary>
        /// True if textbox is focused, false otherwise.
        /// </summary>
        public bool Focused
        {
            get => focused;

            protected set
            {
                if (focused != value)
                {
                    focused = value;
                    OnFocusChanged?.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// History of <see cref="Text"/>, newest is <see cref="LatestIndex"/>
        /// </summary>
        protected string[] TextHistory = new string[10];

        /// <summary>
        /// History of <see cref="CaretPosition"/>, newest is <see cref="LatestIndex"/>
        /// </summary>
        protected int[] CaretHistory = new int[10];

        private int latestIndex;

        /// <summary>
        /// Defines the point between old and new for the arrays <see cref="TextHistory"/> and <see cref="CaretHistory"/>.
        /// </summary>
        protected int LatestIndex
        {
            get => latestIndex;
            set
            {
                latestIndex = value > 9 ? 0 : value;
            }
        }

        /// <summary>
        /// Time till caret will be drawn.
        /// </summary>
        protected int CaretDelta { get; set; }

        private int caretPosition;

        /// <summary>
        /// Position of the caret.
        /// </summary>
        public int CaretPosition
        {
            get => caretPosition;
            set
            {
                caretPosition = value < 0 ? 0 : value > Text.Length ? Text.Length : value;
                if (caretPosition < Text.Length) CaretAnchoredAtEnd = false;
                if (CaretPosition == Text.Length) CaretAnchoredAtEnd = true;
            }
        }

        public bool CaretAnchoredAtEnd { get; set; } = true;

        public CharacterLimitType LimitType { get; set; } = CharacterLimitType.NoLimit;

        /// <summary>
        /// Initializes a new <see cref="UITextbox"/> Element.
        /// </summary>
        /// <param name="Margin">Add space around the element.</param>
        public UITextbox(Vector4 Margin = default) : base(Margin: Margin) => (Width, Height) = (new StyleDimension(100f), new StyleDimension(22f));

        /// <summary>
        /// Initializes a new <see cref="UITextbox"/> Element.
        /// </summary>
        /// <param name="CharacterLimit">Maximum characters <see cref="Text"/> can hold.</param>
        /// <param name="Margin">Add space around the element.</param>
        public UITextbox(uint CharacterLimit, Vector4 Margin = default) : this(Margin) => (this.CharacterLimit, LimitType) = (CharacterLimit, CharacterLimitType.StaticLimit);

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
                if (CaretDelta < 20)
                {
                    DrawCaret(sb, textsize);
                }
                else if (CaretDelta > 39)
                {
                    CaretDelta = 0;
                }
                CaretDelta++;
            }
            else
            {
                DrawText(sb);
            }
        }

        /// <summary>
        /// Draws the textbox's text.
        /// </summary>
        /// <param name="sb">Spritebatch</param>
        protected virtual Vector3 DrawText(SpriteBatch sb)
        {
            Vector2 pos = new Vector2(Dimensions.X, Dimensions.Y);
            float maxWidth = Width.Pixels;
            if (Box)
            {
                pos.X += 4;
                maxWidth -= 8;
            }
            return new Vector3(pos.X, pos.Y, Utils.DrawBorderString(sb, KRUtils.TrimTextReverse(Text, maxWidth, Main.fontMouseText), pos, TextColor).X);
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

        /// <summary>
        /// Draws the caret.
        /// </summary>
        /// <param name="sb">Spritebatch</param>
        protected virtual void DrawCaret(SpriteBatch sb, Vector3 TextPosition)
        {
            sb.Draw(ItemModifier.Textures.Caret, new Vector2(TextPosition.X + TextPosition.Z, Dimensions.Y), Color.Black);
        }

        /// <summary>
        /// Checks for triggered hotkeys.
        /// </summary>
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

        /// <summary>
        /// Triggers & Handles text input.
        /// </summary>
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
                        if (newText.Item1.Length > CharacterLimit)
                        {
                            newText = Tuple.Create(newText.Item1.Remove((int)CharacterLimit), (int)CharacterLimit);
                        }
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

        /// <summary>
        /// Handle new text.
        /// </summary>
        /// <param name="newText">string = new text, int = new caret position.</param>
        protected virtual void TextChanged(Tuple<string, int> newText)
        {
            Text = newText.Item1;
            CaretPosition = newText.Item2;
        }

        /// <summary>
        /// Handles text input.
        /// </summary>
        /// <returns>New text, new caret position.</returns>
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
                if (num == 13)
                {
                    Main.inputTextEnter = true;
                }
                else if (num == 27)
                {
                    Main.inputTextEscape = true;
                }
                else if (num >= 32 && num != 127)
                {
                    inputText += str;
                }
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

        /// <summary>
        /// Inserts text into the textbox with respect to <see cref="CaretPosition"/>.
        /// </summary>
        /// <param name="Text">Text to insert.</param>
        public virtual void InsertText(string Text)
        {
            Text.Insert(CaretPosition, Text);
        }

        /// <summary>
        /// Inserts text at a specific point.
        /// </summary>
        /// <param name="InsertPos">Point to insert text.</param>
        /// <param name="Text">Text to insert.</param>
        public virtual void InsertText(int InsertPos, string Text)
        {
            Text.Insert(InsertPos, Text);
        }

        /// <summary>
        /// Go back one steb in text history.
        /// </summary>
        public void Undo()
        {
            LatestIndex--;
            Text = TextHistory[LatestIndex];
            CaretPosition = CaretHistory[LatestIndex];
        }

        /// <summary>
        /// Go forward one steb in text history.
        /// </summary>
        public void Redo()
        {
            LatestIndex++;
            Text = TextHistory[LatestIndex];
            CaretPosition = CaretHistory[LatestIndex];
        }
    }
}

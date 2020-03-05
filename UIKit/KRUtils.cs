using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Contains utilities.
    /// </summary>
    public static class KRUtils
    {
        public static Color UIBackgroundColor { get => new Color(44, 57, 105, 190); }

        /// <summary>
        /// Measures text(in the MouseText font) more accurately.
        /// </summary>
        /// <param name="Text">Text to be measured.</param>
        /// <param name="SkipDescenderScaling">Wheter the presence of the letters g, j, p, q, Q or y will affect the height.</param>
        /// <returns>The size of the text in <see cref="Vector2"/>.</returns>
        public static Vector2 MeasureTextAccurate(string Text, bool SkipDescenderScaling = false)
        {
            Vector2 Size = Main.fontMouseText.MeasureString(Text);
            Size.Y = !SkipDescenderScaling ? Text.Contains("g") || Text.Contains("j") || Text.Contains("p") || Text.Contains("q") || Text.Contains("Q") || Text.Contains("y") ? 25 : 21 : 25;
            return Size;
        }

        /// <summary>
        /// Cuts text exceeding the size limit(left to right).
        /// </summary>
        /// <param name="Text">Text to be trimmed.</param>
        /// <param name="MaxWidth">Max size(X Axis) the text can be.</param>
        /// <returns>The trimmed text.</returns>
        public static string TrimText(string Text, float MaxWidth, DynamicSpriteFont font)
        {
            string result = "";
            float size = 0;
            float charSize;
            for (int j = 0; j < Text.Length && size + (charSize = font.GetCharacterMetrics(Text[j]).KernedWidth) < MaxWidth; j++)
            {
                size += charSize;
                result += Text[j];
            }
            return result;
        }

        /// <summary>
        /// Cuts text exceeding the size limit(right to left, returned text will still be in normal order).
        /// </summary>
        /// <param name="Text">Text to be trimmed.</param>
        /// <param name="MaxWidth">Max size(X Axis) the text can be.</param>
        /// <returns>The trimmed text.</returns>
        public static string TrimTextReverse(string Text, float MaxWidth, DynamicSpriteFont font)
        {
            string result = "";
            float size = 0;
            float charSize;
            for (int j = Text.Length - 1; j >= 0 && size + (charSize = font.GetCharacterMetrics(Text[j]).KernedWidth) < MaxWidth; j--)
            {
                size += charSize;
                result = Text[j] + result;
            }
            return result;
        }

        /// <summary>
        /// Checks if the specified key is pressed(with reference to the specified <see cref="KeyboardState"/>).
        /// </summary>
        /// <param name="OldKeyboardState">Old keyboard state.</param>
        /// <param name="NewKeyboardState">New keyboard state.</param>
        /// <param name="key">Key to check.</param>
        /// <returns>True if the specified keys is pressed, false otherwise.</returns>
        public static bool IsKeyPressed(KeyboardState OldKeyboardState, KeyboardState NewKeyboardState, Keys key)
        {
            return !OldKeyboardState.IsKeyDown(key) && NewKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks if all the specified keys are pressed(with reference to the specified <see cref="KeyboardState"/>s).
        /// </summary>
        /// <param name="OldKeyboardState">Old keyboard state.</param>
        /// <param name="NewKeyboardState">New keyboard state.</param>
        /// <param name="keys">Keys to check.</param>
        /// <returns>True if all of the specified keys are pressed, false otherwise.</returns>
        public static bool AreAllKeysPressed(KeyboardState OldKeyboardState, KeyboardState NewKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++) if (!IsKeyPressed(OldKeyboardState, NewKeyboardState, keys[i])) return false;
            return true;
        }

        /// <summary>
        /// Checks if any of the specified keys are pressed(with reference to the specified <see cref="KeyboardState"/>s).
        /// </summary>
        /// <param name="OldKeyboardState">Old keyboard state.</param>
        /// <param name="NewKeyboardState">New keyboard state.</param>
        /// <param name="keys">Keys to check.</param>
        /// <returns>True if any of the specified keys are pressed, false otherwise.</returns>
        public static bool IsAnyKeyPressed(KeyboardState OldKeyboardState, KeyboardState NewKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++) if (IsKeyPressed(OldKeyboardState, NewKeyboardState, keys[i])) return true;
            return false;
        }

        /// <summary>
        /// Checks if all of the specified keys are being held down(with reference to the specified <see cref="KeyboardState"/>).
        /// </summary>
        /// <param name="NewKeyboardState">New keyboard state.</param>
        /// <param name="keys">Keys to check.</param>
        /// <returns></returns>
        public static bool AreAllKeysDown(KeyboardState NewKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++) if (!NewKeyboardState.IsKeyDown(keys[i])) return false;
            return true;
        }

        /// <summary>
        /// Checks if any of the specified keys are being held down(with reference to the specified <see cref="KeyboardState"/>).
        /// </summary>
        /// <param name="NewKeyboardState">New keyboard state.</param>
        /// <param name="keys">Keys to check.</param>
        /// <returns></returns>
        public static bool IsAnyKeyDown(KeyboardState NewKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++) if (NewKeyboardState.IsKeyDown(keys[i])) return true;
            return false;
        }

        public static Vector2 DrawString(SpriteBatch sb, DynamicSpriteFont font, string text, Vector2 pos, Color color, int maxChars = -1, float rotation = 0f, Vector2 origin = default, Vector2 scale = default, Vector2 anchor = default, /*SpriteEffects effects = SpriteEffects.None,*/ float layerDepth = 0f)
        {
            if (maxChars != -1 && text.Length > maxChars) text = text.Substring(0, maxChars);
            Vector2 size = font.MeasureString(text);
            ChatManager.DrawColorCodedStringWithShadow(sb, font, text, pos, color, 0f, anchor * size, scale, -1f, 1.5f);
            sb.DrawString(font, text, pos, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
            return size * scale;
        }

        public static Rectangle GetClippingRectangle(SpriteBatch sb, Rectangle rect)
        {
            Vector2 vector = Vector2.Transform(new Vector2(rect.X, rect.Y), Main.UIScaleMatrix);
            Vector2 position = Vector2.Transform(new Vector2(rect.Width, rect.Height) + vector, Main.UIScaleMatrix);
            int width = sb.GraphicsDevice.Viewport.Width;
            int height = sb.GraphicsDevice.Viewport.Height;
            Rectangle result = new Rectangle(Utils.Clamp((int)vector.X, 0, width), Utils.Clamp((int)vector.Y, 0, height), (int)(position.X - vector.X), (int)(position.Y - vector.Y));
            result.Width = Utils.Clamp(result.Width, 0, width - result.X);
            result.Height = Utils.Clamp(result.Height, 0, height - result.Y);
            return result;
        }

        public static List<int> FindItemsByName(string Name, bool CaseSensitive = false, bool ExcludeDeprecated = true)
        {
            List<int> matches = new List<int>();
            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                if (ExcludeDeprecated && ItemID.Sets.Deprecated[i]) continue;
                if (CaseSensitive)
                {
                    if (Lang.GetItemName(i).Value.Contains(Name)) matches.Add(i);
                }
                else
                {
                    if (Lang.GetItemName(i).Value.ToLower().Contains(Name.ToLower())) matches.Add(i);
                }
            }
            return matches;
        }
    }
}

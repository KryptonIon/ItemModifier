using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static Microsoft.Xna.Framework.Vector2;
using static Terraria.ID.ItemID.Sets;
using static Terraria.Lang;
using static Terraria.Main;
using static Terraria.ModLoader.ItemLoader;
using static Terraria.Utils;

namespace ItemModifier.UIKit
{
    public static class Utils
    {
        public static Vector2 MeasureString2(string text, bool skipDescenderScaling = false)
        {
            Vector2 Size = fontMouseText.MeasureString(text);
            Size.Y -= !skipDescenderScaling ? text.Contains("g") || text.Contains("j") || text.Contains("p") || text.Contains("q") || text.Contains("Q") || text.Contains("y") ? 3 : 7 : 3;
            return Size;
        }

        public static bool IsKeyPressed(KeyboardState oldKeyboardState, KeyboardState newKeyboardState, Keys key)
        {
            return !oldKeyboardState.IsKeyDown(key) && newKeyboardState.IsKeyDown(key);
        }

        public static bool AreAllKeysPressed(KeyboardState oldKeyboardState, KeyboardState newKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (!IsKeyPressed(oldKeyboardState, newKeyboardState, keys[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsAnyKeyPressed(KeyboardState oldKeyboardState, KeyboardState newKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (IsKeyPressed(oldKeyboardState, newKeyboardState, keys[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool AreAllKeysDown(KeyboardState newKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (!newKeyboardState.IsKeyDown(keys[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsAnyKeyDown(KeyboardState newKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (newKeyboardState.IsKeyDown(keys[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static Rectangle GetClippingRectangle(SpriteBatch sb, Rectangle rect)
        {
            Vector2 topLeft = Transform(new Vector2(rect.X, rect.Y), UIScaleMatrix);
            Vector2 bottomRight = Transform(topLeft + new Vector2(rect.Width, rect.Height), UIScaleMatrix);
            int width = sb.GraphicsDevice.Viewport.Width;
            int height = sb.GraphicsDevice.Viewport.Height;
            Rectangle result = new Rectangle(Clamp((int)topLeft.X, 0, width), Clamp((int)topLeft.Y, 0, height), (int)(bottomRight.X - topLeft.X), (int)(bottomRight.Y - topLeft.Y));
            result.Width = Clamp(result.Width, 0, width - result.X);
            result.Height = Clamp(result.Height, 0, height - result.Y);
            return result;
        }

        public static int[] FindItemsByName(string name, bool caseSensitive = false, bool excludeDeprecated = true)
        {
            List<int> matches = new List<int>();
            for (int i = 0; i < ItemCount; i++)
            {
                if (excludeDeprecated && Deprecated[i])
                {
                    continue;
                }
                if (caseSensitive)
                {
                    if (GetItemName(i).Value.Contains(name))
                    {
                        matches.Add(i);
                    }
                }
                else
                {
                    if (GetItemName(i).Value.ToLower().Contains(name.ToLower()))
                    {
                        matches.Add(i);
                    }
                }
            }
            return matches.ToArray();
        }

        /// <summary>
        /// Indicates if the specified character is a Hindu/Indo-Arabic Digit(0-9)
        /// </summary>
        /// <returns><see langword="true"/> if <paramref name="c"/> is a Hindu-Arabic Digit; otherwise, <see langword="false"/>.</returns>
        public static bool IsHADigit(this char c)
        {
            return c >= '0' && c <= '9';
        }
    }
}

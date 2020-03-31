using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
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
        public static Color UIBackgroundColor
        {
            get
            {
                return new Color(44, 57, 105, 190);
            }
        }

        public static Vector2 MeasureString2(string Text, bool SkipDescenderScaling = false)
        {
            Vector2 Size = fontMouseText.MeasureString(Text);
            Size.Y -= !SkipDescenderScaling ? Text.Contains("g") || Text.Contains("j") || Text.Contains("p") || Text.Contains("q") || Text.Contains("Q") || Text.Contains("y") ? 3 : 7 : 3;
            return Size;
        }

        public static string TrimText(string Text, float MaxWidth, DynamicSpriteFont font)
        {
            string result = string.Empty;
            float size = 0;
            float charSize;
            for (int j = Text.Length - 1; j >= 0 && size + (charSize = font.GetCharacterMetrics(Text[j]).KernedWidth) < MaxWidth; j--)
            {
                size += charSize;
                result = Text[j] + result;
            }
            return result;
        }

        public static bool IsKeyPressed(KeyboardState OldKeyboardState, KeyboardState NewKeyboardState, Keys key)
        {
            return !OldKeyboardState.IsKeyDown(key) && NewKeyboardState.IsKeyDown(key);
        }

        public static bool AreAllKeysPressed(KeyboardState OldKeyboardState, KeyboardState NewKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++) if (!IsKeyPressed(OldKeyboardState, NewKeyboardState, keys[i])) return false;
            return true;
        }

        public static bool IsAnyKeyPressed(KeyboardState OldKeyboardState, KeyboardState NewKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++) if (IsKeyPressed(OldKeyboardState, NewKeyboardState, keys[i])) return true;
            return false;
        }

        public static bool AreAllKeysDown(KeyboardState NewKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++) if (!NewKeyboardState.IsKeyDown(keys[i])) return false;
            return true;
        }

        public static bool IsAnyKeyDown(KeyboardState NewKeyboardState, params Keys[] keys)
        {
            for (int i = 0; i < keys.Length; i++) if (NewKeyboardState.IsKeyDown(keys[i])) return true;
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

        public static List<int> FindItemsByName(string Name, bool CaseSensitive = false, bool ExcludeDeprecated = true)
        {
            List<int> matches = new List<int>();
            for (int i = 0; i < ItemCount; i++)
            {
                if (ExcludeDeprecated && Deprecated[i]) continue;
                if (CaseSensitive)
                {
                    if (GetItemName(i).Value.Contains(Name)) matches.Add(i);
                }
                else
                {
                    if (GetItemName(i).Value.ToLower().Contains(Name.ToLower())) matches.Add(i);
                }
            }
            return matches;
        }
    }
}

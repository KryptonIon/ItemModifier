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
    public static class KRUtils
    {
        public static Color UIBackgroundColor
        {
            get
            {
                return new Color(44, 57, 105, 190);
            }
        }

        public static Vector2 MeasureTextAccurate(string Text, bool SkipDescenderScaling = false)
        {
            Vector2 Size = Main.fontMouseText.MeasureString(Text);
            Size.Y = !SkipDescenderScaling ? Text.Contains("g") || Text.Contains("j") || Text.Contains("p") || Text.Contains("q") || Text.Contains("Q") || Text.Contains("y") ? 25 : 21 : 25;
            return Size;
        }

        public static string TrimText(string Text, float MaxWidth, DynamicSpriteFont font)
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

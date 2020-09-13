using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.UI;

namespace ItemModifier.UIKit
{
    public class UIItemDisplay : UIElement
    {
        public Item Item { get; set; }

        private Vector2 origin;

        private float scale;

        public UIItemDisplay(int itemID, float size)
        {
            Item = new Item();
            Item.SetDefaults(itemID);
            Width = new SizeDimension(size);
            Height = Width;
        }

        protected override void DrawSelf(SpriteBatch sb)
        {
            base.DrawSelf(sb);
            if (Item.IsAir)
            {
                return;
            }
            Texture2D itemTexture = Main.itemTexture[Item.type];
            Rectangle frameRect = Main.itemAnimations[Item.type] == null ? itemTexture.Frame(1, 1, 0, 0) : Main.itemAnimations[Item.type].GetFrame(itemTexture);
            ItemSlot.GetItemLight(ref Item.color, ref Item.scale, Item, false);
            Vector2 position = new Vector2(InnerX + InnerWidth * 0.5f, InnerY + InnerHeight * 0.5f);
            sb.Draw(itemTexture, position, frameRect, Item.GetAlpha(Color.White), 0f, origin, scale, SpriteEffects.None, 0f);
            if (Item.color.A > 0)
            {
                sb.Draw(itemTexture, position, frameRect, Item.GetColor(Item.color), 0f, origin, scale, SpriteEffects.None, 0f);
            }
            if (MouseHovering)
            {
                Main.hoverItemName = Item.Name;
                if (Item.stack > 1)
                {
                    Main.hoverItemName += $" ({Item.stack})";
                }
                Main.HoverItem = Item.Clone();
            }
        }

        protected internal override void RecalculateSelf()
        {
            base.RecalculateSelf();
            Texture2D itemTexture = Main.itemTexture[Item.type];
            Rectangle frameRect = Main.itemAnimations[Item.type] == null ? itemTexture.Frame(1, 1, 0, 0) : Main.itemAnimations[Item.type].GetFrame(itemTexture);
            ItemSlot.GetItemLight(ref Item.color, ref Item.scale, Item, false);
            scale = (frameRect.Width > (int)InnerWidth || frameRect.Height > (int)InnerWidth ? (InnerWidth / Math.Max(frameRect.Width, frameRect.Height)) : 1f) * Item.scale;
            origin = frameRect.Size() * 0.5f;
        }
    }
}

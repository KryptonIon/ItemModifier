﻿using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static ItemModifier.UIKit.Utils;

namespace ItemModifier.UI
{
    public class NewItemUIW : UIWindow
    {
        internal UIText NoMatches;

        internal UITextbox ItemNameTextbox;

        internal UIIntTextbox ItemIDTextbox;

        internal UIImageButton Generate;

        internal UIItemDisplay ItemDisplay;

        internal UIContainer Matches;

        internal UIIntTextbox ItemStackTextbox;

        internal UICheckbox UseModifiedProperties;

        public NewItemUIW() : base("New Item")
        {
            Visible = false;
            Width = new SizeDimension(542f);
            Height = new SizeDimension(110f);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();
            ItemDisplay = new UIItemDisplay(1, 32f)
            {
                Parent = this
            };

            ItemNameTextbox = new UITextbox()
            {
                Text = "Iron Pickaxe",
                Width = new SizeDimension(309f),
                XOffset = new SizeDimension(ItemDisplay.OuterWidth + 4f),
                YOffset = new SizeDimension(4f),
                Parent = this
            };
            ItemNameTextbox.WhileMouseHover += (source, e) => instance.Tooltip = "Item Name";
            ItemNameTextbox.OnFocused += (source) => Matches.Visible = true;
            ItemNameTextbox.OnUnfocused += (source) => Matches.Visible = false;
            ItemNameTextbox.OnTextChanged += (source, value) =>
            {
                if (string.IsNullOrEmpty(value))
                {
                    Matches.Visible = false;
                }
                else
                {
                    Matches.RemoveAllChildren();
                    int[] ids = FindItemsByName(value);
                    if (ids.Length > 0)
                    {
                        for (int i = 0, j = 0, k = 0; i < ids.Length; i++)
                        {
                            UIItemDisplay item = new UIItemDisplay(ids[i], 32f)
                            {
                                XOffset = new SizeDimension(36f * j),
                                YOffset = new SizeDimension(36f * k),
                                Parent = Matches
                            };
                            item.OnLeftClick += (source2, e) =>
                            {
                                if (item.Item.type != ItemIDTextbox.Value)
                                {
                                    ItemIDTextbox.Value = item.Item.type;
                                }
                                else
                                {
                                    UpdateTextboxes();
                                }
                                Matches.Visible = false;
                            };
                            if (++j > 13)
                            {
                                j = 0;
                                k++;
                            }
                        }
                    }
                    else
                    {
                        NoMatches.Parent = Matches;
                    }
                    Matches.Recalculate();
                    Matches.Visible = true;
                }
            };

            ItemIDTextbox = new UIIntTextbox(1, ItemLoader.ItemCount - 1)
            {
                Negateable = false,
                Width = new SizeDimension(51f),
                XOffset = new SizeDimension(ItemNameTextbox.CalculatedXOffset + ItemNameTextbox.OuterWidth + 4f),
                YOffset = ItemNameTextbox.YOffset,
                Parent = this
            };
            ItemIDTextbox.WhileMouseHover += (source, e) => instance.Tooltip = "Item ID";
            ItemIDTextbox.OnValueChanged += (source, value) => UpdateTextboxes();

            ItemStackTextbox = new UIIntTextbox(1)
            {
                XOffset = new SizeDimension(ItemIDTextbox.CalculatedXOffset + ItemIDTextbox.OuterWidth + 4f),
                YOffset = ItemNameTextbox.YOffset,
                Parent = this
            };

            Matches = new UIContainer(UIBackgroundColor)
            {
                Visible = false,
                Width = new SizeDimension(ItemStackTextbox.CalculatedXOffset + ItemStackTextbox.OuterWidth - 4),
                Height = new SizeDimension(InnerHeight - ItemNameTextbox.CalculatedYOffset - ItemNameTextbox.OuterHeight - 8),
                XOffset = new SizeDimension(4f),
                YOffset = new SizeDimension(ItemNameTextbox.CalculatedYOffset + ItemNameTextbox.OuterHeight + 4f),
                OverflowHidden = true,
                Parent = this
            };
            Matches.OnFocused += (source) => Matches.Visible = true;
            Matches.OnUnfocused += (source) => Matches.Visible = false;

            UIItemDisplay ironPickaxe = new UIItemDisplay(1, 32f)
            {
                Parent = Matches
            };
            ironPickaxe.OnLeftClick += (source, e) =>
            {
                if (ironPickaxe.Item.type != ItemIDTextbox.Value)
                {
                    ItemIDTextbox.Value = ironPickaxe.Item.type;
                }
                else
                {
                    UpdateTextboxes();
                }
                Matches.Visible = false;
            };

            NoMatches = new UIText("No items found.");

            Generate = new UIImageButton(ItemModifier.Textures.NewItem)
            {
                Width = new SizeDimension(22f),
                Height = new SizeDimension(22f),
                XOffset = new SizeDimension(ItemStackTextbox.CalculatedXOffset + ItemStackTextbox.OuterWidth + 4f),
                YOffset = ItemNameTextbox.YOffset,
                Parent = this
            };
            Generate.WhileMouseHover += (source, e) => instance.Tooltip = "New Item";
            Generate.OnLeftClick += (source, e) =>
            {
                int itemIndex = Item.NewItem(Main.LocalPlayer.getRect(), ItemIDTextbox.Value, 1, true);
                if (UseModifiedProperties.Check)
                {
                    Main.item[itemIndex].CopyItemProperties(instance.MainUI.ModifyWindow.ModifiedItem);
                    Main.item[itemIndex].stack = ItemStackTextbox.Value;
                }
            };

            UseModifiedProperties = new UICheckbox()
            {
                XOffset = new SizeDimension(Generate.CalculatedXOffset + 2f),
                YOffset = Matches.YOffset,
                Parent = this
            };
            UseModifiedProperties.WhileMouseHover += (source, e) => instance.Tooltip = "Use Modified Properties";
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            if (UseModifiedProperties.Check)
            {
                ItemDisplay.Item = ModContent.GetInstance<ItemModifier>().MainUI.ModifyWindow.ModifiedItem;
            }
        }

        private void UpdateTextboxes()
        {
            ItemNameTextbox.Text = Lang.GetItemName(ItemIDTextbox.Value).Value;
            ItemDisplay.Item.SetDefaults(ItemIDTextbox.Value);
            ItemDisplay.Recalculate();
        }
    }
}

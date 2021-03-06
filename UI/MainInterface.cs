﻿using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;
using static ItemModifier.ItemModifier;

namespace ItemModifier.UI
{
    public class MainInterface : UILayer
    {
        internal ItemModifyUIW ItemModifierWindow;

        internal NewItemUIW NewItemWindow;

        internal UIImageButton ItemModifierButton;

        internal UIImageButton WikiButton;

        internal UIImageButton NewItemButton;

        public override void OnInitialize()
        {
            base.OnInitialize();
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();

            ItemModifierWindow = new ItemModifyUIW
            {
                XOffset = new SizeDimension(0, 0.3f),
                YOffset = new SizeDimension(0, 0.2f),
                Parent = this
            };

            NewItemWindow = new NewItemUIW
            {
                XOffset = new SizeDimension(ItemModifierWindow.OuterWidth + 10f, 0.3f),
                YOffset = ItemModifierWindow.YOffset,
                Parent = this
            };

            ItemModifierButton = new UIImageButton(ModContent.GetTexture("ItemModifier/UI/ModifyItem"))
            {
                XOffset = new SizeDimension(12f)
            };
            ItemModifierButton.YOffset = new SizeDimension(Main.screenHeight - ItemModifierButton.OuterHeight - 5f);
            ItemModifierButton.Parent = this;
            ItemModifierButton.OnLeftClick += (source, e) => ToggleItemModifierUI();
            ItemModifierButton.WhileMouseHover += (source, e) => instance.Tooltip = "Modify Items";

            NewItemButton = new UIImageButton(ModContent.GetTexture("ItemModifier/UI/NewItem"))
            {
                XOffset = new SizeDimension(12f)
            };
            NewItemButton.YOffset = new SizeDimension(ItemModifierButton.CalculatedYOffset - NewItemButton.OuterHeight - 5f);
            NewItemButton.Parent = this;
            NewItemButton.OnLeftClick += (source, e) => ToggleNewItemUI();
            NewItemButton.WhileMouseHover += (source, e) => instance.Tooltip = "New Item";

            WikiButton = new UIImageButton(ModContent.GetTexture("ItemModifier/UI/Wiki"))
            {
                XOffset = new SizeDimension(20f)
            };
            WikiButton.YOffset = new SizeDimension(NewItemButton.CalculatedYOffset - WikiButton.OuterHeight - 12f);
            WikiButton.Parent = this;
            WikiButton.OnLeftClick += (source, e) => OpenWiki();
            WikiButton.WhileMouseHover += (source, e) => instance.Tooltip = "Open Wiki";
        }

        internal void ToggleItemModifierUI()
        {
            ItemModifierWindow.Visible = !ItemModifierWindow.Visible;
        }

        internal void ToggleNewItemUI()
        {
            NewItemWindow.Visible = !NewItemWindow.Visible;
            if (NewItemWindow.Visible)
            {
                Main.playerInventory = true;
            }
        }
    }
}

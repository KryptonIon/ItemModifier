using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;
using static ItemModifier.ItemModifier;

namespace ItemModifier.UI
{
    public class MainInterface : UserInterface
    {
        internal ItemModifyUIW ItemModifierWindow;

        internal NewItemUIW NewItemWindow;

        internal UIImageButton ItemModifierButton;

        internal UIImageButton WikiButton;

        internal UIImageButton NewItemButton;

        public override void OnInitialize()
        {
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();

            ItemModifierWindow = new ItemModifyUIW
            {
                XOffset = new SizeDimension(0, 0.3f),
                YOffset = new SizeDimension(0, 0.2f),
                ParentUI = this
            };

            NewItemWindow = new NewItemUIW
            {
                XOffset = new SizeDimension(ItemModifierWindow.OuterWidth + 10f, 0.3f),
                YOffset = ItemModifierWindow.YOffset,
                ParentUI = this
            };

            ItemModifierButton = new UIImageButton(Textures.ModifyItem)
            {
                XOffset = new SizeDimension(12f)
            };
            ItemModifierButton.YOffset = new SizeDimension(Main.screenHeight - ItemModifierButton.OuterHeight - 5f);
            ItemModifierButton.ParentUI = this;
            ItemModifierButton.OnLeftClick += (source, e) => ItemModifierWindow.Visible = !ItemModifierWindow.Visible;
            ItemModifierButton.WhileMouseHover += (source, e) => instance.Tooltip = "Modify Items";

            NewItemButton = new UIImageButton(Textures.NewItem)
            {
                XOffset = new SizeDimension(12f)
            };
            NewItemButton.YOffset = new SizeDimension(ItemModifierButton.CalculatedYOffset - NewItemButton.OuterHeight - 5f);
            NewItemButton.ParentUI = this;
            NewItemButton.OnLeftClick += (source, e) =>
            {
                NewItemWindow.Visible = !NewItemWindow.Visible;
                if (NewItemWindow.Visible)
                {
                    Main.playerInventory = true;
                }
            };
            NewItemButton.WhileMouseHover += (source, e) => instance.Tooltip = "New Item";

            WikiButton = new UIImageButton(Textures.Wiki)
            {
                XOffset = new SizeDimension(20f)
            };
            WikiButton.YOffset = new SizeDimension(NewItemButton.CalculatedYOffset - WikiButton.OuterHeight - 12f);
            WikiButton.ParentUI = this;
            WikiButton.OnLeftClick += (source, e) => Process.Start("https://terrariamods.gamepedia.com/Item_Modifier");
            WikiButton.WhileMouseHover += (source, e) => instance.Tooltip = "Open Wiki";
        }
    }
}

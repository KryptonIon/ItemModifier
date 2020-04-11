using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.UI
{
    public class MainInterface : UserInterface
    {
        internal ItemModifyUIW ModifyWindow;

        internal NewItemUIW NewItemWindow;

        internal UIImageButton ModifyWB;

        internal UIImageButton WikiWB;

        internal UIImageButton NewItemWB;

        public override void OnInitialize()
        {
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();

            ModifyWindow = new ItemModifyUIW
            {
                XOffset = new SizeDimension(0, 0.3f),
                YOffset = new SizeDimension(0, 0.2f),
                ParentUI = this
            };

            NewItemWindow = new NewItemUIW
            {
                XOffset = new SizeDimension(ModifyWindow.OuterWidth + 10f, 0.3f),
                YOffset = ModifyWindow.YOffset,
                ParentUI = this
            };

            ModifyWB = new UIImageButton(ItemModifier.Textures.ModifyItem)
            {
                XOffset = new SizeDimension(12f)
            };
            ModifyWB.YOffset = new SizeDimension(Main.screenHeight - ModifyWB.OuterHeight - 5f);
            ModifyWB.ParentUI = this;
            ModifyWB.OnLeftClick += (source, e) => ModifyWindow.Visible = !ModifyWindow.Visible;
            ModifyWB.WhileMouseHover += (source, e) => instance.Tooltip = "Modify Items";

            NewItemWB = new UIImageButton(ItemModifier.Textures.NewItem)
            {
                XOffset = new SizeDimension(12f)
            };
            NewItemWB.YOffset = new SizeDimension(ModifyWB.CalculatedYOffset - NewItemWB.OuterHeight - 5f);
            NewItemWB.ParentUI = this;
            NewItemWB.OnLeftClick += (source, e) => NewItemWindow.Visible = !NewItemWindow.Visible;
            NewItemWB.WhileMouseHover += (source, e) => instance.Tooltip = "New Item";

            WikiWB = new UIImageButton(ItemModifier.Textures.Wiki)
            {
                XOffset = new SizeDimension(20f)
            };
            WikiWB.YOffset = new SizeDimension(NewItemWB.CalculatedYOffset - WikiWB.OuterHeight - 12f);
            WikiWB.ParentUI = this;
            WikiWB.OnLeftClick += (source, e) => Process.Start("https://terrariamods.gamepedia.com/Item_Modifier");
            WikiWB.WhileMouseHover += (source, e) => instance.Tooltip = "Open Wiki";
        }
    }
}

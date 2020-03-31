using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;
using System.Diagnostics;
using System.Threading;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.UI
{
    public class MainInterface : UserInterface
    {
        internal ItemModifyUIW ModifyWindow;

        internal ChangelogUIW ChangelogWindow;

        //internal NewItemUIW NewItemWindow;

        internal UIImageButton ModifyWB;

        internal UIImageButton WikiWB;

        internal UIImageButton ChangelogWB;

        internal UIImageButton NewItemWB;

        internal UIImageButton DiscordLink;

        public override void OnInitialize()
        {
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();

            ChangelogWindow = new ChangelogUIW
            {
                XOffset = new SizeDimension(0f, 0.1f),
                YOffset = new SizeDimension(0f, 0.2f),
                ParentUI = this
            };

            ModifyWindow = new ItemModifyUIW
            {
                XOffset = new SizeDimension(ChangelogWindow.OuterWidth + 10f, 0.1f), // + 10 is spacing between windows
                YOffset = ChangelogWindow.YOffset,
                ParentUI = this
            };

            /*NewItemWindow = new NewItemUIW
            {
                XOffset = new SizeDimension(ModifyWindow.CalculatedXOffset + ModifyWindow.OuterWidth + 10f, 0.1f), // + 10 is spacing between windows
                YOffset = ChangelogWindow.YOffset,
                ParentUI = this
            };*/

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
            //NewItemWB.OnLeftClick += (source, e) => NewItemWindow.Visible = !NewItemWindow.Visible;
            NewItemWB.WhileMouseHover += (source, e) => instance.Tooltip = "New Item";

            WikiWB = new UIImageButton(ItemModifier.Textures.Wiki)
            {
                XOffset = new SizeDimension(20f)
            };
            WikiWB.YOffset = new SizeDimension(NewItemWB.CalculatedYOffset - WikiWB.OuterHeight - 12f);
            WikiWB.ParentUI = this;
            WikiWB.OnLeftClick += (source, e) => new Thread(() => Process.Start("https://kryptonion.github.io/ItemModifier/")).Start();
            WikiWB.WhileMouseHover += (source, e) => instance.Tooltip = "Open Wiki";

            ChangelogWB = new UIImageButton(ItemModifier.Textures.ChangelogIcon)
            {
                XOffset = new SizeDimension(20f)
            };
            ChangelogWB.YOffset = new SizeDimension(WikiWB.CalculatedYOffset - ChangelogWB.OuterHeight - 12f);
            ChangelogWB.ParentUI = this;
            ChangelogWB.OnLeftClick += (source, e) => ChangelogWindow.Visible = !ChangelogWindow.Visible;
            ChangelogWB.WhileMouseHover += (source, e) => instance.Tooltip = "Changelog";

            DiscordLink = new UIImageButton(ItemModifier.Textures.DiscordIcon, activeTransparency: 1.5f, inactiveTransparency: 0.6f)
            {
                XOffset = new SizeDimension(17f)
            };
            DiscordLink.YOffset = new SizeDimension(ChangelogWB.CalculatedYOffset - DiscordLink.OuterHeight - 13.5f);
            DiscordLink.ParentUI = this;
            DiscordLink.OnLeftClick += (source, e) => new Thread(() => Process.Start("https://discord.gg/UjQWNC2")).Start();
            DiscordLink.WhileMouseHover += (source, e) => instance.Tooltip = "Discord";

            if (KRConfig.Instance.HelpMessage)
            {
                UIWindow messageBox = new UIWindow(false)
                {
                    Width = new SizeDimension(350f),
                    Height = new SizeDimension(21f),
                    XOffset = new SizeDimension(Main.screenWidth * 0.05f),
                    YOffset = new SizeDimension(Main.screenHeight - 112),
                    ParentUI = this
                };
                messageBox.Initialize();
                messageBox.OnVisibilityChanged += (source, value) => { if (!value) { messageBox.Parent = null; messageBox = null; } };
                new UIText("These are the ItemModifier UI Buttons.").Parent = messageBox;
            }
        }
    }
}

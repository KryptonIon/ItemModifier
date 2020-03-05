﻿using ItemModifier.UIKit;
using System.Diagnostics;
using System.Threading;
using Terraria;

namespace ItemModifier.UI
{
    public class MainInterface : UserInterface
    {
        internal ItemModifyUIW ModifyWindow;

        internal ChangelogUIW ChangelogWindow;

        internal NewItemUIW GenerateItemWindow;

        internal UIImageButton ModifyWB;

        internal UIImageButton WikiWB;

        internal UIImageButton ChangelogWB;

        internal UIImageButton NewItemWB;

        internal UIImageButton DiscordLink;

        public override void OnInitialize()
        {
            ChangelogWindow = new ChangelogUIW
            {
                Top = new StyleDimension(0, 0.2f),
                Left = new StyleDimension(0, 0.1f),
                ParentInterface = this
            };

            ModifyWindow = new ItemModifyUIW
            {
                Top = new StyleDimension(0f, 0.2f),
                Left = new StyleDimension(ChangelogWindow.OuterDimensions.Width + 10f, 0.1f), // + 10 is spacing between windows
                ParentInterface = this
            };

            GenerateItemWindow = new NewItemUIW
            {
                Left = ModifyWindow.Left,
                Top = new StyleDimension(ModifyWindow.OuterDimensions.Height + 10f, 0.2f), // + 10 is spacing between window
                ParentInterface = this
            };

            ModifyWB = new UIImageButton(ItemModifier.Textures.ModifyItem);
            ModifyWB.Top = new StyleDimension(Main.screenHeight - ModifyWB.Height.Pixels - 5f);
            ModifyWB.Left = new StyleDimension(12f);
            ModifyWB.OnLeftClick += (source, e) => ModifyWindow.Visible = !ModifyWindow.Visible;
            ModifyWB.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Modify Items";
            ModifyWB.ParentInterface = this;

            NewItemWB = new UIImageButton(ItemModifier.Textures.NewItem);
            NewItemWB.Top = new StyleDimension(ModifyWB.Top.Pixels - NewItemWB.Height.Pixels - 5f);
            NewItemWB.Left = new StyleDimension(12f);
            NewItemWB.OnLeftClick += (source, e) => GenerateItemWindow.Visible = !GenerateItemWindow.Visible;
            NewItemWB.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "New Item";
            NewItemWB.ParentInterface = this;

            WikiWB = new UIImageButton(ItemModifier.Textures.Wiki);
            WikiWB.Top = new StyleDimension(NewItemWB.Top.Pixels - WikiWB.Height.Pixels - 12f);
            WikiWB.Left = new StyleDimension(20f);
            WikiWB.OnLeftClick += (source, e) => new Thread(() => Process.Start("https://kryptonion.github.io/ItemModifier/")).Start();
            WikiWB.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Open Wiki";
            WikiWB.ParentInterface = this;

            ChangelogWB = new UIImageButton(ItemModifier.Textures.ChangelogIcon);
            ChangelogWB.Top = new StyleDimension(WikiWB.Top.Pixels - ChangelogWB.Height.Pixels - 12f);
            ChangelogWB.Left = new StyleDimension(20f);
            ChangelogWB.OnLeftClick += (source, e) => ChangelogWindow.Visible = !ChangelogWindow.Visible;
            ChangelogWB.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Changelog";
            ChangelogWB.ParentInterface = this;

            DiscordLink = new UIImageButton(ItemModifier.Textures.DiscordIcon, ActiveTransparency: 1.5f, InactiveTransparency: 0.6f);
            DiscordLink.Top = new StyleDimension(ChangelogWB.Top.Pixels - DiscordLink.Height.Pixels - 13.5f);
            DiscordLink.Left = new StyleDimension(17f);
            DiscordLink.OnLeftClick += (source, e) => new Thread(() => Process.Start("https://discord.gg/UjQWNC2")).Start();
            DiscordLink.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Discord";
            DiscordLink.ParentInterface = this;

            if (KRConfig.Instance.HelpMessage)
            {
                UIWindow messageBox = new UIWindow("", false, Draggable: false)
                {
                    Width = new StyleDimension(350f),
                    Height = new StyleDimension(21f),
                    Top = new StyleDimension(Main.screenHeight - 112),
                    Left = new StyleDimension(Main.screenWidth * 0.05f),
                    ParentInterface = this
                };
                messageBox.Initialize();
                messageBox.OnVisibilityChanged += (source, value) => { if (!value) { messageBox.Parent = null; messageBox = null; } };
                new UIText("These are the ItemModifier UI Buttons.").Parent = messageBox;
            }
        }
    }
}
using ItemModifier.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ItemModifier
{
    public class ItemModifier : Mod
    {
        internal MainInterface MainUI;

        internal string Tooltip { get; set; }

        public static string WikiURL = "https://terrariamods.gamepedia.com/Item_Modifier";

        public bool MouseWheelDisabled { get; set; } = false;

        public bool ItemAtCursorDisabled { get; set; } = false;

        internal ModHotKey ToggleItemModifierUIHotKey;

        internal ModHotKey ToggleNewItemUIHotKey;

        internal ModHotKey OpenWikiHotKey;

        public override void Load()
        {
            ToggleItemModifierUIHotKey = RegisterHotKey("Toggle Item Modifier Window", nameof(Keys.C));
            ToggleNewItemUIHotKey = RegisterHotKey("Toggle New Item Window", nameof(Keys.V));
            OpenWikiHotKey = RegisterHotKey("Open Wiki", nameof(Keys.X));

            if (!Main.dedServ)
            {
                //ItemConfig itemConfig = new ItemConfig();
                //itemConfig.Read();
            }
        }

        public override void PostSetupContent()
        {
            if (!Main.dedServ)
            {
                (MainUI = new MainInterface()).Activate();
                if (UIConfig.Instance.DebugLogs)
                    Logger.Info(MainUI.ToTreeString());
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                MainUI = null;
                //ItemConfig.Instance.Save();
                //ItemConfig.Instance = null;
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            MainUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (ItemAtCursorDisabled)
            {
                int mouseItemIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interact Item Icon"));
                if (mouseItemIndex != -1)
                {
                    layers.RemoveAt(mouseItemIndex);
                }
            }
            int mouseIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (mouseIndex != -1)
            {
                layers.Insert(mouseIndex - 1, new LegacyGameInterfaceLayer(
                    "ItemModifier: ItemModifierUI",
                    delegate
                    {
                        MainUI?.Draw(Main.spriteBatch);
                        Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, Tooltip ?? string.Empty, Main.mouseX + 16f, Main.mouseY + 16f, Color.White, Color.Black, Vector2.Zero);
                        Tooltip = null;
                        return true;
                    },
                    InterfaceScaleType.UI
                    ));
            }
        }

        public override void PreSaveAndQuit()
        {
            MainUI.ItemModifierWindow.Visible = false;
            MainUI.NewItemWindow.Visible = false;
        }

        public static void OpenWiki()
        {
            Process.Start(WikiURL);
        }
    }
}

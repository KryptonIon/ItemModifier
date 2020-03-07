using ItemModifier.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ItemModifier
{
    public class ItemModifier : Mod
    {
        public class Changelog
        {
            public string Version { get; }

            public string Title { get; }

            public string Website { get; }

            public List<string> Raw { get; }

            public Changelog(string Version, List<string> Raw, string Title = "", string Website = "") => (this.Version, this.Raw, this.Title, this.Website) = (Version, Raw, Title, Website);

            public override string ToString()
            {
                return $"{Version} {Title} \n{string.Join("\n", Raw)}";
            }

            public static Changelog Read(string Path)
            {
                using (MemoryStream mStream = new MemoryStream(ModContent.GetFileBytes(Path)))
                {
                    using (BinaryReader reader = new BinaryReader(mStream))
                    {
                        return new Changelog(reader.ReadString(), new List<string>(reader.ReadString().Split('\n')), reader.ReadString(), reader.ReadString());
                    }
                }
            }
        }

        public static class Textures
        {
            public static Texture2D ModifyItem { get; private set; }

            public static Texture2D Settings { get; private set; }

            public static Texture2D Wiki { get; private set; }

            public static Texture2D ChangelogIcon { get; private set; }

            public static Texture2D Save { get; private set; }

            public static Texture2D UpArrow { get; private set; }

            public static Texture2D DownArrow { get; private set; }

            public static Texture2D LeftArrow { get; private set; }

            public static Texture2D RightArrow { get; private set; }

            public static Texture2D NewItem { get; private set; }

            public static Texture2D Print { get; private set; }

            public static Texture2D ClearModifications { get; private set; }

            public static Texture2D UpArrowShort { get; private set; }

            public static Texture2D DownArrowShort { get; private set; }

            public static Texture2D LeftArrowShort { get; private set; }

            public static Texture2D RightArrowShort { get; private set; }

            public static Texture2D Lock { get; private set; }

            public static Texture2D Sync { get; private set; }

            public static Texture2D DiscordIcon { get; private set; }

            public static Texture2D Reset { get; private set; }

            public static Texture2D Checkbox { get; private set; }

            public static Texture2D CircleSelect { get; private set; }

            public static Texture2D FlatButton { get; private set; }

            public static Texture2D FlatButtonSmall { get; private set; }

            public static Texture2D RoundButton { get; private set; }

            public static Texture2D RoundButtonSmall { get; private set; }

            public static Texture2D HorizontalLine { get; private set; }

            public static Texture2D VerticalLine { get; private set; }

            public static Texture2D LineTextbox { get; private set; }

            public static Texture2D Textbox { get; private set; }

            public static Texture2D WindowBackground { get; private set; }

            public static Texture2D WindowBorder { get; private set; }

            public static Texture2D X { get; private set; }

            public static Texture2D Caret { get; private set; }

            public static Texture2D Air { get; private set; }

            public static void Load()
            {
                ModifyItem = ModContent.GetTexture("ItemModifier/UI/ModifyItem");
                Settings = ModContent.GetTexture("ItemModifier/UI/Settings");
                Wiki = ModContent.GetTexture("ItemModifier/UI/Wiki");
                ChangelogIcon = ModContent.GetTexture("ItemModifier/UI/Changelog");
                Save = ModContent.GetTexture("ItemModifier/UI/Save");
                UpArrow = ModContent.GetTexture("ItemModifier/UI/UpArrow");
                DownArrow = ModContent.GetTexture("ItemModifier/UI/DownArrow");
                LeftArrow = ModContent.GetTexture("ItemModifier/UI/LeftArrow");
                RightArrow = ModContent.GetTexture("ItemModifier/UI/RightArrow");
                NewItem = ModContent.GetTexture("ItemModifier/UI/NewItem");
                Print = ModContent.GetTexture("ItemModifier/UI/Print");
                ClearModifications = ModContent.GetTexture("ItemModifier/UI/ClearModifications");
                UpArrowShort = ModContent.GetTexture("ItemModifier/UI/UpArrowShort");
                DownArrowShort = ModContent.GetTexture("ItemModifier/UI/DownArrowShort");
                LeftArrowShort = ModContent.GetTexture("ItemModifier/UI/LeftArrowShort");
                RightArrowShort = ModContent.GetTexture("ItemModifier/UI/RightArrowShort");
                Lock = ModContent.GetTexture("ItemModifier/UI/Lock");
                Sync = ModContent.GetTexture("ItemModifier/UI/Sync");
                DiscordIcon = ModContent.GetTexture("ItemModifier/UI/DiscordIcon");
                Reset = ModContent.GetTexture("ItemModifier/UI/Reset");
                Checkbox = ModContent.GetTexture("ItemModifier/UIKit/Checkbox");
                CircleSelect = ModContent.GetTexture("ItemModifier/UIKit/CircleSelect");
                FlatButton = ModContent.GetTexture("ItemModifier/UIKit/FlatButton");
                FlatButtonSmall = ModContent.GetTexture("ItemModifier/UIKit/FlatButtonSmall");
                RoundButton = ModContent.GetTexture("ItemModifier/UIKit/RoundButton");
                RoundButtonSmall = ModContent.GetTexture("ItemModifier/UIKit/RoundButtonSmall");
                HorizontalLine = ModContent.GetTexture("ItemModifier/UIKit/HorizontalLine");
                LineTextbox = ModContent.GetTexture("ItemModifier/UIKit/LineTextbox");
                Textbox = ModContent.GetTexture("ItemModifier/UIKit/Textbox");
                WindowBackground = ModContent.GetTexture("ItemModifier/UIKit/WindowBackground");
                WindowBorder = ModContent.GetTexture("ItemModifier/UIKit/WindowBorder");
                X = ModContent.GetTexture("ItemModifier/UIKit/X");
                VerticalLine = ModContent.GetTexture("ItemModifier/UIKit/VerticalLine");
                Caret = ModContent.GetTexture("ItemModifier/UIKit/Caret");
                Air = ModContent.GetTexture("ItemModifier/UIKit/Air");
            }

            public static void Unload()
            {
                ModifyItem = null;
                Settings = null;
                Wiki = null;
                ChangelogIcon = null;
                Save = null;
                UpArrow = null;
                DownArrow = null;
                LeftArrow = null;
                RightArrow = null;
                NewItem = null;
                Print = null;
                ClearModifications = null;
                UpArrowShort = null;
                DownArrowShort = null;
                LeftArrowShort = null;
                RightArrowShort = null;
                Lock = null;
                Sync = null;
                DiscordIcon = null;
                Reset = null;
                Checkbox = null;
                CircleSelect = null;
                FlatButton = null;
                FlatButtonSmall = null;
                RoundButton = null;
                RoundButtonSmall = null;
                HorizontalLine = null;
                LineTextbox = null;
                Textbox = null;
                WindowBackground = null;
                WindowBorder = null;
                X = null;
                VerticalLine = null;
                Caret = null;
                Air = null;
            }
        }

        public static ItemModifier Instance { get; private set; }

        internal MainInterface MainUI;

        public static readonly string WikiPage = "https://github.com/KryptonIon/ItemModifier/wiki";

        internal string Tooltip { get; set; }

        public bool MouseWheelDisabled { get; set; } = false;

        public bool ItemAtCursorDisabled { get; set; } = false;

        public bool DimensionsView { get; set; }

        public byte DimensionsType { get; set; }

        public static List<Changelog> Changelogs { get; private set; }

        public override void Load()
        {
            Instance = ModContent.GetInstance<ItemModifier>();

            Textures.Load();
            Changelogs = new List<Changelog> {
                Changelog.Read("ItemModifier/Changelogs/1.0.0.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.1.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.2.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.3.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.4.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.5.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.6.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.7.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.8.clog"),
                Changelog.Read("ItemModifier/Changelogs/1.0.8.1.clog")
                //Changelog.Read("ItemModifier/Changelogs/1.1.0.clog")
            };

            if (!Main.dedServ)
            {
                MainUI = new MainInterface();
                MainUI.Activate();
            }
        }

        public override void Unload()
        {
            Textures.Unload();
            Instance = null;
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
                if (mouseItemIndex != -1) layers.RemoveAt(mouseItemIndex);
            }
            int mouseIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (mouseIndex != -1)
            {
                mouseIndex -= 1;
                layers.Insert(mouseIndex, new LegacyGameInterfaceLayer(
                    "ItemModifier: ItemModifierUI",
                    delegate
                    {
                        MainUI.Draw(Main.spriteBatch);
                        Vector2 pos = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(16);
                        Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, Tooltip ?? "", pos.X, pos.Y, Color.White, Color.Black, Vector2.Zero);
                        Tooltip = null;
                        return true;
                    },
                    InterfaceScaleType.UI
                    ));
            }
        }
    }

    public static class ExtensionMethods
    {
        public static void CopyItemProperties(this Item Target, Item Origin)
        {
            Target.Prefix(Origin.prefix);
            Target.autoReuse = Origin.autoReuse;
            Target.consumable = Origin.consumable;
            Target.potion = Origin.potion;
            Target.melee = Origin.melee;
            Target.magic = Origin.magic;
            Target.ranged = Origin.ranged;
            Target.summon = Origin.summon;
            Target.thrown = Origin.thrown;
            Target.accessory = Origin.accessory;
            Target.damage = Origin.damage;
            Target.knockBack = Origin.damage;
            Target.crit = Origin.crit;
            Target.shoot = Origin.shoot;
            Target.shootSpeed = Origin.shootSpeed;
            Target.createTile = Origin.createTile;
            Target.tileBoost = Origin.tileBoost;
            Target.buffTime = Origin.buffTime;
            Target.buffType = Origin.buffType;
            Target.healLife = Origin.healLife;
            Target.healMana = Origin.healMana;
            Target.axe = Origin.axe;
            Target.pick = Origin.pick;
            Target.hammer = Origin.hammer;
            Target.maxStack = Origin.maxStack;
            Target.useTime = Origin.useTime;
            Target.useAnimation = Origin.useAnimation;
            Target.defense = Origin.defense;
            Target.melee = Origin.melee;
            Target.magic = Origin.magic;
            Target.ranged = Origin.ranged;
            Target.summon = Origin.summon;
            Target.thrown = Origin.thrown;
            Target.fishingPole = Origin.fishingPole;
            Target.scale = Origin.scale;
            Target.useStyle = Origin.useStyle;
        }
    }
}

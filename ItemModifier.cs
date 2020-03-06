using ItemModifier.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ItemModifier
{
    public class ItemModifier : Mod
    {
        public class Changelog
        {
            public static List<Changelog> Changelogs;

            public Version Version { get; }

            public string Title { get; }

            public string Website { get; }

            public Changelog(Version Version, List<string> RawUpdate, List<string> MarkdownUpdate, string Title = "", string Website = "")
            {
                this.Version = Version;
                this.Title = Title;
                this.Website = Website;
                this.RawUpdate = RawUpdate;
                this.MarkdownUpdate = MarkdownUpdate;
            }

            public List<string> RawUpdate { get; }

            public List<string> MarkdownUpdate { get; }

            internal static void Load()
            {
                Changelogs = new List<Changelog>
                {
                    new Changelog(new Version(1, 0, 0), new List<string> { "Added \"ShowProperties\" Setting", "Added \"ShowUnnecessary\" Setting", "Added \"ShowPID\" Setting", "Added \"CreateTile\" Command", "Added \"HealLife\" Command", "Added \"HealMana\" Command", "Added \"Shoot\" Command", "Added \"ShootSpeed\" Command", "Added \"UseTime\" Command", "Added \"UseAnimation\" Command", "Added \"GenerateItem\" Command", "Added \"Set\" Command", "Added \"Properties\" Command", "Added \"Settings\" Command", "Added Utilities", "Added EnterWorld Message" }, new List<string> { "# Additions", "> Settings:", "* Added `ShowProperties` Setting", "* Added `ShowUnnecessary` Setting", "* Added `ShowPID` Setting", "> Commands:", "* Added `CreateTile` Command", "* Added `HealLife` Command", "* Added `HealMana` Command", "* Added `Shoot` Command", "* Added `ShootSpeed` Command", "* Added `UseTime` Command", "* Added `UseAnimation` Command", "* Added `GenerateItem` Command", "* Added `Set` Command", "* Added `Properties` Command", "* Added `Settings` Command", "> Others:", "* Added Utilities", "* Added EnterWorld Message" }, "Demo Update", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.0"),
                    new Changelog(new Version(1, 0, 1), new List<string> { "Changed Description" }, new List<string> { "# Changes", "* Changed Description" }, "Fixes & Changes Update 1", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.1"),
                    new Changelog(new Version(1, 0, 2), new List<string> { "Changed Mod Homepage from Github Repository to Terraria Forums" }, new List<string> { "# Changes", "* Changed Mod Homepage from Github Repository to Terraria Forums" }, "Fixes & Changes Update 2", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.2"),
                    new Changelog(new Version(1, 0, 3), new List<string> { "Changed Description" }, new List<string> { "# Changes", "* Changed Description" }, "Fixes & Changes Update 3", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.3"),
                    new Changelog(new Version(1, 0, 4), new List<string> { "Added ShowEWMessage Setting", "Added Another EnterWorld Message", "Changed Description" }, new List<string> { "# Additions", "> Settings:", "* Added `ShowEWMessage` Setting", "> Others:", "* Added Another EnterWorld Message", "", "# Changes", "* Changed Description" }, "Mini Update 1", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.4"),
                    new Changelog(new Version(1, 0, 5), new List<string> { "Added \"TileBoost(+Range)\" Command", "Added \"Pickaxe Power\" Command", "Added \"Axe Power\" Command", "Added \"Hammer Power\" Command", "Added \"Knockback\" Command", "Added \"Damage\" Command", "Added \"Critical Strike Chance\" Command", "Added \"AutoReuse\" Command", "Added \"Reset\" Command", "Added Settings List", "Changed \"Properties\" Command trigger from \"p\" to \"properties\"", "Changed Description", "Fixed EnterWorld Message 2 being white" }, new List<string> { "# Additions", "> Commands:", "* Added `TileBoost(+Range)` Command", "* Added `Pickaxe Power` Command", "* Added `Axe Power` Command", "* Added `Hammer Power` Command", "* Added `Knockback` Command", "* Added `Damage` Command", "* Added `Critical Strike Chance` Command", "* Added `AutoReuse` Command", "* Added `Reset` Command", "* Added Settings List(/settings)", "", "# Changes", "* Changed `Properties` Command trigger from `p` to `properties`", "* Changed Description", "", "# Fixes", "* Fixed EnterWorld Message 2 being white" }, "Tweaks Update 1", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.5"),
                    new Changelog(new Version(1, 0, 6), new List<string> { "Updated Parameters of \"/gi params\" and \"/set params\" (Sorry, forgot to do so last update)" }, new List<string> { "# Changes", "* Changed Parameters of `/gi` params and `/set params` (Sorry, forgot to do so last update)" }, "Fixes & Changes Update 4", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.6"),
                    new Changelog(new Version(1, 0, 7), new List<string> { "Added Wiki Command", "Changed Enter World Messages", "Properties will refer to TileBoost as both +Range and TileBoost", "Description will now contain changelog of current version", "Removed ShowPID Setting", "Fixed Properties not having colons when Requesting Properties", "Fixed Properties not having colons when using Properties Command", "Fixed TileBoost not being properly Capitalized when Requesting Properties" }, new List<string> { "# Additions", "> Commands:", "Added `Wiki` Command", "", "# Changes", "Changed Enter World Messages", "Properties will refer to TileBoost as both +Range and TileBoost", "Description will now contain changelog of current version", "Removed ShowPID Setting", "", "# Fixes", "Fixed Properties not having colons when Requesting Properties", "Fixed Properties not having colons when using Properties Command", "Fixed TileBoost not being properly Capitalized when Requesting Properties" }, "Wiki Update", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.7"),
                    new Changelog(new Version(1, 0, 8), new List<string> { "Added \"AlwaysUseID\" Setting", "Added \"ShowResultList\" Setting", "Added \"GetRandomItem\" Setting", "Added \"ShowMaxStack\" Setting", "Added \"SetItem\" Command", "Added \"ID\" Command", "Added \"Buff\" Command", "Added \"BuffTime\" Command", "Added \"Stack\" Command", "Added \"Potion\" Command", "Added \"Consumable\" Command", "Added \"MaxStack\" Command", "Added new error message(0 shootspeed)(/shootspeed)", "Added Error Codes(Current Code Count: 5)", "Added Command Aliases", "Changed Changelog Format", "Changed GenerateItem Command behavior", "Changed (most)Error Messages", "Changed All Command Example Usages, Fields are now indicated with Brackets[] instead of Angular Brackets<>, Notes are now indicated with Parentheses() instead of Brackets[]", "Changed \"Settings\" Command trigger from \"/setting\" to \"/settings\"", "The ShootSpeed and Knockback Modifier now uses Floating-point numbers(float) instead of Integers(int), which means decimals can now be used", "Changed Settings Parameter from \"/settings settings\" to \"/settings list\"", "Settings Success Message Changed from \"Success! {SettingName} is now {SettingValue}\" to \"{SettingName} set to {SettingValue}\"", "Killed(\"Changed\") Settings Command's AutoCorrect", "(Internal)CSProj file is now removed from the mod(can still be acquired via Github)", "(Internal)Unified modification processes(fixes some inconsistency between commands eg /set and /shoot).", "Fixed Damage Property showing unnecessarily", "Fixed Shoot Command saying tile count rather than projectile count", "Fixed Errors having Reply Color" }, new List<string> { "# Additions", "> Settings:", "* Added `AlwaysUseID` Setting", "* Added `ShowResultList` Setting", "* Added `GetRandomItem` Setting", "* Added `ShowMaxStack` Setting", "> Commands:", "* Added `SetItem` Command", "* Added `ID` Command", "* Added `Buff` Command", "* Added `BuffTime` Command", "* Added `Stack` Command", "* Added `Potion` Command", "* Added `Consumable` Command", "* Added `MaxStack` Command", "> Others:", "* Added new error message(0 shootspeed)(/shootspeed)", "* Added Error Codes(Current Code Count: 5)", "* Added Command Aliases", "", "# Changes", "* Changed Changelog Format", "* Changed GenerateItem Command behavior", "* Changed (most)Error Messages", "* Changed All Command Example Usages, Fields are now indicated with Brackets[] instead of Angular Brackets<>, Notes are now indicated with Parentheses() instead of Brackets[]", "* Changed `Settings` Command trigger from `/setting` to `/settings`", "* The ShootSpeed and Knockback Modifier now uses Floating-point numbers(float) instead of Integers(int), which means decimals can now be used", "* Changed Settings Parameter from `/settings settings` to `/settings list`", "* Settings Success Message Changed from `Success! {SettingName} is now {SettingValue}` to `{SettingName} set to {SettingValue}`", "* Killed(\"Changed\") `Settings` Command's AutoCorrect", "* (Internal)CSProj file is now removed from the mod(can still be acquired via Github)", "* (Internal)Unified modification processes(fixes some inconsistency between commands eg /set and /shoot).", "", "# Fixes", "* Fixed Damage Property showing unnecessarily", "* Fixed Shoot Command saying tile count rather than projectile count", "* Fixed Errors having Reply Color" }, "1.0 Final Update", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.8"),
                    new Changelog(new Version(1, 0, 8, 1), new List<string> { "Fixed HealLife parsing", "Fixed SetItem $^" }, new List<string> { "# Fixes", "* Fixed HealLife parsing", "* Fixed SetItem $^" }, "Fixes and Changes 5", "https://github.com/KryptonIon/ItemModifier/wiki/1.0.8.1")
                };
            }

            internal static void Unload()
            {
                Changelogs = null;
            }

            public override string ToString()
            {
                return ToString();
            }

            public string ToString(bool Raw = true)
            {
                return $"{Version} {Title} \n{string.Join("\n", Raw ? RawUpdate : MarkdownUpdate)}";
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

        public override void Load()
        {
            Instance = ModContent.GetInstance<ItemModifier>();

            Textures.Load();
            Changelog.Load();

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
            //Target.Prefix(Origin.prefix);
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

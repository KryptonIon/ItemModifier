using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static ItemModifier.ItemModifier;

namespace ItemModifier.UI
{
    public class ItemModifyUIW : UIWindow
    {
        private Item DefaultItem = new Item();

        internal Item ModifiedItem
        {
            get
            {
                Item modifiedItem = new Item
                {
                    autoReuse = AutoReuse.Value,
                    consumable = Consumable.Value,
                    potion = Potion.Value,
                    shoot = Shoot.Value,
                    createTile = Tile.Value,
                    tileBoost = TileBoost.Value,
                    buffType = Buff.Value,
                    buffTime = BuffTime.Value,
                    damage = Damage.Value,
                    crit = Critical.Value,
                    healLife = HealHP.Value,
                    healMana = HealMP.Value,
                    axe = AxePower.Value,
                    pick = PickaxePower.Value,
                    hammer = HammerPower.Value,
                    stack = Stack.Value,
                    maxStack = MaxStack.Value,
                    useAnimation = UseAnimation.Value,
                    useTime = UseTime.Value,
                    shootSpeed = ShootSpeed.Value,
                    knockBack = KnockBack.Value,
                    accessory = Accessory.Value,
                    defense = Defense.Value,
                    melee = DamageType.Choices[0].Value,
                    magic = DamageType.Choices[1].Value,
                    ranged = DamageType.Choices[2].Value,
                    summon = DamageType.Choices[3].Value,
                    thrown = DamageType.Choices[4].Value,
                    fishingPole = FishingPower.Value,
                    scale = Scale.Value,
                    useStyle = UseStyle.Choices.FindIndex(choice => choice.ID == UseStyle.Selected.ID) + 1
                };
                return modifiedItem;
            }
        }

        internal UIBool AutoReuse;

        internal UIBool Consumable;

        internal UIBool Potion;

        internal UIBool Accessory;

        internal UIIntTextbox Shoot;

        internal UIIntTextbox Tile;

        internal UIIntTextbox TileBoost;

        internal UIIntTextbox Buff;

        internal UIIntTextbox BuffTime;

        internal UIIntTextbox Damage;

        internal UIIntTextbox Critical;

        internal UIIntTextbox HealHP;

        internal UIIntTextbox HealMP;

        internal UIIntTextbox AxePower;

        internal UIIntTextbox PickaxePower;

        internal UIIntTextbox HammerPower;

        internal UIIntTextbox Stack;

        internal UIIntTextbox MaxStack;

        internal UIIntTextbox UseAnimation;

        internal UIIntTextbox UseTime;

        internal UIIntTextbox Defense;

        internal UIIntTextbox FishingPower;

        //internal UIIntTextbox ColorTint;

        //internal UIIntTextbox UseSound;

        internal UIFloatTextbox ShootSpeed;

        internal UIFloatTextbox KnockBack;

        internal UIFloatTextbox Scale;

        internal UISelection DamageType;

        internal UISelection UseStyle;

        internal UICategory.UIProperty PAutoReuse;

        internal UICategory.UIProperty PConsumable;

        internal UICategory.UIProperty PPotion;

        internal UICategory.UIProperty PShoot;

        internal UICategory.UIProperty PTile;

        internal UICategory.UIProperty PTileBoost;

        internal UICategory.UIProperty PBuff;

        internal UICategory.UIProperty PBuffTime;

        internal UICategory.UIProperty PDamage;

        internal UICategory.UIProperty PCritical;

        internal UICategory.UIProperty PHealHP;

        internal UICategory.UIProperty PHealMP;

        internal UICategory.UIProperty PAxePower;

        internal UICategory.UIProperty PPickaxePower;

        internal UICategory.UIProperty PHammerPower;

        internal UICategory.UIProperty PStack;

        internal UICategory.UIProperty PMaxStack;

        internal UICategory.UIProperty PUseAnimation;

        internal UICategory.UIProperty PUseTime;

        internal UICategory.UIProperty PShootSpeed;

        internal UICategory.UIProperty PKnockBack;

        internal UICategory.UIProperty PAccessory;

        internal UICategory.UIProperty PDefense;

        internal UICategory.UIProperty PDamageType;

        internal UICategory.UIProperty PFishingPower;

        internal UICategory.UIProperty PScale;

        //internal UICategory.UIProperty PColorTint;

        internal UICategory.UIProperty PUseStyle;

        //internal UICategory.UIProperty PUseSound;

        internal UIImageButton PreviousCategory;

        internal UIImageButton NextCategory;

        internal UIImageButton ToggleLiveSync;

        internal UIText CategoryName;

        internal UIContainer GrayBG;

        internal UIImage LockImage;

        internal UIImage ClearModifications;

        internal UICategory AllCategory;

        internal UICategory ToolsCategory;

        internal UICategory WeaponsCategory;

        internal UICategory PotionsCategory;

        internal UICategory ArmorCategory;

        internal UICategory AccessoriesCategory;

        internal UIContainer CategoryContainer;

        internal List<UICategory> Categories;

        private int categoryIndex;

        internal int CategoryIndex
        {
            get
            {
                return categoryIndex;
            }

            set
            {
                categoryIndex = value < 0 ? Categories.Count - 1 : value >= Categories.Count ? 0 : value;
                CategoryContainer.RemoveAllChildren();
                Categories[CategoryIndex].AppendProperties(CategoryContainer);
                CategoryContainer.Recalculate();
                CategoryName.Text = Categories[CategoryIndex].Name;
                CategoryName.Left = new StyleDimension(-CategoryName.Width.Pixels * 0.5f, 0.5f);
            }
        }

        public StyleDimension[] PropertyHeights { get; } = new StyleDimension[]
        {
            new StyleDimension(0f),
            new StyleDimension(113f),
            new StyleDimension(226f),
            new StyleDimension(339f)
        };

        public bool LiveSync { get; set; } = true;

        public ItemModifyUIW() : base("Item Modifier")
        {
            InheritVisibility = false;
            Visible = false;
            Width = new StyleDimension(300f);
            Height = new StyleDimension(485f);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            CategoryName = new UIText("There's a problem")
            {
                SkipDescenderCheck = true
            };
            CategoryName.Left = new StyleDimension(-CategoryName.Width.Pixels * 0.5f, 0.5f);
            CategoryName.Parent = this;

            PreviousCategory = new UIImageButton(Textures.LeftArrow)
            {
                ColorTint = new Color(0, 100, 255),
                Parent = this
            };
            PreviousCategory.OnLeftClick += (source, e) => CategoryIndex--;
            PreviousCategory.OnRightClick += (source, e) => CategoryIndex++;
            PreviousCategory.WhileMouseHover += (source, e) => Instance.Tooltip = "Previous Category";

            NextCategory = new UIImageButton(Textures.RightArrow)
            {
                ColorTint = new Color(255, 100, 0)
            };
            NextCategory.Left = new StyleDimension(Width.Pixels - NextCategory.Width.Pixels);
            NextCategory.Parent = this;
            NextCategory.OnLeftClick += (source, e) => CategoryIndex++;
            NextCategory.OnRightClick += (source, e) => CategoryIndex--;
            NextCategory.WhileMouseHover += (source, e) => Instance.Tooltip = "Next Category";

            AutoReuse = new UIBool();
            AutoReuse.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.autoReuse = value;
            AutoReuse.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.autoReuse = DefaultItem.autoReuse;
            PAutoReuse = new UICategory.UIProperty(Textures.AutoReuse, "Auto Use:", AutoReuse);

            Consumable = new UIBool();
            Consumable.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.consumable = value;
            Consumable.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.consumable = DefaultItem.consumable;
            PConsumable = new UICategory.UIProperty(Textures.Consumable, "Consumable:", Consumable);

            Potion = new UIBool();
            Potion.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.potion = value;
            Potion.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.potion = DefaultItem.potion;
            PPotion = new UICategory.UIProperty(Textures.PotionSickness, "Potion Sickness:", Potion);

            DamageType = new UISelection(default, "Melee", "Magic", "Ranged", "Summon", "Thrown");
            DamageType.Choices[0].OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.melee = value;
            DamageType.Choices[1].OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.magic = value;
            DamageType.Choices[2].OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.ranged = value;
            DamageType.Choices[3].OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.summon = value;
            DamageType.Choices[4].OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.thrown = value;
            DamageType.OnRightClick += (source, e) =>
            {
                Main.LocalPlayer.HeldItem.melee = DefaultItem.melee;
                Main.LocalPlayer.HeldItem.magic = DefaultItem.magic;
                Main.LocalPlayer.HeldItem.ranged = DefaultItem.ranged;
                Main.LocalPlayer.HeldItem.summon = DefaultItem.summon;
                Main.LocalPlayer.HeldItem.thrown = DefaultItem.thrown;
            };
            PDamageType = new UICategory.UIProperty(Textures.DamageType, "Damage Type:", DamageType);

            Accessory = new UIBool();
            Accessory.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.accessory = value;
            Accessory.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.accessory = DefaultItem.accessory;
            PAccessory = new UICategory.UIProperty(Textures.Accessory, "Accessory:", Accessory);

            Damage = new UIIntTextbox() { MinThreshold = -1 };
            Damage.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.damage = value;
            Damage.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.damage = DefaultItem.damage;
            PDamage = new UICategory.UIProperty(Textures.Damage, "Damage:", Damage);

            Critical = new UIIntTextbox();
            Critical.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.crit = value;
            Critical.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.crit = DefaultItem.crit;
            PCritical = new UICategory.UIProperty(Textures.CritChance, "Crit Chance:", Critical);

            KnockBack = new UIFloatTextbox();
            KnockBack.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.knockBack = value;
            KnockBack.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.knockBack = DefaultItem.knockBack;
            PKnockBack = new UICategory.UIProperty(Textures.Knockback, "KnockBack:", KnockBack);

            Shoot = new UIIntTextbox(0, ProjectileLoader.ProjectileCount - 1) { Sign = false, Negatable = false };
            Shoot.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.shoot = value;
            Shoot.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.shoot = DefaultItem.shoot;
            PShoot = new UICategory.UIProperty(Textures.ProjectileShot, "Projectile Shot:", Shoot);

            ShootSpeed = new UIFloatTextbox();
            ShootSpeed.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.shootSpeed = value;
            ShootSpeed.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.shootSpeed = DefaultItem.shootSpeed;
            PShootSpeed = new UICategory.UIProperty(Textures.ProjectileSpeed, "Shoot Speed:", ShootSpeed);

            Tile = new UIIntTextbox(-1, TileLoader.TileCount - 1);
            Tile.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.createTile = value;
            Tile.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.createTile = DefaultItem.createTile;
            PTile = new UICategory.UIProperty(Textures.CreateTile, "Place Tile:", Tile);

            TileBoost = new UIIntTextbox();
            TileBoost.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.tileBoost = value;
            TileBoost.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.tileBoost = DefaultItem.tileBoost;
            PTileBoost = new UICategory.UIProperty(Textures.AddedRange, "Added Range:", TileBoost);
            PTileBoost.Recalculate();

            Buff = new UIIntTextbox(0, BuffLoader.BuffCount - 1) { Sign = false, Negatable = false };
            Buff.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.buffType = value;
            Buff.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.buffType = DefaultItem.buffType;
            PBuff = new UICategory.UIProperty(Textures.BuffType, "Buff Inflicted:", Buff);

            BuffTime = new UIIntTextbox() { Sign = false, Negatable = false };
            BuffTime.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.buffTime = value;
            BuffTime.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.buffTime = DefaultItem.buffTime;
            PBuffTime = new UICategory.UIProperty(Textures.BuffDuration, "Buff Duration:", BuffTime);

            HealHP = new UIIntTextbox() { Sign = false, Negatable = false };
            HealHP.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.healLife = value;
            HealHP.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.healLife = DefaultItem.healLife;
            PHealHP = new UICategory.UIProperty(Textures.HPHealed, "HP Healed:", HealHP);

            HealMP = new UIIntTextbox() { Sign = false, Negatable = false };
            HealMP.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.healMana = value;
            HealMP.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.healMana = DefaultItem.healMana;
            PHealMP = new UICategory.UIProperty(Textures.MPHealed, "Mana Healed:", HealMP);

            AxePower = new UIIntTextbox() { Sign = false, Negatable = false };
            AxePower.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.axe = value;
            AxePower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.axe = DefaultItem.axe;
            PAxePower = new UICategory.UIProperty(Textures.AxePower, "Axe Power:", AxePower);

            PickaxePower = new UIIntTextbox() { Sign = false, Negatable = false };
            PickaxePower.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.pick = value;
            PickaxePower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.pick = DefaultItem.pick;
            PPickaxePower = new UICategory.UIProperty(Textures.PickaxePower, "Pickaxe Power:", PickaxePower);

            HammerPower = new UIIntTextbox() { Sign = false, Negatable = false };
            HammerPower.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.hammer = value;
            HammerPower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.hammer = DefaultItem.hammer;
            PHammerPower = new UICategory.UIProperty(Textures.HammerPower, "Hammer Power:", HammerPower);

            Stack = new UIIntTextbox() { Sign = false, Negatable = false };
            Stack.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.stack = value;
            Stack.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.stack = DefaultItem.stack;
            PStack = new UICategory.UIProperty(Textures.Stack, "Amount:", Stack);

            MaxStack = new UIIntTextbox() { Sign = false, Negatable = false };
            MaxStack.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.maxStack = value;
            MaxStack.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.maxStack = DefaultItem.maxStack;
            PMaxStack = new UICategory.UIProperty(Textures.MaxStack, "Max Stack:", MaxStack);

            UseAnimation = new UIIntTextbox() { Sign = false, Negatable = false };
            UseAnimation.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.useAnimation = value;
            UseAnimation.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.useAnimation = DefaultItem.useAnimation;
            PUseAnimation = new UICategory.UIProperty(Textures.UseAnimation, "Animation Duration:", UseAnimation);

            UseTime = new UIIntTextbox() { Sign = false, Negatable = false };
            UseTime.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.useTime = value;
            UseTime.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.useTime = DefaultItem.useTime;
            PUseTime = new UICategory.UIProperty(Textures.UseTime, "Use Duration:", UseTime);

            Defense = new UIIntTextbox();
            Defense.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.defense = value;
            Defense.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.defense = DefaultItem.defense;
            PDefense = new UICategory.UIProperty(Textures.Defense, "Defense:", Defense);

            FishingPower = new UIIntTextbox();
            FishingPower.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.fishingPole = value;
            FishingPower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.fishingPole = DefaultItem.fishingPole;
            PFishingPower = new UICategory.UIProperty(Textures.FishingPower, "Fishing Power", FishingPower);

            Scale = new UIFloatTextbox();
            Scale.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.scale = value;
            Scale.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.scale = DefaultItem.scale;
            PScale = new UICategory.UIProperty(Textures.ItemScale, "Item Scale", Scale);

            UseStyle = new UISelection(default, "Swing", "Drink", "Stab", "Above Head", "Held");
            UseStyle.OnSelectedChanged += (source, newSelected) => Main.LocalPlayer.HeldItem.useStyle = UseStyle.Choices.FindIndex(choice => choice.ID == newSelected.ID) + 1;
            PUseStyle = new UICategory.UIProperty(Textures.UseStyle, "Use Style", UseStyle);

            ToggleLiveSync = new UIImageButton(Textures.Sync, new Color(20, 255, 20))
            {
                Width = new StyleDimension(16f),
                Height = new StyleDimension(16f),
                Top = new StyleDimension(-19f)
            };
            ToggleLiveSync.Left = new StyleDimension(Width.Pixels - CloseButton.Width.Pixels - ToggleLiveSync.Width.Pixels - 6); // -3 spacing, -3 spacing
            ToggleLiveSync.Parent = this;
            ToggleLiveSync.OnLeftClick += (source, e) => { LiveSync = !LiveSync; if (!LiveSync) { GrayBG.Visible = false; } };
            ToggleLiveSync.WhileMouseHover += (source, e) => Instance.Tooltip = "Toggle Live Sync";

            ClearModifications = new UIImageButton(Textures.ClearModifications)
            {
                Width = new StyleDimension(16f),
                Height = new StyleDimension(16f),
                Top = new StyleDimension(-19f)
            };
            ClearModifications.Left = new StyleDimension(Width.Pixels - CloseButton.Width.Pixels - ToggleLiveSync.Width.Pixels - ClearModifications.Width.Pixels - 9); // -3 spacing, -3 spacing, -3 spacing
            ClearModifications.Parent = this;
            ClearModifications.OnLeftClick += (source, e) => Main.LocalPlayer.HeldItem.SetDefaults(Main.LocalPlayer.HeldItem.type);
            ClearModifications.WhileMouseHover += (source, e) => Instance.Tooltip = "Clear Modifications";

            AllCategory = new UICategory("All", new List<UICategory.UIProperty> {
                PAutoReuse,
                PAxePower,
                PPickaxePower,
                PHammerPower,
                PDamageType,
                PDamage,
                PCritical,
                PKnockBack,
                PShoot,
                PShootSpeed,
                PAccessory,
                PDefense,
                PTileBoost,
                PTile,
                PConsumable,
                PPotion,
                PBuff,
                PBuffTime,
                PHealHP,
                PHealMP,
                PStack,
                PMaxStack,
                PUseAnimation,
                PUseTime,
                PUseStyle,
                PFishingPower,
                PScale
            });

            ToolsCategory = new UICategory("Tools", new List<UICategory.UIProperty>
            {
                PAutoReuse,
                PAxePower,
                PPickaxePower,
                PHammerPower,
                PUseAnimation,
                PUseTime,
                PUseStyle,
                PFishingPower
            });

            WeaponsCategory = new UICategory("Weapons", new List<UICategory.UIProperty>
            {
                PAutoReuse,
                PDamageType,
                PDamage,
                PCritical,
                PKnockBack,
                PConsumable,
                PShoot,
                PShootSpeed,
                PUseAnimation,
                PUseAnimation,
                PUseStyle
            });

            PotionsCategory = new UICategory("Potions", new List<UICategory.UIProperty>
            {
                PConsumable,
                PPotion,
                PBuff,
                PBuffTime,
                PHealHP,
                PHealMP,
                PUseStyle
            });

            ArmorCategory = new UICategory("Armor", new List<UICategory.UIProperty>
            {
                PDefense
            });

            AccessoriesCategory = new UICategory("Accessories", new List<UICategory.UIProperty>
            {
                PAccessory,
                PDefense
            });

            Categories = new List<UICategory>
            {
                AllCategory,
                ToolsCategory,
                WeaponsCategory,
                PotionsCategory,
                ArmorCategory,
                AccessoriesCategory
            };

            CategoryContainer = new UIContainer(new Vector2(290f, 450f))
            {
                Parent = this,
                Left = new StyleDimension(5f),
                Top = new StyleDimension(30f),
                OverflowHidden = true
            };

            GrayBG = new UIContainer(new Color(47, 79, 79, 150), new Vector2(InnerDimensions.Width, InnerDimensions.Height))
            {
                InheritVisibility = false,
                Parent = this
            };
            GrayBG.OnVisibilityChanged += (source, value) => LockImage.Visible = value;

            LockImage = new UIImage(Textures.Lock);
            LockImage.Left = new StyleDimension((GrayBG.Width.Pixels - LockImage.Width.Pixels) * 0.5f);
            LockImage.Top = new StyleDimension((GrayBG.Height.Pixels - LockImage.Height.Pixels) * 0.5f);
            LockImage.Parent = GrayBG;

            CategoryIndex = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (DefaultItem.type != Main.LocalPlayer.HeldItem.type) DefaultItem.SetDefaults(Main.LocalPlayer.HeldItem.type);

            if (LiveSync)
            {
                AutoReuse.Value = Main.LocalPlayer.HeldItem.autoReuse;
                Consumable.Value = Main.LocalPlayer.HeldItem.consumable;
                Potion.Value = Main.LocalPlayer.HeldItem.potion;
                Accessory.Value = Main.LocalPlayer.HeldItem.accessory;
                DamageType.Choices[0].Value = Main.LocalPlayer.HeldItem.melee;
                DamageType.Choices[1].Value = Main.LocalPlayer.HeldItem.magic;
                DamageType.Choices[2].Value = Main.LocalPlayer.HeldItem.ranged;
                DamageType.Choices[3].Value = Main.LocalPlayer.HeldItem.summon;
                DamageType.Choices[4].Value = Main.LocalPlayer.HeldItem.thrown;
                if (!Shoot.Focused) Shoot.Value = Main.LocalPlayer.HeldItem.shoot;
                if (!Tile.Focused) Tile.Value = Main.LocalPlayer.HeldItem.createTile;
                if (!TileBoost.Focused) TileBoost.Value = Main.LocalPlayer.HeldItem.tileBoost;
                if (!Buff.Focused) Buff.Value = Main.LocalPlayer.HeldItem.buffType;
                if (!BuffTime.Focused) BuffTime.Value = Main.LocalPlayer.HeldItem.buffTime;
                if (!Damage.Focused) Damage.Value = Main.LocalPlayer.HeldItem.damage;
                if (!Critical.Focused) Critical.Value = Main.LocalPlayer.HeldItem.crit;
                if (!ShootSpeed.Focused) ShootSpeed.Value = Main.LocalPlayer.HeldItem.shootSpeed;
                if (!KnockBack.Focused) KnockBack.Value = Main.LocalPlayer.HeldItem.knockBack;
                if (!HealHP.Focused) HealHP.Value = Main.LocalPlayer.HeldItem.healLife;
                if (!HealMP.Focused) HealMP.Value = Main.LocalPlayer.HeldItem.healMana;
                if (!AxePower.Focused) AxePower.Value = Main.LocalPlayer.HeldItem.axe;
                if (!PickaxePower.Focused) PickaxePower.Value = Main.LocalPlayer.HeldItem.pick;
                if (!HammerPower.Focused) HammerPower.Value = Main.LocalPlayer.HeldItem.hammer;
                if (!Stack.Focused) Stack.Value = Main.LocalPlayer.HeldItem.stack;
                if (!MaxStack.Focused) MaxStack.Value = Main.LocalPlayer.HeldItem.maxStack;
                if (!UseAnimation.Focused) UseAnimation.Value = Main.LocalPlayer.HeldItem.useAnimation;
                if (!UseTime.Focused) UseTime.Value = Main.LocalPlayer.HeldItem.useTime;
                if (!Defense.Focused) Defense.Value = Main.LocalPlayer.HeldItem.defense;
                if (!FishingPower.Focused) FishingPower.Value = Main.LocalPlayer.HeldItem.fishingPole;
                if (!Scale.Focused) Scale.Value = Main.LocalPlayer.HeldItem.scale;
                if (Main.LocalPlayer.HeldItem.useStyle == 0) UseStyle.DeselectAll();
                else UseStyle.Select(Main.LocalPlayer.HeldItem.useStyle - 1);
                GrayBG.Visible = Main.LocalPlayer.HeldItem.type == 0;
            }
        }
    }
}

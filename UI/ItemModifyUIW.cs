using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.UI
{
    public class ItemModifyUIW : UIWindow
    {
        public event UIEventHandler<int> OnSelectedIndexChanged;
        internal List<UICategory> Categories;
        private Item MouseItem;
        private Item DefaultItem;
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
        internal UICategory AllCategory;
        internal UICategory PotionsCategory;
        internal UICategory ToolsCategory;
        internal UICategory WeaponsCategory;
        internal UICategory ArmorCategory;
        internal UICategory AccessoryCategory;
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
        public bool LiveSync { get; set; } = true;
        public UICategory ActiveCategory
        {
            get
            {
                for (int i = 0; i < Categories.Count; i++)
                {
                    if (Categories[i].Visible)
                    {
                        return Categories[i];
                    }
                }
                return null;
            }
        }

        private int categoryIndex;
        public int CategoryIndex
        {
            get => categoryIndex;

            set
            {
                categoryIndex = value > Categories.Count - 1 ? 0 : value < 0 ? Categories.Count - 1 : value;
                UpdateCategory();
                OnSelectedIndexChanged?.Invoke(this, categoryIndex);
            }
        }

        public ItemModifyUIW() : base("Item Modifier")
        {
            InheritVisibility = false;
            Visible = false;
            Width = new StyleDimension(740f);
            Height = new StyleDimension(325f);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            MouseItem = new Item();
            DefaultItem = new Item();
            Categories = new List<UICategory>();

            CategoryName = new UIText("");
            CategoryName.Left = new StyleDimension((Width.Pixels - CategoryName.Width.Pixels) * 0.5f);
            CategoryName.Parent = this;

            PreviousCategory = new UIImageButton(ItemModifier.Textures.LeftArrow) { ColorTint = new Color(0, 100, 255) };
            PreviousCategory.OnLeftClick += (source, e) => CategoryIndex -= 1;
            PreviousCategory.OnRightClick += (source, e) => CategoryIndex += 1;
            PreviousCategory.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Previous Category";
            PreviousCategory.Parent = this;

            NextCategory = new UIImageButton(ItemModifier.Textures.RightArrow) { ColorTint = new Color(255, 100, 0) };
            NextCategory.Left = new StyleDimension(Width.Pixels - NextCategory.Width.Pixels);
            NextCategory.OnLeftClick += (source, e) => CategoryIndex += 1;
            NextCategory.OnRightClick += (source, e) => CategoryIndex -= 1;
            NextCategory.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Next Category";
            NextCategory.Parent = this;

            AllCategory = new UICategory("All")
            {
                Top = new StyleDimension(22f),
                Width = new StyleDimension(Width.Pixels),
                Height = new StyleDimension(Height.Pixels - AllCategory.Top.Pixels),
                InheritVisibility = false,
                Visible = false,
                Parent = this
            };
            Categories.Add(AllCategory);

            ToolsCategory = new UICategory("Tools")
            {
                Top = new StyleDimension(22f),
                Width = new StyleDimension(InnerDimensions.Width),
                Height = new StyleDimension(InnerDimensions.Height - ToolsCategory.Top.Pixels),
                InheritVisibility = false,
                Visible = false,
                Parent = this
            };
            Categories.Add(ToolsCategory);

            WeaponsCategory = new UICategory("Weapons")
            {
                Top = new StyleDimension(22f),
                Width = new StyleDimension(Width.Pixels),
                Height = new StyleDimension(Height.Pixels - WeaponsCategory.Top.Pixels),
                InheritVisibility = false,
                Visible = false,
                Parent = this
            };
            Categories.Add(WeaponsCategory);

            PotionsCategory = new UICategory("Potions")
            {
                Top = new StyleDimension(22f),
                Width = new StyleDimension(Width.Pixels),
                Height = new StyleDimension(Height.Pixels - PotionsCategory.Top.Pixels),
                InheritVisibility = false,
                Visible = false,
                Parent = this
            };
            Categories.Add(PotionsCategory);

            ArmorCategory = new UICategory("Armor")
            {
                Top = new StyleDimension(22f),
                Width = new StyleDimension(Width.Pixels),
                Height = new StyleDimension(Height.Pixels - ArmorCategory.Top.Pixels),
                InheritVisibility = false,
                Visible = false,
                Parent = this
            };
            Categories.Add(ArmorCategory);

            AccessoryCategory = new UICategory("Accessories")
            {
                Top = new StyleDimension(22f),
                Width = new StyleDimension(Width.Pixels),
                Height = new StyleDimension(Height.Pixels - AccessoryCategory.Top.Pixels),
                InheritVisibility = false,
                Visible = false,
                Parent = this
            };
            Categories.Add(AccessoryCategory);

            AutoReuse = new UIBool();
            AutoReuse.OnValueChanged += (source, value) => MouseItem.autoReuse = value;
            AutoReuse.OnRightClick += (source, e) => MouseItem.autoReuse = DefaultItem.autoReuse;
            PAutoReuse = new UICategory.UIProperty("Auto Use:")
            {
                InputElement = AutoReuse
            };
            PAutoReuse.Recalculate();
            AllCategory.AddProperty(PAutoReuse);
            ToolsCategory.AddProperty(PAutoReuse);
            WeaponsCategory.AddProperty(PAutoReuse);

            Consumable = new UIBool();
            Consumable.OnValueChanged += (source, value) => MouseItem.consumable = value;
            Consumable.OnRightClick += (source, e) => MouseItem.consumable = DefaultItem.consumable;
            PConsumable = new UICategory.UIProperty("Consumable:")
            {
                InputElement = Consumable
            };
            PConsumable.Recalculate();
            AllCategory.AddProperty(PConsumable);
            PotionsCategory.AddProperty(PConsumable);
            WeaponsCategory.AddProperty(PConsumable);

            Potion = new UIBool();
            Potion.OnValueChanged += (source, value) => MouseItem.potion = value;
            Potion.OnRightClick += (source, e) => MouseItem.potion = DefaultItem.potion;
            PPotion = new UICategory.UIProperty("Potion Sickness:")
            {
                InputElement = Potion
            };
            PPotion.Recalculate();
            AllCategory.AddProperty(PPotion);
            PotionsCategory.AddProperty(PPotion);

            DamageType = new UISelection(default, "Melee", "Magic", "Ranged", "Summon", "Thrown");
            DamageType.Choices[0].OnValueChanged += (source, value) => MouseItem.melee = value;
            DamageType.Choices[1].OnValueChanged += (source, value) => MouseItem.magic = value;
            DamageType.Choices[2].OnValueChanged += (source, value) => MouseItem.ranged = value;
            DamageType.Choices[3].OnValueChanged += (source, value) => MouseItem.summon = value;
            DamageType.Choices[4].OnValueChanged += (source, value) => MouseItem.thrown = value;
            DamageType.OnRightClick += (source, e) =>
            {
                MouseItem.melee = DefaultItem.melee;
                MouseItem.magic = DefaultItem.magic;
                MouseItem.ranged = DefaultItem.ranged;
                MouseItem.summon = DefaultItem.summon;
                MouseItem.thrown = DefaultItem.thrown;
            };
            PDamageType = new UICategory.UIProperty("Damage Type:")
            {
                InputElement = DamageType
            };
            PDamageType.Recalculate();
            AllCategory.AddProperty(PDamageType);
            WeaponsCategory.AddProperty(PDamageType);

            Accessory = new UIBool();
            Accessory.OnValueChanged += (source, value) => MouseItem.accessory = value;
            Accessory.OnRightClick += (source, e) => MouseItem.accessory = DefaultItem.accessory;
            PAccessory = new UICategory.UIProperty("Accessory:")
            {
                InputElement = Accessory
            };
            PAccessory.Recalculate();
            AllCategory.AddProperty(PAccessory);
            AccessoryCategory.AddProperty(PAccessory);

            Damage = new UIIntTextbox() { MinThreshold = -1 };
            Damage.OnValueChanged += (source, value) => MouseItem.damage = value;
            Damage.OnRightClick += (source, e) => MouseItem.damage = DefaultItem.damage;
            PDamage = new UICategory.UIProperty("Damage:")
            {
                InputElement = Damage
            };
            PDamage.Recalculate();
            AllCategory.AddProperty(PDamage);
            WeaponsCategory.AddProperty(PDamage);

            Critical = new UIIntTextbox();
            Critical.OnValueChanged += (source, value) => MouseItem.crit = value;
            Critical.OnRightClick += (source, e) => MouseItem.crit = DefaultItem.crit;
            PCritical = new UICategory.UIProperty("Crit Chance:")
            {
                InputElement = Critical
            };
            PCritical.Recalculate();
            AllCategory.AddProperty(PCritical);
            WeaponsCategory.AddProperty(PCritical);

            KnockBack = new UIFloatTextbox();
            KnockBack.OnValueChanged += (source, value) => MouseItem.knockBack = value;
            KnockBack.OnRightClick += (source, e) => MouseItem.knockBack = DefaultItem.knockBack;
            PKnockBack = new UICategory.UIProperty("KnockBack:")
            {
                InputElement = KnockBack
            };
            PKnockBack.Recalculate();
            AllCategory.AddProperty(PKnockBack);
            WeaponsCategory.AddProperty(PKnockBack);

            Shoot = new UIIntTextbox(0, ProjectileLoader.ProjectileCount - 1) { Sign = false, Negatable = false };
            Shoot.OnValueChanged += (source, value) => MouseItem.shoot = value;
            Shoot.OnRightClick += (source, e) => MouseItem.shoot = DefaultItem.shoot;
            PShoot = new UICategory.UIProperty("Projectile Shot:")
            {
                InputElement = Shoot
            };
            PShoot.Recalculate();
            AllCategory.AddProperty(PShoot);
            WeaponsCategory.AddProperty(PShoot);

            ShootSpeed = new UIFloatTextbox();
            ShootSpeed.OnValueChanged += (source, value) => MouseItem.shootSpeed = value;
            ShootSpeed.OnRightClick += (source, e) => MouseItem.shootSpeed = DefaultItem.shootSpeed;
            PShootSpeed = new UICategory.UIProperty("Shoot Speed:")
            {
                InputElement = ShootSpeed
            };
            PShootSpeed.Recalculate();
            AllCategory.AddProperty(PShootSpeed);
            WeaponsCategory.AddProperty(PShootSpeed);

            Tile = new UIIntTextbox(-1, TileLoader.TileCount - 1);
            Tile.OnValueChanged += (source, value) => MouseItem.createTile = value;
            Tile.OnRightClick += (source, e) => MouseItem.createTile = DefaultItem.createTile;
            PTile = new UICategory.UIProperty("Place Tile:")
            {
                InputElement = Tile
            };
            PTile.Recalculate();
            AllCategory.AddProperty(PTile);

            TileBoost = new UIIntTextbox();
            TileBoost.OnValueChanged += (source, value) => MouseItem.tileBoost = value;
            TileBoost.OnRightClick += (source, e) => MouseItem.tileBoost = DefaultItem.tileBoost;
            PTileBoost = new UICategory.UIProperty("Added Range:")
            {
                InputElement = TileBoost
            };
            PTileBoost.Recalculate();
            AllCategory.AddProperty(PTileBoost);

            Buff = new UIIntTextbox(0, BuffLoader.BuffCount - 1) { Sign = false, Negatable = false };
            Buff.OnValueChanged += (source, value) => MouseItem.buffType = value;
            Buff.OnRightClick += (source, e) => MouseItem.buffType = DefaultItem.buffType;
            PBuff = new UICategory.UIProperty("Buff Inflicted:")
            {
                InputElement = Buff
            };
            PBuff.Recalculate();
            AllCategory.AddProperty(PBuff);
            PotionsCategory.AddProperty(PBuff);

            BuffTime = new UIIntTextbox() { Sign = false, Negatable = false };
            BuffTime.OnValueChanged += (source, value) => MouseItem.buffTime = value;
            BuffTime.OnRightClick += (source, e) => MouseItem.buffTime = DefaultItem.buffTime;
            PBuffTime = new UICategory.UIProperty("Buff Duration:")
            {
                InputElement = BuffTime
            };
            PBuffTime.Recalculate();
            AllCategory.AddProperty(PBuffTime);
            PotionsCategory.AddProperty(PBuffTime);

            HealHP = new UIIntTextbox() { Sign = false, Negatable = false };
            HealHP.OnValueChanged += (source, value) => MouseItem.healLife = value;
            HealHP.OnRightClick += (source, e) => MouseItem.healLife = DefaultItem.healLife;
            PHealHP = new UICategory.UIProperty("HP Healed:")
            {
                InputElement = HealHP
            };
            PHealHP.Recalculate();
            AllCategory.AddProperty(PHealHP);
            PotionsCategory.AddProperty(PHealHP);

            HealMP = new UIIntTextbox() { Sign = false, Negatable = false };
            HealMP.OnValueChanged += (source, value) => MouseItem.healMana = value;
            HealMP.OnRightClick += (source, e) => MouseItem.healMana = DefaultItem.healMana;
            PHealMP = new UICategory.UIProperty("Mana Healed:")
            {
                InputElement = HealMP
            };
            PHealMP.Recalculate();
            AllCategory.AddProperty(PHealMP);
            PotionsCategory.AddProperty(PHealMP);

            AxePower = new UIIntTextbox() { Sign = false, Negatable = false };
            AxePower.OnValueChanged += (source, value) => MouseItem.axe = value;
            AxePower.OnRightClick += (source, e) => MouseItem.axe = DefaultItem.axe;
            PAxePower = new UICategory.UIProperty("Axe Power:")
            {
                InputElement = AxePower
            };
            PAxePower.Recalculate();
            AllCategory.AddProperty(PAxePower);
            ToolsCategory.AddProperty(PAxePower);

            PickaxePower = new UIIntTextbox() { Sign = false, Negatable = false };
            PickaxePower.OnValueChanged += (source, value) => MouseItem.pick = value;
            PickaxePower.OnRightClick += (source, e) => MouseItem.pick = DefaultItem.pick;
            PPickaxePower = new UICategory.UIProperty("Pickaxe Power:")
            {
                InputElement = PickaxePower
            };
            PPickaxePower.Recalculate();
            AllCategory.AddProperty(PPickaxePower);
            ToolsCategory.AddProperty(PPickaxePower);

            HammerPower = new UIIntTextbox() { Sign = false, Negatable = false };
            HammerPower.OnValueChanged += (source, value) => MouseItem.hammer = value;
            HammerPower.OnRightClick += (source, e) => MouseItem.hammer = DefaultItem.hammer;
            PHammerPower = new UICategory.UIProperty("Hammer Power;")
            {
                InputElement = HammerPower
            };
            PHammerPower.Recalculate();
            AllCategory.AddProperty(PHammerPower);
            ToolsCategory.AddProperty(PHammerPower);

            Stack = new UIIntTextbox() { Sign = false, Negatable = false };
            Stack.OnValueChanged += (source, value) => MouseItem.stack = value;
            Stack.OnRightClick += (source, e) => MouseItem.stack = DefaultItem.stack;
            PStack = new UICategory.UIProperty("Amount:")
            {
                InputElement = Stack
            };
            AllCategory.AddProperty(PStack);

            MaxStack = new UIIntTextbox() { Sign = false, Negatable = false };
            MaxStack.OnValueChanged += (source, value) => MouseItem.maxStack = value;
            MaxStack.OnRightClick += (source, e) => MouseItem.maxStack = DefaultItem.maxStack;
            PMaxStack = new UICategory.UIProperty("Max Stack:")
            {
                InputElement = MaxStack
            };
            PMaxStack.Recalculate();
            AllCategory.AddProperty(PMaxStack);

            UseAnimation = new UIIntTextbox() { Sign = false, Negatable = false };
            UseAnimation.OnValueChanged += (source, value) => MouseItem.useAnimation = value;
            UseAnimation.OnRightClick += (source, e) => MouseItem.useAnimation = DefaultItem.useAnimation;
            PUseAnimation = new UICategory.UIProperty("Animation Duration:")
            {
                InputElement = UseAnimation
            };
            PUseAnimation.Recalculate();
            AllCategory.AddProperty(PUseAnimation);
            ToolsCategory.AddProperty(PUseAnimation);
            WeaponsCategory.AddProperty(PUseAnimation);

            UseTime = new UIIntTextbox() { Sign = false, Negatable = false };
            UseTime.OnValueChanged += (source, value) => MouseItem.useTime = value;
            UseTime.OnRightClick += (source, e) => MouseItem.useTime = DefaultItem.useTime;
            PUseTime = new UICategory.UIProperty("Use Duration:")
            {
                InputElement = UseTime
            };
            PUseTime.Recalculate();
            AllCategory.AddProperty(PUseTime);
            ToolsCategory.AddProperty(PUseTime);
            WeaponsCategory.AddProperty(PUseTime);

            Defense = new UIIntTextbox();
            Defense.OnValueChanged += (source, value) => MouseItem.defense = value;
            Defense.OnRightClick += (source, e) => MouseItem.defense = DefaultItem.defense;
            PDefense = new UICategory.UIProperty("Defense:")
            {
                InputElement = Defense
            };
            PDefense.Recalculate();
            AllCategory.AddProperty(PDefense);
            ArmorCategory.AddProperty(PDefense);
            AccessoryCategory.AddProperty(PDefense);

            FishingPower = new UIIntTextbox();
            FishingPower.OnValueChanged += (source, value) => MouseItem.fishingPole = value;
            FishingPower.OnRightClick += (source, e) => MouseItem.fishingPole = DefaultItem.fishingPole;
            PFishingPower = new UICategory.UIProperty("Fishing Power")
            {
                InputElement = FishingPower
            };
            PFishingPower.Recalculate();
            AllCategory.AddProperty(PFishingPower);
            ToolsCategory.AddProperty(PFishingPower);

            Scale = new UIFloatTextbox();
            Scale.OnValueChanged += (source, value) => MouseItem.scale = value;
            Scale.OnRightClick += (source, e) => MouseItem.scale = DefaultItem.scale;
            PScale = new UICategory.UIProperty("Item Scale")
            {
                InputElement = Scale
            };
            PScale.Recalculate();
            AllCategory.AddProperty(PScale);

            UseStyle = new UISelection(default, "Swing", "Drink", "Stab", "Above Head", "Held");
            UseStyle.OnSelectedChanged += (source, newSelected) => MouseItem.useStyle = UseStyle.Choices.FindIndex(choice => choice.ID == newSelected.ID) + 1;
            PUseStyle = new UICategory.UIProperty("Use Style")
            {
                InputElement = UseStyle
            };
            PUseStyle.Recalculate();
            AllCategory.AddProperty(PUseStyle);
            PotionsCategory.AddProperty(PUseStyle);
            ToolsCategory.AddProperty(PUseStyle);
            WeaponsCategory.AddProperty(PUseStyle);

            ToggleLiveSync = new UIImageButton(ItemModifier.Textures.Sync, new Color(20, 255, 20))
            {
                Width = new StyleDimension(16f),
                Height = new StyleDimension(16f),
                Left = new StyleDimension(Width.Pixels - 22f - ToggleLiveSync.Width.Pixels), // -16 for CloseButton, -3 for spacing before close button, -3 for spacing between this button and close button
                Top = new StyleDimension(-19f),
                Parent = this
            };
            ToggleLiveSync.OnLeftClick += (source, e) => { LiveSync = !LiveSync; if (!LiveSync) GrayBG.Visible = false; };

            GrayBG = new UIContainer(new Color(47, 79, 79, 150), new Vector2(InnerDimensions.Width, InnerDimensions.Height))
            {
                InheritVisibility = false
            };
            GrayBG.OnVisibilityChanged += (source, value) => LockImage.Visible = value;
            GrayBG.Parent = this;

            LockImage = new UIImage(ItemModifier.Textures.Lock);
            LockImage.Left = new StyleDimension((GrayBG.Width.Pixels - LockImage.Width.Pixels) * 0.5f);
            LockImage.Top = new StyleDimension((GrayBG.Height.Pixels - LockImage.Height.Pixels) * 0.5f);
            GrayBG.InheritVisibility = false;
            LockImage.Parent = GrayBG;

            AllCategory.Visible = true;
            UpdateCategory();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MouseItem = Main.LocalPlayer.HeldItem;
            if (DefaultItem.type != MouseItem.type) DefaultItem.SetDefaults(MouseItem.type);
            if (LiveSync)
            {
                AutoReuse.Value = MouseItem.autoReuse;
                Consumable.Value = MouseItem.consumable;
                Potion.Value = MouseItem.potion;
                Accessory.Value = MouseItem.accessory;
                DamageType.Choices[0].Value = MouseItem.melee;
                DamageType.Choices[1].Value = MouseItem.magic;
                DamageType.Choices[2].Value = MouseItem.ranged;
                DamageType.Choices[3].Value = MouseItem.summon;
                DamageType.Choices[4].Value = MouseItem.thrown;
                if (!Shoot.Focused) Shoot.Value = MouseItem.shoot;
                if (!Tile.Focused) Tile.Value = MouseItem.createTile;
                if (!TileBoost.Focused) TileBoost.Value = MouseItem.tileBoost;
                if (!Buff.Focused) Buff.Value = MouseItem.buffType;
                if (!BuffTime.Focused) BuffTime.Value = MouseItem.buffTime;
                if (!Damage.Focused) Damage.Value = MouseItem.damage;
                if (!Critical.Focused) Critical.Value = MouseItem.crit;
                if (!ShootSpeed.Focused) ShootSpeed.Value = MouseItem.shootSpeed;
                if (!KnockBack.Focused) KnockBack.Value = MouseItem.knockBack;
                if (!HealHP.Focused) HealHP.Value = MouseItem.healLife;
                if (!HealMP.Focused) HealMP.Value = MouseItem.healMana;
                if (!AxePower.Focused) AxePower.Value = MouseItem.axe;
                if (!PickaxePower.Focused) PickaxePower.Value = MouseItem.pick;
                if (!HammerPower.Focused) HammerPower.Value = MouseItem.hammer;
                if (!Stack.Focused) Stack.Value = MouseItem.stack;
                if (!MaxStack.Focused) MaxStack.Value = MouseItem.maxStack;
                if (!UseAnimation.Focused) UseAnimation.Value = MouseItem.useAnimation;
                if (!UseTime.Focused) UseTime.Value = MouseItem.useTime;
                if (!Defense.Focused) Defense.Value = MouseItem.defense;
                if (!FishingPower.Focused) FishingPower.Value = MouseItem.fishingPole;
                if (!Scale.Focused) Scale.Value = MouseItem.scale;
                if (MouseItem.useStyle == 0)
                {
                    UseStyle.DeselectAll();
                }
                else
                {
                    UseStyle.Select(MouseItem.useStyle - 1);
                }
                GrayBG.Visible = MouseItem.type == 0;
            }
        }

        private void UpdateCategory()
        {
            if (ActiveCategory != null) ActiveCategory.Visible = false;
            CategoryName.Text = Categories[CategoryIndex].Name;
            CategoryName.Left = new StyleDimension((Width.Pixels - CategoryName.Width.Pixels) * 0.5f);
            if (Categories[CategoryIndex] != null) Categories[CategoryIndex].Visible = true;
            Categories[CategoryIndex].GatherProperties();
        }
    }
}

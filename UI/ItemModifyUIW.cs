using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static ItemModifier.ItemModifier;

namespace ItemModifier.UI
{
    public class ItemModifyUIW : UIWindow
    {
        private Item DefaultItem { get; set; } = new Item();

        internal Item ModifiedItem
        {
            get
            {
                return new Item
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
                    melee = RMelee.Selected,
                    magic = RMagic.Selected,
                    ranged = RRanged.Selected,
                    summon = RSummon.Selected,
                    thrown = RThrown.Selected,
                    fishingPole = FishingPower.Value,
                    scale = Scale.Value,
                    useStyle = RSwing.Selected ? 1 : RDrink.Selected ? 2 : RSwing.Selected ? 3 : RAboveHead.Selected ? 4 : RHeld.Selected ? 5 : 0
                };
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

        internal UIContainer DamageType;

        internal UIRadioButton RMelee;

        internal UIRadioButton RMagic;

        internal UIRadioButton RRanged;

        internal UIRadioButton RSummon;

        internal UIRadioButton RThrown;

        internal UIContainer UseStyle;

        internal UIRadioButton RSwing;

        internal UIRadioButton RDrink;

        internal UIRadioButton RStab;

        internal UIRadioButton RAboveHead;

        internal UIRadioButton RHeld;

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
                //CategoryContainer.ScrollValue = 0;
                Categories[CategoryIndex].AppendProperties(CategoryContainer);
                CategoryContainer.Recalculate();
                CategoryName.Text = Categories[CategoryIndex].Name;
            }
        }

        public SizeDimension[] PropertyHeights { get; } = new SizeDimension[]
        {
            new SizeDimension(0f),
            new SizeDimension(113f),
            new SizeDimension(226f),
            new SizeDimension(339f)
        };

        public bool LiveSync { get; set; } = true;

        public ItemModifyUIW() : base("Item Modifier")
        {
            Visible = false;
            Width = new SizeDimension(300f);
            Height = new SizeDimension(485f);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();

            CategoryName = new UIText("There's a problem")
            {
                SkipDescenderCheck = true,
                HorizontalAlign = 0.5f
            };
            CategoryName.Parent = this;

            PreviousCategory = new UIImageButton(Textures.LeftArrow)
            {
                ColorTint = new Color(0, 100, 255),
                Parent = this
            };
            PreviousCategory.OnLeftClick += (source, e) => CategoryIndex--;
            PreviousCategory.OnRightClick += (source, e) => CategoryIndex++;
            PreviousCategory.WhileMouseHover += (source, e) => instance.Tooltip = "Previous Category";

            NextCategory = new UIImageButton(Textures.RightArrow)
            {
                ColorTint = new Color(255, 100, 0)
            };
            NextCategory.XOffset = new SizeDimension(InnerWidth - NextCategory.OuterWidth);
            NextCategory.Parent = this;
            NextCategory.OnLeftClick += (source, e) => CategoryIndex++;
            NextCategory.OnRightClick += (source, e) => CategoryIndex--;
            NextCategory.WhileMouseHover += (source, e) => instance.Tooltip = "Next Category";

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

            DamageType = new UIContainer();
            RMelee = new UIRadioButton("Melee") { Parent = DamageType };
            RMelee.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.melee = e;
            RMagic = new UIRadioButton("Magic") { Parent = DamageType };
            RMagic.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.magic = e;
            RRanged = new UIRadioButton("Ranged") { Parent = DamageType };
            RRanged.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.ranged = e;
            RSummon = new UIRadioButton("Summon") { Parent = DamageType };
            RSummon.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.summon = e;
            RThrown = new UIRadioButton("Thrown") { Parent = DamageType };
            RThrown.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.thrown = e;
            float dTypeWidth = 0f;
            for (int i = 0; i < DamageType.ChildrenCount; i++)
            {
                UIElement child = DamageType[i];
                if (child.InnerWidth > dTypeWidth)
                {
                    dTypeWidth = child.InnerWidth;
                }
                child.YOffset = new SizeDimension(RMelee.InnerHeight * i);
            }
            DamageType.Width = new SizeDimension(dTypeWidth);
            DamageType.Height = new SizeDimension(RMelee.InnerHeight * DamageType.ChildrenCount);
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

            Damage = new UIIntTextbox(minValue: -1);
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

            Shoot = new UIIntTextbox(0, ProjectileLoader.ProjectileCount - 1);
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

            Buff = new UIIntTextbox(0, BuffLoader.BuffCount - 1);
            Buff.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.buffType = value;
            Buff.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.buffType = DefaultItem.buffType;
            PBuff = new UICategory.UIProperty(Textures.BuffType, "Buff Inflicted:", Buff);

            BuffTime = new UIIntTextbox(0);
            BuffTime.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.buffTime = value;
            BuffTime.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.buffTime = DefaultItem.buffTime;
            PBuffTime = new UICategory.UIProperty(Textures.BuffDuration, "Buff Duration:", BuffTime);

            HealHP = new UIIntTextbox(0);
            HealHP.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.healLife = value;
            HealHP.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.healLife = DefaultItem.healLife;
            PHealHP = new UICategory.UIProperty(Textures.HPHealed, "HP Healed:", HealHP);

            HealMP = new UIIntTextbox(0);
            HealMP.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.healMana = value;
            HealMP.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.healMana = DefaultItem.healMana;
            PHealMP = new UICategory.UIProperty(Textures.MPHealed, "Mana Healed:", HealMP);

            AxePower = new UIIntTextbox(0);
            AxePower.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.axe = value;
            AxePower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.axe = DefaultItem.axe;
            PAxePower = new UICategory.UIProperty(Textures.AxePower, "Axe Power:", AxePower);

            PickaxePower = new UIIntTextbox(0);
            PickaxePower.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.pick = value;
            PickaxePower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.pick = DefaultItem.pick;
            PPickaxePower = new UICategory.UIProperty(Textures.PickaxePower, "Pickaxe Power:", PickaxePower);

            HammerPower = new UIIntTextbox(0);
            HammerPower.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.hammer = value;
            HammerPower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.hammer = DefaultItem.hammer;
            PHammerPower = new UICategory.UIProperty(Textures.HammerPower, "Hammer Power:", HammerPower);

            Stack = new UIIntTextbox(1);
            Stack.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.stack = value;
            Stack.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.stack = DefaultItem.stack;
            PStack = new UICategory.UIProperty(Textures.Stack, "Amount:", Stack);

            MaxStack = new UIIntTextbox(1);
            MaxStack.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.maxStack = value;
            MaxStack.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.maxStack = DefaultItem.maxStack;
            PMaxStack = new UICategory.UIProperty(Textures.MaxStack, "Max Stack:", MaxStack);

            UseAnimation = new UIIntTextbox(0);
            UseAnimation.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.useAnimation = value;
            UseAnimation.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.useAnimation = DefaultItem.useAnimation;
            PUseAnimation = new UICategory.UIProperty(Textures.UseAnimation, "Animation Span:", UseAnimation);

            UseTime = new UIIntTextbox(0);
            UseTime.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.useTime = value;
            UseTime.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.useTime = DefaultItem.useTime;
            PUseTime = new UICategory.UIProperty(Textures.UseTime, "Use Span:", UseTime);

            Defense = new UIIntTextbox();
            Defense.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.defense = value;
            Defense.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.defense = DefaultItem.defense;
            PDefense = new UICategory.UIProperty(Textures.Defense, "Defense:", Defense);

            FishingPower = new UIIntTextbox();
            FishingPower.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.fishingPole = value;
            FishingPower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.fishingPole = DefaultItem.fishingPole;
            PFishingPower = new UICategory.UIProperty(Textures.FishingPower, "Fishing Power:", FishingPower);

            Scale = new UIFloatTextbox();
            Scale.OnValueChanged += (source, value) => Main.LocalPlayer.HeldItem.scale = value;
            Scale.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.scale = DefaultItem.scale;
            PScale = new UICategory.UIProperty(Textures.ItemScale, "Item Scale:", Scale);

            UseStyle = new UIContainer();
            RSwing = new UIRadioButton("Swing") { Parent = UseStyle };
            RSwing.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.useStyle = 1;
            RDrink = new UIRadioButton("Drink") { Parent = UseStyle };
            RDrink.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.useStyle = 2;
            RStab = new UIRadioButton("Stab") { Parent = UseStyle };
            RStab.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.useStyle = 3;
            RAboveHead = new UIRadioButton("Above Head") { Parent = UseStyle };
            RAboveHead.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.useStyle = 4;
            RHeld = new UIRadioButton("Held") { Parent = UseStyle };
            RHeld.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.useStyle = 5;
            float uStyleWidth = 0f;
            for (int i = 0; i < UseStyle.ChildrenCount; i++)
            {
                UIElement child = UseStyle[i];
                if (child.InnerWidth > uStyleWidth)
                {
                    uStyleWidth = child.InnerWidth;
                }
                child.YOffset = new SizeDimension(RSwing.InnerHeight * i);
            }
            UseStyle.Width = new SizeDimension(uStyleWidth);
            UseStyle.Height = new SizeDimension(RSwing.InnerHeight * UseStyle.ChildrenCount);
            PUseStyle = new UICategory.UIProperty(Textures.UseStyle, "Use Style:", UseStyle);

            ToggleLiveSync = new UIImageButton(Textures.Sync, false, new Color(20, 255, 20))
            {
                Width = new SizeDimension(16f),
                Height = new SizeDimension(16f),
                YOffset = CloseButton.YOffset
            };
            ToggleLiveSync.Recalculate();
            ToggleLiveSync.XOffset = new SizeDimension(CloseButton.CalculatedXOffset - ToggleLiveSync.OuterWidth - 3); // -3 spacing
            ToggleLiveSync.Parent = this;
            ToggleLiveSync.OnLeftClick += (source, e) => { LiveSync = !LiveSync; if (!LiveSync) { GrayBG.Visible = false; } };
            ToggleLiveSync.WhileMouseHover += (source, e) => instance.Tooltip = "Toggle Live Sync";

            ClearModifications = new UIImageButton(Textures.ClearModifications, false)
            {
                Width = new SizeDimension(16f),
                Height = new SizeDimension(16f),
                YOffset = CloseButton.YOffset
            };
            ClearModifications.Recalculate();
            ClearModifications.XOffset = new SizeDimension(ToggleLiveSync.CalculatedXOffset - ClearModifications.OuterWidth - 3); // -3 spacing
            ClearModifications.Parent = this;
            ClearModifications.OnLeftClick += (source, e) => Main.LocalPlayer.HeldItem.SetDefaults(Main.LocalPlayer.HeldItem.type);
            ClearModifications.WhileMouseHover += (source, e) => instance.Tooltip = "Clear Modifications";

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

            CategoryContainer = new UIContainer()
            {
                Width = new SizeDimension(290f),
                Height = new SizeDimension(450f),
                XOffset = new SizeDimension(5f),
                YOffset = new SizeDimension(30f),
                OverflowHidden = true,
                Parent = this
            };
            CategoryContainer.OnChildAdded += (source, e) =>
            {
                if (source.ChildrenCount == 1)
                {
                    source[0].YOffset = new SizeDimension(0f);
                }
                else
                {
                    UIElement lastChild = source[source.ChildrenCount - 2];
                    source[source.ChildrenCount - 1].YOffset = new SizeDimension(lastChild.CalculatedYOffset + lastChild.OuterHeight);
                }
            };

            GrayBG = new UIContainer(new Color(47, 79, 79, 150))
            {
                Width = CategoryContainer.Width,
                Height = CategoryContainer.Height,
                XOffset = CategoryContainer.XOffset,
                YOffset = CategoryContainer.YOffset,
                Parent = this
            };
            GrayBG.OnVisibilityChanged += (source, value) => LockImage.Visible = value;

            LockImage = new UIImage(Textures.Lock);
            LockImage.XOffset = new SizeDimension((GrayBG.InnerWidth - LockImage.OuterWidth) * 0.5f);
            LockImage.YOffset = new SizeDimension((GrayBG.InnerHeight - LockImage.OuterHeight) * 0.5f);
            LockImage.Parent = GrayBG;

            CategoryIndex = 0;
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            if (DefaultItem.type != Main.LocalPlayer.HeldItem.type)
            {
                DefaultItem.SetDefaults(Main.LocalPlayer.HeldItem.type);
            }
            if (Visible && LiveSync)
            {
                AutoReuse.Value = Main.LocalPlayer.HeldItem.autoReuse;
                Consumable.Value = Main.LocalPlayer.HeldItem.consumable;
                Potion.Value = Main.LocalPlayer.HeldItem.potion;
                Accessory.Value = Main.LocalPlayer.HeldItem.accessory;
                RMelee.Selected = Main.LocalPlayer.HeldItem.melee;
                RMagic.Selected = Main.LocalPlayer.HeldItem.magic;
                RRanged.Selected = Main.LocalPlayer.HeldItem.ranged;
                RSummon.Selected = Main.LocalPlayer.HeldItem.summon;
                RThrown.Selected = Main.LocalPlayer.HeldItem.thrown;
                if (!Shoot.Focused)
                {
                    Shoot.Value = Main.LocalPlayer.HeldItem.shoot;
                }
                if (!Tile.Focused)
                {
                    Tile.Value = Main.LocalPlayer.HeldItem.createTile;
                }
                if (!TileBoost.Focused)
                {
                    TileBoost.Value = Main.LocalPlayer.HeldItem.tileBoost;
                }
                if (!Buff.Focused)
                {
                    Buff.Value = Main.LocalPlayer.HeldItem.buffType;
                }
                if (!BuffTime.Focused)
                {
                    BuffTime.Value = Main.LocalPlayer.HeldItem.buffTime;
                }
                if (!Damage.Focused)
                {
                    Damage.Value = Main.LocalPlayer.HeldItem.damage;
                }
                if (!Critical.Focused)
                {
                    Critical.Value = Main.LocalPlayer.HeldItem.crit;
                }
                if (!ShootSpeed.Focused)
                {
                    ShootSpeed.Value = Main.LocalPlayer.HeldItem.shootSpeed;
                }
                if (!KnockBack.Focused)
                {
                    KnockBack.Value = Main.LocalPlayer.HeldItem.knockBack;
                }
                if (!HealHP.Focused)
                {
                    HealHP.Value = Main.LocalPlayer.HeldItem.healLife;
                }
                if (!HealMP.Focused)
                {
                    HealMP.Value = Main.LocalPlayer.HeldItem.healMana;
                }
                if (!AxePower.Focused)
                {
                    AxePower.Value = Main.LocalPlayer.HeldItem.axe;
                }
                if (!PickaxePower.Focused)
                {
                    PickaxePower.Value = Main.LocalPlayer.HeldItem.pick;
                }
                if (!HammerPower.Focused)
                {
                    HammerPower.Value = Main.LocalPlayer.HeldItem.hammer;
                }
                if (!Stack.Focused)
                {
                    Stack.Value = Main.LocalPlayer.HeldItem.stack;
                }
                if (!MaxStack.Focused)
                {
                    MaxStack.Value = Main.LocalPlayer.HeldItem.maxStack;
                }
                if (!UseAnimation.Focused)
                {
                    UseAnimation.Value = Main.LocalPlayer.HeldItem.useAnimation;
                }
                if (!UseTime.Focused)
                {
                    UseTime.Value = Main.LocalPlayer.HeldItem.useTime;
                }
                if (!Defense.Focused)
                {
                    Defense.Value = Main.LocalPlayer.HeldItem.defense;
                }
                if (!FishingPower.Focused)
                {
                    FishingPower.Value = Main.LocalPlayer.HeldItem.fishingPole;
                }
                if (!Scale.Focused)
                {
                    Scale.Value = Main.LocalPlayer.HeldItem.scale;
                }
                if (Main.LocalPlayer.HeldItem.useStyle == 0)
                {
                    UseStyle.DeselectAllRadio();
                }
                else
                {
                    UseStyle.SelectRadio((UIRadioButton)UseStyle[Main.LocalPlayer.HeldItem.useStyle - 1]);
                }
                GrayBG.Visible = Main.LocalPlayer.HeldItem.type == 0;
            }
        }
    }
}

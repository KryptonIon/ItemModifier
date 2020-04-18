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

        internal UIRadioButtonContainer DamageType;

        internal UIRadioButton RMelee;

        internal UIRadioButton RMagic;

        internal UIRadioButton RRanged;

        internal UIRadioButton RSummon;

        internal UIRadioButton RThrown;

        internal UIRadioButtonContainer UseStyle;

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

        internal UIImageButton SaveItemConfig;

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
            bool limited = UIConfig.Instance.Limited;

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
            AutoReuse.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.autoReuse = e.Value;
            AutoReuse.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.autoReuse = DefaultItem.autoReuse;
            PAutoReuse = new UICategory.UIProperty(Textures.AutoReuse, "Auto Use:", AutoReuse);

            Consumable = new UIBool();
            Consumable.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.consumable = e.Value;
            Consumable.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.consumable = DefaultItem.consumable;
            PConsumable = new UICategory.UIProperty(Textures.Consumable, "Consumable:", Consumable);

            Potion = new UIBool();
            Potion.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.potion = e.Value;
            Potion.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.potion = DefaultItem.potion;
            PPotion = new UICategory.UIProperty(Textures.PotionSickness, "Potion Sickness:", Potion);

            DamageType = new UIRadioButtonContainer();
            RMelee = new UIRadioButton("Melee") { Parent = DamageType };
            RMelee.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.melee = e.Value;
            RMagic = new UIRadioButton("Magic") { Parent = DamageType };
            RMagic.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.magic = e.Value;
            RRanged = new UIRadioButton("Ranged") { Parent = DamageType };
            RRanged.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.ranged = e.Value;
            RSummon = new UIRadioButton("Summon") { Parent = DamageType };
            RSummon.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.summon = e.Value;
            RThrown = new UIRadioButton("Thrown") { Parent = DamageType };
            RThrown.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.thrown = e.Value;
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
            Accessory.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.accessory = e.Value;
            Accessory.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.accessory = DefaultItem.accessory;
            PAccessory = new UICategory.UIProperty(Textures.Accessory, "Accessory:", Accessory);

            Damage = limited ? new UIIntTextbox(-1) : new UIIntTextbox();
            Damage.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.damage = e.Value;
            Damage.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.damage = DefaultItem.damage;
            PDamage = new UICategory.UIProperty(Textures.Damage, "Damage:", Damage);

            Critical = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            Critical.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.crit = e.Value;
            Critical.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.crit = DefaultItem.crit;
            PCritical = new UICategory.UIProperty(Textures.CritChance, "Crit Chance:", Critical);

            KnockBack = new UIFloatTextbox();
            KnockBack.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.knockBack = e.Value;
            KnockBack.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.knockBack = DefaultItem.knockBack;
            PKnockBack = new UICategory.UIProperty(Textures.Knockback, "KnockBack:", KnockBack);

            Shoot = new UIIntTextbox(0, ProjectileLoader.ProjectileCount - 1);
            Shoot.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.shoot = e.Value;
            Shoot.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.shoot = DefaultItem.shoot;
            PShoot = new UICategory.UIProperty(Textures.ProjectileShot, "Projectile Shot:", Shoot);

            ShootSpeed = new UIFloatTextbox();
            ShootSpeed.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.shootSpeed = e.Value;
            ShootSpeed.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.shootSpeed = DefaultItem.shootSpeed;
            PShootSpeed = new UICategory.UIProperty(Textures.ProjectileSpeed, "Shoot Speed:", ShootSpeed);

            Tile = new UIIntTextbox(-1, TileLoader.TileCount - 1);
            Tile.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.createTile = e.Value;
            Tile.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.createTile = DefaultItem.createTile;
            PTile = new UICategory.UIProperty(Textures.CreateTile, "Place Tile:", Tile);

            TileBoost = limited ? new UIIntTextbox(sbyte.MinValue, sbyte.MaxValue) : new UIIntTextbox();
            TileBoost.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.tileBoost = e.Value;
            TileBoost.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.tileBoost = DefaultItem.tileBoost;
            PTileBoost = new UICategory.UIProperty(Textures.AddedRange, "Added Range:", TileBoost);
            PTileBoost.Recalculate();

            Buff = new UIIntTextbox(0, BuffLoader.BuffCount - 1);
            Buff.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.buffType = e.Value;
            Buff.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.buffType = DefaultItem.buffType;
            PBuff = new UICategory.UIProperty(Textures.BuffType, "Buff Inflicted:", Buff);

            BuffTime = limited ? new UIIntTextbox(0) : new UIIntTextbox();
            BuffTime.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.buffTime = e.Value;
            BuffTime.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.buffTime = DefaultItem.buffTime;
            PBuffTime = new UICategory.UIProperty(Textures.BuffDuration, "Buff Duration:", BuffTime);

            HealHP = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            HealHP.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.healLife = e.Value;
            HealHP.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.healLife = DefaultItem.healLife;
            PHealHP = new UICategory.UIProperty(Textures.HPHealed, "HP Healed:", HealHP);

            HealMP = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            HealMP.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.healMana = e.Value;
            HealMP.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.healMana = DefaultItem.healMana;
            PHealMP = new UICategory.UIProperty(Textures.MPHealed, "Mana Healed:", HealMP);

            AxePower = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            AxePower.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.axe = e.Value;
            AxePower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.axe = DefaultItem.axe;
            PAxePower = new UICategory.UIProperty(Textures.AxePower, "Axe Power:", AxePower);

            PickaxePower = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            PickaxePower.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.pick = e.Value;
            PickaxePower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.pick = DefaultItem.pick;
            PPickaxePower = new UICategory.UIProperty(Textures.PickaxePower, "Pickaxe Power:", PickaxePower);

            HammerPower = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            HammerPower.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.hammer = e.Value;
            HammerPower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.hammer = DefaultItem.hammer;
            PHammerPower = new UICategory.UIProperty(Textures.HammerPower, "Hammer Power:", HammerPower);

            Stack = limited ? new UIIntTextbox(1) : new UIIntTextbox();
            Stack.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.stack = e.Value;
            Stack.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.stack = DefaultItem.stack;
            PStack = new UICategory.UIProperty(Textures.Stack, "Amount:", Stack);

            MaxStack = limited ? new UIIntTextbox(1) : new UIIntTextbox();
            MaxStack.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.maxStack = e.Value;
            MaxStack.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.maxStack = DefaultItem.maxStack;
            PMaxStack = new UICategory.UIProperty(Textures.MaxStack, "Max Stack:", MaxStack);

            UseAnimation = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            UseAnimation.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.useAnimation = e.Value;
            UseAnimation.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.useAnimation = DefaultItem.useAnimation;
            PUseAnimation = new UICategory.UIProperty(Textures.UseAnimation, "Animation Span:", UseAnimation);

            UseTime = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            UseTime.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.useTime = e.Value;
            UseTime.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.useTime = DefaultItem.useTime;
            PUseTime = new UICategory.UIProperty(Textures.UseTime, "Use Span:", UseTime);

            Defense = new UIIntTextbox();
            Defense.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.defense = e.Value;
            Defense.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.defense = DefaultItem.defense;
            PDefense = new UICategory.UIProperty(Textures.Defense, "Defense:", Defense);

            FishingPower = limited ? new UIIntTextbox(ushort.MinValue, ushort.MaxValue) : new UIIntTextbox();
            FishingPower.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.fishingPole = e.Value;
            FishingPower.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.fishingPole = DefaultItem.fishingPole;
            PFishingPower = new UICategory.UIProperty(Textures.FishingPower, "Fishing Power:", FishingPower);

            Scale = new UIFloatTextbox();
            Scale.OnValueChanged += (source, e) => Main.LocalPlayer.HeldItem.scale = e.Value;
            Scale.OnRightClick += (source, e) => Main.LocalPlayer.HeldItem.scale = DefaultItem.scale;
            PScale = new UICategory.UIProperty(Textures.ItemScale, "Item Scale:", Scale);

            UseStyle = new UIRadioButtonContainer();
            RSwing = new UIRadioButton("Swing") { Parent = UseStyle };
            RSwing.OnValueChanged += (source, e) =>
            {
                if (e.Value)
                {
                    Main.LocalPlayer.HeldItem.useStyle = 1;
                }
            };
            RDrink = new UIRadioButton("Drink") { Parent = UseStyle };
            RDrink.OnValueChanged += (source, e) =>
            {
                if (e.Value)
                {
                    Main.LocalPlayer.HeldItem.useStyle = 2;
                }
            };
            RStab = new UIRadioButton("Stab") { Parent = UseStyle };
            RStab.OnValueChanged += (source, e) =>
            {
                if (e.Value)
                {
                    Main.LocalPlayer.HeldItem.useStyle = 3;
                }
            };
            RAboveHead = new UIRadioButton("Above Head") { Parent = UseStyle };
            RAboveHead.OnValueChanged += (source, e) =>
            {
                if (e.Value)
                {
                    Main.LocalPlayer.HeldItem.useStyle = 4;
                }
            };
            RHeld = new UIRadioButton("Held") { Parent = UseStyle };
            RHeld.OnValueChanged += (source, e) =>
            {
                if (e.Value)
                {
                    Main.LocalPlayer.HeldItem.useStyle = 5;
                }
            };
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
            UseStyle.OnDeselected += (source, e) =>
            {
                if (UseStyle.Selected.Length < 1)
                {
                    Main.LocalPlayer.HeldItem.useStyle = 0;
                }
            };
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

            /*SaveItemConfig = new UIImageButton(Textures.Globe, false)
            {
                Width = new SizeDimension(16f),
                Height = new SizeDimension(16f),
                YOffset = CloseButton.YOffset
            };
            SaveItemConfig.Recalculate();
            SaveItemConfig.XOffset = new SizeDimension(ClearModifications.CalculatedXOffset - SaveItemConfig.OuterWidth - 3);
            SaveItemConfig.Parent = this;
            SaveItemConfig.OnLeftClick += (source, e) =>
            {
                Item heldItem = Main.LocalPlayer.HeldItem;
                if (heldItem.type == 0)
                {
                    Main.NewText("Cannot modify air!");
                    return;
                }
                List<ItemProperties> itemConfig = ItemConfig.Instance.ItemChanges;
                int index = itemConfig.FindIndex(prop => prop.Type == heldItem.type);
                if (index == -1)
                {
                    ItemProperties props = new ItemProperties(heldItem.type);
                    props.FromItem(heldItem);
                    itemConfig.Add(props);
                }
                else
                {
                    itemConfig[index].FromItem(heldItem);
                }
                Main.NewText("Global Change Added! Reload Required", Color.Green);
            };
            SaveItemConfig.WhileMouseHover += (source, e) => instance.Tooltip = "Add to Item Config";*/

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
            GrayBG.OnVisibilityChanged += (source, e) => LockImage.Visible = e.Value;

            LockImage = new UIImage(Textures.Lock);
            LockImage.XOffset = new SizeDimension((GrayBG.InnerWidth - LockImage.OuterWidth) * 0.5f);
            LockImage.YOffset = new SizeDimension((GrayBG.InnerHeight - LockImage.OuterHeight) * 0.5f);
            LockImage.Parent = GrayBG;

            CategoryIndex = 0;
        }

        protected override void UpdateSelf(GameTime gameTime)
        {
            if (Visible)
            {
                Item heldItem = Main.LocalPlayer.HeldItem;
                if (DefaultItem.type != heldItem.type)
                {
                    DefaultItem.SetDefaults(heldItem.type);
                }
                if (LiveSync)
                {
                    AutoReuse.Value = heldItem.autoReuse;
                    Consumable.Value = heldItem.consumable;
                    Potion.Value = heldItem.potion;
                    Accessory.Value = heldItem.accessory;
                    RMelee.Selected = heldItem.melee;
                    RMagic.Selected = heldItem.magic;
                    RRanged.Selected = heldItem.ranged;
                    RSummon.Selected = heldItem.summon;
                    RThrown.Selected = heldItem.thrown;
                    if (!Shoot.Focused)
                    {
                        Shoot.Value = heldItem.shoot;
                    }
                    if (!Tile.Focused)
                    {
                        Tile.Value = heldItem.createTile;
                    }
                    if (!TileBoost.Focused)
                    {
                        TileBoost.Value = heldItem.tileBoost;
                    }
                    if (!Buff.Focused)
                    {
                        Buff.Value = heldItem.buffType;
                    }
                    if (!BuffTime.Focused)
                    {
                        BuffTime.Value = heldItem.buffTime;
                    }
                    if (!Damage.Focused)
                    {
                        Damage.Value = heldItem.damage;
                    }
                    if (!Critical.Focused)
                    {
                        Critical.Value = heldItem.crit;
                    }
                    if (!ShootSpeed.Focused)
                    {
                        ShootSpeed.Value = heldItem.shootSpeed;
                    }
                    if (!KnockBack.Focused)
                    {
                        KnockBack.Value = heldItem.knockBack;
                    }
                    if (!HealHP.Focused)
                    {
                        HealHP.Value = heldItem.healLife;
                    }
                    if (!HealMP.Focused)
                    {
                        HealMP.Value = heldItem.healMana;
                    }
                    if (!AxePower.Focused)
                    {
                        AxePower.Value = heldItem.axe;
                    }
                    if (!PickaxePower.Focused)
                    {
                        PickaxePower.Value = heldItem.pick;
                    }
                    if (!HammerPower.Focused)
                    {
                        HammerPower.Value = heldItem.hammer;
                    }
                    if (!Stack.Focused)
                    {
                        Stack.Value = heldItem.stack;
                    }
                    if (!MaxStack.Focused)
                    {
                        MaxStack.Value = heldItem.maxStack;
                    }
                    if (!UseAnimation.Focused)
                    {
                        UseAnimation.Value = heldItem.useAnimation;
                    }
                    if (!UseTime.Focused)
                    {
                        UseTime.Value = heldItem.useTime;
                    }
                    if (!Defense.Focused)
                    {
                        Defense.Value = heldItem.defense;
                    }
                    if (!FishingPower.Focused)
                    {
                        FishingPower.Value = heldItem.fishingPole;
                    }
                    if (!Scale.Focused)
                    {
                        Scale.Value = heldItem.scale;
                    }
                    if (heldItem.useStyle == 0)
                    {
                        UseStyle.DeselectAllRadio();
                    }
                    else
                    {
                        UseStyle.GetChoice(heldItem.useStyle - 1).Selected = true;
                    }
                    GrayBG.Visible = heldItem.type == 0;
                }
            }
        }
    }
}

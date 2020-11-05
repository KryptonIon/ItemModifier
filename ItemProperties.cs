using ItemModifier.Extensions;
using Terraria;

namespace ItemModifier
{
    public class ItemProperties
    {
        public int Type { get; set; }

        public bool AutoReuse { get; set; }

        public bool Consumable { get; set; }

        public bool Potion { get; set; }

        public bool Accessory { get; set; }

        public int DamageType { get; set; }

        public int Damage { get; set; }

        public int Crit { get; set; }

        public float KnockBack { get; set; }

        public int Shoot { get; set; }

        public float ShootSpeed { get; set; }

        public int CreateTile { get; set; }

        public int TileBoost { get; set; }

        public int BuffType { get; set; }

        public int BuffTime { get; set; }

        public int HealLife { get; set; }

        public int HealMana { get; set; }

        public int Axe { get; set; }

        public int Pickaxe { get; set; }

        public int Hammer { get; set; }

        public int MaxStack { get; set; }

        public int UseAnimation { get; set; }

        public int UseTime { get; set; }

        public int Defense { get; set; }

        public int FishingPole { get; set; }

        public float Scale { get; set; }

        public int UseStyle { get; set; }

        public int CostMP { get; set; }

        public ItemProperties(int type)
        {
            Type = type;
        }

        public void FromItem(Item item)
        {
            AutoReuse = item.autoReuse;
            Consumable = item.consumable;
            Potion = item.potion;
            Accessory = item.accessory;
            DamageType = item.DamageType();
            Damage = item.damage;
            KnockBack = item.knockBack;
            Crit = item.crit;
            Shoot = item.shoot;
            ShootSpeed = item.shootSpeed;
            CreateTile = item.createTile + 1;
            TileBoost = item.tileBoost;
            BuffTime = item.buffTime;
            BuffType = item.buffType;
            HealLife = item.healLife;
            HealMana = item.healMana;
            Axe = item.axe;
            Pickaxe = item.pick;
            Hammer = item.hammer;
            MaxStack = item.maxStack;
            UseTime = item.useTime;
            UseAnimation = item.useAnimation;
            Defense = item.defense;
            FishingPole = item.fishingPole;
            Scale = item.scale;
            UseStyle = item.useStyle;
            CostMP = item.mana;
        }
    }
}

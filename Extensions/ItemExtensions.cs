using System;
using Terraria;

namespace ItemModifier.Extensions
{
    public static class ItemExtensions
    {
        public static void CopyItemProperties(this Item target, Item origin)
        {
            target.Prefix(origin.prefix);
            target.autoReuse = origin.autoReuse;
            target.consumable = origin.consumable;
            target.potion = origin.potion;
            target.melee = origin.melee;
            target.magic = origin.magic;
            target.ranged = origin.ranged;
            target.summon = origin.summon;
            target.thrown = origin.thrown;
            target.accessory = origin.accessory;
            target.damage = origin.damage;
            target.knockBack = origin.knockBack;
            target.crit = origin.crit;
            target.shoot = origin.shoot;
            target.shootSpeed = origin.shootSpeed;
            target.createTile = origin.createTile;
            target.tileBoost = origin.tileBoost;
            target.buffTime = origin.buffTime;
            target.buffType = origin.buffType;
            target.healLife = origin.healLife;
            target.healMana = origin.healMana;
            target.axe = origin.axe;
            target.pick = origin.pick;
            target.hammer = origin.hammer;
            target.maxStack = origin.maxStack;
            target.useTime = origin.useTime;
            target.useAnimation = origin.useAnimation;
            target.defense = origin.defense;
            target.fishingPole = origin.fishingPole;
            target.scale = origin.scale;
            target.useStyle = origin.useStyle;
        }

        public static int DamageType(this Item item)
        {
            return item.melee ? 1 : item.magic ? 2 : item.ranged ? 3 : item.summon ? 4 : item.thrown ? 5 : 0;
        }

        public static void SetDamageType(this Item item, int damageType)
        {
            switch (damageType)
            {
                case 0:
                    item.melee = false;
                    item.magic = false;
                    item.ranged = false;
                    item.summon = false;
                    item.thrown = false;
                    break;
                case 1:
                    item.melee = true;
                    item.magic = false;
                    item.ranged = false;
                    item.summon = false;
                    item.thrown = false;
                    break;
                case 2:
                    item.melee = false;
                    item.magic = true;
                    item.ranged = false;
                    item.summon = false;
                    item.thrown = false;
                    break;
                case 3:
                    item.melee = false;
                    item.magic = false;
                    item.ranged = true;
                    item.summon = false;
                    item.thrown = false;
                    break;
                case 4:
                    item.melee = false;
                    item.magic = false;
                    item.ranged = false;
                    item.summon = true;
                    item.thrown = false;
                    break;
                case 5:
                    item.melee = false;
                    item.magic = false;
                    item.ranged = false;
                    item.summon = false;
                    item.thrown = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(damageType));
            }
        }
    }
}

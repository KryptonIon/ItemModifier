using ItemModifier.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using static ItemModifier.ItemConfig;

namespace ItemModifier
{
    public class ItemModifications : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            Dictionary<int, ItemProperties> globalModifications = ItemConfig.Instance.GlobalModifications;
            for (int i = 0; i < globalModifications.Count; i++)
            {
                if(globalModifications.TryGetValue(item.type, out ItemProperties modifications))
                {
                    item.autoReuse = modifications.AutoReuse;
                    item.consumable = modifications.Consumeable;
                    item.potion = modifications.Potion;
                    item.accessory = modifications.Accessory;
                    item.SetDamageType(modifications.DamageType);
                    item.damage = modifications.Damage;
                    item.crit = modifications.Crit;
                    item.knockBack = modifications.Knockback;
                    item.shoot = modifications.Shoot;
                    item.shootSpeed = modifications.ShootSpeed;
                    item.createTile = modifications.CreateTile;
                    item.tileBoost = modifications.TileBoost;
                    item.buffType = modifications.BuffType;
                    item.buffTime = modifications.BuffTime;
                    item.healLife = modifications.HealLife;
                    item.healMana = modifications.HealMana;
                    item.axe = modifications.Axe;
                    item.pick = modifications.Pickaxe;
                    item.hammer = modifications.Hammer;
                    item.maxStack = modifications.MaxStack;
                    item.useAnimation = modifications.UseAnimation;
                    item.useTime = modifications.UseTime;
                    item.defense = modifications.Defense;
                    item.fishingPole = modifications.FishingPole;
                    item.scale = modifications.Scale;
                    item.useStyle = modifications.UseStyle;
                }
            }
        }
    }
}

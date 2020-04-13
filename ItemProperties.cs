using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemModifier
{
    public class ItemProperties
    {
        public bool AutoReuse { get; set; }

        public bool Consumeable { get; set; }

        public bool Potion { get; set; }

        public bool Accessory { get; set; }

        public int DamageType { get; set; }

        public int Damage { get; set; }

        public int Crit { get; set; }

        public float Knockback { get; set; }

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

        public int Scale { get; set; }

        public int UseStyle { get; set; }
    }
}

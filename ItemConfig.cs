using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader.Config;

namespace ItemModifier
{
    public class ItemConfig
    {
        public static string ItemConfigPath { get; } = Path.Combine(ConfigManager.ModConfigPath, "ItemConfig.dat");

        public static ItemConfig Instance { get; set; }

        public List<ItemProperties> ItemChanges { get; set; }

        public ItemConfig()
        {
            Instance = this;
        }

        public void Save()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(ItemConfigPath, FileMode.Create)))
            {
                writer.Write((ushort)ItemChanges.Count);
                for (int i = 0; i < ItemChanges.Count; i++)
                {
                    // 57 Bytes and 4 bits 
                    ItemProperties changes = ItemChanges[i];
                    writer.Write((ushort)changes.Type);
                    writer.Write(changes.AutoReuse);
                    writer.Write(changes.Consumable);
                    writer.Write(changes.Potion);
                    writer.Write(changes.Accessory);
                    writer.Write((byte)changes.DamageType);
                    writer.Write(changes.Damage);
                    writer.Write(changes.KnockBack);
                    writer.Write((ushort)changes.Crit);
                    writer.Write((ushort)changes.Shoot);
                    writer.Write(changes.ShootSpeed);
                    writer.Write((ushort)changes.CreateTile);
                    writer.Write((sbyte)changes.TileBoost);
                    writer.Write((ushort)changes.BuffType);
                    writer.Write(changes.BuffTime);
                    writer.Write((ushort)changes.HealLife);
                    writer.Write((ushort)changes.HealMana);
                    writer.Write((ushort)changes.Axe);
                    writer.Write((ushort)changes.Pickaxe);
                    writer.Write((ushort)changes.Hammer);
                    writer.Write(changes.MaxStack);
                    writer.Write((ushort)changes.UseAnimation);
                    writer.Write((ushort)changes.UseTime);
                    writer.Write(changes.Defense);
                    writer.Write((ushort)changes.FishingPole);
                    writer.Write(changes.Scale);
                    writer.Write((byte)changes.UseStyle);
                    writer.Write((ushort)changes.CostMP);
                }
            }
        }

        public void Read()
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(ItemConfigPath, FileMode.Open)))
                {
                    ushort count = reader.ReadUInt16();
                    ItemChanges = new List<ItemProperties>();
                    for (int i = 0; i < count; i++)
                    {
                        ItemProperties changes = new ItemProperties(reader.ReadUInt16())
                        {
                            AutoReuse = reader.ReadBoolean(),
                            Consumable = reader.ReadBoolean(),
                            Potion = reader.ReadBoolean(),
                            Accessory = reader.ReadBoolean(),
                            DamageType = reader.ReadByte(),
                            Damage = reader.ReadInt32(),
                            KnockBack = reader.ReadSingle(),
                            Crit = reader.ReadUInt16(),
                            Shoot = reader.ReadUInt16(),
                            ShootSpeed = reader.ReadSingle(),
                            CreateTile = reader.ReadUInt16(),
                            TileBoost = reader.ReadSByte(),
                            BuffType = reader.ReadUInt16(),
                            BuffTime = reader.ReadInt32(),
                            HealLife = reader.ReadUInt16(),
                            HealMana = reader.ReadUInt16(),
                            Axe = reader.ReadUInt16(),
                            Pickaxe = reader.ReadUInt16(),
                            Hammer = reader.ReadUInt16(),
                            MaxStack = reader.ReadInt32(),
                            UseAnimation = reader.ReadUInt16(),
                            UseTime = reader.ReadUInt16(),
                            Defense = reader.ReadInt32(),
                            FishingPole = reader.ReadUInt16(),
                            Scale = reader.ReadSingle(),
                            UseStyle = reader.ReadByte(),
                            CostMP = reader.ReadUInt16()
                        };
                        ItemChanges.Add(changes);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                ItemChanges = new List<ItemProperties>();
                return;
            }
        }
    }
}

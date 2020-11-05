using ItemModifier.Extensions;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ItemModifier
{
    public class ItemModifications : GlobalItem
    {
        /*public override void SetDefaults(Item item)
        {
            List<ItemProperties> itemConfig = ItemConfig.Instance.ItemChanges;
            int index = itemConfig.FindIndex(prop => prop.Type == item.type);
            if (index != -1)
            {
                item.CopyItemProperties(itemConfig[index]);
            }
        }*/

        public override void NetSend(Item item, BinaryWriter writer)
        {
            Item defaultItem = new Item();
            defaultItem.SetDefaults(item.type);
            BitsByte flags1 = new BitsByte();
            BitsByte flags2 = new BitsByte();
            BitsByte flags3 = new BitsByte();
            BitsByte flags4 = new BitsByte();
            BitsByte flags5 = new BitsByte();
            if (item.autoReuse != defaultItem.autoReuse)
            {
                flags2[0] = true;
            }
            if (item.consumable != defaultItem.consumable)
            {
                flags2[1] = true;
            }
            if (item.potion != defaultItem.potion)
            {
                flags2[2] = true;
            }
            if (item.accessory != defaultItem.accessory)
            {
                flags2[3] = true;
            }
            int damageType = item.DamageType();
            if (damageType != defaultItem.DamageType())
            {
                flags2[4] = true;
            }
            if (item.damage != defaultItem.damage)
            {
                flags2[5] = true;
            }
            if (item.knockBack != defaultItem.knockBack)
            {
                flags2[6] = true;
            }
            if (item.crit != defaultItem.crit)
            {
                flags2[7] = true;
            }
            if (item.shoot != defaultItem.shoot)
            {
                flags3[0] = true;
            }
            if (item.shootSpeed != defaultItem.shootSpeed)
            {
                flags3[1] = true;
            }
            if (item.createTile != defaultItem.createTile)
            {
                flags3[2] = true;
            }
            if (item.tileBoost != defaultItem.tileBoost)
            {
                flags3[3] = true;
            }
            if (item.buffType != defaultItem.buffType)
            {
                flags3[4] = true;
            }
            if (item.buffTime != defaultItem.buffTime)
            {
                flags3[5] = true;
            }
            if (item.healLife != defaultItem.healLife)
            {
                flags3[6] = true;
            }
            if (item.healMana != defaultItem.healMana)
            {
                flags3[7] = true;
            }
            if (item.axe != defaultItem.axe)
            {
                flags4[0] = true;
            }
            if (item.pick != defaultItem.pick)
            {
                flags4[1] = true;
            }
            if (item.hammer != defaultItem.hammer)
            {
                flags4[2] = true;
            }
            if (item.maxStack != defaultItem.maxStack)
            {
                flags4[3] = true;
            }
            if (item.useAnimation != defaultItem.useAnimation)
            {
                flags4[4] = true;
            }
            if (item.useTime != defaultItem.useTime)
            {
                flags4[5] = true;
            }
            if (item.defense != defaultItem.defense)
            {
                flags4[6] = true;
            }
            if (item.fishingPole != defaultItem.fishingPole)
            {
                flags4[7] = true;
            }
            if (item.scale != defaultItem.scale)
            {
                flags5[0] = true;
            }
            if (item.useStyle != defaultItem.useStyle)
            {
                flags5[1] = true;
            }
            if (item.mana != defaultItem.mana)
            {
                flags5[2] = true;
            }
            if (flags2 != 0)
            {
                flags1[0] = true;
            }
            if (flags3 != 0)
            {
                flags1[1] = true;
            }
            if (flags4 != 0)
            {
                flags1[2] = true;
            }
            if (flags5 != 0)
            {
                flags1[3] = true;
            }
            writer.Write(flags1);
            if (flags1 != 0)
            {
                if (flags1[0])
                {
                    writer.Write(flags2);
                    if (flags2[0])
                    {
                        writer.Write(item.autoReuse);
                    }
                    if (flags2[1])
                    {
                        writer.Write(item.consumable);
                    }
                    if (flags2[2])
                    {
                        writer.Write(item.potion);
                    }
                    if (flags2[3])
                    {
                        writer.Write(item.accessory);
                    }
                    if (flags2[4])
                    {
                        writer.Write((byte)damageType);
                    }
                    if (flags2[5])
                    {
                        writer.Write(item.damage);
                    }
                    if (flags2[6])
                    {
                        writer.Write(item.knockBack);
                    }
                    if (flags2[7])
                    {
                        writer.Write((ushort)item.crit);
                    }
                }
                if (flags1[1])
                {
                    writer.Write(flags3);
                    if (flags3[0])
                    {
                        writer.Write((ushort)item.shoot);
                    }
                    if (flags3[1])
                    {
                        writer.Write(item.shootSpeed);
                    }
                    if (flags3[2])
                    {
                        writer.Write((ushort)(item.createTile + 1));
                    }
                    if (flags3[3])
                    {
                        writer.Write((sbyte)item.tileBoost);
                    }
                    if (flags3[4])
                    {
                        writer.Write((ushort)item.buffType);
                    }
                    if (flags3[5])
                    {
                        writer.Write(item.buffTime);
                    }
                    if (flags3[6])
                    {
                        writer.Write((ushort)item.healLife);
                    }
                    if (flags3[7])
                    {
                        writer.Write((ushort)item.healMana);
                    }
                }
                if (flags1[2])
                {
                    writer.Write(flags4);
                    if (flags4[0])
                    {
                        writer.Write((ushort)item.axe);
                    }
                    if (flags4[1])
                    {
                        writer.Write((ushort)item.pick);
                    }
                    if (flags4[2])
                    {
                        writer.Write((ushort)item.hammer);
                    }
                    if (flags4[3])
                    {
                        writer.Write(item.maxStack);
                    }
                    if (flags4[4])
                    {
                        writer.Write((ushort)item.useAnimation);
                    }
                    if (flags4[5])
                    {
                        writer.Write((ushort)item.useTime);
                    }
                    if (flags4[6])
                    {
                        writer.Write(item.defense);
                    }
                    if (flags4[7])
                    {
                        writer.Write((ushort)item.fishingPole);
                    }
                }
                if (flags1[3])
                {
                    writer.Write(flags5);
                    if (flags5[0])
                    {
                        writer.Write(item.scale);
                    }
                    if (flags5[1])
                    {
                        writer.Write((byte)item.useStyle);
                    }
                    if (flags5[2])
                    {
                        writer.Write((ushort)item.mana);
                    }
                }
            }
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            BitsByte flags1 = reader.ReadByte();
            if (flags1 == 0)
            {
                return;
            }
            if (flags1[0])
            {
                BitsByte flags2 = reader.ReadByte();
                if (flags2[0])
                {
                    item.autoReuse = reader.ReadBoolean();
                }
                if (flags2[1])
                {
                    item.consumable = reader.ReadBoolean();
                }
                if (flags2[2])
                {
                    item.potion = reader.ReadBoolean();
                }
                if (flags2[3])
                {
                    item.accessory = reader.ReadBoolean();
                }
                if (flags2[4])
                {
                    item.SetDamageType(reader.ReadByte());
                }
                if (flags2[5])
                {
                    item.damage = reader.ReadInt32();
                }
                if (flags2[6])
                {
                    item.knockBack = reader.ReadSingle();
                }
                if (flags2[7])
                {
                    item.crit = reader.ReadUInt16();
                }
            }
            if (flags1[1])
            {
                BitsByte flags3 = reader.ReadByte();
                if (flags3[0])
                {
                    item.shoot = reader.ReadUInt16();
                }
                if (flags3[1])
                {
                    item.shootSpeed = reader.ReadSingle();
                }
                if (flags3[2])
                {
                    item.createTile = reader.ReadUInt16() - 1;
                }
                if (flags3[3])
                {
                    item.tileBoost = reader.ReadSByte();
                }
                if (flags3[4])
                {
                    item.buffType = reader.ReadUInt16();
                }
                if (flags3[5])
                {
                    item.buffTime = reader.ReadInt32();
                }
                if (flags3[6])
                {
                    item.healLife = reader.ReadUInt16();
                }
                if (flags3[7])
                {
                    item.healMana = reader.ReadUInt16();
                }
            }
            if (flags1[2])
            {
                BitsByte flags4 = reader.ReadByte();
                if (flags4[0])
                {
                    item.axe = reader.ReadUInt16();
                }
                if (flags4[1])
                {
                    item.pick = reader.ReadUInt16();
                }
                if (flags4[2])
                {
                    item.hammer = reader.ReadUInt16();
                }
                if (flags4[3])
                {
                    item.maxStack = reader.ReadInt32();
                }
                if (flags4[4])
                {
                    item.useAnimation = reader.ReadUInt16();
                }
                if (flags4[5])
                {
                    item.useTime = reader.ReadUInt16();
                }
                if (flags4[6])
                {
                    item.defense = reader.ReadInt32();
                }
                if (flags4[7])
                {
                    item.fishingPole = reader.ReadUInt16();
                }
            }
            if (flags1[3])
            {
                BitsByte flags5 = reader.ReadByte();
                if (flags5[0])
                {
                    item.scale = reader.ReadSingle();
                }
                if (flags5[1])
                {
                    item.useStyle = reader.ReadByte();
                }
                if (flags5[2])
                {
                    item.mana = reader.ReadUInt16();
                }
            }
        }

        public override bool NeedsSaving(Item item)
        {
            return true;
        }

        public override TagCompound Save(Item item)
        {
            Item defaultItem = new Item();
            defaultItem.SetDefaults(item.type);
            TagCompound tag = new TagCompound();
            if (item.autoReuse != defaultItem.autoReuse)
            {
                tag.Add("AutoReuse", item.autoReuse);
            }
            if (item.consumable != defaultItem.consumable)
            {
                tag.Add("Consumable", item.consumable);
            }
            if (item.potion != defaultItem.potion)
            {
                tag.Add("Potion", item.potion);
            }
            if (item.accessory != defaultItem.accessory)
            {
                tag.Add("Accessory", item.accessory);
            }
            int damageType = item.DamageType();
            if (damageType != defaultItem.DamageType())
            {
                tag.Add("DamageType", (byte)damageType);
            }
            if (item.damage != defaultItem.damage)
            {
                tag.Add("Damage", item.damage);
            }
            if (item.knockBack != defaultItem.knockBack)
            {
                tag.Add("KnockBack", item.knockBack);
            }
            if (item.crit != defaultItem.crit)
            {
                tag.Add("Crit", (ushort)item.crit);
            }
            if (item.shoot != defaultItem.shoot)
            {
                tag.Add("Shoot", (ushort)item.shoot);
            }
            if (item.shootSpeed != defaultItem.shootSpeed)
            {
                tag.Add("ShootSpeed", item.shootSpeed);
            }
            if (item.createTile != defaultItem.createTile)
            {
                tag.Add("CreateTile", (ushort)item.createTile);
            }
            if (item.tileBoost != defaultItem.tileBoost)
            {
                tag.Add("TileBoost", (byte)(sbyte)item.tileBoost);
            }
            if (item.buffType != defaultItem.buffType)
            {
                tag.Add("BuffType", (ushort)item.buffType);
            }
            if (item.buffTime != defaultItem.buffTime)
            {
                tag.Add("BuffTime", item.buffTime);
            }
            if (item.healLife != defaultItem.healLife)
            {
                tag.Add("HealLife", (ushort)item.healLife);
            }
            if (item.healMana != defaultItem.healMana)
            {
                tag.Add("HealMana", (ushort)item.healMana);
            }
            if (item.axe != defaultItem.axe)
            {
                tag.Add("Axe", (ushort)item.axe);
            }
            if (item.pick != defaultItem.pick)
            {
                tag.Add("Pickaxe", (ushort)item.pick);
            }
            if (item.hammer != defaultItem.hammer)
            {
                tag.Add("Hammer", (ushort)item.hammer);
            }
            if (item.maxStack != defaultItem.maxStack)
            {
                tag.Add("MaxStack", item.maxStack);
            }
            if (item.useAnimation != defaultItem.useAnimation)
            {
                tag.Add("UseAnimation", (ushort)item.useAnimation);
            }
            if (item.useTime != defaultItem.useTime)
            {
                tag.Add("UseTime", (ushort)item.useTime);
            }
            if (item.defense != defaultItem.defense)
            {
                tag.Add("Defense", item.defense);
            }
            if (item.fishingPole != defaultItem.fishingPole)
            {
                tag.Add("FishingPole", (ushort)item.fishingPole);
            }
            if (item.scale != defaultItem.scale)
            {
                tag.Add("Scale", item.scale);
            }
            if (item.useStyle != defaultItem.useStyle)
            {
                tag.Add("UseStyle", (byte)item.useStyle);
            }
            if(item.mana != defaultItem.mana)
            {
                tag.Add("CostMP", (ushort)item.mana);
            }
            CustomProperties cItem = item.GetGlobalItem<CustomProperties>();
            bool save = false;
            for (int i = 0; i < cItem.BuffTypes.Length; i++)
            {
                if (cItem.BuffTypes[i] != 0 || cItem.BuffTimes[i] != 0)
                {
                    save = true;
                    break;
                }
            }
            if (save)
            {
                tag.Add("BuffTypes", cItem.BuffTypes);
                tag.Add("BuffTimes", cItem.BuffTimes);
            }
            return tag;
        }

        public override void Load(Item item, TagCompound tag)
        {
            if (tag.ContainsKey("AutoReuse"))
            {
                item.autoReuse = tag.GetBool("AutoReuse");
            }
            if (tag.ContainsKey("Consumable"))
            {
                item.consumable = tag.GetBool("Consumable");
            }
            if (tag.ContainsKey("Potion"))
            {
                item.potion = tag.GetBool("Potion");
            }
            if (tag.ContainsKey("Accessory"))
            {
                item.accessory = tag.GetBool("Accessory");
            }
            if (tag.ContainsKey("DamageType"))
            {
                item.SetDamageType(tag.GetByte("DamageType"));
            }
            if (tag.ContainsKey("Damage"))
            {
                item.damage = tag.GetInt("Damage");
            }
            if (tag.ContainsKey("KnockBack"))
            {
                item.knockBack = tag.GetFloat("KnockBack");
            }
            if (tag.ContainsKey("Crit"))
            {
                item.crit = tag.Get<ushort>("Crit");
            }
            if (tag.ContainsKey("Shoot"))
            {
                item.shoot = tag.Get<ushort>("Shoot");
            }
            if (tag.ContainsKey("ShootSpeed"))
            {
                item.shootSpeed = tag.GetFloat("ShootSpeed");
            }
            if (tag.ContainsKey("CreateTile"))
            {
                item.createTile = tag.Get<ushort>("CreateTile");
            }
            if (tag.ContainsKey("TileBoost"))
            {
                item.tileBoost = (sbyte)tag.Get<byte>("TileBoost");
            }
            if (tag.ContainsKey("BuffType"))
            {
                item.buffType = tag.Get<ushort>("BuffType");
            }
            if (tag.ContainsKey("BuffTime"))
            {
                item.buffTime = tag.GetInt("BuffTime");
            }
            if (tag.ContainsKey("HealLife"))
            {
                item.healLife = tag.Get<ushort>("HealLife");
            }
            if (tag.ContainsKey("HealMana"))
            {
                item.healMana = tag.Get<ushort>("HealMana");
            }
            if (tag.ContainsKey("Axe"))
            {
                item.axe = tag.Get<ushort>("Axe");
            }
            if (tag.ContainsKey("Pickaxe"))
            {
                item.pick = tag.Get<ushort>("Pickaxe");
            }
            if (tag.ContainsKey("Hammer"))
            {
                item.hammer = tag.Get<ushort>("Hammer");
            }
            if (tag.ContainsKey("MaxStack"))
            {
                item.maxStack = tag.GetInt("MaxStack");
            }
            if (tag.ContainsKey("UseAnimation"))
            {
                item.useAnimation = tag.Get<ushort>("UseAnimation");
            }
            if (tag.ContainsKey("UseTime"))
            {
                item.useTime = tag.Get<ushort>("UseTime");
            }
            if (tag.ContainsKey("Defense"))
            {
                item.defense = tag.GetInt("Defense");
            }
            if (tag.ContainsKey("FishingPole"))
            {
                item.fishingPole = tag.Get<ushort>("FishingPole");
            }
            if (tag.ContainsKey("Scale"))
            {
                item.scale = tag.GetFloat("Scale");
            }
            if (tag.ContainsKey("UseStyle"))
            {
                item.useStyle = tag.GetByte("UseStyle");
            }
            if (tag.ContainsKey("CostMP"))
            {
                item.mana = tag.Get<ushort>("CostMP");
            }
            if (tag.ContainsKey("BuffTypes"))
            {
                CustomProperties cItem = item.GetGlobalItem<CustomProperties>();
                int[] buffTypes = tag.GetIntArray("BuffTypes");
                int[] buffTimes = tag.GetIntArray("BuffTimes");
                for (int i = 0; i < cItem.BuffTypes.Length; i++)
                {
                    cItem.BuffTypes[i] = buffTypes[i];
                    cItem.BuffTimes[i] = buffTimes[i];
                }
            }
        }
    }
}

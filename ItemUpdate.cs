using ItemModifier.Extensions;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier
{
    public class ItemUpdate : GlobalItem
    {
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
            if (item.melee != defaultItem.melee || item.magic != defaultItem.magic || item.ranged != defaultItem.ranged || item.summon != defaultItem.summon || item.thrown != defaultItem.thrown)
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
                        writer.Write((byte)item.DamageType());
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
            }
        }
    }
}

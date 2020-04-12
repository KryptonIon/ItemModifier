using ItemModifier.Extensions;
using ItemModifier.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ItemModifier
{
    public class ItemModifier : Mod
    {
        public static class Textures
        {
            public static Texture2D ModifyItem { get; private set; }

            public static Texture2D Settings { get; private set; }

            public static Texture2D Wiki { get; private set; }

            public static Texture2D Save { get; private set; }

            public static Texture2D UpArrow { get; private set; }

            public static Texture2D DownArrow { get; private set; }

            public static Texture2D LeftArrow { get; private set; }

            public static Texture2D RightArrow { get; private set; }

            public static Texture2D NewItem { get; private set; }

            public static Texture2D Print { get; private set; }

            public static Texture2D ClearModifications { get; private set; }

            public static Texture2D UpArrowShort { get; private set; }

            public static Texture2D DownArrowShort { get; private set; }

            public static Texture2D LeftArrowShort { get; private set; }

            public static Texture2D RightArrowShort { get; private set; }

            public static Texture2D Lock { get; private set; }

            public static Texture2D Sync { get; private set; }

            public static Texture2D Reset { get; private set; }

            public static Texture2D Checkbox { get; private set; }

            public static Texture2D HorizontalLine { get; private set; }

            public static Texture2D X { get; private set; }

            public static Texture2D Caret { get; private set; }

            public static Texture2D AutoReuse { get; private set; }

            public static Texture2D Consumable { get; private set; }

            public static Texture2D PotionSickness { get; private set; }

            public static Texture2D DamageType { get; private set; }

            public static Texture2D Accessory { get; private set; }

            public static Texture2D Damage { get; private set; }

            public static Texture2D CritChance { get; private set; }

            public static Texture2D Knockback { get; private set; }

            public static Texture2D ProjectileShot { get; private set; }

            public static Texture2D ProjectileSpeed { get; private set; }

            public static Texture2D CreateTile { get; private set; }

            public static Texture2D AddedRange { get; private set; }

            public static Texture2D BuffDuration { get; private set; }

            public static Texture2D BuffType { get; private set; }

            public static Texture2D HPHealed { get; private set; }

            public static Texture2D MPHealed { get; private set; }

            public static Texture2D AxePower { get; private set; }

            public static Texture2D PickaxePower { get; private set; }

            public static Texture2D HammerPower { get; private set; }

            public static Texture2D Stack { get; private set; }

            public static Texture2D MaxStack { get; private set; }

            public static Texture2D UseAnimation { get; private set; }

            public static Texture2D UseTime { get; private set; }

            public static Texture2D Defense { get; private set; }

            public static Texture2D FishingPower { get; private set; }

            public static Texture2D ItemScale { get; private set; }

            public static Texture2D UseStyle { get; private set; }

            public static Texture2D OpaqueWindowBackground { get; private set; }

            public static Texture2D WhiteDot { get; private set; }

            public static Texture2D SquareSelect { get; private set; }

            public static Texture2D BlackDot { get; private set; }

            public static void Load()
            {
                ModifyItem = ModContent.GetTexture("ItemModifier/UI/ModifyItem");
                Settings = ModContent.GetTexture("ItemModifier/UI/Settings");
                Wiki = ModContent.GetTexture("ItemModifier/UI/Wiki");
                Save = ModContent.GetTexture("ItemModifier/UI/Save");
                UpArrow = ModContent.GetTexture("ItemModifier/UI/UpArrow");
                DownArrow = ModContent.GetTexture("ItemModifier/UI/DownArrow");
                LeftArrow = ModContent.GetTexture("ItemModifier/UI/LeftArrow");
                RightArrow = ModContent.GetTexture("ItemModifier/UI/RightArrow");
                NewItem = ModContent.GetTexture("ItemModifier/UI/NewItem");
                Print = ModContent.GetTexture("ItemModifier/UI/Print");
                ClearModifications = ModContent.GetTexture("ItemModifier/UI/ClearModifications");
                UpArrowShort = ModContent.GetTexture("ItemModifier/UI/UpArrowShort");
                DownArrowShort = ModContent.GetTexture("ItemModifier/UI/DownArrowShort");
                LeftArrowShort = ModContent.GetTexture("ItemModifier/UI/LeftArrowShort");
                RightArrowShort = ModContent.GetTexture("ItemModifier/UI/RightArrowShort");
                Lock = ModContent.GetTexture("ItemModifier/UI/Lock");
                Sync = ModContent.GetTexture("ItemModifier/UI/Sync");
                Reset = ModContent.GetTexture("ItemModifier/UI/Reset");
                Checkbox = ModContent.GetTexture("ItemModifier/UIKit/Inputs/Checkbox");
                HorizontalLine = ModContent.GetTexture("ItemModifier/UIKit/HorizontalLine");
                X = ModContent.GetTexture("ItemModifier/UIKit/X");
                Caret = ModContent.GetTexture("ItemModifier/UIKit/Inputs/Caret");
                AutoReuse = ModContent.GetTexture("ItemModifier/UI/AutoReuse");
                Consumable = ModContent.GetTexture("ItemModifier/UI/Consumable");
                PotionSickness = ModContent.GetTexture("ItemModifier/UI/PotionSickness");
                DamageType = ModContent.GetTexture("ItemModifier/UI/DamageType");
                Accessory = ModContent.GetTexture("ItemModifier/UI/Accessory");
                Damage = ModContent.GetTexture("ItemModifier/UI/Damage");
                CritChance = ModContent.GetTexture("ItemModifier/UI/CritChance");
                Knockback = ModContent.GetTexture("ItemModifier/UI/Knockback");
                ProjectileShot = ModContent.GetTexture("ItemModifier/UI/ProjectileShot");
                ProjectileSpeed = ModContent.GetTexture("ItemModifier/UI/ProjectileSpeed");
                CreateTile = ModContent.GetTexture("ItemModifier/UI/CreateTile");
                AddedRange = ModContent.GetTexture("ItemModifier/UI/AddedRange");
                BuffDuration = ModContent.GetTexture("ItemModifier/UI/BuffDuration");
                BuffType = ModContent.GetTexture("ItemModifier/UI/BuffType");
                HPHealed = ModContent.GetTexture("ItemModifier/UI/HealHP");
                MPHealed = ModContent.GetTexture("ItemModifier/UI/HealMP");
                AxePower = ModContent.GetTexture("ItemModifier/UI/AxePower");
                PickaxePower = ModContent.GetTexture("ItemModifier/UI/PickaxePower");
                HammerPower = ModContent.GetTexture("ItemModifier/UI/HammerPower");
                Stack = ModContent.GetTexture("ItemModifier/UI/Stack");
                MaxStack = ModContent.GetTexture("ItemModifier/UI/MaxStack");
                UseAnimation = ModContent.GetTexture("ItemModifier/UI/UseAnimation");
                UseTime = ModContent.GetTexture("ItemModifier/UI/UseTime");
                Defense = ModContent.GetTexture("ItemModifier/UI/Defense");
                FishingPower = ModContent.GetTexture("ItemModifier/UI/FishingPower");
                ItemScale = ModContent.GetTexture("ItemModifier/UI/Scale");
                UseStyle = ModContent.GetTexture("ItemModifier/UI/UseStyle");
                OpaqueWindowBackground = new Texture2D(Main.spriteBatch.GraphicsDevice, 1, 1);
                OpaqueWindowBackground.SetData(new Color[] { new Color(44, 57, 105) });
                WhiteDot = new Texture2D(Main.spriteBatch.GraphicsDevice, 1, 1);
                WhiteDot.SetData(new Color[] { Color.White });
                BlackDot = new Texture2D(Main.spriteBatch.GraphicsDevice, 1, 1);
                BlackDot.SetData(new Color[] { Color.Black });
                SquareSelect = ModContent.GetTexture("ItemModifier/UIKit/Inputs/SquareSelect");
            }

            public static void Unload()
            {
                ModifyItem = null;
                Settings = null;
                Wiki = null;
                Save = null;
                UpArrow = null;
                DownArrow = null;
                LeftArrow = null;
                RightArrow = null;
                NewItem = null;
                Print = null;
                ClearModifications = null;
                UpArrowShort = null;
                DownArrowShort = null;
                LeftArrowShort = null;
                RightArrowShort = null;
                Lock = null;
                Sync = null;
                Reset = null;
                Checkbox = null;
                HorizontalLine = null;
                X = null;
                Caret = null;
                AutoReuse = null;
                Consumable = null;
                PotionSickness = null;
                DamageType = null;
                Accessory = null;
                Damage = null;
                CritChance = null;
                Knockback = null;
                ProjectileShot = null;
                ProjectileSpeed = null;
                CreateTile = null;
                AddedRange = null;
                BuffDuration = null;
                BuffType = null;
                HPHealed = null;
                MPHealed = null;
                AxePower = null;
                PickaxePower = null;
                HammerPower = null;
                Stack = null;
                MaxStack = null;
                UseAnimation = null;
                UseTime = null;
                Defense = null;
                FishingPower = null;
                ItemScale = null;
                UseStyle = null;
                OpaqueWindowBackground = null;
                WhiteDot = null;
                SquareSelect = null;
            }
        }

        internal MainInterface MainUI;

        internal string Tooltip { get; set; }

        public bool MouseWheelDisabled { get; set; } = false;

        public bool ItemAtCursorDisabled { get; set; } = false;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                Textures.Load();
            }
        }

        public override void PostSetupContent()
        {
            if (!Main.dedServ)
            {
                (MainUI = new MainInterface()).Activate();
            }
        }

        public override void Unload()
        {
            Textures.Unload();
            MainUI = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            MainUI?.Update(gameTime);
        }

        public override void PostUpdateInput()
        {
            MainUI?.PostUpdateInput();
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (ItemAtCursorDisabled)
            {
                int mouseItemIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interact Item Icon"));
                if (mouseItemIndex != -1)
                {
                    layers.RemoveAt(mouseItemIndex);
                }
            }
            int mouseIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (mouseIndex != -1)
            {
                layers.Insert(mouseIndex - 1, new LegacyGameInterfaceLayer(
                    "ItemModifier: ItemModifierUI",
                    delegate
                    {
                        MainUI?.Draw(Main.spriteBatch);
                        Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, Tooltip ?? string.Empty, Main.mouseX + 16f, Main.mouseY + 16f, Color.White, Color.Black, Vector2.Zero);
                        Tooltip = null;
                        return true;
                    },
                    InterfaceScaleType.UI
                    ));
            }
        }

        public void WriteItemModifyPacket(BinaryWriter writer, Item item)
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
                        writer.Write((ushort)item.createTile);
                    }
                    if (flags3[3])
                    {
                        writer.Write((byte)item.tileBoost);
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
                        writer.Write(item.fishingPole);
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

        public void ParseItemModifyPacket(BinaryReader reader, Item item)
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
                    item.createTile = reader.ReadUInt16();
                }
                if (flags3[3])
                {
                    item.tileBoost = reader.ReadByte();
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
                    item.hammer = reader.ReadInt32();
                }
                if (flags4[3])
                {
                    item.maxStack = reader.ReadUInt16();
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
                    item.fishingPole = reader.ReadInt32();
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

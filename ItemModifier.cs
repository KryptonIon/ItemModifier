using ItemModifier.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
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

			public static Texture2D UpArrow { get; private set; }

			public static Texture2D DownArrow { get; private set; }

			public static Texture2D LeftArrow { get; private set; }

			public static Texture2D RightArrow { get; private set; }

			public static Texture2D NewItem { get; private set; }

			public static Texture2D ClearModifications { get; private set; }

			public static Texture2D UpArrowShort { get; private set; }

			public static Texture2D DownArrowShort { get; private set; }

			public static Texture2D LeftArrowShort { get; private set; }

			public static Texture2D RightArrowShort { get; private set; }

			public static Texture2D Lock { get; private set; }

			public static Texture2D Sync { get; private set; }

			public static Texture2D Reset { get; private set; }

			public static Texture2D Checkbox { get; private set; }

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

			public static Texture2D Globe { get; private set; }

			public static void Load()
			{
				ModifyItem = ModContent.GetTexture("ItemModifier/UI/ModifyItem");
				Settings = ModContent.GetTexture("ItemModifier/UI/Settings");
				Wiki = ModContent.GetTexture("ItemModifier/UI/Wiki");
				UpArrow = ModContent.GetTexture("ItemModifier/UI/UpArrow");
				DownArrow = ModContent.GetTexture("ItemModifier/UI/DownArrow");
				LeftArrow = ModContent.GetTexture("ItemModifier/UI/LeftArrow");
				RightArrow = ModContent.GetTexture("ItemModifier/UI/RightArrow");
				NewItem = ModContent.GetTexture("ItemModifier/UI/NewItem");
				ClearModifications = ModContent.GetTexture("ItemModifier/UI/ClearModifications");
				UpArrowShort = ModContent.GetTexture("ItemModifier/UI/UpArrowShort");
				DownArrowShort = ModContent.GetTexture("ItemModifier/UI/DownArrowShort");
				LeftArrowShort = ModContent.GetTexture("ItemModifier/UI/LeftArrowShort");
				RightArrowShort = ModContent.GetTexture("ItemModifier/UI/RightArrowShort");
				Lock = ModContent.GetTexture("ItemModifier/UI/Lock");
				Sync = ModContent.GetTexture("ItemModifier/UI/Sync");
				Reset = ModContent.GetTexture("ItemModifier/UI/Reset");
				Checkbox = ModContent.GetTexture("ItemModifier/UIKit/Inputs/Checkbox");
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
				Globe = ModContent.GetTexture("ItemModifier/UI/Globe");
			}

			public static void Unload()
			{
				ModifyItem = null;
				Settings = null;
				Wiki = null;
				UpArrow = null;
				DownArrow = null;
				LeftArrow = null;
				RightArrow = null;
				NewItem = null;
				ClearModifications = null;
				UpArrowShort = null;
				DownArrowShort = null;
				LeftArrowShort = null;
				RightArrowShort = null;
				Lock = null;
				Sync = null;
				Reset = null;
				Checkbox = null;
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
				Globe = null;
			}
		}

		internal MainInterface MainUI;

		internal string Tooltip { get; set; }

		public static string WikiURL = "https://terrariamods.gamepedia.com/Item_Modifier";

		public bool MouseWheelDisabled { get; set; } = false;

		public bool ItemAtCursorDisabled { get; set; } = false;

		internal ModHotKey ToggleItemModifierUIHotKey;

		internal ModHotKey ToggleNewItemUIHotKey;

		internal ModHotKey OpenWikiHotKey;

		public override void Load()
		{
			ToggleItemModifierUIHotKey = RegisterHotKey("Toggle Item Modifier Window", nameof(Keys.C));
			ToggleNewItemUIHotKey = RegisterHotKey("Toggle New Item Window", nameof(Keys.V));
			OpenWikiHotKey = RegisterHotKey("Open Wiki", nameof(Keys.X));

			if (!Main.dedServ)
			{
				//ItemConfig itemConfig = new ItemConfig();
				//itemConfig.Read();
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
			if (!Main.dedServ)
			{
				MainUI = null;
				Textures.Unload();
				//ItemConfig.Instance.Save();
				//ItemConfig.Instance = null;
			}
		}

		public override void UpdateUI(GameTime gameTime)
		{
			MainUI?.Update(gameTime);
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

		public override void PreSaveAndQuit()
		{
			MainUI.ItemModifierWindow.Visible = false;
			MainUI.NewItemWindow.Visible = false;
		}

		public static void OpenWiki()
		{
			Process.Start(WikiURL);
		}
	}
}

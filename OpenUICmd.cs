using ItemModifier.UI;
using Terraria.ModLoader;

namespace ItemModifier
{
	public class OpenUICmd : ModCommand
	{
		public override string Command => "imui";

		public override string Description => "Force UI Buttons to show up";

		public override string Usage => "/imui";

		public override CommandType Type => CommandType.Chat;

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			MainInterface mainUI = ((ItemModifier)mod).MainUI;
			mainUI.ItemModifierButton.Visible = true;
			mainUI.NewItemButton.Visible = true;
			mainUI.WikiButton.Visible = true;
		}
	}
}

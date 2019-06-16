using ItemModifier.Utilities;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class ID : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "id";

        public override string Description => "Gets the IDs of the currently held item, do /id params to see a list of TypeIDs";

        public override string Usage => "/id (Optional Parameters)[TypeIDs]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;
            var MouseItem = caller.Player.HeldItem;

            if (args.Length == 0)
            {
                string Reply = "";
                List<string> ids = new List<string> { };

                ids.Add($"ItemID: {MouseItem.type}");

                if (MouseItem.createTile != -1 || Config.ShowUnnecessary)
                {
                    ids.Add($"TileID: {MouseItem.createTile}");
                }

                if (MouseItem.shoot != 0 || Config.ShowUnnecessary)
                {
                    ids.Add($"ProjectileID: {MouseItem.shoot}");
                }

                if (MouseItem.buffType != 0 || Config.ShowUnnecessary)
                {
                    ids.Add($"BuffID: {MouseItem.buffType}");
                }

                for (int i = 0; i < ids.Count; i++)
                {
                    Reply += ids[i];

                    if (i < ids.Count - 1)
                    {
                        Reply += ", ";
                    }
                }

                caller.Reply(Reply, replyColor);
            }
            else
            {
                string Reply = "";
                List<string> ids = new List<string> { };

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i].ToLower())
                    {
                        case "itemid":
                        case "item":
                        case "i":
                            ids.Add($"ItemID: {MouseItem.type}");
                            break;
                        case "tileid":
                        case "tile":
                        case "t":
                            ids.Add($"TileID: {MouseItem.createTile}");
                            break;
                        case "projectileid":
                        case "projid":
                        case "proj":
                        case "p":
                        case "shoot":
                        case "s":
                            ids.Add($"ProjectileID: {MouseItem.shoot}");
                            break;
                        case "buffid":
                        case "buff":
                        case "b":
                            ids.Add($"BuffID: {MouseItem.buffType}");
                            break;
                        default:
                            caller.Reply(ErrorHandler.InvalidArguementError(args[i]), errorColor);
                            break;
                    }
                }

                for (int i2 = 0; i2 < ids.Count; i2++)
                {
                    Reply += ids[i2];

                    if (i2 < ids.Count - 1)
                    {
                        Reply += ", ";
                    }
                }

                if (Reply != "")
                {
                    caller.Reply(Reply, replyColor);
                }
            }
        }
    }
}
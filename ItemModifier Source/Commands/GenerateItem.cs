using ItemModifier.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ItemModifier.Commands
{
    public class GenerateItem : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "gi";

        public override string Description => "Generates an item, and/or with custom properties, do /gi params to see a list of parameters";

        public override string Usage => "/gi <ItemID> [Optional Parameters]<Property> <Value>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;

            string help = Description +
                    $" \nUsage: {Usage}, examples:" +
                    " \n/gi 757, Spawns a Terra Blade" +
                    " \n/gi 757 s 5, Spawns a Terra Blade that shoots Jester's Arrows" +
                    " \n/gi 757 ct 26 s 5, Spawns a Terra Blade that shoots Jester's Arrows whilst placing Demon Altars like a block";

            string parameters = "Parameters: shoot or s, shootspeed or ss, createtile or ct, usetime or ut, useanimation or ua, healmana or hm, heallife or hl" +
                    "\nParameters are caps insensitive meaning, you can do /gi 757 ShOoT 5 and it will still shoot a jester's arrow" +
                    "\nParameters have shortcuts, eg /gi 757 s 5, they're listed above";

            if (!CommandHelper.ValidifySyntax(caller, args, 1, help, ItemModifier.ConstructIdentifier("params", parameters)))
            {
                return;
            }

            if (int.Parse(args[0]) > ItemLoader.ItemCount || int.Parse(args[0]) <= 0)
            {
                caller.Reply($"ItemID must not be less than 1 or exceed current amount of items: {ItemLoader.ItemCount}", errorColor);
                return;
            }

            if (ItemModifier.FindSlot(caller.Player.inventory) == -1)
            {
                caller.Reply("Inventory Full", errorColor);
                return;
            }
            else
            {
                Item ogitem = new Item();
                ogitem.SetDefaults(int.Parse(args[0]));
                Item item = new Item();
                item.SetDefaults(ogitem.type);

                if (args.Length > 1)
                {
                    if ((args.Length - 1) % 2 == 0)
                    {
                        for (int i = 1; i < args.Length; i++)
                        {
                            switch (args[i].ToLower())
                            {
                                case "s":
                                case "shoot":
                                    int s;
                                    if (!int.TryParse(args[i + 1], out s))
                                    {
                                        caller.Reply($"Error, ProjectileID({args[i + 1]}) must be a number", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        if (s > ProjectileLoader.ProjectileCount)
                                        {
                                            caller.Reply($"ProjectileID({args[i + 1]}) exceeds current projectile count: {ProjectileLoader.ProjectileCount}", errorColor);
                                            break;
                                        }
                                        else if (s < 0)
                                        {
                                            caller.Reply($"ProjectileID({args[i + 1]}) can't be negative");
                                            break;
                                        }
                                        else if (s == ProjectileID.Count)
                                        {
                                            caller.Reply($"Sorry, ProjectileID: {ProjectileID.Count} is prohibited from use due to issues with crashing", errorColor);
                                            break;
                                        }
                                        else
                                        {
                                            item.shoot = s;
                                            break;
                                        }
                                    }
                                case "ss":
                                case "shootspeed":
                                    int ss;
                                    if (!int.TryParse(args[i + 1], out ss))
                                    {
                                        caller.Reply($"Error, Shoot Speed must be a number: {args[i + 1]}", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        item.shootSpeed = ss;
                                        break;
                                    }
                                case "ct":
                                case "createtile":
                                    int ct;
                                    if (!int.TryParse(args[i + 1], out ct))
                                    {
                                        caller.Reply($"Error, TileID({args[i + 1]}) must be a number", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        if (ct < -1)
                                        {
                                            caller.Reply($"TileID({args[i + 1]} can't be negative(-1 Excluded)");
                                            break;
                                        }
                                        else if (ct > TileLoader.TileCount - 1)
                                        {
                                            caller.Reply($"TileID({args[i + 1]}) is bigger than tile count: {TileLoader.TileCount - 1}", errorColor);
                                            break;
                                        }
                                        else if (ct == TileID.Count)
                                        {
                                            caller.Reply($"Sorry, TileID: {TileID.Count} is prohibited from use due to issues with crashing", errorColor);
                                            break;
                                        }
                                        else
                                        {
                                            item.createTile = ct;
                                            break;
                                        }
                                    }
                                case "ut":
                                case "usetime":
                                    int ut;
                                    if (!int.TryParse(args[i + 1], out ut))
                                    {
                                        caller.Reply($"Error, UseTime({args[i + 1]}) must be a number", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        if (ut < 0)
                                        {
                                            caller.Reply($"UseTime({args[i + 1]}) can't be negative, further attempts will result in a glitch", errorColor);
                                            break;
                                        }
                                        else
                                        {
                                            item.useTime = ut;
                                            break;
                                        }
                                    }
                                case "ua":
                                case "useanimation":
                                    int ua;
                                    if (!int.TryParse(args[i + 1], out ua))
                                    {
                                        caller.Reply($"Error, UseAnimation({args[i + 1]}) must be a number", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        if (ua < 0)
                                        {
                                            caller.Reply($"UseAnimation({args[i + 1]}) can't be negative, further attempts will result in a glitch", errorColor);
                                            break;
                                        }
                                        else
                                        {
                                            item.useAnimation = ua;
                                            break;
                                        }
                                    }
                                case "hl":
                                case "heallife":
                                    int hl;
                                    if (!int.TryParse(args[i + 1], out hl))
                                    {
                                        caller.Reply($"Error, HealLife({args[i + 1]}) must be a number", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        item.healLife = hl;
                                        break;
                                    }
                                case "hm":
                                case "healmana":
                                    int hm;
                                    if (!int.TryParse(args[i + 1], out hm))
                                    {
                                        caller.Reply($"Error, HealMana({args[i + 1]}) must be a number", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        item.healMana = hm;
                                        break;
                                    }
                                default:
                                    caller.Reply($"{args[i]} is an invalid parameter", errorColor);
                                    break;
                            }
                            i++;
                        }
                    }
                    else
                    {
                        caller.Reply($"Invalid Syntax: {input}", errorColor);
                        return;
                    }
                }

                caller.Player.inventory[ItemModifier.FindSlot(caller.Player.inventory)] = item;


                string Reply = $"Spawned [i/s{item.stack}p{item.prefix}:{item.type}] [{item.stack} {item.AffixName()}(s)]";

                List<string> Properties = new List<string> { };
                if (Config.ShowProperties)
                {
                    if (!item.accessory || !ogitem.accessory || Config.ShowUnnecessary)
                    {
                        string ut = $"UseTime {item.useTime}";
                        if (item.useTime != ogitem.useTime)
                        {
                            ut += $"(Normal: {ogitem.useTime})";
                        }
                        Properties.Add(ut);

                        string ua = $"UseAnimation {item.useAnimation}";
                        if (item.useAnimation != ogitem.useAnimation)
                        {
                            ua += $"(Normal: {ogitem.useAnimation})";
                        }
                        Properties.Add(ua);
                    }

                    if (item.shoot != 0 || ogitem.shoot != 0 || Config.ShowUnnecessary)
                    {
                        string s = "Shoot";
                        if (Config.ShowPID) s += "(ProjectileID)";
                        s += $" {item.shoot}";
                        if (item.shoot != ogitem.shoot)
                        {
                            s += $"(Normal: {ogitem.shoot})";
                        }
                        Properties.Add(s);
                    }

                    if (item.shootSpeed != 0 || ogitem.shootSpeed != 0 || Config.ShowUnnecessary)
                    {
                        string ss = $"ShootSpeed {item.shootSpeed}";
                        if (item.shootSpeed != ogitem.shootSpeed)
                        {
                            ss += $"(Normal: {ogitem.shootSpeed})";
                        }
                        Properties.Add(ss);
                    }

                    if (item.createTile > -1 || ogitem.createTile > -1 || Config.ShowUnnecessary)
                    {
                        string ct = $"CreateTile: {item.createTile}";
                        if (item.createTile != ogitem.createTile)
                        {
                            ct += $"(Normal: {ogitem.createTile})";
                        }
                        Properties.Add(ct);
                    }

                    if (item.healLife != 0 || ogitem.healLife != 0 || Config.ShowUnnecessary)
                    {
                        string hl = $"HealLife: {item.healLife}";
                        if (item.healLife != ogitem.healLife)
                        {
                            hl += $"(Normal: {ogitem.healLife})";
                        }
                        Properties.Add(hl);
                    }

                    if (item.healMana != 0 || ogitem.healMana != 0 || Config.ShowUnnecessary)
                    {
                        string hm = $"HealMana: {item.healMana}";
                        if (item.healMana != ogitem.healMana)
                        {
                            hm += $"(Normal: {ogitem.healMana})";
                        }
                        Properties.Add(hm);
                    }

                    Reply += "\nProperties:";

                    for (int i = 0; i < Properties.Count; i++)
                    {
                        if (Properties[i] != "")
                        {
                            Reply += " " + Properties[i];
                            if(i < Properties.Count-1)
                            {
                                Reply += ",";
                            }
                        }
                    }
                }

                caller.Reply(Reply, replyColor);
            }
        }
    }
}
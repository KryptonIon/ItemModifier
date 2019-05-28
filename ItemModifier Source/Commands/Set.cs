using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using ItemModifier.Utilities;
using System.Collections.Generic;

namespace ItemModifier.Commands
{
    public class Set : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "set";

        public override string Description => "Modifies the data of an item";

        public override string Usage => "/set [Parameters]<Property> <Value>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;

            string help = Description +
                $"\nUsage: {Usage}, examples" +
                "\n/set ct 26, Modifies the CreateTile property of the selected item(yellow box) to 26" +
                "\n/set ct 26 s 5, Modifies CreateTile to 26 and Shoot to 5";

            string parameters = "Parameters: shoot or s, shootspeed or ss, createtile or ct, usetime or ut, useanimation or ua, healmana or hm, heallife or hl" +
                    "\nParameters are caps insensitive meaning, you can do /set ShOoT 5 and it will still shoot a jester's arrow" +
                    "\nParameters have shortcuts, eg /set s 5, they're listed above";

            if(!CommandHelper.ValidifySyntax(caller, args, 2, help, ItemModifier.ConstructIdentifier("params", parameters)))
            {
                return;
            }

            if (MouseItem.type > 0)
            {
                Item umitem = new Item();
                umitem.SetDefaults(MouseItem.type);
                if (args.Length % 2 == 0)
                {
                    for (int i = 0; i < args.Length; i++)
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
                                        caller.Reply($"ProjectileID({args[i+1]}) can't be negative");
                                        break;
                                    }
                                    else if (s == ProjectileID.Count)
                                    {
                                        caller.Reply($"Sorry, ProjectileID: {ProjectileID.Count} is prohibited from use due to issues with crashing", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        MouseItem.shoot = s;
                                        break;
                                    }
                                }
                            case "ss":
                            case "shootspeed":
                                int ss;
                                if (!int.TryParse(args[i + 1], out ss))
                                {
                                    caller.Reply($"Error, Shoot Speed must be a number: {args[i+1]}", errorColor);
                                    break;
                                }
                                else
                                {
                                    if (ss < 0)
                                    {
                                        caller.Reply("Shoot Speed can't be negative");
                                        break;
                                    }
                                    else
                                    {
                                        MouseItem.shootSpeed = ss;
                                        break;
                                    }
                                }
                            case "ct":
                            case "createtile":
                                int ct;
                                if (!int.TryParse(args[i + 1], out ct))
                                {
                                    caller.Reply($"Error, TileID({args[i+1]}) must be a number", errorColor);
                                    break;
                                }
                                else
                                {
                                    if (ct < -1)
                                    {
                                        caller.Reply($"TileID({args[i+1]} can't be negative(-1 Excluded)");
                                        break;
                                    }
                                    else if (ct > TileLoader.TileCount - 1)
                                    {
                                        caller.Reply($"TileID({args[i+1]}) is bigger than tile count: {TileLoader.TileCount - 1}", errorColor);
                                        break;
                                    }
                                    else if (ct == TileID.Count)
                                    {
                                        caller.Reply($"Sorry, TileID: {TileID.Count} is prohibited from use due to issues with crashing", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        MouseItem.createTile = ct;
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
                                        caller.Reply($"UseTime({args[i+1]}) can't be negative, further attempts will result in a glitch", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        MouseItem.useTime = ut;
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
                                        caller.Reply($"UseAnimation({args[i+1]}) can't be negative, further attempts will result in a glitch", errorColor);
                                        break;
                                    }
                                    else
                                    {
                                        MouseItem.useAnimation = ua;
                                        break;
                                    }
                                }
                            case "hl":
                            case "heallife":
                                int hl;
                                if (!int.TryParse(args[i + 1], out hl))
                                {
                                    caller.Reply($"Error, HealLife({args[i+1]}) must be a number", errorColor);
                                    break;
                                }
                                else
                                {
                                    MouseItem.healLife = hl;
                                    break;
                                }
                            case "hm":
                            case "healmana":
                                int hm;
                                if (!int.TryParse(args[i + 1], out hm))
                                {
                                    caller.Reply($"Error, HealMana({args[i+1]}) must be a number", errorColor);
                                    break;
                                }
                                else
                                {
                                    MouseItem.healMana = hm;
                                    break;
                                }
                            default:
                                caller.Reply($"{args[i]} is an invalid parameter", errorColor);
                                break;
                        }
                        i++;
                    }

                    string Reply = $"Modified [i/s{MouseItem.stack}p{MouseItem.prefix}:{MouseItem.type}] ({MouseItem.stack} {MouseItem.AffixName()}(s))";

                    List<string> Properties = new List<string> { };
                    if (Config.ShowProperties)
                    {
                        if (!MouseItem.accessory || !umitem.accessory || Config.ShowUnnecessary)
                        {
                            string ut = $"UseTime {MouseItem.useTime}";
                            if (MouseItem.useTime != umitem.useTime)
                            {
                                ut += $"(Normal: {umitem.useTime})";
                            }
                            Properties.Add(ut);

                            string ua = $"UseAnimation {MouseItem.useAnimation}";
                            if (MouseItem.useAnimation != umitem.useAnimation)
                            {
                                ua += $"(Normal: {umitem.useAnimation})";
                            }
                            Properties.Add(ua);
                        }

                        if (MouseItem.shoot != 0 || umitem.shoot != 0 || Config.ShowUnnecessary)
                        {
                            string s = "Shoot";
                            if (Config.ShowPID) s += "(ProjectileID)";
                            s += $" {MouseItem.shoot}";
                            if (MouseItem.shoot != umitem.shoot)
                            {
                                s += $"(Normal: {umitem.shoot})";
                            }
                            Properties.Add(s);
                        }

                        if (MouseItem.shootSpeed != 0 || umitem.shootSpeed != 0 || Config.ShowUnnecessary)
                        {
                            string ss = $"ShootSpeed {MouseItem.shootSpeed}";
                            if (MouseItem.shootSpeed != umitem.shootSpeed)
                            {
                                ss += $"(Normal: {umitem.shootSpeed})";
                            }
                            Properties.Add(ss);
                        }

                        if (MouseItem.createTile > -1 || umitem.createTile > -1 || Config.ShowUnnecessary)
                        {
                            string ct = $"CreateTile: {MouseItem.createTile}";
                            if (MouseItem.createTile != umitem.createTile)
                            {
                                ct += $"(Normal: {umitem.createTile})";
                            }
                            Properties.Add(ct);
                        }

                        if (MouseItem.healLife != 0 || umitem.healLife != 0 || Config.ShowUnnecessary)
                        {
                            string hl = $"HealLife: {MouseItem.healLife}";
                            if (MouseItem.healLife != umitem.healLife)
                            {
                                hl += $"(Normal: {umitem.healLife})";
                            }
                            Properties.Add(hl);
                        }

                        if (MouseItem.healMana != 0 || umitem.healMana != 0 || Config.ShowUnnecessary)
                        {
                            string hm = $"HealMana: {MouseItem.healMana}";
                            if (MouseItem.healMana != umitem.healMana)
                            {
                                hm += $"(Normal: {umitem.healMana})";
                            }
                            Properties.Add(hm);
                        }

                        Reply += "\nProperties:";

                        for (int i = 0; i < Properties.Count; i++)
                        {
                            if (Properties[i] != "")
                            {
                                Reply += " " + Properties[i];
                                if (i < Properties.Count - 1)
                                {
                                    Reply += ",";
                                }
                            }
                        }
                    }

                    caller.Reply(Reply, replyColor);
                }
                else
                {
                    caller.Reply($"Invalid Syntax: {input}", errorColor);
                    return;
                }
            }
            else
            {
                caller.Reply("No Item Selected", errorColor);
                return;
            }
        }
    }
}
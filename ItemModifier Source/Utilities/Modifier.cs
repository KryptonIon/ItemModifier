using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ItemModifier.Utilities
{
    public class Modifier
    {
        internal static void ModifyItem(Item item, CommandCaller caller, string[] args, int start)
        {
            var errorColor = ItemModifier.errorColor;

            for (int i = start; i < args.Length; i++)
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
                    case "tb":
                    case "tileboost":
                        int tb;
                        if (!int.TryParse(args[i + 1], out tb))
                        {
                            caller.Reply($"Error, TileBoost({args[i + 1]}) must be a number", errorColor);
                            break;
                        }
                        else
                        {
                            item.tileBoost = tb;
                            break;
                        }
                    case "p":
                    case "pick":
                    case "pickaxe":
                        int p;
                        if (!int.TryParse(args[i + 1], out p))
                        {
                            caller.Reply($"Error, Pickaxe Power({args[i + 1]}) must be a number", errorColor);
                            break;
                        }
                        else
                        {
                            item.pick = p;
                            break;
                        }
                    case "a":
                    case "axe":
                        int a;
                        if (!int.TryParse(args[i + 1], out a))
                        {
                            caller.Reply($"Error, Axe Power({args[i + 1]}) must be a number", errorColor);
                            break;
                        }
                        else
                        {
                            item.axe = a;
                            break;
                        }
                    case "h":
                    case "hammer":
                        int h;
                        if (!int.TryParse(args[i + 1], out h))
                        {
                            caller.Reply($"Error, Hammer({args[i + 1]}) must be a number", errorColor);
                            break;
                        }
                        else
                        {
                            item.hammer = h;
                            break;
                        }
                    case "kb":
                    case "knockback":
                        int kb;
                        if (!int.TryParse(args[i + 1], out kb))
                        {
                            caller.Reply($"Error, Knockback({args[i + 1]}) must be a number", errorColor);
                            break;
                        }
                        else
                        {
                            item.knockBack = kb;
                            break;
                        }
                    case "d":
                    case "damage":
                        int d;
                        if (!int.TryParse(args[i + 1], out d))
                        {
                            caller.Reply($"Error, Damage({args[i + 1]}) must be a number", errorColor);
                            break;
                        }
                        else
                        {
                            item.damage = d;
                            break;
                        }
                    case "c":
                    case "crit":
                    case "critical":
                        int c;
                        if (!int.TryParse(args[i + 1], out c))
                        {
                            caller.Reply($"Error, Critical Strike Chance({args[i + 1]}) must be a number", errorColor);
                            break;
                        }
                        else
                        {
                            item.crit = c;
                            break;
                        }
                    case "au":
                    case "autoreuse":
                        bool au;
                        if (!bool.TryParse(args[i + 1], out au))
                        {
                            caller.Reply($"Error, AutoReuse({args[i + 1]}) must be a bool(true/false)", errorColor);
                            break;
                        }
                        else
                        {
                            item.autoReuse = au;
                            break;
                        }
                    default:
                        caller.Reply($"{args[i]} is an invalid parameter", errorColor);
                        break;
                }
                i++;
            }
        }

        internal static string GetProperties(Item item)
        {
            string Reply = "";
            Item ogitem = new Item();
            ogitem.SetDefaults(item.type);
            List<string> Properties = new List<string> { };

            if (!item.accessory || !ogitem.accessory || Config.ShowUnnecessary)
            {
                string ut = $"UseTime: {item.useTime}";
                if (item.useTime != ogitem.useTime)
                {
                    ut += $"(Normal: {ogitem.useTime})";
                }
                Properties.Add(ut);

                string ua = $"UseAnimation: {item.useAnimation}";
                if (item.useAnimation != ogitem.useAnimation)
                {
                    ua += $"(Normal: {ogitem.useAnimation})";
                }
                Properties.Add(ua);
            }

            if (item.shoot != 0 || ogitem.shoot != 0 || Config.ShowUnnecessary)
            {
                string s = $"Shoot: {item.shoot}";
                if (item.shoot != ogitem.shoot)
                {
                    s += $"(Normal: {ogitem.shoot})";
                }
                Properties.Add(s);
            }

            if (item.shootSpeed != 0 || ogitem.shootSpeed != 0 || Config.ShowUnnecessary)
            {
                string ss = $"ShootSpeed: {item.shootSpeed}";
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

            if (item.tileBoost != 0 || ogitem.tileBoost != 0 || Config.ShowUnnecessary)
            {
                string tb = $"+Range/TileBoost: {item.tileBoost}";
                if (item.tileBoost != ogitem.tileBoost)
                {
                    tb += $"(Normal: {ogitem.tileBoost})";
                }
                Properties.Add(tb);
            }

            if (item.pick != 0 || ogitem.pick != 0 || Config.ShowUnnecessary)
            {
                string p = $"Pickaxe Power: {item.pick}";
                if (item.pick != ogitem.pick)
                {
                    p += $"(Normal: {ogitem.pick})";
                }
                Properties.Add(p);
            }

            if (item.axe != 0 || ogitem.axe != 0 || Config.ShowUnnecessary)
            {
                string a = $"Axe Power: {item.axe}";
                if (item.axe != ogitem.axe)
                {
                    a += $"(Normal: {ogitem.axe})";
                }
                Properties.Add(a);
            }

            if (item.hammer != 0 || ogitem.hammer != 0 || Config.ShowUnnecessary)
            {
                string h = $"Hammer Power: {item.hammer}";
                if (item.hammer != ogitem.hammer)
                {
                    h += $"(Normal: {ogitem.hammer})";
                }
                Properties.Add(h);
            }

            if (item.knockBack != 0 || ogitem.knockBack != 0 || Config.ShowUnnecessary)
            {
                string kb = $"Knockback: {item.knockBack}";
                if (item.knockBack != ogitem.knockBack)
                {
                    kb += $"(Normal: {ogitem.knockBack})";
                }
                Properties.Add(kb);
            }

            if (item.damage != 0 || ogitem.damage != 0 || Config.ShowUnnecessary)
            {
                string d = $"Damage: {item.damage}";
                if (item.damage != ogitem.damage)
                {
                    d += $"(Normal: {ogitem.damage})";
                }
                Properties.Add(d);
            }

            if (item.crit != 0 || ogitem.crit != 0 || Config.ShowUnnecessary)
            {
                string c = $"Critical Strike Chance: {item.crit}";
                if (item.crit != ogitem.crit)
                {
                    c += $"(Normal: {ogitem.crit})";
                }
                Properties.Add(c);
            }

            if (item.autoReuse || ogitem.autoReuse || Config.ShowUnnecessary)
            {
                string au = $"AutoReuse: {item.autoReuse}";
                if (item.autoReuse != ogitem.autoReuse)
                {
                    au += $"(Normal: {ogitem.autoReuse})";
                }
                Properties.Add(au);
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

            return Reply;
        }

        internal static string GetProperties2(Item item, CommandCaller caller, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            Item umitem = new Item();
            string Reply = "";
            umitem.SetDefaults(item.type);
            List<string> Properties = new List<string> { };

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "s":
                    case "shoot":
                        string s = $"Shoot: {item.shoot}";
                        if (item.shoot != umitem.shoot)
                        {
                            s += $"(Normal: {umitem.shoot})";
                        }
                        Properties.Add(s);
                        break;
                    case "ss":
                    case "shootspeed":
                        string ss = $"ShootSpeed: {item.shootSpeed}";
                        if (item.shootSpeed != umitem.shootSpeed)
                        {
                            ss += $"(Normal: {umitem.shootSpeed})";
                        }
                        Properties.Add(ss);
                        break;
                    case "ct":
                    case "createtile":
                        string ct = $"CreateTile: {item.createTile}";
                        if (item.createTile != umitem.createTile)
                        {
                            ct += $"(Normal: {umitem.createTile})";
                        }
                        Properties.Add(ct);
                        break;
                    case "ut":
                    case "usetime":
                        string ut = $"UseTime: {item.useTime}";
                        if (item.useTime != umitem.useTime)
                        {
                            ut += $"(Normal: {umitem.useTime})";
                        }
                        Properties.Add(ut);
                        break;
                    case "ua":
                    case "useanimation":
                        string ua = $"UseAnimation: {item.useAnimation}";
                        if (item.useAnimation != umitem.useAnimation)
                        {
                            ua += $"(Normal: {umitem.useAnimation})";
                        }
                        Properties.Add(ua);
                        break;
                    case "hl":
                    case "heallife":
                        string hl = $"HealLife: {item.healLife}";
                        if (item.healLife != umitem.healLife)
                        {
                            hl += $"(Normal: {umitem.healLife})";
                        }
                        Properties.Add(hl);
                        break;
                    case "hm":
                    case "healmana":
                        string hm = $"HealMana: {item.healMana}";
                        if (item.healMana != umitem.healMana)
                        {
                            hm += $"(Normal: {umitem.healMana})";
                        }
                        Properties.Add(hm);
                        break;
                    case "tb":
                    case "tileboost":
                        string tb = $"+Range/TileBoost: {item.tileBoost}";
                        if (item.tileBoost != umitem.tileBoost)
                        {
                            tb += $"(Normal: {umitem.tileBoost})";
                        }
                        Properties.Add(tb);
                        break;
                    case "p":
                    case "pick":
                    case "pickaxe":
                        string p = $"Pickaxe Power: {item.pick}";
                        if (item.pick != umitem.pick)
                        {
                            p += $"(Normal: {umitem.pick})";
                        }
                        Properties.Add(p);
                        break;
                    case "a":
                    case "axe":
                        string a = $"Axe Power: {item.axe}";
                        if (item.axe != umitem.axe)
                        {
                            a += $"(Normal: {umitem.axe})";
                        }
                        Properties.Add(a);
                        break;
                    case "h":
                    case "hammer":
                        string h = $"Hammer Power: {item.hammer}";
                        if (item.hammer != umitem.hammer)
                        {
                            h += $"(Normal: {umitem.hammer})";
                        }
                        Properties.Add(h);
                        break;
                    case "kb":
                    case "knockback":
                        string kb = $"Knockback: {item.knockBack}";
                        if (item.knockBack != umitem.knockBack)
                        {
                            kb += $"(Normal: {umitem.knockBack})";
                        }
                        Properties.Add(kb);
                        break;
                    case "d":
                    case "damage":
                        string d = $"Damage: {item.damage}";
                        if (item.damage != umitem.damage)
                        {
                            d += $"(Normal: {umitem.damage})";
                        }
                        Properties.Add(d);
                        break;
                    case "c":
                    case "crit":
                    case "critical":
                        string c = $"Critical Strike Chance: {item.crit}";
                        if (item.crit != umitem.crit)
                        {
                            c += $"(Normal: {umitem.crit})";
                        }
                        Properties.Add(c);
                        break;
                    case "au":
                    case "autoreuse":
                        string au = $"AutoReuse: {item.autoReuse}";
                        if (item.autoReuse != umitem.autoReuse)
                        {
                            au += $"(Normal: {umitem.autoReuse})";
                        }
                        Properties.Add(au);
                        break;
                    default:
                        caller.Reply($"{args[i]} is an invalid parameter", errorColor);
                        break;
                }
            }

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

            return Reply;
        }

        internal static string GetItem(Item item)
        {
            string Reply = "[i";

            if (item.stack != 1 || item.prefix != 0)
            {
                Reply += "/";
                if (item.stack != 1)
                {
                    Reply += $"s{item.stack}";
                }

                if (item.prefix != 0)
                {
                    Reply += $"p{item.prefix}";
                }
            }

            Reply += $":{item.type}] [{item.stack} {item.AffixName()}(s)]";

            return Reply;
        }

        internal static string GetItem2(Item item)
        {
            string Reply = "[i";

            if (item.stack != 1 || item.prefix != 0)
            {
                Reply += "/";
                if (item.stack != 1)
                {
                    Reply += $"s{item.stack}";
                }

                if (item.prefix != 0)
                {
                    Reply += $"p{item.prefix}";
                }
            }

            Reply += $":{item.type}]";

            return Reply;
        }
    }
}

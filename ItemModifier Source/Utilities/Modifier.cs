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
            var errorColor = Config.errorColor;

            for (int i = start; i < args.Length; i++)
            {
                var value = args[i + 1];

                switch (args[i].ToLower())
                {
                    case "s":
                    case "shoot":
                        ModifyShoot(caller, item, value, false);
                        break;
                    case "ss":
                    case "shootspeed":
                        ModifyShootSpeed(caller, item, value, false);
                        break;
                    case "ct":
                    case "createtile":
                        ModifyCreateTile(caller, item, value, false);
                        break;
                    case "ut":
                    case "usetime":
                        ModifyUseTime(caller, item, value, false);
                        break;
                    case "ua":
                    case "useanimation":
                        ModifyUseAnimation(caller, item, value, false);
                        break;
                    case "hl":
                    case "heallife":
                        ModifyHealLife(caller, item, value, false);
                        break;
                    case "hm":
                    case "healmana":
                        ModifyHealMana(caller, item, value, false);
                        break;
                    case "tb":
                    case "tileboost":
                        ModifyTileBoost(caller, item, value, false);
                        break;
                    case "p":
                    case "pick":
                    case "pickaxe":
                        ModifyPickaxe(caller, item, value, false);
                        break;
                    case "a":
                    case "axe":
                        ModifyAxe(caller, item, value, false);
                        break;
                    case "h":
                    case "hammer":
                        ModifyHammer(caller, item, value, false);
                        break;
                    case "kb":
                    case "knockback":
                        ModifyKnockBack(caller, item, value, false);
                        break;
                    case "d":
                    case "damage":
                        ModifyDamage(caller, item, value, false);
                        break;
                    case "c":
                    case "crit":
                    case "critical":
                        ModifyCritical(caller, item, value, false);
                        break;
                    case "au":
                    case "autoreuse":
                        ModifyAutoReuse(caller, item, value, false);
                        break;
                    case "bid":
                    case "buff":
                    case "buffid":
                        ModifyBuffID(caller, item, value, false);
                        break;
                    case "bufftime":
                    case "bt":
                        ModifyBuffTime(caller, item, value, false);
                        break;
                    case "amount":
                    case "amt":
                    case "stack":
                    case "st":
                        ModifyStack(caller, item, value, false);
                        break;
                    case "cons":
                    case "consumable":
                        ModifyConsumable(caller, item, value, false);
                        break;
                    case "pot":
                    case "potion":
                        ModifyPotion(caller, item, value, false);
                        break;
                    case "maxstack":
                    case "ms":
                        ModifyMaxStack(caller, item, value, false);
                        break;
                    default:
                        caller.Reply($"{args[i]} is an invalid parameter", errorColor);
                        break;
                }
                i++;
            }
        }

        internal static string GetProperties(Item item, bool newLine = true)
        {
            string Reply = "";
            Item ogitem = new Item();
            ogitem.SetDefaults(item.type);
            List<string> Properties = new List<string> { };

            if (!item.accessory || !ogitem.accessory || Config.ShowUnnecessary)
            {
                string s = $"UseTime: {item.useTime}";
                if (item.useTime != ogitem.useTime)
                {
                    s += $"(Normal: {ogitem.useTime})";
                }
                Properties.Add(s);

                string s2 = $"UseAnimation: {item.useAnimation}";
                if (item.useAnimation != ogitem.useAnimation)
                {
                    s2 += $"(Normal: {ogitem.useAnimation})";
                }
                Properties.Add(s2);
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
                string s = $"ShootSpeed: {item.shootSpeed}";
                if (item.shootSpeed != ogitem.shootSpeed)
                {
                    s += $"(Normal: {ogitem.shootSpeed})";
                }
                Properties.Add(s);
            }

            if (item.createTile > -1 || ogitem.createTile > -1 || Config.ShowUnnecessary)
            {
                string s = $"CreateTile: {item.createTile}";
                if (item.createTile != ogitem.createTile)
                {
                    s += $"(Normal: {ogitem.createTile})";
                }
                Properties.Add(s);
            }

            if (item.healLife != 0 || ogitem.healLife != 0 || Config.ShowUnnecessary)
            {
                string s = $"HealLife: {item.healLife}";
                if (item.healLife != ogitem.healLife)
                {
                    s += $"(Normal: {ogitem.healLife})";
                }
                Properties.Add(s);
            }

            if (item.healMana != 0 || ogitem.healMana != 0 || Config.ShowUnnecessary)
            {
                string s = $"HealMana: {item.healMana}";
                if (item.healMana != ogitem.healMana)
                {
                    s += $"(Normal: {ogitem.healMana})";
                }
                Properties.Add(s);
            }

            if (item.tileBoost != 0 || ogitem.tileBoost != 0 || Config.ShowUnnecessary)
            {
                string s = $"+Range/TileBoost: {item.tileBoost}";
                if (item.tileBoost != ogitem.tileBoost)
                {
                    s += $"(Normal: {ogitem.tileBoost})";
                }
                Properties.Add(s);
            }

            if (item.pick != 0 || ogitem.pick != 0 || Config.ShowUnnecessary)
            {
                string s = $"Pickaxe Power: {item.pick}";
                if (item.pick != ogitem.pick)
                {
                    s += $"(Normal: {ogitem.pick})";
                }
                Properties.Add(s);
            }

            if (item.axe != 0 || ogitem.axe != 0 || Config.ShowUnnecessary)
            {
                string s = $"Axe Power: {item.axe}";
                if (item.axe != ogitem.axe)
                {
                    s += $"(Normal: {ogitem.axe})";
                }
                Properties.Add(s);
            }

            if (item.hammer != 0 || ogitem.hammer != 0 || Config.ShowUnnecessary)
            {
                string s = $"Hammer Power: {item.hammer}";
                if (item.hammer != ogitem.hammer)
                {
                    s += $"(Normal: {ogitem.hammer})";
                }
                Properties.Add(s);
            }

            if (item.knockBack != 0 || ogitem.knockBack != 0 || Config.ShowUnnecessary)
            {
                string s = $"Knockback: {item.knockBack}";
                if (item.knockBack != ogitem.knockBack)
                {
                    s += $"(Normal: {ogitem.knockBack})";
                }
                Properties.Add(s);
            }

            if (item.damage > 0 || ogitem.damage > 0 || Config.ShowUnnecessary)
            {
                string s = $"Damage: {item.damage}";
                if (item.damage != ogitem.damage)
                {
                    s += $"(Normal: {ogitem.damage})";
                }
                Properties.Add(s);
            }

            if (item.crit != 0 || ogitem.crit != 0 || Config.ShowUnnecessary)
            {
                string s = $"Critical Strike Chance: {item.crit}";
                if (item.crit != ogitem.crit)
                {
                    s += $"(Normal: {ogitem.crit})";
                }
                Properties.Add(s);
            }

            if (item.autoReuse || ogitem.autoReuse || Config.ShowUnnecessary)
            {
                string s = $"AutoReuse: {item.autoReuse}";
                if (item.autoReuse != ogitem.autoReuse)
                {
                    s += $"(Normal: {ogitem.autoReuse})";
                }
                Properties.Add(s);
            }

            if (item.buffType != 0 || ogitem.buffType != 0 || Config.ShowUnnecessary)
            {
                string s = $"BuffID: {item.buffType}";
                if (item.buffType != ogitem.buffType)
                {
                    s += $"(Normal: {ogitem.buffType})";
                }
                Properties.Add(s);
            }

            if (item.buffTime != 0 || ogitem.buffTime != 0 || Config.ShowUnnecessary)
            {
                string s = $"BuffTime: {item.buffTime}";
                if (item.buffTime != ogitem.buffTime)
                {
                    s += $"(Normal: {ogitem.buffTime})";
                }
                Properties.Add(s);
            }

            if (item.consumable != false || ogitem.consumable != false || Config.ShowUnnecessary)
            {
                string s = $"Consumable: {item.consumable}";
                if (item.consumable != ogitem.consumable)
                {
                    s += $"(Normal: {ogitem.consumable})";
                }
                Properties.Add(s);
            }

            if (item.potion != false || ogitem.potion != false || Config.ShowUnnecessary)
            {
                string s = $"Potion: {item.potion}";
                if (item.potion != ogitem.potion)
                {
                    s += $"(Normal: {ogitem.potion})";
                }
                Properties.Add(s);
            }

            if (Config.ShowMaxStack)
            {
                string s = $"MaxStack: {item.maxStack}";
                if (item.maxStack != ogitem.maxStack)
                {
                    s += $"(Normal: {ogitem.maxStack})";
                }
                Properties.Add(s);
            }

            if (newLine)
            {
                Reply += "\n";
            }

            Reply += "Properties:";

            for (int i = 0; i < Properties.Count; i++)
            {
                Reply += " " + Properties[i];
                if (i < Properties.Count - 1)
                {
                    Reply += ",";
                }
            }

            return Reply;
        }

        internal static string GetProperties2(Item item, CommandCaller caller, string[] args)
        {
            var errorColor = Config.errorColor;
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
                    case "bid":
                    case "buff":
                    case "buffid":
                        string bid = $"BuffID: {item.buffType}";
                        if (item.buffType != umitem.buffType)
                        {
                            bid += $"(Normal: {umitem.buffType})";
                        }
                        Properties.Add(bid);
                        break;
                    case "bt":
                    case "bufftime":
                        string bt = $"BuffTime: {item.buffTime}";
                        if (item.buffTime != umitem.buffTime)
                        {
                            bt += $"(Normal: {umitem.buffTime})";
                        }
                        Properties.Add(bt);
                        break;
                    case "cons":
                    case "consumable":
                        string cons = $"Consumable: {item.consumable}";
                        if (item.consumable != umitem.consumable)
                        {
                            cons += $"(Normal: {umitem.consumable})";
                        }
                        Properties.Add(cons);
                        break;
                    case "pot":
                    case "potion":
                        string pot = $"Potion: {item.potion}";
                        if (item.potion != umitem.potion)
                        {
                            pot += $"(Normal: {umitem.potion})";
                        }
                        Properties.Add(pot);
                        break;
                    case "ms":
                    case "maxstack":
                        string ms = $"MaxStack: {item.maxStack}";
                        if (item.maxStack != umitem.maxStack)
                        {
                            ms += $"(Normal: {umitem.maxStack})";
                        }
                        Properties.Add(ms);
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

        internal static void ModifyAutoReuse(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            bool v;
            if (!bool.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("bool", "AutoReuse", value), errorColor);
            }
            else
            {
                item.autoReuse = v;
                if (successMessage)
                {
                    caller.Reply($"Set {GetItem2(item)}'s AutoReuse to {value}", replyColor);
                }
                return;
            }
        }

        internal static void ModifyAxe(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Axe Power", value), errorColor);
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("Axe Power", v), errorColor);
                    return;
                }
                else
                {
                    item.axe = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s Axe Power to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyCreateTile(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "CreateTile", value), errorColor);
            }
            else
            {
                if (v > TileLoader.TileCount - 1)
                {
                    caller.Reply(ErrorHandler.BiggerThanError("TileID", v, "Tile Count", TileLoader.TileCount), errorColor);
                    return;
                }
                else if (v < -1)
                {
                    caller.Reply(ErrorHandler.NegativeError("CreateTile", v), errorColor);
                    return;
                }
                else if (v == TileID.Count)
                {
                    caller.Reply($"Sorry, TileID: {TileID.Count} is prohibited from use due to issues with crashing", errorColor);
                    return;
                }
                else
                {
                    item.createTile = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s CreateTile property to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyCritical(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Critical Strike Chance", value), errorColor);
            }
            else
            {
                item.crit = v;
                if (successMessage)
                {
                    caller.Reply($"Set {GetItem2(item)}'s Critical Strike Chance to {value}", replyColor);
                }
                return;
            }
        }

        internal static void ModifyDamage(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Damage", value), errorColor);
            }
            else
            {
                if (v < -1)
                {
                    caller.Reply(ErrorHandler.NegativeError("Damage", v), errorColor);
                    return;
                }
                else
                {
                    item.damage = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s Damage to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyHammer(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Hammer Power", value), errorColor);
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("Hammer Power", v), errorColor);
                    return;
                }
                else
                {
                    item.hammer = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s Hammer Power to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyHealLife(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "HP", value), errorColor);
                return;
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("HP", v), errorColor);
                }
                else
                {
                    item.healLife = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s HealLife property to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyHealMana(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Mana", value), errorColor);
                return;
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("Mana", v), errorColor);
                }
                else
                {
                    item.healMana = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s HealMana property to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyKnockBack(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            float v;
            if (!float.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("float", "Knockback", value), errorColor);
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("Knockback", v), errorColor);
                    return;
                }
                else
                {
                    item.knockBack = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s Knockback to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyPickaxe(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Pickaxe Power", value), errorColor);
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("Pickaxe Power", v), errorColor);
                    return;
                }
                else
                {
                    item.pick = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s Pickaxe Power to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyShoot(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "ProjectileID", value), errorColor);
            }
            else
            {
                if (v > ProjectileLoader.ProjectileCount)
                {
                    caller.Reply(ErrorHandler.BiggerThanError("ProjectileID", v, "Projectile Count", ProjectileLoader.ProjectileCount), errorColor);
                    return;
                }
                else if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("ProjectileID", v), errorColor);
                    return;
                }
                else if (v == ProjectileID.Count)
                {
                    caller.Reply($"Sorry, ProjectileID: {ProjectileID.Count} is prohibited from use due to issues with crashing", errorColor);
                    return;
                }
                else
                {
                    item.shoot = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s Shoot property to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyShootSpeed(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int/float", "ShootSpeed", value), errorColor);
            }
            else
            {
                item.shootSpeed = v;
                if (successMessage)
                {
                    caller.Reply($"Set {GetItem2(item)}'s ShootSpeed to {value}", replyColor);
                }
                return;
            }
        }

        internal static void ModifyTileBoost(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "+Range", value), errorColor);
            }
            else
            {
                item.tileBoost = v;
                if (successMessage)
                {
                    caller.Reply($"Set {GetItem2(item)}'s TileBoost to {value}", replyColor);
                }
                return;
            }
        }

        internal static void ModifyUseAnimation(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "UseAnimation", value), errorColor);
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("UseAnimation", v), errorColor);
                    return;
                }
                else
                {
                    item.useAnimation = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s UseAnimation to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyUseTime(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "UseTime", value), errorColor);
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("UseTime", v), errorColor);
                    return;
                }
                else
                {
                    item.useTime = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s UseTime to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyBuffID(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "BuffID", value), errorColor);
                return;
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("BuffID", v), errorColor);
                    return;
                }
                else if (v > BuffLoader.BuffCount)
                {
                    caller.Reply(ErrorHandler.BiggerThanError("BuffID", v, "Buff Count", BuffLoader.BuffCount), errorColor);
                    return;
                }
                else if (v == BuffID.Count)
                {
                    caller.Reply($"Sorry, BuffID: {BuffID.Count} is prohibited from use due to issues with crashing", errorColor);
                    return;
                }
                else
                {
                    item.buffType = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s BuffID to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyBuffTime(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "BuffTime", value), errorColor);
                return;
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("BuffTime", v), errorColor);
                    return;
                }
                else
                {
                    item.buffTime = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s BuffTime to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyStack(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Stack", value), errorColor);
                return;
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("Stack", v), errorColor);
                    return;
                }
                else
                {
                    item.stack = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s Stack to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyMaxStack(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            int v;
            if (!int.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "MaxStack", value), errorColor);
                return;
            }
            else
            {
                if (v < 0)
                {
                    caller.Reply(ErrorHandler.NegativeError("MaxStack", v), errorColor);
                    return;
                }
                else
                {
                    item.maxStack = v;
                    if (successMessage)
                    {
                        caller.Reply($"Set {GetItem2(item)}'s MaxStack to {value}", replyColor);
                    }
                    return;
                }
            }
        }

        internal static void ModifyConsumable(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            bool v;
            if (!bool.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Consumability", value), errorColor);
                return;
            }
            else
            {
                item.consumable = v;
                if (successMessage)
                {
                    caller.Reply($"Set {GetItem2(item)}'s Consumability to {value}", replyColor);
                }
                return;
            }
        }

        internal static void ModifyPotion(CommandCaller caller, Item item, string value, bool successMessage = true)
        {
            var errorColor = Config.errorColor;
            var replyColor = Config.replyColor;

            bool v;
            if (!bool.TryParse(value, out v))
            {
                caller.Reply(ErrorHandler.ParsingError("int", "Potion(ness)", value), errorColor);
                return;
            }
            else
            {
                item.potion = v;
                if (successMessage)
                {
                    caller.Reply($"Set {GetItem2(item)}'s Potion(ness) to {value}", replyColor);
                }
                return;
            }
        }
    }
}
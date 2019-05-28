using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;

namespace ItemModifier.Commands
{
    public class Properties : ModCommand
    {
        public override CommandType Type => CommandType.Chat;

        public override string Command => "p";

        public override string Description => "Gets the data of an Item";

        public override string Usage => "/p (Optional Parameters)<Property>";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var errorColor = ItemModifier.errorColor;
            var replyColor = ItemModifier.replyColor;
            var MouseItem = caller.Player.HeldItem;
            Item umitem = new Item();
            umitem.SetDefaults(MouseItem.type);

            if (MouseItem.type > 0)
            {
                if (args.Length <= 0)
                {
                    string Reply = $"[i/s{MouseItem.stack}p{MouseItem.prefix}:{MouseItem.type}]'s properties are:";

                    List<string> Properties = new List<string> { };
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

                    caller.Reply(Reply, replyColor);
                }
                else
                {
                    string Reply = "Requested Properties are:";
                    List<string> Properties = new List<string> { };
                    for(int i = 0; i < args.Length; i++)
                    {
                        switch(args[i].ToLower())
                        {
                            case "s":
                            case "shoot":
                                string s = "Shoot";
                                if(Config.ShowPID) s += "(ProjectileID)";
                                s += $" {MouseItem.shoot}";
                                if (MouseItem.shoot != umitem.shoot)
                                {
                                    s += $"(Normal: {umitem.shoot})";
                                }
                                Properties.Add(s);
                                break;
                            case "ss":
                            case "shootspeed":
                                string ss = "ShootSpeed";
                                ss += $" {MouseItem.shootSpeed}";
                                if (MouseItem.shootSpeed != umitem.shootSpeed)
                                {
                                    ss += $"(Normal: {umitem.shootSpeed})";
                                }
                                Properties.Add(ss);
                                break;
                            case "ct":
                            case "createtile":
                                string ct = "CreateTile";
                                ct += $" {MouseItem.createTile}";
                                if (MouseItem.createTile != umitem.createTile)
                                {
                                    ct += $"(Normal: {umitem.createTile})";
                                }
                                Properties.Add(ct);
                                break;
                            case "ut":
                            case "usetime":
                                string ut = "UseTime";
                                ut += $" {MouseItem.useTime}";
                                if (MouseItem.useTime != umitem.useTime)
                                {
                                    ut += $"(Normal: {umitem.useTime})";
                                }
                                Properties.Add(ut);
                                break;
                            case "ua":
                            case "useanimation":
                                string ua = "UseAnimation";
                                ua += $" {MouseItem.useAnimation}";
                                if (MouseItem.useAnimation != umitem.useAnimation)
                                {
                                    ua += $"(Normal: {umitem.useAnimation})";
                                }
                                Properties.Add(ua);
                                break;
                            case "hl":
                            case "heallife":
                                string hl = "HealLife";
                                hl += $" {MouseItem.healLife}";
                                if (MouseItem.healLife != umitem.healLife)
                                {
                                    hl += $"(Normal: {umitem.healLife})";
                                }
                                Properties.Add(hl);
                                break;
                            case "hm":
                            case "healmana":
                                string hm = "HealMana";
                                hm += $" {MouseItem.healMana}";
                                if (MouseItem.healMana != umitem.healMana)
                                {
                                    hm += $"(Normal: {umitem.healMana})";
                                }
                                Properties.Add(hm);
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

                    caller.Reply(Reply, replyColor);
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
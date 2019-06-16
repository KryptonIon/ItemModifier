using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.Utilities
{
    public class ItemName
    {
        public static int GetID(string name, CommandCaller caller)
        {
            var replyColor = Config.replyColor;
            Item it = new Item();
            List<int> results = new List<int> { };

            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                it.SetDefaults(i);
                if (it.HoverName.ToLower().StartsWith(name.ToLower()))
                {
                    results.Add(i);
                }
            }

            if (Config.ShowResultList && results.Count > 1)
            {
                string resultReply = $"Found {results.Count} results:";
                for (int i = 0; i < results.Count; i++)
                {
                    it.SetDefaults(results[i]);
                    resultReply += $" {it.HoverName}({results[i]}),";
                }
                caller.Reply(resultReply, replyColor);
            }

            if (results.Count > 0)
            {
                if (Config.GetRandomItem && results.Count > 1)
                {
                    var rng = new Random();
                    return results[rng.Next(0, results.Count)];
                }
                else
                {
                    return results[0];
                }
            }
            else
            {
                return -1;
            }
        }

        public static string CombineString(string[] strings)
        {
            string combined = "";

            for (int i = 0; i < strings.Length; i++)
            {
                combined += strings[i];
                if (i < strings.Length - 1)
                {
                    combined += " ";
                }
            }

            return combined;
        }
    }
}
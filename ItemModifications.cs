using ItemModifier.Extensions;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier
{
    public class ItemModifications : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            List<ItemProperties> itemConfig = ItemConfig.Instance.ItemChanges;
            int index = itemConfig.FindIndex(prop => prop.Type == item.type);
            if (index != -1)
            {
                item.CopyItemProperties(itemConfig[index]);
            }
        }
    }
}

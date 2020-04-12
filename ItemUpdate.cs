using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier
{
    public class ItemUpdate : GlobalItem
    {
        public override void NetSend(Item item, BinaryWriter writer)
        {
            ModContent.GetInstance<ItemModifier>().WriteItemModifyPacket(writer, item);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            ModContent.GetInstance<ItemModifier>().ParseItemModifyPacket(reader, item);
        }
    }
}

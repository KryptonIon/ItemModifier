using Terraria;
using Terraria.ModLoader;

namespace ItemModifier
{
    public class CustomProperties : GlobalItem
    {
        public int[] BuffTypes { get; } = new int[21];

        public int[] BuffTimes { get; } = new int[21];

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override GlobalItem NewInstance(Item item)
        {
            return new CustomProperties();
        }

        public override bool UseItem(Item item, Player player)
        {
            for (int i = 0; i < BuffTypes.Length; i++)
            {
                player.AddBuff(BuffTypes[i], BuffTimes[i]);
            }
            return true;
        }
    }
}

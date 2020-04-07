using Terraria.GameInput;
using Terraria.ModLoader;

namespace ItemModifier.UIKit
{
    public class DisableScroll : ModPlayer
    {
        public override void PreUpdate()
        {
            if (ModContent.GetInstance<ItemModifier>().MouseWheelDisabled)
            {
                PlayerInput.ScrollWheelDelta = 0;
            }
        }
    }
}

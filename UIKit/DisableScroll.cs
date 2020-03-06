using Terraria.GameInput;
using Terraria.ModLoader;

namespace ItemModifier.UIKit
{
    public class DisableScroll : ModPlayer
    {
        public override void PreUpdate()
        {
            if (ItemModifier.Instance.MouseWheelDisabled) PlayerInput.ScrollWheelDelta = 0;
        }
    }
}

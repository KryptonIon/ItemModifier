using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.GameInput;
using Terraria.ModLoader;
using System.Threading.Tasks;

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

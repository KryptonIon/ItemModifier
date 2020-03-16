namespace ItemModifier.UIKit
{
    public readonly struct StyleDimension
    {
        public static readonly StyleDimension Fill = new StyleDimension(0f, 1f);

        public static readonly StyleDimension Empty = new StyleDimension(0f, 0f);

        public float Pixels { get; }

        public float Percent { get; }

        public StyleDimension(float pixels)
        {
            Pixels = pixels;
            Percent = 0;
        }

        public StyleDimension(float pixels, float percent)
        {
            Pixels = pixels;
            Percent = percent;
        }

        public float CalculateValue(float ContainerSize)
        {
            return Pixels + ContainerSize * Percent;
        }
    }
}

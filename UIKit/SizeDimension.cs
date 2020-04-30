namespace ItemModifier.UIKit
{
    public readonly struct SizeDimension
    {
        public static readonly SizeDimension Fill = new SizeDimension(0f, 1f);

        public static readonly SizeDimension Empty = new SizeDimension(0f, 0f);

        public float Pixels { get; }

        public float Percent { get; }

        public SizeDimension(float pixels) : this(pixels, 0)
        {

        }

        public SizeDimension(float pixels, float percent)
        {
            Pixels = pixels;
            Percent = percent;
        }

        public float CalculateValue(float containerSize)
        {
            return Pixels + containerSize * Percent;
        }

        public override bool Equals(object obj)
        {
            return obj is SizeDimension dimensions && this == dimensions;
        }

        public override int GetHashCode()
        {
            return Pixels.GetHashCode() + Percent.GetHashCode();
        }

        public static bool operator ==(SizeDimension a, SizeDimension b)
        {
            return a.Pixels == b.Pixels && a.Percent == b.Percent;
        }

        public static bool operator !=(SizeDimension a, SizeDimension b)
        {
            return a.Pixels != b.Pixels || a.Percent != b.Percent;
        }
    }
}

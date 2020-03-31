namespace ItemModifier.UIKit
{
    // From WPF
    public readonly struct Thickness
    {
        public float Top { get; }

        public float Bottom { get; }

        public float Left { get; }

        public float Right { get; }

        public Thickness(float pixels)
        {
            Top = pixels;
            Bottom = pixels;
            Left = pixels;
            Right = pixels;
        }

        public Thickness(float xPixels, float yPixels)
        {
            Top = yPixels;
            Bottom = yPixels;
            Left = xPixels;
            Right = xPixels;
        }

        public Thickness(float top, float bottom, float left, float right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
    }
}

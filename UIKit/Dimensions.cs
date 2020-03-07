using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    public readonly struct Dimensions
    {
        public float X { get; }

        public float Y { get; }

        public float Width { get; }

        public float Height { get; }

        public Vector2 Position => new Vector2(X, Y);

        public Vector2 Size => new Vector2(Width, Height);

        public Rectangle Rectangle => new Rectangle((int)X, (int)Y, (int)Width, (int)Height);

        public Dimensions(float X, float Y, float Width, float Height) => (this.X, this.Y, this.Width, this.Height) = (X, Y, Width, Height);
    }
}

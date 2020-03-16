using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    public readonly struct Dimensions
    {
        public float X { get; }

        public float Y { get; }

        public float Width { get; }

        public float Height { get; }

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(Width, Height);
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
            }
        }

        public Dimensions(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}

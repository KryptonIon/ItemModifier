using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    /// <summary>
    /// Represents the dimensions of UIElements.
    /// </summary>
    public readonly struct Dimensions
    {
        /// <summary>
        /// Position in the X Axis.
        /// </summary>
        public float X { get; }

        /// <summary>
        /// Position in the Y Axis.
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// Size in the X Axis.
        /// </summary>
        public float Width { get; }

        /// <summary>
        /// Size in the Y Axis.
        /// </summary>
        public float Height { get; }

        /// <summary>
        /// Returns <see cref="X"/> and <see cref="Y"/> as <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Position => new Vector2(X, Y);

        /// <summary>
        /// Returns <see cref="Width"/> and <see cref="Height"/> as <see cref="Vector2"/>.
        /// </summary>
        public Vector2 Size => new Vector2(Width, Height);

        /// <summary>
        /// Combines <see cref="X"/>, <see cref="Y"/> and <see cref="Width"/>, <see cref="Height"/> into a <see cref="Rectangle"/>.
        /// </summary>
        public Rectangle Rectangle => new Rectangle((int)X, (int)Y, (int)Width, (int)Height);

        /// <summary>
        /// Initializes a new <see cref="Dimensions"/> object.
        /// </summary>
        /// <param name="X">Position in the X Axis.</param>
        /// <param name="Y">Position in the Y Axis.</param>
        /// <param name="Width">Size in the X Axis.</param>
        /// <param name="Height">Size in the Y Axis.</param>
        public Dimensions(float X, float Y, float Width, float Height) => (this.X, this.Y, this.Width, this.Height) = (X, Y, Width, Height);
    }
}

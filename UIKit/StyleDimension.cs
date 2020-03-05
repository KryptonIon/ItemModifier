namespace ItemModifier.UIKit
{
    /// <summary>
    /// Represents a pixels.
    /// </summary>
    public readonly struct StyleDimension
    {
        /// <summary>
        /// <see cref="StyleDimension"/> that fills an entire container.
        /// </summary>
        public static readonly StyleDimension Fill = new StyleDimension(0, 1);

        /// <summary>
        /// Blank <see cref="StyleDimension"/>.
        /// </summary>
        public static readonly StyleDimension Empty = new StyleDimension(0, 0);

        /// <summary>
        /// Unchanging offset.
        /// </summary>
        public float Pixels { get; }

        /// <summary>
        /// Represents a percent of the container's size.
        /// </summary>
        public float Percent { get; }

        public StyleDimension(float Pixels) => (this.Pixels, Percent) = (Pixels, 0);

        public StyleDimension(float Pixels, float Percent) => (this.Pixels, this.Percent) = (Pixels, Percent);

        /// <summary>
        /// Calculates the value by combining Pixels and Percent(with respect to the specified Container's Size).
        /// </summary>
        /// <param name="ContainerSize">Size of the parent <see cref="UIElement"/>.</param>
        /// <returns>Evaluated value of Pixels and Percent.</returns>
        public float CalculateValue(float ContainerSize)
        {
            return Pixels + ContainerSize * Percent;
        }
    }
}

using Microsoft.Xna.Framework;

namespace ItemModifier.UIKit
{
    public class SnapPoint
    {
        public string Name { get; private set; }

        public int ID { get; private set; }

        private Vector2 anchor;

        private Vector2 offset;

        public UIElement BoundElement { get; set; }

        public Vector2 Position { get; private set; }

        public SnapPoint(string name, int id, Vector2 anchor, Vector2 offset) => (Name, ID, this.anchor, this.offset) = (name, id, anchor, offset);

        public void Calculate(UIElement element)
        {
            BoundElement = element;
            Dimensions dimensions = element.Dimensions;
            Position = dimensions.Position + offset + anchor * dimensions.Size;
        }

        public override string ToString() => $"SnapPoint - ID: {ID}, Name: {Name}";
    }
}

using Microsoft.Xna.Framework;

namespace ItemModifier.Utilities
{
    public struct Identifier
    {
        public string name, value;
        public Color color;
        public Identifier(string Name, string Value, Color Color)
        {
            name = Name;
            value = Value;
            color = Color;
        }
    }

    public struct SettingInfo
    {
        public string name;
        public object value;
        public SettingInfo(string Name, object Value)
        {
            name = Name;
            value = Value;
        }
    }
}
namespace ItemModifier.Utilities
{
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
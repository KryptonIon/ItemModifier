namespace ItemModifier.Utilities
{
    public class SettingInfo
    {
        public string name { get; set; }
        public object value { get; set; }

        public SettingInfo(string Name, object Value)
        {
            name = Name;
            value = Value;
        }

        public SettingInfo()
        {

        }
    }
}
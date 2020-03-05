public static class FloatingPointExtensions
{
    public static string ToStringLong(this float num)
    {
        string str = num.ToString();

        if (!str.Contains("E")) return str;

        int indexOfE = str.IndexOf("E");
        char ope = str[indexOfE + 1];
        int numOfPlaces = int.Parse(str.Substring(indexOfE + 2, str.Length - indexOfE - 2));
        str = str.Remove(indexOfE);

        if (ope == '+')
        {
            if (str.Contains("."))
            {
                int indexOfP = str.IndexOf(".");
                int newPPos = indexOfP + numOfPlaces;
                str = str.Replace(".", "");
                return str + new string('0', str.Length - newPPos);

            }
            else
            {
                return str + new string('0', numOfPlaces);
            }
        }
        else
        {
            if (str.Contains("."))
            {
                return "0." + new string('0', numOfPlaces - 1) + str.Replace(".", "");
            }
            else
            {
                return "0." + new string('0', numOfPlaces - 1) + str;
            }
        }
    }

    public static string ToStringLong(this double num)
    {
        string str = num.ToString();

        if (!str.Contains("E")) return str;

        int indexOfE = str.IndexOf("E");
        char ope = str[indexOfE + 1];
        int numOfPlaces = int.Parse(str.Substring(indexOfE + 2, str.Length - indexOfE - 2));
        str = str.Remove(indexOfE);

        if (ope == '+')
        {
            if (str.Contains("."))
            {
                int indexOfP = str.IndexOf(".");
                int newPPos = indexOfP + numOfPlaces;
                str = str.Replace(".", "");
                return str + new string('0', str.Length - newPPos);

            }
            else
            {
                return str + new string('0', numOfPlaces);
            }
        }
        else
        {
            if (str.Contains("."))
            {
                return "0." + new string('0', numOfPlaces - 1) + str.Replace(".", "");
            }
            else
            {
                return "0." + new string('0', numOfPlaces - 1) + str;
            }
        }
    }
}

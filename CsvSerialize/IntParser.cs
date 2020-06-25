using System;

namespace Csv.Serialize
{
    public class IntParser : IValueParser
    {
        public Type typeOfValue => typeof(int);

        public object Parse(string value)
        { 
            if (value != null && value != "")
            {
                int result = 0;
                if (int.TryParse(value, out result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}

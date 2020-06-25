using System;

namespace Csv.Serialize
{
    public class NDateTimeParser : IValueParser
    {
        public Type typeOfValue => typeof(DateTime?);
        public object Parse(string value)
        {
            if (value != null && value != "")
            {
                DateTime result;
                if (DateTime.TryParse(value, out result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}

using System;

namespace Csv.Serialize
{
    public class StrParser : IValueParser
    {
        public Type typeOfValue => typeof(string);
        public object Parse(string value)
        {
            return value;
        }
    }
}

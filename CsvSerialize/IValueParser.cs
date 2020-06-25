using System;

namespace Csv.Serialize
{
    public interface IValueParser
    {
        Type typeOfValue { get; }
        object Parse(string value);
    }
}

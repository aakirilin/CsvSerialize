using System;

namespace Csv.Serialize
{
    public class CsvFieldAttribute : Attribute
    {
        public int index { get; private set; }
        public string datePattern { get; private set; }
        public CsvFieldAttribute(int index, string datePattern = null)
        {
            this.index = index;
            this.datePattern = datePattern;
        }
    }
}

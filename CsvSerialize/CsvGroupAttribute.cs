using System;

namespace Csv.Serialize
{
    public class CsvGroupAttribute : Attribute
    {
        public int offset { get; private set; }
        public CsvGroupAttribute(int offset)
        {
            this.offset = offset;
        }
    }
}

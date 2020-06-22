using System;
using System.Reflection;

namespace Csv.Serialize
{
    public class CsvFieldExtruder
    {
        public int index { get; private set; }
        private PropertyInfo[] propertes { get;  set; }
        private string datePattern { get; set; }

        public CsvFieldExtruder(int index, string datePattern, params PropertyInfo[] propertes)
        {
            this.propertes = propertes;
            this.datePattern = datePattern;
            this.index = index;
        }

        public string ExtrudedValue(object obj)
        {
            object result = obj;
            foreach (var property in propertes)
            {
                result = property.GetValue(result);
            }
            if (datePattern != null && datePattern != "")
                return ((DateTime?)result)?.ToString(datePattern);
            else 
                return result?.ToString();
        }
    }
}

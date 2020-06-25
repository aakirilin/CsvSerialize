using System;
using System.Linq;
using System.Reflection;

namespace Csv.Serialize
{

    public class CsvFieldHelper
    {
        public int index { get; private set; }
        private PropertyInfo[] propertes { get;  set; }
        private string datePattern { get; set; }

        public CsvFieldHelper(int index, string datePattern, params PropertyInfo[] propertes)
        {
            this.propertes = propertes;
            this.datePattern = datePattern;
            this.index = index;
        }

        public string GetObjectValue(object obj)
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

        public void SetObjectValue(string[] partsOfLine, ref object obj, params IValueParser[] parsers)
        {
            object o = obj;
            //var currentProperty = propertes.First();
            // propertes.Take(propertes.Length -1) потому что последний это 
            // кнкретное свойство а остальные это группы свойств
            foreach (var property in propertes.Take(propertes.Length - 1))
            {
                var prop = property.GetValue(o);
                if (prop == null)
                {
                    prop = Activator.CreateInstance(property.PropertyType);
                    property.SetValue(o, prop);
                }
                o = prop;
            }
            var lastProp = propertes.Last();
            var parser = Parsers.First(p => p.typeOfValue == lastProp.PropertyType);
            if (parser == null)
            {
                parser = parsers.First(p => p.typeOfValue == lastProp.PropertyType);
            }
            lastProp.SetValue(o, parser.Parse(partsOfLine[index]));
        }

        public static IValueParser[] Parsers =
        {
            new IntParser(),
            new DateTimeParser(),
            new NDateTimeParser(),
            new StrParser(),
        };
    }
}

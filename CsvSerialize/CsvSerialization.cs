using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Csv.Serialize
{
    public class CsvSerialization<T> where T: class
    {

        private IEnumerable<CsvFieldHelper> GetWriters(
            int offset, 
            Type typeObj, 
            params PropertyInfo[] ownerProps)
        {
            var props = typeObj.GetProperties();
            foreach (var prop in props)
            {
                var csvFieldAttribute = prop.GetCustomAttribute<CsvFieldAttribute>();
                if (csvFieldAttribute != null)
                {
                    yield return new CsvFieldHelper(
                        csvFieldAttribute.index + offset,
                        csvFieldAttribute.datePattern,
                        ownerProps.Append(prop).ToArray() );
                }
                var csvGroupAttribute = prop.GetCustomAttribute<CsvGroupAttribute>();
                if (csvGroupAttribute != null)
                {
                    foreach (var writer in GetWriters(
                        csvGroupAttribute.offset + offset, 
                        prop.PropertyType,
                        ownerProps.Append(prop).ToArray()))
                    {
                        yield return writer;
                    }
                }
            }
        }

        private string ConvertToString(T obj, int sizeLine, CsvFieldHelper[] writers)
        {
            var line = new string[sizeLine];
            foreach (var w in writers)
            {
                line[w.index] = w.GetObjectValue(obj);
            }
            return String.Join(";", line);
        }

        public void Serialize(string fileName, params T[] objs)
        {
            Type typeObj = typeof(T);
            var writers = GetWriters(0, typeObj).ToArray();
            var sizeLine = writers.Max(w => w.index) + 1;
            using (FileStream file = File.Create(fileName))
            {
                using (StreamWriter writer = new StreamWriter(file, Encoding.Default))
                {
                    foreach (var obj in objs)
                    {
                        writer.WriteLine(ConvertToString(obj, sizeLine, writers));
                    }
                }
            }
        }

        public IEnumerable<T> Deserializr(string fileName, params IValueParser[] parsers)
        {
            var lines = File.ReadAllLines(fileName);
            Type typeObj = typeof(T);
            var writers = GetWriters(0, typeObj).ToArray();
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                var obj = Activator.CreateInstance(typeObj);
                foreach (var writer in writers)
                {
                    writer.SetObjectValue(parts, ref obj, parsers);
                }
                yield return (T)obj;
            }
        }
    }
}

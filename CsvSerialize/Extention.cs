using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace Csv.Serialize
{
    public static class Extention
    {
        public static IEnumerable<PropertyInfo> Append(
            this IEnumerable<PropertyInfo> array, 
            PropertyInfo element)
        {
            if (array != null)
            {
                foreach (var item in array)
                {
                    yield return item;
                }
            }
            yield return element;
        }
    }
}

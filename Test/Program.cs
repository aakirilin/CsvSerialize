using Csv.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new TestClass[2];
            var aaa = new TestGroup() { Name = "awd", Count = 2, Date = DateTime.Now };
            test[0] = new TestClass() { Name = "awawd", Count = 1, Date = DateTime.Now, Group = aaa };
            test[1] = test[0];
            var serialise = new CsvSerialization<TestClass>();
            serialise.Serialize("test.csv", test);
        }
    }

    public class TestClass
    {
        [CsvField(0)]
        public string Name { get; set; }
        [CsvField(1)]
        public int Count { get; set; }
        [CsvGroup(3)]
        public TestGroup Group { get; set; }
        [CsvField(8, datePattern: "yyyy-MM-dd")]
        public DateTime Date { get; set; }
    }

    public class TestGroup
    {
        [CsvField(0)]
        public string Name { get; set; }
        [CsvField(1)]
        public int Count { get; set; }
        [CsvField(3)]
        public DateTime Date { get; set; }
    }
}

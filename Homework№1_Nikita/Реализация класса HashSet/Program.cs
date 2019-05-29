using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Реализация_класса_HashSet
{
    class Program
    {
        static void Main()
        {
            int[] array1 = { 1, 2, 3 };
            int[] array2 = { 3, 4, 5 };
            int[] array3 = { 9, 10, 11 };
            Console.WriteLine("array1: " + string.Join(", ", array1));
            Console.WriteLine("array2: " + string.Join(", ", array2));
            Console.WriteLine("array3: " + string.Join(", ", array3));

            Console.WriteLine();

            HashSet<int> set = new HashSet<int>(array1);

            bool a = set.Overlaps(array2);
            bool b = set.Overlaps(array3);

            Console.WriteLine($"Наличие общих элементов в множестве array1 & array2: {a}");
            Console.WriteLine($"Наличие общих элементов в множестве array1 & array3: {b}");

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine();

            string[] array4 =
            {
            "cat",
            "dog",
            "cat",
            "leopard",
            "tiger",
            "cat"
            };

            Console.WriteLine("array4: " + string.Join(", ", array4));

            var hash = new HashSet<string>(array4);

            string[] array5 = hash.ToArray();

            Console.WriteLine("array4 после HashSet: " + string.Join(", ", array5));
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine();

            HashSet<int> theSet1 = new HashSet<int>();
            theSet1.Add(1);
            theSet1.Add(2);
            theSet1.Add(2);
            Console.WriteLine("theSet1: " + string.Join(", ", theSet1));

            HashSet<int> theSet2 = new HashSet<int>();
            theSet2.Add(1);
            theSet2.Add(3);
            theSet2.Add(4);
            Console.WriteLine("theSet2: " + string.Join(", ", theSet2));

            theSet1.UnionWith(theSet2);
            Console.WriteLine("Объединение множеств theSet1 & theSet2: " + string.Join(", ", theSet1));
            Console.WriteLine("--------------------------------------------------------------");

            Console.ReadKey();
        }
    }
}

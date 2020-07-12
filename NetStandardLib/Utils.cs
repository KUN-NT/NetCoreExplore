using System;
using System.Collections.Generic;

namespace NetStandardLib
{
    public class Utils
    {
        public static void PrintAssemblyNames()
        {
            List<string> list = new List<string>() { "Tom", "Cat", "Jack", "Rose" };
            list.ForEach(p => Console.WriteLine(p));
            Console.WriteLine(typeof(Dictionary<,>).Assembly.FullName);
            Console.WriteLine(typeof(SortedDictionary<,>).Assembly.FullName);
        }
    }
}

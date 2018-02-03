using Lib1;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new Class1();
            c1.PrintNames();
            c1.PrintAssemblyNames();

            var c2 = new Class2();
            c2.PrintNames();
            c2.PrintAssemblyNames();

            Console.Read();
        }
    }
}

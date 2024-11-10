using System;

namespace m1
{
    internal class Program
    {
        static void Task1()
        {
            Console.WriteLine("Hello World!!!");
            Console.ReadKey(true);
        }

        static void Task2()
        {
            Console.Write("Hello ");
            Console.Write("World");
            Console.Write("!!!");
            Console.ReadKey(true);
        }

        static void Main(string[] args)
        {
            Task1();
            Task2();
        }
    }
}
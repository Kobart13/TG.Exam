using System;
using System.Diagnostics;
using System.Linq;

namespace TG.Exam.Algorithms
{
    class Program
    {
        /// <summary>
        /// Method implements recursion algorithm of generating Fibonacci-like subsequence 
        /// </summary>
        /// <remarks>
        /// Advantages:
        /// - Recursion adds clarity and reduces the time needed to write and debug code
        /// Disadvantages:
        /// - Recursion uses more memory
        /// - Recursion can be slow
        /// </remarks>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        static int Foo(int a, int b, int c)
        {
            if (1 < c)
                return Foo(b, b + a, c - 1);
            else
                return a;
        }

        /// <summary>
        /// Iterative version of Foo function
        /// A function repeats a defined process until a condition fails
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        static int FooIterative(int first, int second, int number)
        {
            int sum = 0;
            while (number > 1)
            {
                sum = first + second;
                first = second;
                second = sum;
                number -= 1;
            }

            return first;
        }

        /// <summary>
        /// Method implements classic bubble sort algorithm
        /// </summary>
        /// <remarks>
        /// Advantages:
        /// - Simple to write
        /// - Easy to understand
        /// - It only takes a few lines of code
        /// - The data is sorted in place
        /// Disadvantages:
        /// - It is very slow and runs in O(n^2) time in worst as well as average case
        /// - There are many sorting algorithms that run in linear time i.e O(n) and some algorithms in O(n log n)
        /// </remarks>
        /// <param name="arr"></param>
        /// <returns></returns>
        static int[] Bar(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                for (int j = 0; j < arr.Length - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        int t = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = t;
                    }
            return arr;
        }

        /// <summary>
        /// This method uses Array.Sort, which uses the QuickSort algorithm.
        /// On average, this method is an O(n log n) operation,
        /// where n is Count; in the worst case it is an O(n^2) operation.
        /// Quicksort is often faster in practice than other O(n log n) algorithms
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        static int[] BarQuickSort(int[] arr)
        {
            int[] sortedArray = new int[arr.Length];

            arr.CopyTo(sortedArray, 0);
            Array.Sort(sortedArray);

            return sortedArray;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Foo result: {0}", Foo(7, 2, 8));
            Console.WriteLine("FooIterative result: {0}", FooIterative(7, 2, 8));

            Console.WriteLine("Bar result: {0}", string.Join(", ", Bar(new[] { 7, 2, 8 })));
            Console.WriteLine("BarQuickSort result: {0}", string.Join(", ", BarQuickSort(new[] { 7, 2, 8 })));

            TestPerformanceFoo();
            TestPerformanceBar();

            Console.ReadKey();

        }

        static void TestPerformanceFoo()
        {
            Console.WriteLine("----------------------------------------------");
            Stopwatch watch;

            int number = 100000;

            watch = Stopwatch.StartNew();
            Foo(7, 2, number);
            watch.Stop();
            Console.WriteLine($"Foo time: {watch.ElapsedTicks}");

            watch = Stopwatch.StartNew();
            FooIterative(7, 2, number);
            watch.Stop();
            Console.WriteLine($"FooIterative time: {watch.ElapsedTicks}");

            Console.WriteLine("----------------------------------------------");
        }

        static void TestPerformanceBar()
        {
            Console.WriteLine("----------------------------------------------");
            Stopwatch watch;
            Random randNum = new Random();

            int[] array = Enumerable.Repeat(0, 1000)
                                    .Select(i => randNum.Next(0, 10000))
                                    .ToArray();

            watch = Stopwatch.StartNew();
            var resultBar = Bar(array);
            watch.Stop();
            Console.WriteLine($"Bar time: {watch.ElapsedTicks}");

            watch = Stopwatch.StartNew();
            BarQuickSort(array);
            watch.Stop();
            Console.WriteLine($"BarQuickSort time: {watch.ElapsedTicks}");

            Console.WriteLine("----------------------------------------------");
        }
    }
}

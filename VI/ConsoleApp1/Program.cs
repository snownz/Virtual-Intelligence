using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;

namespace ConsoleApp1
{
    class Program
    {
        [DllImport("VI.Core.CPU", CallingConvention = CallingConvention.Cdecl)]
        public static extern int print(String txt);
        
        static void Main(string[] args)
        {
            Console.WriteLine(Environment.CurrentDirectory);
            print("Test");
            Console.ReadKey();

            var time = Stopwatch.StartNew();
            ProcessingDevice.Device = DeviceType.CPU;
            time.Stop();
            Console.WriteLine($"Setup Time: {time.ElapsedMilliseconds} ms");

            int sizeVector = 10000;

            time = Stopwatch.StartNew();
            var a = NumMath.Array(100, 100);
            var b = NumMath.Array(100, 100);
            var c = NumMath.Array(sizeVector);
            time.Stop();
            Console.WriteLine($"Alocate Time: {time.ElapsedMilliseconds} ms");

            time = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                var d = c * 20 * 30 * 40;
            }
            time.Stop();
            Console.WriteLine($"Execution Array Time: {time.ElapsedMilliseconds} ms");

            time = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < sizeVector; j++)
                {
                   c[j]  = c[j] * 20 * 30 * 40;
                }
            }
            time.Stop();
            Console.WriteLine($"Execution Array Full Time: {time.ElapsedMilliseconds} ms");

            time = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                var d = c.Sqrt() + c.Pow(2);
            }
            time.Stop();
            Console.WriteLine($"Execution Array SQRT Time: {time.ElapsedMilliseconds} ms");

            time = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < sizeVector; j++)
                {
                    c[j] = (float)(Math.Sqrt(c[j]) + Math.Pow(c[j], 2)); 
                }
            }
            time.Stop();
            Console.WriteLine($"Execution Array SQRT Full Time: {time.ElapsedMilliseconds} ms");

            //time = Stopwatch.StartNew();
            //for (int i = 0; i < 1000; i++)
            //{
            //    var d = (a + 10 * b * 18 / 4 + 15);
            //}
            //time.Stop();
            //Console.WriteLine($"Execution Matrix Time: {time.ElapsedMilliseconds} ms");
            //
            //time = Stopwatch.StartNew();
            //for (int i = 0; i < 1000; i++)
            //{
            //    var d = (a + 10 * b * 18 / 4 + 15).SumLine() * c * 20;
            //}
            //time.Stop();
            //Console.WriteLine($"Execution Heavy Time: {time.ElapsedMilliseconds} ms");
        }
    }
}

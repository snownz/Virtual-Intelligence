using System;
using System.Diagnostics;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;

namespace ConsoleApp1
{
    class Program
    {        
        static void Main(string[] args)
        {
            var time = Stopwatch.StartNew();

            //ProcessingDevice.Device = DeviceType.CSharp_CPU;
            ProcessingDevice.Device = DeviceType.C_CPU;

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
                var d = c * 20;
            }
            time.Stop();
            Console.WriteLine($"Execution Array Time: {time.ElapsedMilliseconds} ms");
            
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

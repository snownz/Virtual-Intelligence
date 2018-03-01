using System;
using System.Diagnostics;
using VI.NumSharp;
using VI.ParallelComputing;

namespace ConsoleApp1
{
    class Program
    {        
        static void executeTests(DeviceType d)
        {
            ProcessingDevice.Device = d;

            int sizeVector = 100000000;
            int loops = 1;
            var c = NumMath.Repeat(sizeVector, 4);

            var time = Stopwatch.StartNew();
            for (int i = 0; i < loops; i++)
            {
                var hd = c * 20;
            }
            time.Stop();
            Console.WriteLine($"{Enum.GetName(typeof(DeviceType), d)} - Execution Array Time: {time.ElapsedMilliseconds} ms"); 
            System.Console.WriteLine("\n------------------------------------------------------------------------\n");
        }
        static void Main(string[] args)
        {
            System.Console.WriteLine("\n------------------------------------------------------------------------\n");
            executeTests(DeviceType.CSharp_CPU);
            executeTests(DeviceType.C_CPU);
        }
    }
}

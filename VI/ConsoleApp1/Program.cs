using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
            Console.Clear();
            System.Console.WriteLine("\n------------------------------------------------------------------------\n");
            int size = 100000;
            var range = Enumerable.Range(0, size);
            
            var time = Stopwatch.StartNew();      
            VectorMulti(range, size); 
            time.Stop();
            Console.WriteLine($"Execution Array Multi Time: {time.ElapsedMilliseconds} ms");  

            time = Stopwatch.StartNew();      
            VecotrLog(range, size); 
            time.Stop();
            Console.WriteLine($"Execution Array Log Time: {time.ElapsedMilliseconds} ms");  

            time = Stopwatch.StartNew();      
            VecotrSqrt(range, size); 
            time.Stop();
            Console.WriteLine($"Execution Array Sqrt Time: {time.ElapsedMilliseconds} ms");  

            time = Stopwatch.StartNew();      
            VecotrPow(range, size); 
            time.Stop();
            Console.WriteLine($"Execution Array Pow Time: {time.ElapsedMilliseconds} ms");  

            time = Stopwatch.StartNew();      
            VecotrExp(range, size); 
            time.Stop();
            Console.WriteLine($"Execution Array Exp Time: {time.ElapsedMilliseconds} ms");  

            System.Console.WriteLine("Finish");
        }

        private static void VectorMulti(int size)
        {
            var a = new float[size];
            for(int i = 0; i < size; i++)
            {
                a[i] = a[i] * a[i];
            }
        }
        private static void VecotrLog(int size)
        {
            var a = new float[size];
            for(int i = 0; i < size; i++)
            {
                a[i] = (float)Math.Log(a[i]);
            }
        }
        private static void VecotrSqrt(int size)
        {
            var a = new float[size];
            for(int i = 0; i < size; i++)
            {
                a[i] = (float)Math.Sqrt(100);
            }
        }
        private static void VecotrPow(int size)
        {
            var a = new float[size];
            for(int i = 0; i < size; i++)
            {
                a[i] = (float)Math.Pow(a[i], 100);
            }
        }
        private static void VecotrExp(int size)
        {
            var a = new float[size];
            for(int i = 0; i < size; i++)
            {
                a[i] = (float)Math.Exp(a[i]);
            }
        }

        private static void VectorMulti(IEnumerable<int> size, int s)
        {
            var a = new float[s];
            Parallel.ForEach((size), x => a[x] = a[x] * a[x]);
        }
        private static void VecotrLog(IEnumerable<int> size, int s)
        {
            var a = new float[s];
            Parallel.ForEach((size), i =>a[i] = (float)Math.Log(a[i]));
        }
        private static void VecotrSqrt(IEnumerable<int> size, int s)
        {
            var a = new float[s];
            Parallel.ForEach((size), i =>  a[i] = (float)Math.Sqrt(100));
        }
        private static void VecotrPow(IEnumerable<int> size, int s)
        {
            var a = new float[s];
            Parallel.ForEach((size), i => a[i] = (float)Math.Pow(a[i], 100));
        }
        private static void VecotrExp(IEnumerable<int> size, int s)
        {
            var a = new float[s];
            Parallel.ForEach((size), i =>   a[i] = (float)Math.Exp(a[i]));
        }





    }
}

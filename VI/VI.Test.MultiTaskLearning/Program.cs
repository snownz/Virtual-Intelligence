using System;
using System.Threading.Tasks;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;

namespace VI.Test.MultiTaskLearning
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // isso vai instanciar uma classe estatica que sera chamada praca dada operação tensorial
            ProcessingDevice.Device = DeviceType.CPU;

            int operacoes = 5;

            var a = new Array<FloatArray2D>(operacoes);

            for (int i  = 0; i <  operacoes; i++)
            {
                a[i] = new FloatArray2D(100, 100);
            }

            // isso vai executar em paralelo mesmo ou vai ser sequenciaal? Olhar a classe FloatArray2D, nela tem overload dos operadores
            Parallel.For(0, operacoes, i => {  var b = a[i] * a[i]; });


            Console.WriteLine("Hello World!");
        }
    }
}
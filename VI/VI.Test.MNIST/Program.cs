using System;
using System.Diagnostics;
using VI.Data.Array;
using VI.Data.MNIST;
using VI.Neural.Factory;
using VI.Neural.LossFunction;
using VI.Neural.Models;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;
using RoslynTools.Console;
using VI.Neural.Drivers.Executors;

namespace MNIST
{
    public class Program : Cw
    {
#if DEBUG
        private static string path = "../VI.Test.MNIST/Data";
#else
        private static string path = "VI.Test.MNIST/Data";
#endif
        private static void Main(string[] args)
        {
            Console.Clear();

            var watch = Stopwatch.StartNew();

            ProcessingDevice.Device = DeviceType.CPU_Parallel;

            watch.Stop();
            Console.WriteLine($"Device Time: {watch.ElapsedMilliseconds}ms");

            var model = new DenseModel();
            model.AddLayer( BuildedModels.DenseLeakRelu ( 784, 100, 1e-4f, 2e-1f, EnumOptimizerFunction.Adadelta ) );
            model.AddLayer( BuildedModels.DenseLeakRelu ( 100, 30,  1e-4f, 2e-1f, EnumOptimizerFunction.Adadelta ) );
            model.AddLayer( BuildedModels.DenseSoftMax  ( 30,  10,  1e-4f, 2e-1f, EnumOptimizerFunction.Adadelta ) );
            model.SetLossFunction( new CrossEntropyLossFunction() );

            watch = Stopwatch.StartNew();
            watch.Stop();
            Console.WriteLine($"Sinapse Time: {watch.ElapsedMilliseconds}ms");

            MnistLoader.DataPath = path;
            var trainingValues = MnistLoader.OpenMnist();

            int cont = 0;
            int sizeTrain = trainingValues.Count;

            var e = 0f;
            while ( true )
            {
                watch = Stopwatch.StartNew();
                e = 0f;
                int ct = 0;
                for ( int i = 0; i < sizeTrain; i++ )
                {
                    var index = i;

                    var inputs = new FloatArray( ArrayMethods.ByteToArray( trainingValues[index].pixels, 28, 28 ) );
                    var target = new FloatArray( ArrayMethods.ByteToArray( trainingValues[index].label,  10     ) );

                    //  Learning
                    e += model.Learn( inputs, target );

                    // Sample
                    if ( ct % 1000 == 0 )
                    {
                        Write( trainingValues[index].ToString() );
                        Write( ArrayMethods.PrintArray( model.Output(inputs), 10 ) );
                        Write( "\n" );
                    }       

                    ct++;                    
                }
                cont++;
                watch.Stop();
                var time = watch.ElapsedMilliseconds;               
                Console.Title =
                    $"Error: {e} --- TSPS (Training Sample per Second): {Math.Ceiling(1000d / ((double)time / (double)sizeTrain))}";
            }
        }
    }
}
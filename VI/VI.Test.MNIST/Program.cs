using System;
using System.Drawing;
using System.Diagnostics;
using VI.Data.Array;
using VI.Data.MNIST;
using VI.Neural.Factory;
using VI.Neural.LossFunction;
using VI.Neural.Models;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;
using Roslyn.ConsoleTools;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using SkiaSharp;

namespace MNIST
{
    internal class Program : Cw
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
            model.AddLayer( BuildedModels.DenseLeakRelu ( 784, 100, 1e-1f, 2e-1f, EnumOptimizerFunction.Adagrad ) );
            model.AddLayer( BuildedModels.DenseLeakRelu ( 100, 30,  1e-1f, 2e-1f, EnumOptimizerFunction.Adagrad ) );
            model.AddLayer( BuildedModels.DenseSoftMax  ( 30,  10,  1e-1f, 2e-1f, EnumOptimizerFunction.Adagrad ) );
            model.SetLossFunction(new CrossEntropyLossFunction());

            watch = Stopwatch.StartNew();
            watch.Stop();
            Console.WriteLine($"Sinapse Time: {watch.ElapsedMilliseconds}ms");

            MnistLoader.DataPath = path;
            var trainingValues = MnistLoader.OpenMnist();

            int cont = 0;
            int sizeTrain = trainingValues.Count;

            var err = 100f;
            var e = 0f;
            while ( true )
            {
                watch = Stopwatch.StartNew();
                e = 0f;
                int ct = 0;
                for ( int i = 0; i < sizeTrain; i++ )
                {
                    var index = i;

                    var inputs   = new FloatArray( ArrayMethods.ByteToArray( trainingValues[index].pixels, 28, 28 ) );
                    var desireds = new FloatArray( ArrayMethods.ByteToArray( trainingValues[index].label,  10     ) );

                    // Backward, Gradient N Update
                    ( var lerror, _ ) =  model.ComputeErrorNBackward( inputs, desireds );
                    ( var dw, var db ) = model.ComputeGradient( inputs );
                    model.UpdateParams( dw, db );

                    e += lerror;

                    // Sample
                    if ( ct % 1000 == 0 )
                    {
                        Console.WriteLine( trainingValues[index].ToString() );
                        Console.WriteLine( ArrayMethods.PrintArray( model.Output(inputs), 10 ) );
                        Console.WriteLine( "\n" );
                    }       

                    ct++;                    
                }

                err = 0.999f * err + 0.001f * e;

                cont++;
                watch.Stop();
                var time = watch.ElapsedMilliseconds;               
                Console.Title =
                    $"Error: {err} --- TSPS (Training Sample per Second): {Math.Ceiling(1000d / ((double)time / (double)sizeTrain))}";
            }
        }
    }
}
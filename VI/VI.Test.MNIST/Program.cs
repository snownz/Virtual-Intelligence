using System;
using VI.Data.Array;
using VI.Data.MNIST;
using VI.Neural.Factory;
using VI.Neural.LossFunction;
using VI.Neural.Models;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;

namespace MNIST
{
    class Program
    {
#if DEBUG
        private static string path = "../VI.Test.MNIST/Data";
#else
        private static string path = "VI.Test.MNIST/Data";
#endif
        static void Main(string[] args)
        {

            Console.Clear();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            ProcessingDevice.Device = DeviceType.CPU;

            var loss = new CrossEntropyLossFunction();

            watch.Stop();
            Console.WriteLine($"Device Time: {watch.ElapsedMilliseconds}ms");

            var hiddens = BuildedModels.DenseLeakRelu(784, 256, 1e-3f, 1e-1f, OptimizerFunctionEnum.Adagrad);
            var hiddens2 = BuildedModels.DenseLeakRelu(128, 256, 1e-3f, 1e-1f, OptimizerFunctionEnum.Adagrad); 
            var outputs = BuildedModels.DenseSoftMax(10, 128, 1e-3f, 1e-1f, OptimizerFunctionEnum.Adagrad); 

            var model = new DenseModel();
            model.AddLayer(hiddens);
            model.AddLayer(hiddens2);
            model.AddLayer(outputs);

            watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            Console.WriteLine($"Sinapse Time: {watch.ElapsedMilliseconds}ms");

            MnistLoader.DataPath = path;
            var trainingValues = MnistLoader.OpenMnist();

            int cont = 0;
            int sizeTrain = trainingValues.Count;

            var e = double.MaxValue;

            while (true)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();
                e = 0;
                int ct = 0;
                for (int i = 0; i < sizeTrain; i++)
                {
                    var index = i;

                    var inputs = new FloatArray(ArrayMethods.ByteToArray(trainingValues[index].pixels, 28, 28));
                    var desireds = new FloatArray(ArrayMethods.ByteToArray(trainingValues[index].label, 10));

                    // Feed Forward
                    var _out = model.Output(inputs);

                    // Backward
                    model.ComputeErrorNBackward(desireds);
                    (var dw, var db) =  model.ComputeGradient(inputs);
                    model.UpdateParams(dw, db);

                    if (ct % 1000 == 0)
                    {
                        Console.WriteLine(trainingValues[index].ToString());
                        Console.WriteLine(ArrayMethods.PrintArray(_out, 10));
                        Console.WriteLine("\n");
                    }
                    ct++;

                    // Error
                    e += loss.Loss(desireds, _out);
                }

                e /= sizeTrain;
                cont++;
                watch.Stop();
                var time = watch.ElapsedMilliseconds;
                Console.WriteLine($"Interactions: {cont}\nError: {e}");
                Console.Title =
                    $"Error: {e} --- TSPS (Training Sample per Second): {Math.Ceiling(1000d / ((double)time / (double)sizeTrain))}";
            }
        }
    }
}

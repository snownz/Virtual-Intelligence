using System;
using VI.Data.Array;
using VI.Data.MNIST;
using VI.Neural.Factory;
using VI.Neural.LossFunction;
using VI.Neural.Node;
using VI.NumSharp;
using VI.ParallelComputing;

namespace MNIST
{
    class Program
    {
        private static Random rd = new Random();
        static void Main(string[] args)
        {
            var rd = new Random();
            var values = new[] { .01f, .000f };

            var watch = System.Diagnostics.Stopwatch.StartNew();

            ProcessingDevice.Device = DeviceType.C_CPU;

            var loss = new CrossEntropyLossFunction();

            watch.Stop();
            Console.WriteLine($"Device Time: {watch.ElapsedMilliseconds}ms");

            var hiddens = new LayerCreator(100, 784)
                .WithLearningRate(values[0])
                .FullSynapse(.01f)
                .Supervised()
                .DenseLayer()
                .WithLeakRelu()
                .WithSgd()
                .Hidden()
                .Build();

            var hiddens2 = new LayerCreator(30, 100)
                .WithLearningRate(values[0])
                .FullSynapse(.01f)
                .Supervised()
                .DenseLayer()
                .WithLeakRelu()
                .WithSgd()
                .Hidden()
                .Build();

            var outputs = new LayerCreator(10, 30)
                .WithLearningRate(values[0])
                .FullSynapse(.01f)
                .Supervised()
                .SoftmaxLayer()
                .WithSgd()
                .Output()
                .Build();

            watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            Console.WriteLine($"Sinapse Time: {watch.ElapsedMilliseconds}ms");

            MnistLoader.DataPath = "MNIST/Data";
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

                    var inputs = ArrayMethods.ByteToArray(trainingValues[index].pixels, 28, 28);
                    var desireds = ArrayMethods.ByteToArray(trainingValues[index].label, 10);

                    // Feed Forward
                    var _h = hiddens.Output(inputs);
                    var _h2 = hiddens2.Output(_h);
                    var _o = outputs.Output(_h2);

                    // Backward
                    var _oe = ((ISupervisedLearning)outputs).Learn(_h2, desireds);
                    var _he2 = ((ISupervisedLearning)hiddens2).Learn(_h, _oe);
                    ((ISupervisedLearning)hiddens).Learn(inputs, _he2);

                    if (ct % 1000 == 0)
                    {
                        Console.WriteLine(trainingValues[index].ToString());
                        Console.WriteLine(ArrayMethods.PrintArray(_o, 10));
                        Console.WriteLine("\n");
                    }
                    ct++;
                    // Error
                    e += loss.Loss(desireds, _o);
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

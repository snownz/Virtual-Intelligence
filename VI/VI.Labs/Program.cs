using System;
using System.Collections.Generic;
using System.Linq;
using VI.Data;
using VI.Neural.Node;
using VI.Labs.Models;
using VI.NumSharp;
using System.Threading;

namespace VI.Labs
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = new[] { .1f, .00f, 0f, 0f };

            string header = $"Learning Rate: {values[0]}\nMin Error:{values[1]}\nMomentum: {values[3]}\n";

            var watch = System.Diagnostics.Stopwatch.StartNew();

            ProcessingDevice.Device = Device.CPU;

            watch.Stop();
            Console.WriteLine($"Device Time: {watch.ElapsedMilliseconds}ms");

            var hiddens = new LayerBuilder(2, 4, values[0])
                .Supervised()
                .WithLeakRelu()
                .Hidden()
                .WithSGD()
                .WithMomentum(values[1])
                .FullSynapse(.3f)
                .Build();
            
            var hiddens2 = new LayerBuilder(2, 2, values[0])
                .Supervised()
                .WithLeakRelu()
                .Hidden()
                .WithSGD()
                .WithMomentum(values[1])
                .FullSynapse(.3f)
                .Build();
            
            var outputs = new LayerBuilder(2, 2, values[0])
                .Supervised()
                .WithSigmoid()
                .Output()
                .WithSGD()
                .WithMomentum(values[1])
                .FullSynapse(.3f)
                .Build();

            watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            Console.WriteLine($"Sinapse Time: {watch.ElapsedMilliseconds}ms");

            int cont = 0;

            var t = EvenOrOddData("even", "odd");

            var e = double.MaxValue;

            while (true)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();
                e = 0;
                foreach (var item in t)
                {
                    var inputs = item.Values.Select(x => x.Value).ToArray();

                    var desireds = item.DesiredValues.Select(x => x.Value).ToArray();

                    //watch = System.Diagnostics.Stopwatch.StartNew();
                    // Feed Forward
                    var _h = hiddens.Output(inputs);
                    var _h2 = hiddens2.Output(_h);
                    var _o = outputs.Output(_h2);
                    //watch.Stop();
                    //Console.WriteLine($"\nForward Time: { watch.ElapsedMilliseconds}ms");
                    //Thread.Sleep(100);

                    //watch = System.Diagnostics.Stopwatch.StartNew();
                    // Backward
                    var _oe = ((ISupervisedLearning)outputs).Learn(_h2, desireds);
                    var _he2 = ((ISupervisedLearning)hiddens2).Learn(_h, _oe);
                    ((ISupervisedLearning) hiddens).Learn(inputs, _he2);
                    //watch.Stop();
                    //Console.WriteLine($"\nBackward Time: { watch.ElapsedMilliseconds}ms");

                    // Error
                    var e0 = Math.Abs(_o[0] - desireds[0]);
                    var e1 = Math.Abs(_o[1] - desireds[1]);
                    var error = Math.Sqrt(Math.Abs(e0 * e0 + e1 * e0));
                    e += error / 2.0;
                }
                e /= t.Length;
                cont++;
                watch.Stop();
                var time = watch.ElapsedMilliseconds;
                Console.WriteLine($"Interactions: {cont}\nError: {e}\nTime: { time / (double)t.Length }ms");
                Console.Title = $"TSPS (Training Sample per Second): {Math.Ceiling(1000d / ((double)time / (double)t.Length))}";
            }
        }

        public static InputOutputTrainning[] EvenOrOddData(string even, string odd)
        {
            return new InputOutputTrainning[]
            {
                new InputOutputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 0 },
                        new TrainningValues { InputName = "i1", Value = 0 },
                        new TrainningValues { InputName = "i2", Value = 1 },
                        new TrainningValues { InputName = "i3", Value = 0 },
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = even, Value = 1 },
                        new Desired { Neuron = odd, Value = 0 },
                    }
                },
                new InputOutputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 1 },
                        new TrainningValues { InputName = "i1", Value = 0 },
                        new TrainningValues { InputName = "i2", Value = 0 },
                        new TrainningValues { InputName = "i3", Value = 0 },
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = even, Value = 1 },
                        new Desired { Neuron = odd, Value = 0 },
                    }
                },
                 new InputOutputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 0 },
                        new TrainningValues { InputName = "i1", Value = 0 },
                        new TrainningValues { InputName = "i2", Value = 0 },
                        new TrainningValues { InputName = "i3", Value = 0 },
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = even, Value = 1 },
                        new Desired { Neuron = odd, Value = 0 },
                    }
                },
                new InputOutputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 0 },
                        new TrainningValues { InputName = "i1", Value = 1 },
                        new TrainningValues { InputName = "i2", Value = 0 },
                        new TrainningValues { InputName = "i3", Value = 0 },
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = even, Value = 1 },
                        new Desired { Neuron = odd, Value = 0 },
                    }
                },
                new InputOutputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 0 },
                        new TrainningValues { InputName = "i1", Value = 0 },
                        new TrainningValues { InputName = "i2", Value = 0 },
                        new TrainningValues { InputName = "i3", Value = 1 },
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = even, Value = 0 },
                        new Desired { Neuron = odd, Value = 1 },
                    }
                },

                new InputOutputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 1 },
                        new TrainningValues { InputName = "i1", Value = 0 },
                        new TrainningValues { InputName = "i2", Value = 0 },
                        new TrainningValues { InputName = "i3", Value = 1 },
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = even, Value = 0 },
                        new Desired { Neuron = odd, Value = 1 },
                    }
                },

                new InputOutputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 0 },
                        new TrainningValues { InputName = "i1", Value = 1 },
                        new TrainningValues { InputName = "i2", Value = 0 },
                        new TrainningValues { InputName = "i3", Value = 1 },
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = even, Value = 0 },
                        new Desired { Neuron = odd, Value = 1 },
                    }
                },

                new InputOutputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 0 },
                        new TrainningValues { InputName = "i1", Value = 0 },
                        new TrainningValues { InputName = "i2", Value = 1 },
                        new TrainningValues { InputName = "i3", Value = 1 },
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = even, Value = 0 },
                        new Desired { Neuron = odd, Value = 1 },
                    }
                },
            };
        }
    }
}

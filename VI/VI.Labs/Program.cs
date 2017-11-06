using System;
using System.Collections.Generic;
using System.Linq;
using VI.Neural.Node;
using VI.Labs.Models;
using VI.NumSharp;

namespace VI.Labs
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = new[] { .9f, .01f, 0f, 0f };

            string header = $"Learning Rate: {values[0]}\nMin Error:{values[1]}\nMomentum: {values[3]}\n";

            var watch = System.Diagnostics.Stopwatch.StartNew();

            ProcessingDevice.Device = Device.CUDA;

            watch.Stop();
            Console.WriteLine($"Device Time: {watch.ElapsedMilliseconds}ms");

            var hiddens = LayerCreator.TANHSupervisedHiddenBPArray(2000, 4, values[0], values[3]);
            var hiddens2 = LayerCreator.TANHSupervisedHiddenBPArray(2000, 2000, values[0], values[3]);
            var outputs = LayerCreator.SigmoidSupervisedOutputBPArray(2, 2000, values[0], values[3]);

            watch = System.Diagnostics.Stopwatch.StartNew();
            LayerCreator.SynapseFull(hiddens, 0.3f);
            LayerCreator.SynapseFull(hiddens2, 0.3f);
            LayerCreator.SynapseFull(outputs, 0.3f);
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

                    // Feed Forward
                    var _h = hiddens.Output(inputs);
                    var _h2 = hiddens2.Output(_h);
                    var _o = outputs.Output(_h2);

                    // Backward
                    var _oe = outputs.Learn(_h2, desireds);
                    var _he2 = hiddens2.Learn(_h, _oe);
                    hiddens.Learn(inputs, _he2);

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
            Console.ReadKey();
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

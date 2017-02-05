using Brain.Node;
using Brain.Train.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Tests.DataTest
{
    public static class DataTrain
    {
        public static InputTrainning[] XORData(string output)
        {
            return new InputTrainning[]
            {
                new InputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 0 },
                        new TrainningValues { InputName = "i1", Value = 0 }
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = output, Value = 0 }
                    }
                },
                new InputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 0 },
                        new TrainningValues { InputName = "i1", Value = 1 }
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = output, Value = 1 }
                    }
                },
                new InputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 1 },
                        new TrainningValues { InputName = "i1", Value = 0 }
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = output, Value = 1 }
                    }
                },
                new InputTrainning
                {
                    Values = new List<TrainningValues>
                    {
                        new TrainningValues { InputName = "i0", Value = 1 },
                        new TrainningValues { InputName = "i1", Value = 1 }
                    },
                    DesiredValues = new List<Desired>
                    {
                        new Desired { Neuron = output, Value = 0 }
                    }
                }
            };
        }

        public static InputTrainning[] EvenOrOddData(string even, string odd)
        {
            return new InputTrainning[]
            {
                new InputTrainning
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
                new InputTrainning
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
                new InputTrainning
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
                new InputTrainning
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

                new InputTrainning
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

                new InputTrainning
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

                new InputTrainning
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

                new InputTrainning
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

        public static List<TrainningValues> MakeValues(int[] values)
        {
            var list = new List<TrainningValues>();
            for (int i = 0; i < values.Length; i++)
            {
                list.Add(new TrainningValues { Value = values[i], InputName = $"i{i}" });
            }
            return list;
        }
    }
}

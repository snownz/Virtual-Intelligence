using System;
using Brain.Train;
using System.Collections.Generic;
using Brain.Train.Models;
using Brain.Node;
using Brain.Tests.DataTest;
using NUnit.Framework;

namespace Brain.Tests
{
    [TestFixture]
    public class BackPropagationTrainTest
    {
        private static double[][] learning = new[] 
        {
            new[] { .9, .09,  0 },
            new[] { .8, .09,  0 },
            new[] { .7, .09,  0 },
            new[] { .6, .09,  0 },
            new[] { .5, .09,  10 },
            new[] { .4, .01,  10 },
            new[] { .3, .01,  10 },
            new[] { .2, .01,  10 },
            new[] { .1, .01,  10 }
        };        

        [Test, TestCaseSource("learning")]
        public void XorTestLearning(double[] values)
        {
            string header = $"Learning Rate: {values[0]}\nMin Error:{values[1]}\n";

            var input = new SensorNeuron[] { new SensorNeuron { Name = "i0" }, new SensorNeuron { Name = "i1" } };

            var hidden = NodeCreator.SigmoidSupervisedBPArray(4, "hidden", values[0], 0);
            var output = NodeCreator.SigmoidSupervisedBPArray(1, "output", values[0], 0);

            NodeCreator.ConnectNodes(hidden, input);
            NodeCreator.ConnectNodes(output, hidden);

            var teacher = new BackPropagationTrain(input, output);

            var t = DataTrain.XORData(teacher.Outputs[0]);

            var stats = WhileXTrain.TrainError(values[1], teacher, t, (int)values[2]);           

            var test = new[]
            {
                teacher.Test(t[0].Values),
                teacher.Test(t[1].Values),
                teacher.Test(t[2].Values),
                teacher.Test(t[3].Values),
            };

            header += $"Interactions: {stats.Epoch.ToString()}, Error: {stats.Error.ToString()}\n";
            header += $"t1: {test[0][0]}\nt: {test[1][0]}\nt3: {test[2][0]}\nt4: {test[3][0]}\n";

            if (test[0][0] < 0.5 && 
                test[1][0] > 0.5 && 
                test[2][0] > 0.5 && 
                test[3][0] < 0.5)
            {
                Assert.Pass(header);
            }
            else
            {
                Assert.Fail(header);
            }            
        }

        [Test, TestCaseSource("learning")]
        public void EvenOrOddTestLearning(double[] values)
        {
            string header = $"Learning Rate: {values[0]}\nMin Error:{values[1]}\n";

            var input = new SensorNeuron[] { new SensorNeuron { Name = "i0" }, new SensorNeuron { Name = "i1" }, new SensorNeuron { Name = "i2" }, new SensorNeuron { Name = "i3" } };

            var hidden = NodeCreator.SigmoidSupervisedBPArray(4, "hidden", values[0], 0);
            var output = NodeCreator.SigmoidSupervisedBPArray(2, "output", values[0], 0);

            NodeCreator.ConnectNodes(hidden, input);
            NodeCreator.ConnectNodes(output, hidden);

            var teacher = new BackPropagationTrain(input, output);

            var t = DataTrain.EvenOrOddData(teacher.Outputs[0], teacher.Outputs[1]);

            var stats = WhileXTrain.TrainError(values[1], teacher, t, (int)values[2]);

            var test = new[]
            {
                teacher.Test(DataTrain.MakeValues(new[] {1, 1, 1, 0})),
                teacher.Test(DataTrain.MakeValues(new[] {0, 1, 0, 0})),
                teacher.Test(DataTrain.MakeValues(new[] {1, 1, 0, 1})),
                teacher.Test(DataTrain.MakeValues(new[] {0, 0, 0, 1}))
            };

            header += $"Interactions: {stats.Epoch.ToString()}, Error: {stats.Error.ToString()}\n";
            header += $"t1: {test[0][0]}, {test[0][1]}\nt: {test[1][0]}, {test[1][1]}\nt3: {test[2][0]}, {test[2][1]}\nt4: {test[3][0]}, {test[3][1]}\n";

            if ( test[0][0] > test[0][1] &&
                test[1][0] > test[1][1] &&
                test[2][0] < test[2][1] &&
                test[3][0] < test[3][1])
            {
                Assert.Pass(header);
            }
            else
            {
                Assert.Fail(header);
            }

            Assert.Pass();
        }
    }
}

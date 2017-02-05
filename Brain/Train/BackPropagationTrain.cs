using Brain.Node;
using Brain.Train.Models;
using Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain.Train
{
    public class BackPropagationTrain : ISupervisedTrain
    {
        public BaseNode[] inputs;
        public BaseNode[] outputs;
        
        public BackPropagationTrain(BaseNode[] inputs, BaseNode[] outputs)
        {
            this.inputs = inputs;
            this.outputs = outputs;
        }
        
        public double Run(InputTrainning input)
        {
            foreach (var node in inputs)
            {
                node.Value = input.Values.FirstOrDefault(x => x.InputName == node.Name).Value;
            }

            var error = 0.0;

            foreach (var node in outputs)
            {
                var desired = input.DesiredValues.FirstOrDefault(x => x.Neuron == node.GetHashCode().ToString());
                var output = node.Output();
                node.UpdateConnections(input.DesiredValues.ToArray());
                error += Math.Abs(desired.Value - output ?? 0.0);
            }
            return Math.Abs(error);
        }

        public double RunEpoch(InputTrainning[] inputs)
        {
            var error = 0.0;
            foreach (var train in inputs)
            {
                error += Run(train);
            }
            return error;
        }

        public string[] Outputs => outputs.Select(x => x.GetHashCode().ToString()).ToArray();

        public double[] Test(List<TrainningValues> inputs)
        {
            var results = new double[outputs.Length];

            foreach (var node in this.inputs)
            {
                node.Value = inputs.FirstOrDefault(x => x.InputName == node.Name).Value;
            }

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = outputs[i].Output() ?? 0.0;
            }

            return results;
        }
    }
}

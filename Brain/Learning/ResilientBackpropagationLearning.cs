using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brain.Node;
using Brain.Train;
using Brain.Train.Models;
using Brain.Learning.Interface;

namespace Brain.Learning
{
    class ResilientBackpropagationLearning : ISupervisedLearning
    {
        public void UpdateWeights(BaseNode neuron, Desired[] desired)
        {
            double S = 0;
            neuron.ConnectionsTo.ForEach(Cnode =>
            {

            });
        }

        private void CalculateGradient(BaseNode neuron, Desired[] desiredOutput)
        {
          
        }

        private double CalculateError(BaseNode neuron, Desired[] desiredOutput)
        {
            if (neuron.ConnectionsFrom.Any())
            {
                var sum = 0.0;
                var output = neuron.Value ?? 0;
                var nextErrors = neuron.ConnectionsFrom.Select(x => (x.Node.CurrentError ?? CalculateError(x.Node, desiredOutput)) * x.Link.WeightPrior).ToArray();

                for (int k = 0; k < nextErrors.Length; k++)
                {
                    sum += nextErrors[k];
                }
                var error = sum * neuron.activation.Derivative2(neuron, output);
                return error;
            }
            else
            {
                var d = desiredOutput.FirstOrDefault(x => x.Neuron.Equals(neuron.GetHashCode()));
                var output = neuron.Value ?? 0;
                var e = d.Value - output;
                var error = e * neuron.activation.Derivative2(neuron, output);
                return error;
            }

        }
    }
}

using Brain.Node;
using System.Linq;
using Brain.Learning.Interface;
using Brain.Train.Models;

namespace Brain.Learning
{
    public class BackPropagationLearning : ISupervisedLearning
    {
        public void UpdateWeights(BaseNode neuron, Desired[] desired)
        {
            var cachedMomentum = neuron.LearningRate * neuron.Momentum;
            var cached1mMomentum = neuron.LearningRate * (1 - neuron.Momentum);
            var error = CalculateError(neuron, desired);
            var cachedError = error * cached1mMomentum;

            neuron.CurrentError = error;
            neuron.ConnectionsTo.ForEach(Cnode =>
            {
                Cnode.Weight += cachedMomentum * Cnode.Weight + cachedError * (Cnode.ConnectedNode.Value ?? 0.0);
                Cnode.ConnectedNode.UpdateConnections(desired);
            });
            neuron.Threshold += cachedMomentum * neuron.Threshold + cachedError;
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
                var d = desiredOutput.FirstOrDefault(x => x.Neuron == neuron.GetHashCode().ToString());
                var output = neuron.Value ?? 0;
                var e = d.Value - output;
                var error = e * neuron.activation.Derivative2(neuron, output);
                return error;
            }
        }
    }
}

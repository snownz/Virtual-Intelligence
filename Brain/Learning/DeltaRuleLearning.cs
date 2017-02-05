using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brain.Node;
using Brain.Train;
using Brain.Learning.Interface;
using Brain.Train.Models;

namespace Brain.Learning
{
    public class DeltaRuleLearning : ISupervisedLearning
    {
        public void UpdateWeights(BaseNode neuron, Desired[] desired)
        {
            var learningRate = neuron.LearningRate;
            var d = desired.FirstOrDefault(x => x.Neuron.Equals(neuron.GetHashCode()));

            if(d != null)
            {
                var e = d.Value - (neuron.Value ?? 0.0);
                var functionDerivative = neuron.activation.Derivative2(neuron, neuron.Value ?? 0.0);

                neuron.ConnectionsTo.ForEach(Cnode =>
                {
                    Cnode.Weight += learningRate * e * functionDerivative * (Cnode.ConnectedNode.Value ?? 0.0);
                    Cnode.ConnectedNode.UpdateConnections(desired);
                });

                neuron.Threshold += learningRate * e * functionDerivative;
            }            
        }
    }
}

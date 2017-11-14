using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.ANNOperations;
using VI.Neural.Node;
using VI.NumSharp.Arrays;

namespace VI.Neural.LearningMethods
{
    public class AnnSgdOutputOperations : IAnnSupervisedLearningMethod
    {
        private readonly AnnBasicOperations _ann;

        public AnnSgdOutputOperations(AnnBasicOperations ann)
        {
            _ann = ann;
        }

        public Array<float> Learn(INeuron neuron, Array<float> inputs, float[] error)
        {
            using (var e = new Array<float>(error))
            {
                _ann.BackWardDesired(neuron.Nodes, e);
                _ann.BackWardError(neuron.Nodes);
                _ann.ErrorGradient(neuron.Nodes, inputs);
                _ann.UpdateWeight(neuron.Nodes);
                _ann.UpdateBias(neuron.Nodes);
                return neuron.Nodes.ErrorWeightVector;
            }
        }

        public Array<float> Learn(INeuron neuron, float[] inputs, Array<float> error)
        {
            using (var i = new Array<float>(inputs))
            {
                _ann.BackWardDesired(neuron.Nodes, error);
                _ann.BackWardError(neuron.Nodes);
                _ann.ErrorGradient(neuron.Nodes, i);
                _ann.UpdateWeight(neuron.Nodes);
                _ann.UpdateBias(neuron.Nodes);
                return neuron.Nodes.ErrorWeightVector;
            }
        }

        public Array<float> Learn(INeuron neuron, Array<float> inputs, Array<float> error)
        {
            _ann.BackWardDesired(neuron.Nodes, error);
            _ann.BackWardError(neuron.Nodes);
            _ann.ErrorGradient(neuron.Nodes, inputs);
            _ann.UpdateWeight(neuron.Nodes);
            _ann.UpdateBias(neuron.Nodes);
            return neuron.Nodes.ErrorWeightVector;
        }
    }
}

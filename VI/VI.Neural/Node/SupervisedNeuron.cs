using VI.Maths.Random;
using VI.Neural.ANNOperations;
using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public class SupervisedNeuron : NeuronBase, INeuron, ISupervisedLearning
    {
        protected readonly ISupervisedOperations _operations;

        public SupervisedNeuron(int nodeSize,
            int connectionSize,
            float learningRate,
            float momentum,
            ISupervisedOperations operations) : base(nodeSize, connectionSize, learningRate, momentum)
        {
            _operations = operations;     
            _operations.SetLayer(_layer);
        }

        

        public Array<float> Output(float[] inputs)
        {
            using (var i = new Array<float>(inputs))
            {
                return Output(i);
            }
        }
        public Array<float> Output(Array<float> inputs)
        {
            _operations.FeedForward(inputs);
            return _layer.OutputVector;
        }

        public Array<float> ComputeGradient(float[] inputs, Array<float> error)
        {
            using (var i = new Array<float>(inputs))
            {
                return ComputeGradient(i, error);
            }
        }
        public Array<float> ComputeGradient(Array<float> inputs, float[] error)
        {
            using (var e = new Array<float>(error))
            {
                return ComputeGradient(inputs, e);
            }
        }
        public Array<float> ComputeGradient(Array<float> inputs, Array<float> error)
        {
            _operations.BackWard(error);
            _operations.ComputeGradient(inputs);
            return _layer.ErrorWeightVector;
        }

        public void UpdateParams()
        {
            _operations.UpdateParams();
        }

        public Array<float> Learn(float[] inputs, Array<float> error)
        {
            using (var i = new Array<float>(inputs))
            {
                return Learn(i, error);
            }
        }
        public Array<float> Learn(Array<float> inputs, float[] error)
        {
            using (var e = new Array<float>(error))
            {
                return Learn(inputs, e);
            }
        }
        public Array<float> Learn(Array<float> inputs, Array<float> error)
        {
            _operations.BackWard(error);
            _operations.ErrorGradient(inputs);
            _operations.UpdateParams();
            return _layer.ErrorWeightVector;
        }      
    }
}

using VI.Neural.ANNOperations;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public class UnsupervisedNeuron : NeuronBase, INeuron, IUnsupervisedLearning
    {
        protected readonly IUnsupervisedOperations _operations;

        public UnsupervisedNeuron(int nodeSize,
           int connectionSize,
           float learningRate,
           float momentum,
           IUnsupervisedOperations operations) : base(nodeSize, connectionSize, learningRate, momentum)
        {
            _operations = operations;
            _operations.SetLayer(_layer);
        }

        public Array<float> Output(Array<float> inputs)
        {
            _operations.FeedForward(inputs);
            return _layer.OutputVector;
        }
        public Array<float> Output(float[] inputs)
        {
            using (var i = new Array<float>(inputs))
            {
                return Output(i);
            }
        }

        public void Learn(float[] inputs)
        {
            using (var i = new Array<float>(inputs))
            {
                Learn(i);
            }
        }
        public void Learn(Array<float> inputs)
        {
            _operations.ErrorGradient(inputs);
            _operations.UpdateParams();
        }
    }
}

using VI.Maths.Random;
using VI.Neural.ANNOperations;
using VI.Neural.ANNOutputMethods;
using VI.Neural.Layer;
using VI.Neural.LearningMethods;
using VI.NumSharp.Array;

namespace VI.Neural.Node
{
    public class SupervisedNeuron : INeuron
    {
        private static ThreadSafeRandom _tr = new ThreadSafeRandom();

        private readonly ILayer _layer;
        private readonly IAnnSupervisedLearningMethod _learningMethod;
        private readonly IAnnOutput _outputMethod;

        public int NodesSize => _layer.Size;
        public int Connections => _layer.ConectionsSize;

        public ILayer Nodes => _layer;
        
        public SupervisedNeuron(int nodeSize, 
                    int connectionSize,
                    float learningRate, 
                    IAnnSupervisedLearningMethod learningMethod)
        {
            _layer = new ActivationLayer(nodeSize, connectionSize);

            _layer.KnowlodgeMatrix = new Array2D<float>(nodeSize, connectionSize);

            _layer.LearningRate = learningRate;
            _layer.CachedLearningRate = learningRate;

            //target.Momentum = momentum;
            //target.CachedMomentum = target.LearningRate * target.Momentum;
            //target.CachedLearningRate = target.LearningRate * (1 - target.Momentum);

            _layer.OutputVector = new Array<float>(nodeSize);

            _layer.BiasVector = new Array<float>(nodeSize);
            for (var i = 0; i < nodeSize; i++)
            {
                _layer.BiasVector[i] = 1;
            }
        }

        public Array<float> Output(Array<float> inputs)
        {
            return _outputMethod.Output(this, inputs);
        }
        public Array<float> Output(float[] inputs)
        {
            return _outputMethod.Output(this, inputs);
        }

        public Array<float> Learn(float[] inputs, Array<float> error)
        {
            return _learningMethod.Learn(this, inputs, error);
        }
        public Array<float> Learn(Array<float> inputs, float[] error)
        {
            return _learningMethod.Learn(this, inputs, error);
        }
        public Array<float> Learn(Array<float> inputs, Array<float> error)
        {
            return _learningMethod.Learn(this, inputs, error);
        }

        public void Synapsis(int node, int connection)
        {
            _layer.KnowlodgeMatrix[node, connection] = (float)_tr.NextDouble();
        }
        public void Synapsis(int node, int connection, float w)
        {
            _layer.KnowlodgeMatrix[node, connection] = w;
        }
    }
}

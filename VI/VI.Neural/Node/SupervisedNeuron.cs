using VI.Maths.Random;
using VI.Neural.ANNOperations;
using VI.Neural.ANNOutputMethods;
using VI.Neural.Layer;
using VI.Neural.LearningMethods;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public class SupervisedNeuron : INeuron
    {
        private static readonly ThreadSafeRandom _tr = new ThreadSafeRandom();

        private readonly ILayer _layer;
        private readonly IAnnSupervisedLearningMethod _learningMethod;
        private readonly IAnnOutput _outputMethod;

        public int NodesSize => _layer.Size;
        public int Connections => _layer.ConectionsSize;

        public ILayer Nodes => _layer;
        
        public SupervisedNeuron(int nodeSize, 
                    int connectionSize,
                    float learningRate, 
                    IAnnSupervisedLearningMethod learningMethod,
                    IAnnOutput outputMethod
            )
        {
            _layer = new ActivationLayer(nodeSize, connectionSize)
            {
                LearningRate = learningRate,
                CachedLearningRate = learningRate
            };
            _learningMethod = learningMethod;
            _outputMethod = outputMethod;
            InitializeArrays(nodeSize, connectionSize);
        }
      
        private void InitializeArrays(int nodeSize, int connectionSize)
        {
            _layer.KnowlodgeMatrix = new Array2D<float>(nodeSize, connectionSize);
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

using System.ComponentModel;
using VI.Maths.Random;
using VI.Neural.ANNOperations;
using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public class SupervisedNeuron : INeuron
    {
        private static readonly ThreadSafeRandom _tr = new ThreadSafeRandom();

        private readonly ILayer _layer;
        private readonly ISupervisedOperations _operations;
        
        public int NodesSize => _layer.Size;
        public int Connections => _layer.ConectionsSize;
        
        public ILayer Nodes => _layer;
        
        public SupervisedNeuron(int nodeSize, 
                    int connectionSize,
                    float learningRate, 
            ISupervisedOperations operations)
        {
            _operations = operations;
            _layer = new ActivationLayer(nodeSize, connectionSize)
            {
                LearningRate = learningRate,
                CachedLearningRate = learningRate
            };
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
            _operations.FeedForward(inputs);
            return _layer.OutputVector;
        }
        public Array<float> Output(float[] inputs)
        {
            using (var i = new Array<float>(inputs))
            {
                _operations.FeedForward(i);
                return _layer.OutputVector;
            }
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

using VI.Cognitive.ANNOperations;
using VI.Cognitive.Layer;
using VI.Maths.Random;
using VI.NumSharp.Array;

namespace VI.Cognitive.Node
{
    public class OutputNeuron2 : INeuron
    {
        private ActivationLayer2 _layer;
        private static ThreadSafeRandom _tr = new ThreadSafeRandom();
        private readonly ANNBasicOperations _ann;

        public int Nodes => _layer.Size;
        public int Connections => _layer.ConectionsSize;

        public OutputNeuron2(int nodeSize, int connectionSize, float learningRate, ANNBasicOperations operations)
        {
            _layer = new ActivationLayer2(nodeSize, connectionSize);
            _ann = operations;

            _layer.KnowlodgeMatrix = new Array2D<float>(nodeSize, connectionSize);

            _layer.LearningRate = learningRate;
            _layer.CachedLearningRate = learningRate;

            _layer.OutputVector = new Array<float>(nodeSize);

            _layer.BiasVector = new Array<float>(nodeSize);
            for (var i = 0; i < nodeSize; i++)
            {
                _layer.BiasVector[i] = 1;
            }
        }
        
        public Array<float> Output(Array<float> inputs)
        {
            _ann.FeedForward(_layer, inputs);
            return _layer.OutputVector;
        }
        
        public Array<float> Learn(float[] inputs, Array<float> error)
        {
            using (var i = new Array<float>(inputs))
            {
                _ann.BackWardDesired(_layer, error);
                _ann.BackWardError(_layer, _layer.ErrorVector);
                _ann.ErrorGradient(_layer, _layer.ErrorVector, i);
                _ann.UpdateWeight(_layer);
                _ann.UpdateBias(_layer);
                return _layer.ErrorWeightVector;
            }
        }
        public Array<float> Learn(Array<float>  inputs, float[] error)
        {
            using (var e = new Array<float>(error))
            {
                _ann.BackWardDesired(_layer, e);
                _ann.BackWardError(_layer, _layer.ErrorVector);
                _ann.ErrorGradient(_layer, _layer.ErrorVector, inputs);
                _ann.UpdateWeight(_layer);
                _ann.UpdateBias(_layer);
                return _layer.ErrorWeightVector;
            }
        }
        public Array<float> Learn(Array<float>  inputs, Array<float> error)
        {
            _ann.BackWardDesired(_layer, error);
            _ann.BackWardError(_layer, _layer.ErrorVector);
            _ann.ErrorGradient(_layer, _layer.ErrorVector, inputs);
            _ann.UpdateWeight(_layer);
            _ann.UpdateBias(_layer);
            return _layer.ErrorWeightVector;
        }

        public void Synapsis(int node, int connection)
        {
            _layer.KnowlodgeMatrix[node, connection] = (float)_tr.NextDouble();
        }
    }
}

﻿using VI.Cognitive.ANNOperations;
using VI.Cognitive.Layer;
using VI.Maths.Random;
using VI.NumSharp.Array;

namespace VI.Cognitive.Node
{
    public class HiddenNeuron : INeuron
    {
        private ActivationLayer2 _layer;
        private static ThreadSafeRandom _tr = new ThreadSafeRandom();
        private readonly ANNBasicOperations _ann;

        public int Nodes => _layer.Size;
        public int Connections => _layer.ConectionsSize;

        public HiddenNeuron(int nodeSize, int connectionSize, float learningRate, ANNBasicOperations operations)
        {
            _layer = new ActivationLayer2(nodeSize, connectionSize);
            _ann = operations;

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
            _ann.FeedForward(_layer, inputs);
            return _layer.OutputVector;
        }
        public Array<float> Output(float[] inputs)
        {
            using (var i = new Array<float>(inputs))
            {
                _ann.FeedForward(_layer, i);
                return _layer.OutputVector;
            }                
        }

        public Array<float> Learn(float[] inputs, Array<float> error)
        {
            using (var i = new Array<float>(inputs))
            {
                _ann.BackWard(_layer, error);
                _ann.BackWardError(_layer);
                _ann.ErrorGradient(_layer, i);
                _ann.UpdateWeight(_layer);
                _ann.UpdateBias(_layer);
                return _layer.ErrorWeightVector;
            }
        }
        public Array<float> Learn(Array<float>  inputs, float[] error)
        {
            using (var e = new Array<float>(error))
            {
                _ann.BackWard(_layer, e);
                _ann.BackWardError(_layer);
                _ann.ErrorGradient(_layer, inputs);
                _ann.UpdateWeight(_layer);
                _ann.UpdateBias(_layer);
                return _layer.ErrorWeightVector;
            }
        }
        public Array<float> Learn(Array<float>  inputs, Array<float> error)
        {
            _ann.BackWard(_layer, error);
            _ann.BackWardError(_layer);
            _ann.ErrorGradient(_layer, inputs);
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

﻿using System.Collections.Generic;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerCreatorActivation : LayerCreator
    {
        public LayerCreatorOptimizer WithSigmoid()
        {
            _activation = new SigmoidFunction();
            
            return new LayerCreatorOptimizer(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }

        public LayerCreatorOptimizer WithTANH()
        {
            _activation = new TANHFunction();
            
            return new LayerCreatorOptimizer(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }

        public LayerCreatorOptimizer WithLeakRelu()
        {
            _activation = new LeakReluFunction();
            
            return new LayerCreatorOptimizer(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }

        public LayerCreatorOptimizer Nothing()
        {
            
            return new LayerCreatorOptimizer(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }

        public LayerCreatorActivation(float learningRate, float dropout, float momentum,
            ISupervisedOperations supervised, IUnsupervisedOperations unsupervised, IActivationFunction activation,
            IOptimizerFunction optimizer, IErrorFunction error, IList<(int x, int y)> nodesToSynapsys, float weight,
            int size, int connections) : base(learningRate, dropout, momentum, supervised, unsupervised, activation,
            optimizer, error, nodesToSynapsys, weight, size, connections)
        {
        }
    }
}

using System.Collections.Generic;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerCreatorUnsupervised : LayerCreator
    {
        public LayerCreatorOptimizer SomLayer()
        {
            _unsupervised = new ANNSelfOrganizedMapsOperations();
            
            return  new LayerCreatorOptimizer(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }

        public LayerCreatorUnsupervised(float learningRate, float dropout, float momentum,
            ISupervisedOperations supervised, IUnsupervisedOperations unsupervised, IActivationFunction activation,
            IOptimizerFunction optimizer, IErrorFunction error, IList<(int x, int y)> nodesToSynapsys, float weight,
            int size, int connections) : base(learningRate, dropout, momentum, supervised, unsupervised, activation,
            optimizer, error, nodesToSynapsys, weight, size, connections)
        {
        }
    }
}

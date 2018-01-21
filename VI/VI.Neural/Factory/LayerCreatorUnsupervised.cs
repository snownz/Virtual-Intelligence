using System.Collections.Generic;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerCreatorUnsupervised : LayerCreator
    {
        public LayerBuilder SomLayer(float maxDist, float degradation)
        {
            _unsupervised = new ANNSelfOrganizedMapsOperations();

            (_unsupervised as ANNSelfOrganizedMapsOperations).Set2DSize(_2Dsize.w, _2Dsize.h);
            (_unsupervised as ANNSelfOrganizedMapsOperations).SetNeighborhoodDistance(maxDist, degradation);

            return  new LayerBuilder(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections, _2Dsize);
        }

        public LayerCreatorUnsupervised(float learningRate, float dropout, float momentum,
            ISupervisedOperations supervised, IUnsupervisedOperations unsupervised, IActivationFunction activation,
            IOptimizerFunction optimizer, IErrorFunction error, IList<(int x, int y)> nodesToSynapsys, float weight,
            int size, int connections, (int w, int h) size2D) : base(learningRate, dropout, momentum, supervised, unsupervised, activation,
            optimizer, error, nodesToSynapsys, weight, size, connections, size2D)
        {
        }
    }
}

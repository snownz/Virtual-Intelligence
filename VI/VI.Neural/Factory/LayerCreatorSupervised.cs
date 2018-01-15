using System.Collections.Generic;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerCreatorSupervised : LayerCreator
    {
        public LayerCreatorOptimizer SoftmaxLayer()
        {
            _supervised = new ANNSoftmaxOperations();
            
            return new LayerCreatorOptimizer(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }
        public LayerCreatorActivation Conv2DLayer()
        {
            _supervised = new ANNConv2DOperations();
            
            return new LayerCreatorActivation(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }
        public LayerCreatorActivation Conv3DLayer()
        {
            _supervised = new ANNConv3DOperations();
            
            return new LayerCreatorActivation(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }
        public LayerCreatorActivation DenseLayer()
        {
            _supervised = new ANNDenseOperations();
            
            return new LayerCreatorActivation(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }
        public LayerCreatorActivation MaxPolling2DLayer()
        {
            _supervised = new ANNMaxPooling2DOperations();
            
            return new LayerCreatorActivation(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }
        public LayerCreatorActivation MaxPolling3DLayer()
        {
            _supervised = new ANNMaxPooling3DOperations();
            
            return new LayerCreatorActivation(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }

        public LayerCreatorSupervised(float learningRate, float dropout, float momentum,
            ISupervisedOperations supervised, IUnsupervisedOperations unsupervised, IActivationFunction activation,
            IOptimizerFunction optimizer, IErrorFunction error, IList<(int x, int y)> nodesToSynapsys, float weight,
            int size, int connections) : base(learningRate, dropout, momentum, supervised, unsupervised, activation,
            optimizer, error, nodesToSynapsys, weight, size, connections)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.Node;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerCreator : IDisposable
    {
        protected float _learningRate;
        protected float _dropout;
        protected float _momentum;
        protected float _weight;

        protected int _size;
        protected int _connections;
        
        protected IList<(int x, int y)> _nodesToSynapsys;

        protected ISupervisedOperations _supervised;
        protected IUnsupervisedOperations _unsupervised;
        protected IActivationFunction _activation;
        protected IOptimizerFunction _optimizer;
        protected IErrorFunction _error;
      
        public LayerCreator(float learningRate, float dropout, float momentum, ISupervisedOperations supervised,
            IUnsupervisedOperations unsupervised, IActivationFunction activation, IOptimizerFunction optimizer,
            IErrorFunction error, IList<(int x, int y)> nodesToSynapsys, float weight, int size, int connections) : this(size, connections)
        {
            _learningRate = learningRate;
            _dropout = dropout;
            _momentum = momentum;
            _supervised = supervised;
            _unsupervised = unsupervised;
            _activation = activation;
            _optimizer = optimizer;
            _error = error;
            _nodesToSynapsys = nodesToSynapsys;
            _weight = weight;
        }
        public LayerCreator(int size, int connections)
        {
            _size = size;
            _connections = connections;
        }

        public LayerCreatorSupervised Supervised()
        {
            return new LayerCreatorSupervised(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }
        public LayerCreatorUnsupervised Unsupervised()
        {
            return new LayerCreatorUnsupervised(_learningRate, _dropout, _momentum, _supervised, _unsupervised,
                _activation, _optimizer, _error, _nodesToSynapsys, _weight, _size, _connections);
        }

        public LayerCreator WithLearningRate(float value)
        {
            _learningRate = value;
            return this;
        }
        public LayerCreator AddDropout(float value)
        {
            _dropout = value;
            return this;
        }
        public LayerCreator WithMomentum(float value)
        {
            _momentum = value;
            return this;
        }
        
        public LayerCreator AddSynapse(int position, int connection)
        {
            _nodesToSynapsys.Add((position, connection));
            return this;
        }
        public LayerCreator FullSynapse()
        {
            _nodesToSynapsys = null;
            return this;
        }
        public LayerCreator FullSynapse(float value)
        {
            _nodesToSynapsys = null;
            _weight = value;
            return this;
        }
        
        public void Dispose()
        {
            _supervised = null;
            _unsupervised = null;
            _activation = null;
            _optimizer = null;
            _error = null;
        }
    }
}

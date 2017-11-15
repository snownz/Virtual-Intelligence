using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VI.Neural.ANNOperations;
using VI.Neural.Factory;

namespace VI.Neural.Node
{
    public class LayerBuilder : LayerCreator
    {
        public LayerBuilder(int size, int connections, float learningRate)
        {
            _nodesToSynapsys = new List<(int, int)>();
            _size = size;
            _connections = connections;
            _learningRate = learningRate;
        }
        public LayerBuilder(int size, int connections, float learningRate, float momentum, EActivationFunction activationFunction, EErrorFunction errorFunction, EOptimizerFunction optimizerFunction, ELearningMethod learningMethod) 
            : base(size, connections, learningRate, momentum, activationFunction, errorFunction, optimizerFunction, learningMethod)
        {
        }

        public LayerActivation Supervised()
        {
            _learningMethod = ELearningMethod.Supervised;
            return (LayerActivation) this.Clone(typeof(LayerActivation));
        }
    }
    
    public class LayerActivation : LayerCreator
    {
        public LayerActivation(int size, int connections, float learningRate, float momentum, EActivationFunction activationFunction, EErrorFunction errorFunction, EOptimizerFunction optimizerFunction, ELearningMethod learningMethod) : base(size, connections, learningRate, momentum, activationFunction, errorFunction, optimizerFunction, learningMethod)
        {
        }

        public LayerType WithSigmoid()
        {
            _activationFunction = EActivationFunction.Sigmoid;
            return (LayerType) this.Clone(typeof(LayerType));
        }
        
        public LayerType WithLeakRelu()
        {
            _activationFunction = EActivationFunction.LeakRelu;
            return (LayerType) this.Clone(typeof(LayerType));
        }
    }

    public class LayerType : LayerCreator
    {
        public LayerType(int size, int connections, float learningRate, float momentum, EActivationFunction activationFunction, EErrorFunction errorFunction, EOptimizerFunction optimizerFunction, ELearningMethod learningMethod) : base(size, connections, learningRate, momentum, activationFunction, errorFunction, optimizerFunction, learningMethod)
        {
        }

        public LayerOptimizer Hidden()
        {
            _errorFunction = EErrorFunction.Dense;
            return (LayerOptimizer) this.Clone(typeof(LayerOptimizer));
        }

        public LayerOptimizer Output()
        {
            _errorFunction = EErrorFunction.Desired;
            return (LayerOptimizer) this.Clone(typeof(LayerOptimizer));
        }
    }

    public class LayerOptimizer : LayerCreator
    {
        public LayerOptimizer(int size, int connections, float learningRate, float momentum, EActivationFunction activationFunction, EErrorFunction errorFunction, EOptimizerFunction optimizerFunction, ELearningMethod learningMethod) : base(size, connections, learningRate, momentum, activationFunction, errorFunction, optimizerFunction, learningMethod)
        {
        }

        public LayerCreator WithSGD()
        {
            _optimizerFunction = EOptimizerFunction.SGD;
            return this;
        }
    }
    
    public class LayerCreator
    {
        static ILayerFactory _factory = new LayerFactory();

        protected IList<(int x, int y)> _nodesToSynapsys;

        protected int _size;
        protected int _connections;
        protected float _learningRate;
        protected float _momentum;
        protected float _weight;

        protected EActivationFunction _activationFunction;
        protected EErrorFunction _errorFunction;
        protected EOptimizerFunction _optimizerFunction;
        protected ELearningMethod _learningMethod;
        
        public LayerCreator(int size,
            int connections,
            float learningRate,
            float momentum,
            EActivationFunction activationFunction,
            EErrorFunction errorFunction,
            EOptimizerFunction optimizerFunction,
            ELearningMethod learningMethod)
        {
            _size = size;
            _connections = connections;
            _learningRate = learningRate;
            _momentum = momentum;
            _activationFunction = activationFunction;
            _errorFunction = errorFunction;
            _optimizerFunction = optimizerFunction;
            _learningMethod = learningMethod;
        }

        public LayerCreator()
        {

        }

        public object Clone(Type t)
        {
            return Activator.CreateInstance(t, _size, _connections, _learningRate, _momentum, _activationFunction,
                _errorFunction, _optimizerFunction, _learningMethod);
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


        public INeuron Build()
        {
            INeuron layer = null;

            switch (_learningMethod)
            {
                case ELearningMethod.Supervised:
                    layer = _factory.Supervised(_size, _connections, _learningRate, _momentum,
                        _factory.SupervisedOperations(_activationFunction, _errorFunction, _optimizerFunction));
                    break;
                case ELearningMethod.Unsipervised:
                    break;
            }

            if (_nodesToSynapsys == null)
            {
                if (_weight > 0)
                    SynapseFull(layer, _weight);
                else
                    SynapseFull(layer);
            }
            else
            {
                Parallel.ForEach(_nodesToSynapsys, node =>
                {
                    if (_weight > 0)
                        Synapse(layer, node.x, node.y, _weight);
                    else
                        Synapse(layer, node.x, node.y);
                });
            }
            
            return layer;
        }
        
        private static void SynapseFull(INeuron n)
        {
            for (var i = 0; i < n.NodesSize; i++)
            {
                for (var j = 0; j < n.Connections; j++)
                {
                    Synapse(n, i, j);
                }
            }
        }
        private static void Synapse(INeuron n, int node, int connection)
        {
            n.Synapsis(node, connection);
        }
        private static void SynapseFull(INeuron n, float w)
        {
            for (var i = 0; i < n.NodesSize; i++)
            {
                for (var j = 0; j < n.Connections; j++)
                {
                    Synapse(n, i, j, w);
                }
            }
        }
        private static void Synapse(INeuron n, int node, int connection, float w)
        {
            n.Synapsis(node, connection, w);
        }
    }
}

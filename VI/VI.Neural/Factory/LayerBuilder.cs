using System.Collections.Generic;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.Node;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerBuilder : LayerCreator
    {
        public INeuron Build()
        {
            INeuron neuron = null;

            if (_supervised != null)
            {
                _supervised.SetActivation(_activation);
                _supervised.SetError(_error);
                _supervised.SetOptimizer(_optimizer);
                neuron = new SupervisedNeuron(_size, _connections, _learningRate, _momentum, _supervised);
            }
            else if (_unsupervised != null)
            {
                
            }

            if (_weight > 0) SynapseFull(neuron, _weight);
            else             SynapseFull(neuron);
            
            return neuron;
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

        public LayerBuilder(float learningRate, float dropout, float momentum, ISupervisedOperations supervised,
            IUnsupervisedOperations unsupervised, IActivationFunction activation, IOptimizerFunction optimizer,
            IErrorFunction error, IList<(int x, int y)> nodesToSynapsys, float weight, int size, int connections) :
            base(learningRate, dropout, momentum, supervised, unsupervised, activation, optimizer, error,
                nodesToSynapsys, weight, size, connections)
        {
        }
    }
}
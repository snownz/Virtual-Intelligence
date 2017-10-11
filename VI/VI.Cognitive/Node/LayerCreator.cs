using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.Cognitive.ANNOperations;
using VI.Cognitive.Factory;
using VI.Maths.LogisticFunctions;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Node
{
    public class LayerCreator
    {
        static CudaLayerFactory _factory = new CudaLayerFactory();

        public static OutputNeuron SigmoidSupervisedOutputBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.OutputNeuron(size, connections, learning, momentum, _factory.Sigmoid());
        }

        public static OutputNeuron TANHSupervisedOutputBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.OutputNeuron(size, connections, learning, momentum, _factory.TANH());
        }

        public static HiddenNeuron SigmoidSupervisedHiddenBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.HiddenNeuron(size, connections, learning, momentum, _factory.Sigmoid());
        }

        public static HiddenNeuron LeakReluSupervisedHiddenBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.HiddenNeuron(size, connections, learning, momentum, _factory.LeakRelu());
        }

        public static HiddenNeuron TANHSupervisedHiddenBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.HiddenNeuron(size, connections, learning, momentum, _factory.TANH());
        }

        public static void SynapseFull(INeuron n)
        {
            for (var i = 0; i < n.Nodes; i++)
            {
                for (var j = 0; j < n.Connections; j++)
                {
                    Synapse(n, i, j);
                }
            }
        }
        public static void Synapse(INeuron n, int node, int connection)
        {
            n.Synapsis(node, connection);
        }
    }
}

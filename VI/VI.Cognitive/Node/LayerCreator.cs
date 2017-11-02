using VI.Cognitive.Factory;

namespace VI.Cognitive.Node
{ 
    public class LayerCreator
    {
        static ILayerFactory _factory = new LayerFactory();
        
        public static OutputNeuron2 SigmoidSupervisedOutputBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.OutputNeuron(size, connections, learning, momentum, _factory.Sigmoid());
        }

        public static OutputNeuron2 TANHSupervisedOutputBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.OutputNeuron(size, connections, learning, momentum, _factory.TANH());
        }

        public static HiddenNeuron2 SigmoidSupervisedHiddenBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.HiddenNeuron(size, connections, learning, momentum, _factory.Sigmoid());
        }

        public static HiddenNeuron2 LeakReluSupervisedHiddenBPArray(int size, int connections, float learning, float momentum)
        {
            return _factory.HiddenNeuron(size, connections, learning, momentum, _factory.LeakRelu());
        }

        public static HiddenNeuron2 TANHSupervisedHiddenBPArray(int size, int connections, float learning, float momentum)
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

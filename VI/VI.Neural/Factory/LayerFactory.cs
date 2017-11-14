using VI.Neural.ANNOperations;
using VI.Neural.Node;

namespace VI.Neural.Factory
{
    public class LayerFactory : ILayerFactory
    {
        public AnnBasicOperations Sigmoid()
        {
            return 
                null;
        }

        public AnnBasicOperations LeakRelu()
        {
            return 
                null;
        }

        public AnnBasicOperations TANH()
        {
            return 
                null;
        }

        public SupervisedNeuron Supervised(int size, int connections, float learning, float momentum, AnnBasicOperations operations)
        {
            return null;
        }
    }
}

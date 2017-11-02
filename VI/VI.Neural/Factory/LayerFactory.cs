using VI.Neural.ANNOperations;
using VI.Neural.Node;
using VI.NumSharp;

namespace VI.Neural.Factory
{
    public class LayerFactory : ILayerFactory
    {
        public ANNBasicOperations Sigmoid()
        {
            return 
                new ANNBasicOperations(ProcessingDevice.Sigmoid, ProcessingDevice.SquaredLoss);
        }

        public ANNBasicOperations LeakRelu()
        {
            return 
                new ANNBasicOperations(ProcessingDevice.LeakRelu, ProcessingDevice.SquaredLoss);
        }

        public ANNBasicOperations TANH()
        {
            return 
                new ANNBasicOperations(ProcessingDevice.TANH, ProcessingDevice.SquaredLoss);
        }

        public HiddenNeuron HiddenNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations)
        {
            return 
                new HiddenNeuron(size, connections, learning, operations);
        }

        public OutputNeuron OutputNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations)
        {
            return
                new OutputNeuron(size, connections, learning, operations);
        }
    }
}

using VI.Cognitive.ANNOperations;
using VI.Cognitive.Node;
using VI.NumSharp;

namespace VI.Cognitive.Factory
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
                new ANNBasicOperations(ProcessingDevice.Sigmoid, ProcessingDevice.SquaredLoss);
        }

        public ANNBasicOperations TANH()
        {
            return 
                new ANNBasicOperations(ProcessingDevice.Sigmoid, ProcessingDevice.SquaredLoss);
        }

        public HiddenNeuron2 HiddenNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations)
        {
            return 
                new HiddenNeuron2(size, connections, learning, operations);
        }

        public OutputNeuron2 OutputNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations)
        {
            return
                new OutputNeuron2(size, connections, learning, operations);
        }
    }
}

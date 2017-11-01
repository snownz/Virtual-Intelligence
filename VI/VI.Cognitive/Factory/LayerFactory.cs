using VI.Cognitive.ANNOperations;
using VI.Cognitive.Node;
using VI.Cognitive.Provider;
using VI.Maths.LogisticFunctions;
using VI.ParallelComputing;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Factory
{
    public class LayerFactory : ILayerFactory
    {
        public ANNBasicOperations Sigmoid()
        {
            return new 
                ANNBasicOperations(
                                    new ActivationFunctionProvider(ProcessingDevice.CPUSigmoidDevice), 
                                    new LossFunctionProvider(ProcessingDevice.CPUSquaredLossDevice)
                                    );
        }

        public ANNBasicOperations LeakRelu()
        {
            return new
                ANNBasicOperations(
                                    new ActivationFunctionProvider(ProcessingDevice.CPULeakReluDevice),
                                    new LossFunctionProvider(ProcessingDevice.CPUSquaredLossDevice)
                                    );
        }

        public ANNBasicOperations TANH()
        {
            return new
                ANNBasicOperations(
                                    new ActivationFunctionProvider(ProcessingDevice.CPUTANHDevice),
                                    new LossFunctionProvider(ProcessingDevice.CPUSquaredLossDevice)
                                    );
        }

        public HiddenNeuron2 HiddenNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations)
        {
            return new HiddenNeuron2(size, connections, learning, operations);
        }

        public OutputNeuron2 OutputNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations)
        {
            return new OutputNeuron2(size, connections, learning, operations);
        }
    }
}

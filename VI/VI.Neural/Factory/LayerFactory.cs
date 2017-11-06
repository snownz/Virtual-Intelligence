using VI.Neural.ANNOperations;
using VI.Neural.LearningMethods;
using VI.Neural.Node;
using VI.NumSharp;

namespace VI.Neural.Factory
{
    public class LayerFactory : ILayerFactory
    {
        public AnnBasicOperations Sigmoid()
        {
            return 
                new AnnBasicOperations(ProcessingDevice.Sigmoid, ProcessingDevice.SquaredLoss);
        }

        public AnnBasicOperations LeakRelu()
        {
            return 
                new AnnBasicOperations(ProcessingDevice.LeakRelu, ProcessingDevice.SquaredLoss);
        }

        public AnnBasicOperations TANH()
        {
            return 
                new AnnBasicOperations(ProcessingDevice.TANH, ProcessingDevice.SquaredLoss);
        }

        public IAnnSupervisedLearningMethod SgdHiddenLearning(AnnBasicOperations operations)
        {
            return new AnnSgdHiddenOperations(operations);
        }


        public HiddenNeuron HiddenNeuron(int size, int connections, float learning, float momentum, AnnBasicOperations operations)
        {
            return 
                new HiddenNeuron(size, connections, learning, operations);
        }

        public OutputNeuron OutputNeuron(int size, int connections, float learning, float momentum, AnnBasicOperations operations)
        {
            return
                new OutputNeuron(size, connections, learning, operations);
        }

        public SupervisedNeuron Supervised(int size, int connections, float learning, float momentum, AnnBasicOperations operations)
        {
            return null;
        }
    }
}

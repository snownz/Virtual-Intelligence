using VI.Neural.ANNOperations;
using VI.Neural.Node;

namespace VI.Neural.Factory
{
    public interface ILayerFactory
    {
        AnnBasicOperations LeakRelu();
        AnnBasicOperations Sigmoid();
        AnnBasicOperations TANH();
        SupervisedNeuron Supervised(int size, int connections, float learning, float momentum,
            AnnBasicOperations operations);
    }
}
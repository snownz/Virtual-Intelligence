using VI.Neural.ANNOperations;
using VI.Neural.Node;

namespace VI.Neural.Factory
{
    public interface ILayerFactory
    {
        HiddenNeuron HiddenNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations);
        ANNBasicOperations LeakRelu();
        OutputNeuron OutputNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations);
        ANNBasicOperations Sigmoid();
        ANNBasicOperations TANH();
    }
}
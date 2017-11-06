using VI.Neural.ANNOperations;
using VI.Neural.Node;

namespace VI.Neural.Factory
{
    public interface ILayerFactory
    {
        HiddenNeuron HiddenNeuron(int size, int connections, float learning, float momentum, AnnBasicOperations operations);
        AnnBasicOperations LeakRelu();
        OutputNeuron OutputNeuron(int size, int connections, float learning, float momentum, AnnBasicOperations operations);
        AnnBasicOperations Sigmoid();
        AnnBasicOperations TANH();
    }
}
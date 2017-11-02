using VI.Cognitive.ANNOperations;
using VI.Cognitive.Node;

namespace VI.Cognitive.Factory
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
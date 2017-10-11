using VI.Cognitive.ANNOperations;
using VI.Cognitive.Node;

namespace VI.Cognitive.Factory
{
    public interface ILayerFactory
    {
        HiddenNeuron HiddenNeuron(int size, int connections, float learning, float momentum, ANNOperationsInterface operations);
        ANNOperationsInterface LeakRelu();
        OutputNeuron OutputNeuron(int size, int connections, float learning, float momentum, ANNOperationsInterface operations);
        ANNOperationsInterface Sigmoid();
        ANNOperationsInterface TANH();
    }
}
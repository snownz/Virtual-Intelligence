using VI.Cognitive.ANNOperations;
using VI.Cognitive.Node;

namespace VI.Cognitive.Factory
{
    public interface ILayerFactory
    {
        HiddenNeuron2 HiddenNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations);
        ANNBasicOperations LeakRelu();
        OutputNeuron2 OutputNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations);
        ANNBasicOperations Sigmoid();
        ANNBasicOperations TANH();
    }
}
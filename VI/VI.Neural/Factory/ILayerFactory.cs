using VI.Neural.ANNOperations;
using VI.Neural.Node;

namespace VI.Neural.Factory
{
    public interface ILayerFactory
    {
        ISupervisedOperations SupervisedOperations(EActivationFunction act, EErrorFunction err, 
            EOptimizerFunction opt);
        SupervisedNeuron Supervised(int size, int connections, float learning, float momentum,
            ISupervisedOperations operations);
    }
}
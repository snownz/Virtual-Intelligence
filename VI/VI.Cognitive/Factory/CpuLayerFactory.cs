using VI.Cognitive.ANNOperations;
using VI.Cognitive.Node;
using VI.Maths.LogisticFunctions;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Factory
{
    public class CpuLayerFactory : ILayerFactory
    {
        public ANNOperationsInterface Sigmoid()
        {
            return new ANNOperationsInterface(new CpuAnnInterface<SigmoidFunction>());
        }

        public ANNOperationsInterface LeakRelu()
        {
            return new ANNOperationsInterface(new CpuAnnInterface<LeakyRELUFunction>());
        }

        public ANNOperationsInterface TANH()
        {
            return new ANNOperationsInterface(new CpuAnnInterface<TANHFuncion>());
        }

        public HiddenNeuron HiddenNeuron(int size, int connections, float learning, float momentum, ANNOperationsInterface operations)
        {
            return new HiddenNeuron(size, connections, learning, momentum, operations);
        }

        public OutputNeuron OutputNeuron(int size, int connections, float learning, float momentum, ANNOperationsInterface operations)
        {
            return new OutputNeuron(size, connections, learning, momentum, operations);
        }
    }
}

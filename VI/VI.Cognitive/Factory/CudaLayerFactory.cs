using VI.Cognitive.ANNOperations;
using VI.Cognitive.Node;
using VI.Maths.LogisticFunctions;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Factory
{
    public class CudaLayerFactory
    {
        public ANNOperationsInterface Sigmoid()
        {
            return new ANNOperationsInterface(new CudaAnnInterface<SigmoidFunction>());
        }

        public ANNOperationsInterface LeakRelu()
        {
            return new ANNOperationsInterface(new CudaAnnInterface<LeakyRELUFunction>());
        }

        public ANNOperationsInterface TANH()
        {
            return new ANNOperationsInterface(new CudaAnnInterface<TANHFuncion>());
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

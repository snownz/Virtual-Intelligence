using System;
using VI.Cognitive.ANNOperations;
using VI.Cognitive.Node;
using VI.Maths.LogisticFunctions;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Factory
{
    public class CudaLayerFactory : ILayerFactory
    {
        public HiddenNeuron2 HiddenNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations)
        {
            throw new NotImplementedException();
        }

        public ANNBasicOperations LeakRelu()
        {
            throw new NotImplementedException();
        }

        public OutputNeuron2 OutputNeuron(int size, int connections, float learning, float momentum, ANNBasicOperations operations)
        {
            throw new NotImplementedException();
        }

        public ANNBasicOperations Sigmoid()
        {
            throw new NotImplementedException();
        }

        public ANNBasicOperations TANH()
        {
            throw new NotImplementedException();
        }
    }
}

using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Error;
using VI.Neural.Node;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerFactory : ILayerFactory
    {
        public ISupervisedOperations SupervisedOperations(EActivationFunction act,
            EErrorFunction err,
            EOptimizerFunction opt)
        {
            IActivationFunction activationFunction = null;
            IErrorFunction errorFunction= null;
            IOptimizerFunction optimizerFunction= null;

            switch (act)
            {
                case EActivationFunction.Sigmoid:
                    activationFunction = new SigmoidFunction();
                    break;
                case EActivationFunction.LeakRelu:
                    activationFunction =new LeakReluFunction();
                    break;
            }

            switch (err)
            {
                case EErrorFunction.Dense:
                    errorFunction = new HiddenErrorFunction();
                    break;
                case EErrorFunction.Desired:
                    errorFunction = new OutputErrorFunction();
                    break;
            }

            switch (opt)
            {
                case EOptimizerFunction.SGD:
                    optimizerFunction = new SGDOptimizerFunction();
                    break;
            }

            return new ANNDenseOperations(activationFunction,
                errorFunction,
                optimizerFunction);
        }

        public SupervisedNeuron Supervised(int size,
            int connections,
            float learning,
            float momentum,
            ISupervisedOperations operations)
        {
            return new SupervisedNeuron(size, connections, learning, momentum, operations);
        }
    }
}

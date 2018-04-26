using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    /// <summary>
    /// (Sinmle, Momentum, RMSProp, AdaGrad) LayerBuilder
    /// </summary>
    public class LayerCreatorOptmizer
    {
        private ANNOperationsEnum operation;
        private ActivationFunctionEnum activation;
        private int size;
        private int connections;
        private float lr;
        private float mo;

        public LayerCreatorOptmizer(int size, int connections, float lr, float mo, ANNOperationsEnum operation, ActivationFunctionEnum activation)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
            this.activation = activation;
            this.operation = operation;
        }

        public LayerBuilder Simple_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.SGD);
        }

        public LayerBuilder Momentum_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.Momentum);
        }

        public LayerBuilder RMSProp_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.RmsProp);
        }

        public LayerBuilder Adadelta_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.Adadelta);
        }

        public LayerBuilder AdaGrad_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.Adagrad);
        }

        public LayerBuilder Adam_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.Adam);
        }

        public LayerBuilder Nadam_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.Nadam);
        }

        public LayerBuilder WithOpetimizator_f(EnumOptimizerFunction opt)
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, opt);
        }
    }
}
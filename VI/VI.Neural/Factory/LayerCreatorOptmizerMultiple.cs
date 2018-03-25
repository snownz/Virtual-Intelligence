using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerCreatorOptmizerMultiple
    {
        private int size;
        private int[] connections;
        private float lr;
        private float mo;
        private ANNOperationsEnum operation;
        private ActivationFunctionEnum activation;

        public LayerCreatorOptmizerMultiple(int size, int[] connections, float lr, float mo, ANNOperationsEnum activator, ActivationFunctionEnum tANH)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
            this.operation = activator;
            this.activation = tANH;
        }

        public LayerBuilderMultiple Simple_f()
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.SGD);
        }

        public LayerBuilderMultiple Momentum_f()
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.Momentum);
        }

        public LayerBuilderMultiple RMSProp_f()
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.RmsProp);
        }

        public LayerBuilderMultiple AdaGrad_f()
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, EnumOptimizerFunction.Adagrad);
        }

        public LayerBuilderMultiple WithOpetimizator_f(EnumOptimizerFunction opt)
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, opt);
        }
    }
}

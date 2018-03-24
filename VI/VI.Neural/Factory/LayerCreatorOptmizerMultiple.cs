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
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, OptimizerFunctionEnum.Simple);
        }

        public LayerBuilderMultiple Momentum_f()
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, OptimizerFunctionEnum.Momentum);
        }

        public LayerBuilderMultiple RMSProp_f()
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, OptimizerFunctionEnum.RmsProp);
        }

        public LayerBuilderMultiple AdaGrad_f()
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, OptimizerFunctionEnum.Adagrad);
        }

        public LayerBuilderMultiple WithOpetimizator_f(OptimizerFunctionEnum opt)
        {
            return new LayerBuilderMultiple(size, connections, lr, mo, operation, activation, opt);
        }
    }
}

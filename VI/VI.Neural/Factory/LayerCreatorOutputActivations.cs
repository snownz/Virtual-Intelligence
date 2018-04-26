using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;

namespace VI.Neural.Factory
{
    /// <summary>
    /// (ArcTANH, Binary, Sigmoid, Sinusoid, TANH)  LayerCreatorOptmizer
    /// </summary>
    public class LayerCreatorOutputActivations
    {
        private int size;
        private int connections;
        private float lr;
        private float mo;
        private ANNOperationsEnum operation;

        public LayerCreatorOutputActivations(int size, int connections, float lr, float mo, ANNOperationsEnum activator)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
            this.operation = activator;
        }

        public LayerCreatorOptmizer ArcTANH_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, operation, ActivationFunctionEnum.ArcTANH);
        }

        public LayerCreatorOptmizer Binary_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, operation, ActivationFunctionEnum.Binary);
        }

        public LayerCreatorOptmizer Sigmoid_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, operation, ActivationFunctionEnum.Sigmoid);
        }

        public LayerCreatorOptmizer Sinusoid_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, operation, ActivationFunctionEnum.Sinusoid);
        }

        public LayerCreatorOptmizer TANH_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, operation, ActivationFunctionEnum.TANH);
        }
    }
}
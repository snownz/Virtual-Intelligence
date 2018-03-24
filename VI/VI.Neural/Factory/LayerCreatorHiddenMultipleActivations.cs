using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;

namespace VI.Neural.Factory
{
    public class LayerCreatorHiddenMultipleActivations
    {
        private int size;
        private int[] connections;
        private float lr;
        private float mo;
        private ANNOperationsEnum activator;

        public LayerCreatorHiddenMultipleActivations(int size, int[] connections, float lr, float mo, ANNOperationsEnum activator)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
            this.activator = activator;
        }

        public LayerCreatorOptmizerMultiple ArcTANH_f()
        {
            return new LayerCreatorOptmizerMultiple(size, connections, lr, mo, activator, ActivationFunctionEnum.ArcTANH);
        }

        public LayerCreatorOptmizerMultiple Binary_f()
        {
            return new LayerCreatorOptmizerMultiple(size, connections, lr, mo, activator, ActivationFunctionEnum.Binary);
        }

        public LayerCreatorOptmizerMultiple LeakRelu_f()
        {
            return new LayerCreatorOptmizerMultiple(size, connections, lr, mo, activator, ActivationFunctionEnum.LeakRelu);
        }

        public LayerCreatorOptmizerMultiple Relu_f()
        {
            return new LayerCreatorOptmizerMultiple(size, connections, lr, mo, activator, ActivationFunctionEnum.Relu);
        }

        public LayerCreatorOptmizerMultiple Sigmoid_f()
        {
            return new LayerCreatorOptmizerMultiple(size, connections, lr, mo, activator, ActivationFunctionEnum.Sigmoid);
        }

        public LayerCreatorOptmizerMultiple Sinusoid_f()
        {
            return new LayerCreatorOptmizerMultiple(size, connections, lr, mo, activator, ActivationFunctionEnum.Sinusoid);
        }

        public LayerCreatorOptmizerMultiple TANH_f()
        {
            return new LayerCreatorOptmizerMultiple(size, connections, lr, mo, activator, ActivationFunctionEnum.TANH);
        }
    }
}

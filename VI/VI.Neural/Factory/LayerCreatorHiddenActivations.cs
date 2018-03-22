using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;

namespace VI.Neural.Factory
{
    /// <summary>
    /// (ArcTANH, Binary, LeakRelu, Relu, Sigmoid, Sinusoid, TANH) LayerCreatorOptmizer  
    /// </summary>
    public class LayerCreatorHiddenActivations
    {
        private ANNOperationsEnum activator;
        private int size;
        private int connections;
        private float lr;
        private float mo;

        public LayerCreatorHiddenActivations(int size, int connections, float lr, float mo, ANNOperationsEnum activator)
        {
            this.activator = activator;
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
        }

        public LayerCreatorOptmizer ArcTANH_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, activator, ActivationFunctionEnum.ArcTANH);
        }

        public LayerCreatorOptmizer Binary_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, activator, ActivationFunctionEnum.Binary);
        }

        public LayerCreatorOptmizer LeakRelu_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, activator, ActivationFunctionEnum.LeakRelu);
        }

        public LayerCreatorOptmizer Relu_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, activator, ActivationFunctionEnum.Relu);
        }

        public LayerCreatorOptmizer Sigmoid_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, activator, ActivationFunctionEnum.Sigmoid);
        }

        public LayerCreatorOptmizer Sinusoid_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, activator, ActivationFunctionEnum.Sinusoid);
        }

        public LayerCreatorOptmizer TANH_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, activator, ActivationFunctionEnum.TANH);
        }
    }
}

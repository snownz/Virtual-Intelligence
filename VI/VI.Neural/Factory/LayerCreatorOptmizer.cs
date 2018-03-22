using System;
using System.Collections.Generic;
using System.Text;
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
            return new LayerBuilder(size, connections, lr, mo, operation, activation, OptimizerFunctionEnum.Simple);
        }

        public LayerBuilder Momentum_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, OptimizerFunctionEnum.Momentum);
        }

        public LayerBuilder RMSProp_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, OptimizerFunctionEnum.RmsProp);
        }

        public LayerBuilder AdaGrad_f()
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, OptimizerFunctionEnum.Adagrad);
        }

        public LayerBuilder WithOpetimizator_f(OptimizerFunctionEnum opt)
        {
            return new LayerBuilder(size, connections, lr, mo, operation, activation, opt);
        }        
    }
}

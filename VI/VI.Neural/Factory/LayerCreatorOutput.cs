using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;

namespace VI.Neural.Factory
{
    /// <summary>
    ///  (Activation) LayerCreatorOutputActivations
    ///  (SoftMax)    LayerCreatorOptmizer
    /// </summary>
    public class LayerCreatorOutput
    {
        private int size;
        private int connections;
        private float lr;
        private float mo;

        public LayerCreatorOutput(int size, int connections, float lr, float mo)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
        }

        public LayerCreatorOutputActivations Activation_f()
        {
            return new LayerCreatorOutputActivations(size, connections, lr, mo, ANNOperationsEnum.Activator);
        }

        public LayerCreatorOptmizer SoftMax_f()
        {
            return new LayerCreatorOptmizer(size, connections, lr, mo, ANNOperationsEnum.SoftMax, ActivationFunctionEnum.Nothing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.ANNOperations;

namespace VI.Neural.Factory
{
    /// <summary>
    /// (Activation, Recurrent) LayerCreatorHiddenActivations
    /// </summary>
    public class LayerCreatorHidden
    {
        private int size;
        private int connections;
        private float lr;
        private float mo;

        public LayerCreatorHidden(int size, int connections, float lr, float mo)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
        }

        public LayerCreatorHiddenActivations Activation_f()
        {
            return new LayerCreatorHiddenActivations(size, connections, lr, mo, ANNOperationsEnum.Activator);
        }

        public LayerCreatorHiddenMultipleActivations MultipleActivator_f(int[] con)
        {
            return new LayerCreatorHiddenMultipleActivations(size, con, lr, mo, ANNOperationsEnum.Activator);
        }
    }
}

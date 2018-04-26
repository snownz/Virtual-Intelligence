using System;

namespace VI.Neural.Factory
{
    /// <summary>
    /// LayerCreator
    ///     (Supervised)   LayerCreatorSupervised
    ///                    (Hidden) LayerCreatorHidden
    ///                             (Activation, Recurrent) LayerCreatorHiddenActivations
    ///                                                     (ArcTANH, Binary, LeakRelu, Relu, Sigmoid, Sinusoid, TANH) LayerCreatorOptmizer
    ///                                                                                                                (Sinple, Momentum, RMSProp, AdaGrad) LayerBuilder
    ///                    (Output) LayerCreatorOutput
    ///                             (Activation) LayerCreatorOutputActivations
    ///                                          (ArcTANH, Binary, Sigmoid, Sinusoid, TANH)  LayerCreatorOptmizer
    ///                                                                                      (Sinple, Momentum, RMSProp, AdaGrad) LayerBuilder
    ///                             (SoftMax)    LayerCreatorOptmizer
    ///                                          (Sinple, Momentum, RMSProp, AdaGrad) LayerBuilder
    ///     (Unsupervised) LayerCreatorUnsupervised
    /// </summary>
    public class LayerCreator
    {
        private int size, connections;
        private float lr, mo;

        public LayerCreator(int size, int connections)
        {
            this.size = size;
            this.connections = connections;
        }

        /// <summary>
        /// Define Learning Rate
        /// </summary>
        /// <param name="lr"></param>
        /// <returns></returns>
        public LayerCreator WithLearningRate(float lr)
        {
            this.lr = lr;
            return this;
        }

        /// <summary>
        /// Define Momentum
        /// </summary>
        /// <param name="lr"></param>
        /// <returns></returns>
        public LayerCreator WithMomentum(float mo)
        {
            this.mo = mo;
            return this;
        }

        /// <summary>
        /// Choose Supervised Layer
        /// </summary>
        /// <returns></returns>
        public LayerCreatorSupervised Supervised_f()
        {
            return new LayerCreatorSupervised(size, connections, lr, mo);
        }

        /// <summary>
        /// Choose Unsupervised Layer
        /// </summary>
        /// <returns></returns>
        public LayerCreatorUnsupervised Unsupervised_f()
        {
            throw new NotImplementedException();
        }
    }
}
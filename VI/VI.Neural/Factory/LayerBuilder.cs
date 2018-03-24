using System;
using System.Collections.Generic;
using System.Text;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Node;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public class LayerBuilder
    {
        private int size, connections;
        private float lr,  mo, std;
        private ANNOperationsEnum operation;
        private ActivationFunctionEnum activation;
        private OptimizerFunctionEnum optmizator;
        private NodeEnum nodeType;

        public LayerBuilder(int size, int connections, float lr, float mo, ANNOperationsEnum operation, ActivationFunctionEnum activation, OptimizerFunctionEnum optmizator)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
            this.operation = operation;
            this.activation = activation;
            this.optmizator = optmizator;
        }

        public LayerBuilder FullSynapse(float std)
        {
            this.std = std;
            return this;
        }

        public INeuron Build()
        {
            switch (nodeType)
            {
                case NodeEnum.Supervised:
                    return BuildSupervised();
                case NodeEnum.Unsupervised:
                    return BuildUnsupervised();
                default:
                    throw new InvalidOperationException();
            }
        }

        private INeuron BuildSupervised()
        {
            ISupervisedOperations opr = null;
            IActivationFunction act = null;
            IOptimizerFunction opt = null;

            switch (activation)
            {
                case ActivationFunctionEnum.ArcTANH:
                    act = new ArcTANHFunction();
                    break;
                case ActivationFunctionEnum.TANH:
                    act = new TANHFunction();
                    break;
                case ActivationFunctionEnum.Binary:
                    act = new BinaryStepFunction();
                    break;
                case ActivationFunctionEnum.LeakRelu:
                    act = new LeakReluFunction();
                    break;
                case ActivationFunctionEnum.Relu:
                    act = new ReluFunction();
                    break;
                case ActivationFunctionEnum.Sigmoid:
                    act = new SigmoidFunction();
                    break;
                case ActivationFunctionEnum.Sinusoid:
                    act = new SinusoidFunction();
                    break;
                case ActivationFunctionEnum.Nothing:
                    act = null;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            switch (optmizator)
            {
                case OptimizerFunctionEnum.Adagrad:
                    opt = new AdagradOptimizerFunction();
                    break;
                case OptimizerFunctionEnum.RmsProp:
                    opt = new RMSOptimizerFunction();
                    break;
                case OptimizerFunctionEnum.Simple:
                    opt = new SimpleOptimizerFunction();
                    break;
                case OptimizerFunctionEnum.Momentum:
                    opt = new OptmizerFunctionMomentum();
                    break;
                default:
                    throw new InvalidOperationException();
            }

            switch (operation)
            {
                case ANNOperationsEnum.Activator:
                    opr = new ANNActivatorOperations();
                    break;
                case ANNOperationsEnum.SoftMax:
                    opr = new ANNSoftmaxOperations();
                    break;
                default:
                    throw new InvalidOperationException();
            }

            opr.SetActivation(act);
            opr.SetOptimizer(opt);

            var neuron = new SupervisedNeuron(size, connections, lr, mo, opr);
            neuron.FullSynapsis(size, connections, std);

            return neuron;
        }

        private INeuron BuildUnsupervised()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using VI.Neural.ActivationFunction;
using VI.Neural.ANNOperations;
using VI.Neural.Node;
using VI.Neural.OptimizerFunction;
using VI.Neural.OptimizerFunction.MultipleLayer;

namespace VI.Neural.Factory
{
    public class LayerBuilderMultiple
    {
        private int size;
        private int[] connections;
        private float lr, mo, std;
        private ANNOperationsEnum operation;
        private ActivationFunctionEnum activation;
        private EnumOptimizerFunction optmizator;
        private NodeEnum nodeType;

        public LayerBuilderMultiple(int size, int[] connections, float lr, float mo, ANNOperationsEnum operation, ActivationFunctionEnum activation, EnumOptimizerFunction optmizator)
        {
            this.size = size;
            this.connections = connections;
            this.lr = lr;
            this.mo = mo;
            this.operation = operation;
            this.activation = activation;
            this.optmizator = optmizator;
        }

        public LayerBuilderMultiple FullSynapse(float std)
        {
            this.std = std;
            return this;
        }

        public IMultipleNeuron Build()
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

        private IMultipleNeuron BuildSupervised()
        {
            ISupervisedMultipleOperations opr = null;
            IActivationFunction act = null;
            IOptimizerMultipleLayerFunction opt = null;

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
                case EnumOptimizerFunction.Adagrad:
                    opt = new AdagradOptimizerFunction_M();
                    break;

                case EnumOptimizerFunction.Adadelta:
                    opt = new AdadeltaOptimizerFunction_M();
                    break;

                case EnumOptimizerFunction.Adam:
                    opt = new AdamOptimizerFunction_M();
                    break;

                case EnumOptimizerFunction.Nadam:
                    opt = new NadamOptimizerFunction_M();
                    break;

                case EnumOptimizerFunction.RmsProp:
                    opt = new RMSOptimizerFunction_M();
                    break;

                case EnumOptimizerFunction.SGD:
                    opt = new SGDOptimizerFunction_M();
                    break;

                case EnumOptimizerFunction.Momentum:
                    opt = new MomentumOptmizerFunction_M();
                    break;

                default:
                    throw new InvalidOperationException();
            }

            switch (operation)
            {
                case ANNOperationsEnum.Activator:
                    opr = new ANNMultipleActivatorOperations();
                    break;

                case ANNOperationsEnum.SoftMax:
                    throw new InvalidOperationException();
                    break;

                default:
                    throw new InvalidOperationException();
            }

            opr.SetActivation(act);
            opr.SetOptimizer(opt);

            var neuron = new SupervisedMultipleNeuron(size, connections, lr, mo, opr);
            neuron.FullSynapsis(std);

            return neuron;
        }

        private IMultipleNeuron BuildUnsupervised()
        {
            throw new NotImplementedException();
        }
    }
}
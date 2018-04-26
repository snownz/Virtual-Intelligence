using VI.Neural.Node;
using VI.Neural.OptimizerFunction;

namespace VI.Neural.Factory
{
    public static class BuildedModels
    {
        public static INeuron DenseSigmoid(int connections, int size, float learningRate, float std, EnumOptimizerFunction opt)
        {
            return new LayerCreator(size, connections)
                       .WithLearningRate(learningRate)
                       .Supervised_f()
                       .Hidden_f()
                       .Activation_f()
                       .Sigmoid_f()
                       .WithOpetimizator_f(opt)
                       .FullSynapse(std)
                       .Build();
        }

        public static INeuron DenseTanh(int connections, int size, float learningRate, float std, EnumOptimizerFunction opt)
        {
            return new LayerCreator(size, connections)
                       .WithLearningRate(learningRate)
                       .Supervised_f()
                       .Hidden_f()
                       .Activation_f()
                       .TANH_f()
                       .WithOpetimizator_f(opt)
                       .FullSynapse(std)
                       .Build();
        }

        public static IMultipleNeuron RecurrentTanh(int connections, int size, float learningRate, float std, EnumOptimizerFunction opt)
        {
            return new LayerCreator(size, 0)
                       .WithLearningRate(learningRate)
                       .Supervised_f()
                       .Hidden_f()
                       .MultipleActivator_f(new[] { connections, size })
                       .TANH_f()
                       .WithOpetimizator_f(opt)
                       .FullSynapse(std)
                       .Build();
        }

        public static INeuron DenseLeakRelu(int connections, int size, float learningRate, float std, EnumOptimizerFunction opt)
        {
            return new LayerCreator(size, connections)
                       .WithLearningRate(learningRate)
                       .Supervised_f()
                       .Hidden_f()
                       .Activation_f()
                       .LeakRelu_f()
                       .WithOpetimizator_f(opt)
                       .FullSynapse(std)
                       .Build();
        }

        public static INeuron DenseSoftMax(int connections, int size, float learningRate, float std, EnumOptimizerFunction opt)
        {
            return new LayerCreator(size, connections)
                       .WithLearningRate(learningRate)
                       .Supervised_f()
                       .Output_f()
                       .SoftMax_f()
                       .WithOpetimizator_f(opt)
                       .FullSynapse(std)
                       .Build();
        }
    }
}
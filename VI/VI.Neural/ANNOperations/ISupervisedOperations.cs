using VI.Neural.ActivationFunction;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public interface ISupervisedOperations
    {
        void FeedForward(FloatArray feed);
        FloatArray BackWard(FloatArray backprop);
        FloatArray ComputeErrorNBackWard(FloatArray values);
        void ComputeGradient(FloatArray inputs);
        void UpdateParams(FloatArray2D dW, FloatArray dB);
        void SetLayer(ILayer layer);
        void SetActivation(IActivationFunction act);
        void SetOptimizer(IOptimizerFunction opt);
    }
}
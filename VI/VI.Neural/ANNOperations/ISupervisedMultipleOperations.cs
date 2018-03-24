using VI.Neural.ActivationFunction;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
    public interface ISupervisedMultipleOperations
    {
        void Activate();
        Array<FloatArray> BackWard(FloatArray backprop);
        Array<FloatArray> ComputeErrorNBackWard(FloatArray values);
        void ComputeGradient(Array<FloatArray> inputs);
        void SetActivation(IActivationFunction act);
        void SetLayer(IMultipleLayer layer);
        void SetOptimizer(IOptimizerMultipleLayerFunction opt);
        void Summarization(Array<FloatArray> feed);
        void UpdateParams(Array<FloatArray2D> dW, FloatArray dB);
    }
}
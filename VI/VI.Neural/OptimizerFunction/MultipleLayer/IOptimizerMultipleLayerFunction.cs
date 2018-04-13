using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction.MultipleLayer
{
    public interface IOptimizerMultipleLayerFunction
    {
        void CalculateParams(IMultipleLayer target);

        FloatArray Error(FloatArray targetOutputVector, FloatArray values);

        void UpdateWeight(IMultipleLayer target, Array<FloatArray2D> dW);

        void UpdateBias(IMultipleLayer target, FloatArray dB);
    }
}
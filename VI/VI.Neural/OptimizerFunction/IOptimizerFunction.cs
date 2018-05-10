using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public interface IOptimizerFunction
    {
        void CalculateParams(ILayer target);
        
        void UpdateWeight(ILayer target, FloatArray2D dW);

        void UpdateBias(ILayer target, FloatArray dB);
    }
}
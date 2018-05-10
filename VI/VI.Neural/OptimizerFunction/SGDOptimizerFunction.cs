using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class SGDOptimizerFunction : IOptimizerFunction
    {
        public void CalculateParams(ILayer target)
        {
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {
            target.KnowlodgeMatrix -= (target.LearningRate * dW);
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            target.BiasVector -= (target.LearningRate * dB);
        }
    }
}
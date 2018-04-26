using VI.Neural.Layer;
using VI.Neural.OptimizerFunction.MultipleLayer;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class MomentumOptmizerFunction_M : IOptimizerMultipleLayerFunction
    {
        public void CalculateParams(IMultipleLayer target)
        {
            target.CachedMomentum = target.LearningRate * target.Momentum;
            target.CachedLearningRate = target.LearningRate * (1 - target.Momentum);
        }

        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }

        public void UpdateWeight(IMultipleLayer target, Array<FloatArray2D> dW)
        {
            for(int i = 0; i < target.ConectionsSize.Length; i++)
            {
                var update = dW[i] * target.CachedLearningRate;
                var momentum = target.KnowlodgeMatrix[i] * target.CachedMomentum;
                target.KnowlodgeMatrix[i] += (update + momentum);
            }
        }

        public void UpdateBias(IMultipleLayer target, FloatArray dB)
        {
            var update = dB * target.CachedLearningRate;
            var momentum = target.BiasVector * target.CachedMomentum;
            target.BiasVector += (update + momentum);
        }
    }
}
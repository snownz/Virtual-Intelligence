using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class SGDOptimizerFunctionWithMask : IOptimizerFunction
    {
        public void CalculateParams(ILayer target)
        {
            target.CachedMomentum = target.LearningRate * target.Momentum;
            target.CachedLearningRate = target.LearningRate * (1 - target.Momentum);
        }

        public void UpdateWeight(ILayer target)
        {
            var update = target.GradientMatrix * target.CachedLearningRate;
            target.KnowlodgeMatrix += update;
            target.KnowlodgeMatrix.ApplyMask(target.ConnectionMask);
        }

        public void UpdateBias(ILayer target)
        {
            var update = target.ErrorVector * target.CachedLearningRate;
            target.BiasVector += update;
        }
    }
}
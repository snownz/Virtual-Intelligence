using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
	public class MomentumOptmizerFunction : IOptimizerFunction
    {
		public void CalculateParams(ILayer target)
		{
			target.CachedMomentum     = target.LearningRate * target.Momentum;
			target.CachedLearningRate = target.LearningRate * (1 - target.Momentum);
		}
        
        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {
            var update = dW * target.CachedLearningRate;
            var momentum = target.KnowlodgeMatrix * target.CachedMomentum;
            target.KnowlodgeMatrix += (update + momentum);
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            var update = dB * target.CachedLearningRate;
            var momentum = target.BiasVector * target.CachedMomentum;
            target.BiasVector += (update + momentum);
        }
    }
}
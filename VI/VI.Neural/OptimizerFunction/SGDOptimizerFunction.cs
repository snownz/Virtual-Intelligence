using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
	public class SGDOptimizerFunction : IOptimizerFunction
	{
		public void CalculateParams(ILayer target)
		{

        }

        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {
            var update = dW * target.LearningRate;
            target.KnowlodgeMatrix += update;
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            var update = dB * target.LearningRate;
            target.BiasVector += update;
        }
    }
}
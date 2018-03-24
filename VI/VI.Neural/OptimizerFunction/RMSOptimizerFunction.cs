using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
	public class RMSOptimizerFunction : IOptimizerFunction
    {
		private FloatArray2D gW;
		private FloatArray bW;

		public void CalculateParams(ILayer target)
		{
			gW = NumMath.Array(target.Size, target.ConectionsSize);
			bW = NumMath.Array(target.Size);
		}
        
        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {
            gW = (9e-1f * gW) + (1e-1f * (dW * dW));
            target.KnowlodgeMatrix -= target.LearningRate / (gW + 1e-8f).Sqrt() * dW;
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            bW = (9e-1f * bW) + (1e-1f * (dB * dB));
            target.BiasVector -= target.LearningRate / (bW + 1e-8f).Sqrt() * dB;
        }
    }
}
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

		public void UpdateWeight(ILayer target)
		{
			gW =
				9e-1f                                        * gW  + 1e-1f         * (target.GradientMatrix * target.GradientMatrix);
			target.KnowlodgeMatrix -= target.LearningRate / (gW + 1e-8f).Sqrt() * target.GradientMatrix;
		}

		public void UpdateBias(ILayer target)
		{
			bW                =  9e-1f               * bW  + 1e-1f         * (target.ErrorVector * target.ErrorVector);
			target.BiasVector -= target.LearningRate / (bW + 1e-8f).Sqrt() * target.ErrorVector;
		}
	}
}
using System;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
	public class AdagradOptimizerFunction : IOptimizerFunction
	{
		private FloatArray2D mW;
		private FloatArray mB;

		public void CalculateParams(ILayer target)
		{
			mW = NumMath.Array(target.Size, target.ConectionsSize);
			mB = NumMath.Array(target.Size);
		}

		public void UpdateBias(ILayer target)
		{
			mB                += target.ErrorVector   * target.ErrorVector;
			target.BiasVector += -target.LearningRate * target.ErrorVector / (mB + 1e-8f).Sqrt();
		}

		public void UpdateWeight(ILayer target)
		{
			mW                     += target.GradientMatrix * target.GradientMatrix;
			target.KnowlodgeMatrix += -target.LearningRate  * target.GradientMatrix / (mW + 1e-8f).Sqrt();
		}
	}
}
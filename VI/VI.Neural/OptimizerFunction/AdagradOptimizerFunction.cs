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

        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }
        
        public void UpdateBias(ILayer target, FloatArray dB)
        {
            mB += dB * dB;
            target.BiasVector += -target.LearningRate * dB / (mB + 1e-8f).Sqrt();
        }
        
        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {
            mW += dW * dW;
            target.KnowlodgeMatrix += -target.LearningRate * dW / (mW + 1e-8f).Sqrt();
        }
    }
}
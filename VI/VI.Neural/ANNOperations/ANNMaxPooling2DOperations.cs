using System;
using VI.Neural.ActivationFunction;
using VI.Neural.Error;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
	public class ANNMaxPooling2DOperations : ISupervisedOperations
	{
		public void FeedForward(FloatArray feed)
		{
			throw new NotImplementedException();
		}

		public void BackWard(FloatArray values)
		{
			throw new NotImplementedException();
		}

		public void ErrorGradient(FloatArray inputs)
		{
			throw new NotImplementedException();
		}

		public void UpdateParams()
		{
			throw new NotImplementedException();
		}

		public void SetLayer(ILayer layer)
		{
			throw new NotImplementedException();
		}

		public void SetActivation(IActivationFunction act)
		{
			throw new NotImplementedException();
		}

		public void SetError(IErrorFunction err)
		{
			throw new NotImplementedException();
		}

		public void SetOptimizer(IOptimizerFunction opt)
		{
			throw new NotImplementedException();
		}

		public void ComputeGradient(FloatArray inputs)
		{
			throw new NotImplementedException();
		}
	}
}
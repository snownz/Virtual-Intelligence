using VI.Neural.ActivationFunction;
using VI.Neural.Error;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;

namespace VI.Neural.ANNOperations
{
	public interface ISupervisedOperations
	{
		void FeedForward(FloatArray feed);
		void BackWard(FloatArray values);
		void ErrorGradient(FloatArray inputs);
		void ComputeGradient(FloatArray inputs);
		void UpdateParams();
		void SetLayer(ILayer                   layer);
		void SetActivation(IActivationFunction act);
		void SetError(IErrorFunction           err);
		void SetOptimizer(IOptimizerFunction   opt);
	}
}
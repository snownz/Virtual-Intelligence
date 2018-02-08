using VI.Neural.Layer;

namespace VI.Neural.OptimizerFunction
{
	public class SGDOptimizerFunction : IOptimizerFunction
	{
		public void CalculateParams(ILayer target)
		{
			
		}

		public void UpdateWeight(ILayer target)
		{
			var update = target.GradientMatrix * target.LearningRate;
			target.KnowlodgeMatrix += update; 
		}

		public void UpdateBias(ILayer target)
		{
			var update = target.ErrorVector * target.LearningRate;
			target.BiasVector += update;
		}
	}
}
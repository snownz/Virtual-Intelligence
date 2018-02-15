using VI.Neural.Layer;

namespace VI.Neural.OptimizerFunction
{
	public interface IOptimizerFunction
	{
		void CalculateParams(ILayer target);
		void UpdateWeight(ILayer target);
		void UpdateBias(ILayer target);
	}
}
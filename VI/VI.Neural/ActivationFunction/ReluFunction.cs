using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class ReluFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray sum)
		{
			return NumMath.Max(.0f * sum, sum);
		}

		public FloatArray Derivate(FloatArray sum, FloatArray act)
		{
			return sum >= 0;
		}
	}
}
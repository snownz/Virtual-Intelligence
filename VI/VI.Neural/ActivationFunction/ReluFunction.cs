using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class ReluFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray v)
		{
			return NumMath.Max(.0f * v, v);
		}

		public FloatArray Derivate(FloatArray v)
		{
			return v >= 0;
		}
	}
}
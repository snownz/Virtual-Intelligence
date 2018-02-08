using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class LeakReluFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray v)
		{
			return NumMath.Max(.001f * v, v);
		}

		public FloatArray Derivate(FloatArray v)
		{
			return (v >= 0) + .001f;
		}
	}
}
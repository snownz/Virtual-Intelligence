using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class BinaryStepFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray v)
		{
			return v >= 0;
		}

		public FloatArray Derivate(FloatArray v)
		{
			return v >= 0;
		}
	}
}
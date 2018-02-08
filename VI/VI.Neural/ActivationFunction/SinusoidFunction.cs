using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class SinusoidFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray v)
		{
			return v.Sin();
		}

		public FloatArray Derivate(FloatArray v)
		{
			return v.Cos();
		}
	}
}
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class ArcTANHFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray v)
		{
			return v.Tanh().Pow(-1);
		}

		public FloatArray Derivate(FloatArray v)
		{
			return 1 / (v * v + 1);
		}
	}
}
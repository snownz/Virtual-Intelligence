using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class ArcTANHFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray sum)
		{
			return sum.Tanh().Pow(-1);
		}

		public FloatArray Derivate(FloatArray sum, FloatArray act)
		{
			return 1 / (sum * sum + 1);
		}
	}
}
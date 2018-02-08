using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class SigmoidFunction : IActivationFunction
	{
		private const float Alpha = 2f;

		public FloatArray Activate(FloatArray v)
		{
			return 1f / (1 + (-Alpha * v).Exp());
		}

		public FloatArray Derivate(FloatArray v)
		{
			var y = Activate(v);
			return Alpha * y * (1 - y);
		}
	}
}
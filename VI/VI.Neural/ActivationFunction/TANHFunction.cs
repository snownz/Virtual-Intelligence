using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class TANHFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray v)
		{
			return v.Tanh();
		}

		public FloatArray Derivate(FloatArray v)
		{
			var y = Activate(v);
			return 1 - y * y;
		}
	}
}
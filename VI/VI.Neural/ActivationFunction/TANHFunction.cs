using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public class TANHFunction : IActivationFunction
	{
		public FloatArray Activate(FloatArray sum)
		{
			return sum.Tanh();
		}

		public FloatArray Derivate(FloatArray sum, FloatArray act)
		{
			return (1 - act * act);
		}
	}
}
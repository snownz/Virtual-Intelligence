using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
	public interface IActivationFunction
	{
		FloatArray Activate(FloatArray v);
		FloatArray Derivate(FloatArray v);
	}
}
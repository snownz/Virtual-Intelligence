using VI.NumSharp.Arrays;

namespace VI.Neural.Error
{
	public interface IErrorFunction
	{
		FloatArray Error(FloatArray targetOutputVector, FloatArray values);
	}
}
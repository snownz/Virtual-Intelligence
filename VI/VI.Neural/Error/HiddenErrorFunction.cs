using VI.NumSharp.Arrays;

namespace VI.Neural.Error
{
	public class HiddenErrorFunction : IErrorFunction
	{
		public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
		{
			return values;
		}
	}
}
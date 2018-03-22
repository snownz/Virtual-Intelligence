using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
	public interface ILossFunction
	{
		float Loss(FloatArray targets, FloatArray prediction);
	}
}
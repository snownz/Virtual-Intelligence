using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
    /// <summary>
    ///     https://rdipietro.github.io/friendly-intro-to-cross-entropy-loss/
    /// </summary>
    public class CrossEntropyLossFunction : ILossFunction
	{
		public float Loss(FloatArray targets, FloatArray prediction)
		{
			return -(targets * prediction.Log()).Sum();
		}
	}
}
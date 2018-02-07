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

		public float Loss(float[] targets, FloatArray prediction)
		{
			using (var t = new FloatArray(targets))
			{
				return Loss(t, prediction);
			}
		}
	}
}
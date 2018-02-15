using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
	public interface IUnsupervisedLearning
	{
		void Learn(float[] inputs);
		void Learn(FloatArray inputs);
	}
}
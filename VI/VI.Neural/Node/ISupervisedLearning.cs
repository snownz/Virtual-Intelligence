using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
	public interface ISupervisedLearning
	{
		FloatArray Learn(float[]              inputs, FloatArray error);
		FloatArray Learn(FloatArray           inputs, float[]    error);
		FloatArray Learn(FloatArray           inputs, FloatArray error);
		FloatArray ComputeGradient(float[]    inputs, FloatArray error);
		FloatArray ComputeGradient(FloatArray inputs, float[]    error);
		FloatArray ComputeGradient(FloatArray inputs, FloatArray error);
		void       UpdateParams();
	}
}
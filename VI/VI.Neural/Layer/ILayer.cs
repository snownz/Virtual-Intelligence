using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
	public interface ILayer
	{
		FloatArray2D KnowlodgeMatrix { get; set; }
		FloatArray2D GradientMatrix  { get; set; }
		ByteArray2D ConnectionMask  { get; set; }
		ByteArray    BiasMask        { get; set; }

		FloatArray BiasVector         { get; set; }
		FloatArray OutputVector       { get; set; }
		FloatArray SumVector          { get; set; }
		FloatArray ErrorVector        { get; set; }
		FloatArray ErrorWeightVector  { get; set; }
		FloatArray DropOutProbability { get; set; }

		int   Size               { get; set; }
		int   ConectionsSize     { get; set; }
		float LearningRate       { get; set; }
		float CachedLearningRate { get; set; }
		float Momentum           { get; set; }
		float CachedMomentum     { get; set; }
	}
}
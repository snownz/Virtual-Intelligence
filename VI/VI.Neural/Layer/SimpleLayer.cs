using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
	public class SimpleLayer
	{
		public FloatArray2D KnowlodgeMatrix { get; set; }
		public FloatArray2D DerivateMatrix { get; set; }

		public FloatArray OutputVector { get; set; }
		public FloatArray SumVector { get; set; }
		public FloatArray GradientVector { get; set; }
		public FloatArray BackPropVector { get; set; }

		public FloatArray BiasVector { get; set; }

		public float LearningRate { get; set; }
		public int Size { get; set; }
		public int ConectionsSize { get; set; }
	}
}
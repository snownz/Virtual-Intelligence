using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
	public class CubicLayer
	{
		
		public FloatArray3D KnowlodgeMatrix { get; set; }
		public FloatArray3D DerivateMatrix { get; set; }
		
		public FloatArray2D OutputVector { get; set; }
		public FloatArray2D SumVector { get; set; }
		public FloatArray2D GradientVector { get; set; }
		public FloatArray2D BackPropVector { get; set; }
		
		public FloatArray2D BiasVector { get; set; }
		
		public float LearningRate { get; set; }
		
		public int Size { get; set; }
		public int ConectionsSize { get; set; }
		public int DepthSize  { get; set; }
	}
}
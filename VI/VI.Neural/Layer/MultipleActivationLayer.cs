using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
	public class MultipleActivationLayer : IMultipleLayer
	{
		public MultipleActivationLayer(int size, int[] conectionsSize)
		{
			Size           = size;
			ConectionsSize = conectionsSize;
		}

		public Array<FloatArray2D> KnowlodgeMatrix { get; set; }
		public Array<FloatArray2D> GradientMatrix { get; set; }
        public Array<FloatArray> SumVector { get; set; }

        public FloatArray Sum { get; set; }

        public FloatArray ErrorVector { get; set; }
        public FloatArray BiasVector { get; set; }
		public FloatArray OutputVector { get; set; }
		
		public int Size { get; set; }
		public int[] ConectionsSize { get; set; }

        public float LearningRate { get; set; }
        public float CachedLearningRate { get; set; }
		public float Momentum { get; set; }
		public float CachedMomentum { get; set; }
    }
}
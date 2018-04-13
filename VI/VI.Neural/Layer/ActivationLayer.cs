using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
    public class ActivationLayer : ILayer
    {
        public ActivationLayer(int size, int conectionsSize)
        {
            Size = size;
            ConectionsSize = conectionsSize;
        }

        public FloatArray2D KnowlodgeMatrix { get; set; }
        public FloatArray2D GradientMatrix { get; set; }
        public ByteArray2D ConnectionMask { get; set; }

        public FloatArray DropOutProbability { get; set; }
        public FloatArray BiasVector { get; set; }
        public FloatArray OutputVector { get; set; }
        public FloatArray SumVector { get; set; }
        public FloatArray ErrorVector { get; set; }
        public FloatArray ErrorWeightVector { get; set; }

        public ByteArray BiasMask { get; set; }

        public float LearningRate { get; set; }

        public int Size { get; set; }

        public int ConectionsSize { get; set; }

        public float CachedLearningRate { get; set; }

        public float Momentum { get; set; }

        public float CachedMomentum { get; set; }
    }
}
using VI.NumSharp.Arrays;

namespace VI.Neural.Layer
{
    public interface ILayer
    {
        FloatArray2D KnowlodgeMatrix { get; set; }
        FloatArray2D GradientMatrix { get; set; }
        FloatArray ErrorVector { get; set; }
        FloatArray SumVector { get; set; }

        FloatArray BiasVector { get; set; }
        FloatArray OutputVector { get; set; }

        int Size { get; set; }
        int ConectionsSize { get; set; }
        float LearningRate { get; set; }
        float CachedLearningRate { get; set; }
        float Momentum { get; set; }
        float CachedMomentum { get; set; }
    }

    public interface IMultipleLayer
    {
        Array<FloatArray2D> KnowlodgeMatrix { get; set; }
        Array<FloatArray2D> GradientMatrix { get; set; }
        Array<FloatArray> SumVector { get; set; }

        FloatArray Sum { get; set; }

        FloatArray ErrorVector { get; set; }
        FloatArray BiasVector { get; set; }
        FloatArray OutputVector { get; set; }

        int Size { get; set; }
        int[] ConectionsSize { get; set; }
        float LearningRate { get; set; }
        float CachedLearningRate { get; set; }
        float Momentum { get; set; }
        float CachedMomentum { get; set; }
    }
}
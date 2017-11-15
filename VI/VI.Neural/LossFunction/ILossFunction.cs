using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
    public interface ILossFunction
    {
        float Loss(Array2D<float> targets, Array2D<float> prediction);
    }
}
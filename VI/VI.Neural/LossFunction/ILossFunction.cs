using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
    public interface ILossFunction
    {
        float Loss(Array<float> targets, Array<float> prediction);
        float Loss(Array<float> targets, float[] prediction);
    }
}
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public interface IActivationFunction
    {
        FloatArray Activate(FloatArray sum);

        FloatArray Derivate(FloatArray sum, FloatArray act);
    }
}
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public interface IActivationFunction
    {
        Array<float> Activate(Array<float> v);
        Array<float> Derivate(Array<float> v);
    }
}
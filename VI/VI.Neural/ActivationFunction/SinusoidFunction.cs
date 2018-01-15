using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class SinusoidFunction : IActivationFunction
    {
        public Array<float> Activate(Array<float> v)
        {
            return v.Sin();
        }

        public Array<float> Derivate(Array<float> v)
        {
            return v.Cos();
        }
    }
}
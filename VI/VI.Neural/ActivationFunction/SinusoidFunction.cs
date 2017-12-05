using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class SinusoidFunction : IActivationFunction
    {
        public Array<float> Activate(Array<float> v)
        {
            return NumMath.Sin(v);
        }

        public Array<float> Derivate(Array<float> v)
        {
            return NumMath.Cos(v);
        }
    }
}
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class ReluFunction : IActivationFunction
    {
        public Array<float> Activate(Array<float> v)
        {
            return NumMath.Max(.0f * v, v);
        }

        public Array<float> Derivate(Array<float> v)
        {
            return (v >= 0);
        }
    }
}
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class LeakReluFunction : IActivationFunction
    {
        public Array<float> Activate(Array<float> v)
        {
            return NumMath.Max(.01f * v, v);
        }

        public Array<float> Derivate(Array<float> v)
        {
            return (v >= 0) + .01f;
        }
    }
}
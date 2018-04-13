using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class LeakReluFunction : IActivationFunction
    {
        public FloatArray Activate(FloatArray sum)
        {
            return NumMath.Max(.001f * sum, sum);
        }

        public FloatArray Derivate(FloatArray sum, FloatArray act)
        {
            return (sum >= .001f) + .001f;
        }
    }
}
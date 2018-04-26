using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class BinaryStepFunction : IActivationFunction
    {
        public FloatArray Activate(FloatArray sum)
        {
            return sum >= 0;
        }

        public FloatArray Derivate(FloatArray sum, FloatArray act)
        {
            return sum >= 0;
        }
    }
}
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class SinusoidFunction : IActivationFunction
    {
        public FloatArray Activate(FloatArray sum)
        {
            return sum.Sin();
        }

        public FloatArray Derivate(FloatArray sum, FloatArray act)
        {
            return act.Cos();
        }
    }
}
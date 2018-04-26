using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class SigmoidFunction : IActivationFunction
    {
        private const float Alpha = 2f;

        public FloatArray Activate(FloatArray sum)
        {
            return 1f / (1 + (-Alpha * sum).Exp());
        }

        public FloatArray Derivate(FloatArray sum, FloatArray act)
        {
            return Alpha * act * (1 - act);
        }
    }
}
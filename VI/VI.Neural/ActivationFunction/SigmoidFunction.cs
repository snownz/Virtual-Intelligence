using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class SigmoidFunction :IActivationFunction
    {
        private const float Alpha = 2f;

        public Array<float> Activate(Array<float> v)
        {
            return (1f / (1 + (-Alpha * v).Exp()));
        }

        public Array<float> Derivate(Array<float> v)
        {
            var y = Activate(v);
            return (Alpha * y * (1 - y));
        }
    }
}
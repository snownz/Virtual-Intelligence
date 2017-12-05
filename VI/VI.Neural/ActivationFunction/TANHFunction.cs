using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class TANHFunction : IActivationFunction
    {
        public Array<float> Activate(Array<float> v)
        {
            return (2 / (1 + NumMath.Exp(-2 * v))) - 1;
        }

        public Array<float> Derivate(Array<float> v)
        {
            var y = Activate(v);
            return 1 - (y * y);
        }
    }
}

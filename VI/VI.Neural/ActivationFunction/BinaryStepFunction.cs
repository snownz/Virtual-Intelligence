using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class BinaryStepFunction : IActivationFunction
    {
        public Array<float> Activate(Array<float> v)
        {
            return (v >= 0);
        }

        public Array<float> Derivate(Array<float> v)
        {
            return (v >= 0);
        }
    }
}

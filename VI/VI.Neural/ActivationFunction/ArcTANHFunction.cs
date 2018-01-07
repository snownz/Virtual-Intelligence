using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class ArcTANHFunction : IActivationFunction
    {
        public Array<float> Activate(Array<float> v)
        {
            return NumMath.Pow(NumMath.Tan(v), -1);
        }

        public Array<float> Derivate(Array<float> v)
        {
            return 1 / ((v * v) + 1); 
        }
    }
}

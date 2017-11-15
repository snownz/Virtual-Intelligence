using System;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class SigmoidFunction :IActivationFunction
    {
        public Array<float> Activate(Array<float> v)
        {
            return (1f / (1f + NumMath.Exp(-2f * v)));
        }

        public Array<float> Derivate(Array<float> v)
        {
            var y = Activate(v);
            return (2 * y * (1 - y));
        }
    }
}
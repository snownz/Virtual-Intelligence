using ILGPU;
using System;

namespace VI.Maths.LogisticFunctions
{
    public class SigmoidFunction : IActivationFunction
    {
        public static void Function(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            v[p] = (float)(1 / (1 + Math.Exp(-2 * x[p])));
        }
        
        public static void Derivative(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            double y = (1 / (1 + Math.Exp(-2 * x[p])));
            v[p] = (float)(2 * y * (1 - y));
        }
    }
}

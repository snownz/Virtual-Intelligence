using ILGPU;
using ILGPU.Runtime;
using System;

namespace VI.Maths.LogisticFunctions
{
    public class SigmoidFunction : IActivationFunction
    {
        public static void Function(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            v[p] = (float)(1 / (1 + Math.Exp(-x[p])));
        }
        
        public static void Derivative(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            double y = (1 / (1 + Math.Exp(-x[p])));
            v[p] = (float)(y * (1 - y));
        }
    }
}

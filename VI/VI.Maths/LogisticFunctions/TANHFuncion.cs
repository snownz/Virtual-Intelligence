using ILGPU;
using System;

namespace VI.Maths.LogisticFunctions
{
    public class TANHFuncion
    {
        public static void Derivative(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            var y = (2 / (1 + Math.Pow(Math.E, -2 * x[p]))) - 1;
            v[p] = (float)(1 - (y * y));
        }

        public static void Function(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            v[p] = (float)((2 / (1 + Math.Pow(Math.E, -2 * x[p]))) - 1);
        }
    }
}

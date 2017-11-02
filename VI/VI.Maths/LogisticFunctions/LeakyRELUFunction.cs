using ILGPU;
using System;

namespace VI.Maths.LogisticFunctions
{
    public class LeakyRELUFunction
    {
        public static void Derivative(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            var y = Math.Max(0.01 * x[p], x[p]);
            v[p] = y >= 0 ? 1 : 0.01f;
        }

        public static void Function(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            v[p] = (float)Math.Max(0.01 * x[p], x[p]);
        }
    }
}

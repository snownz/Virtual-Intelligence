using ILGPU;
using System;


namespace VI.Maths.LogisticFunctions
{
    public static class RELUFunction
    {
        public static void Derivative(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            double y = Math.Max(0, x[p]);
            v[p] = (float)Convert.ToDouble(y > 0);
        }

        public static void Function(Index t, ArrayView<float> v, ArrayView<float> x)
        {
            var p = t.X;
            v[p] = Math.Max(0, x[p]);
        }
    }
}

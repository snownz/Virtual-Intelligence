using ILGPU;

namespace VI.Maths.ANNArray
{
    public static class ANNArrayOperations
    {
        public static void _zeros_(Index t, ArrayView<float> newV)
        {
            int x = t.X;
            newV[x] = 0;
        }
        public static void _multi_1D_1D_(Index t, ArrayView<float> newV, ArrayView<float> v1, ArrayView<float> v2)
        {
            int x = t.X;
            newV[x] = v1[x] * v2[x];
        }
        public static void _sub_1D_(Index t, ArrayView<float> newV, ArrayView<float> v1, ArrayView<float> v2)
        {
            int x = t.X;
            newV[x] = (v1[x] - v2[x]);
        }
        public static void _sum_1D_(Index t, ArrayView<float> newV, ArrayView<float> v1, ArrayView<float> v2)
        {
            int x = t.X;
            newV[x] = v1[x] + v2[x];
        }
        public static void _update_weights_(Index2 t, ArrayView2D<float> newM, ArrayView2D<float> wight, 
            ArrayView<float> error, ArrayView<float> inputs, float lr, float momentum)
        {
            int x = t.X;
            int y = t.Y;
            newM[x, y] = wight[x, y] + (error[x] * inputs[y] * lr) + (momentum * wight[x, y]);
        }
        public static void _sum_weights_(Index t, ArrayView<float> newV, ArrayView<float> inputs, ArrayView2D<float> wight, int h)
        {
            int x = t.X;
            for (int y = 0; y < h; y++)
            {
                newV[x] += inputs[y] * wight[x, y];
            }
        }
        public static void _process_error_to_back_propagate(Index t, ArrayView<float> newV, ArrayView<float> error, ArrayView2D<float> wight, int h)
        {
            int x = t.X;
            for (int y = 0; y < h; y++)
            {
                newV[y] += wight[x, y] * error[x];
            }
        }
        public static void _update_bias_(Index t, ArrayView<float> newV, ArrayView<float> bias, ArrayView<float> error, float lr, float momentum)
        {
            int x = t.X;
            newV[x] = bias[x] + ((bias[x] * momentum) + (error[x] * lr));
        }
    }
}

using ILGPU;

namespace VI.Maths.Array
{
    public class ArrayOperations
    {
        public static void _M_mask(Index2 size, ArrayView2D<float> m, ArrayView2D<byte> mask)
        {
            var x = size.X;
            var y = size.Y;
            m[x, y] = m[x, y] * mask[x, y];
        }

        public static void _V_mask(Index size, ArrayView<float> m, ArrayView<byte> mask)
        {
            var x = size.X;
            m[x] = m[x] * mask[x];
        }

        public static void _M_sum_lines(Index2 size, int r, ArrayView2D<float> m, int boxSize)
        {
            var x = size.X;
            var y = size.Y;
            m[x, y] = m[x, y] + m[x, y + r + boxSize];
        }

        public static void _M_sum_columns(Index2 size, int r, ArrayView2D<float> m, int boxSize)
        {
            var x = size.X;
            var y = size.Y;
            m[x, y] = m[x, y] + m[x + r + boxSize, y];
        }

        public static void _M_2_lines_V(Index size, ArrayView<float> v, ArrayView2D<float> m)
        {
            var x = size.X;
            v[x] = m[x, 0] + m[x, 1];
        }

        public static void _M_2_columns_V(Index size, ArrayView<float> v, ArrayView2D<float> m)
        {
            var y = size.X;
            v[y] = m[0, y] + m[1, y];
        }

        public static void _V_X_V(Index size, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            var x = size.X;
            output[x] = v0[x] * v1[x];
        }

        public static void _V_X_V_M(Index2 size, ArrayView2D<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            var x = size.X;
            var y = size.Y;
            output[x, y] = v0[x] * v1[y];
        }

        public static void _C_X_M(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m, float c)
        {
            var x = size.X;
            var y = size.Y;
            output[x, y] = m[x, y] * c;
        }

        public static void _M_sum_M(Index2 size, ArrayView2D<float> m0, ArrayView2D<float> m1)
        {
            var x = size.X;
            var y = size.Y;
            m0[x, y] = m0[x, y] + m1[x, y];
        }

        public static void _M_sum_C(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m0, float c)
        {
            var x = size.X;
            var y = size.Y;
            output[x, y] = m0[x, y] + c;
        }

        public static void _M_X_M(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
        {
            var x = size.X;
            var y = size.Y;
            output[x, y] = m0[x, y] * m1[x, y];
        }

        public static void _M_div_M(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
        {
            var x = size.X;
            var y = size.Y;
            output[x, y] = m0[x, y] / m1[x, y];
        }

        public static void _V_sum_V(Index size, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            var x = size.X;
            output[x] = v0[x] + v1[x];
        }

        public static void _V_sub_V(Index size, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            var x = size.X;
            output[x] = v0[x] - v1[x];
        }

        public static void _V_div_V(Index size, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            var x = size.X;
            output[x] = v0[x] / v1[x];
        }

        public static void _V_X_M_column_M(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView<float> v)
        {
            var x = size.X;
            var y = size.Y;
            output[x, y] = v[y] * m[x, y];
        }

        public static void _V_X_M_line_M(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView<float> v)
        {
            var x = size.X;
            var y = size.Y;
            output[x, y] = v[x] * m[x, y];
        }

        public static void _C_X_V(Index size, float c, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = c * v[x];
        }

        public static void _C_sum_V(Index size, float c, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = c + v[x];
        }

        public static void _C_sub_V(Index size, float c, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = c - v[x];
        }

        public static void _V_sub_C(Index size, float c, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = v[x] - c;
        }

        public static void _C_div_V(Index size, float c, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = c / v[x];
        }

        public static void _V_div_C(Index size, float c, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = v[x] / c;
        }

        public static void _V_Exp(Index size, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = GPUMath.Exp(v[x]); // v[x];
        }

        public static void _V_C_More_Equal(Index size, ArrayView<float> output, float c, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = v[x] >= c ? 1 : 0;
        }

        public static void _M_C_Less_Equal(Index2 size, ArrayView2D<byte> output, float c, ArrayView2D<float> m)
        {
            var x = size.X;
            var y = size.Y;

            byte one = 0x0001;
            byte zero = 0x0000;

            output[x, y] = m[x, y] <= c ? one : zero;
        }

        public static void _V_Max(Index size, ArrayView<float> output, ArrayView<float> v, ArrayView<float> v1)
        {
            var x = size.X;
            output[x] = GPUMath.Max(v[x], v1[x]);
        }

        public static void _V_Sum(Index size, float output, ArrayView<float> v, int w)
        {
            for (var i = 0; i < w; i++) output += v[i];
        }

        public static void _M_Sqrt(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m)
        {
            var x = size.X;
            var y = size.Y;

            output[x, y] = GPUMath.Sqrt(m[x, y]);
        }

        public static void _V_Sqrt(Index size, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;

            output[x] = GPUMath.Sqrt(v[x]);
        }

        public static void _V_Sin(Index size, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = GPUMath.Sin(v[x]);
        }

        public static void _V_Tan(Index size, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = GPUMath.Tan(v[x]);
        }

        public static void _V_Tanh(Index size, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = GPUMath.Tanh(v[x]);
        }

        public static void _V_Pow(Index size, ArrayView<float> output, ArrayView<float> v, int p)
        {
            var x = size.X;
            output[x] = GPUMath.Pow(v[x], p);
        }

        public static void _M_Pow(Index2 size, ArrayView2D<float> m, int p)
        {
            var x = size.X;
            var y = size.Y;
            m[x, y] = GPUMath.Pow(m[x, y], p);
        }

        public static void _V_Cos(Index size, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = GPUMath.Cos(v[x]);
        }

        public static void _V_Log(Index size, ArrayView<float> output, ArrayView<float> v)
        {
            var x = size.X;
            output[x] = GPUMath.Log(v[x]);
        }

        public static void _V_euclidian_distance(Index size, ArrayView<float> output, Index position)
        {
            var x = size.X;
            output[x] = GPUMath.Sqrt(GPUMath.Pow(x - position.X, 2));
        }

        public static void _M_euclidian_distance(Index2 size, ArrayView2D<float> output, Index2 position)
        {
            var x = size.X;
            var y = size.Y;
            output[x, y] = GPUMath.Sqrt(GPUMath.Pow(x - position.X, 2) + GPUMath.Pow(y - position.Y, 2));
        }

        public static void _1D_to_2D(Index2 size, ArrayView2D<float> output, ArrayView<float> v, int w)
        {
            var x = size.X;
            var y = size.Y;
            var point_projected = y * w + x;

            output[x, y] = v[point_projected];
        }

        public static void _M_normalize(Index2 size, ArrayView2D<float> output, float p0, float p1)
        {
            var x = size.X;
            var y = size.Y;

            output[x, y] = output[x, y] > p1 ? p1 : (output[x, y] < p0 ? p0 : output[x, y]);
        }

        public static void _V_normalize(Index size, ArrayView<float> output, float p0, float p1)
        {
            var x = size.X;

            output[x] = output[x] > p1 ? p1 : (output[x] < p0 ? p0 : output[x]);
        }
    }
}
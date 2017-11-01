using ILGPU;

namespace VI.Maths.Array
{
    public class ArrayOperations
    {
        public static void _V_zeros(Index size, ArrayView<float> v)
        {
            int x = size.X;

            v[x] = 0;
        }

        public static void _M_sum_lines(Index2 size, int r, ArrayView2D<float> m, int boxSize)
        {
            int x = size.X;
            int y = size.Y;

            m[x, y] = m[x, y] + m[x, y + r + boxSize];
        }
        public static void _M_sum_columns(Index2 size, int r, ArrayView2D<float> m, int boxSize)
        {
            int x = size.X;
            int y = size.Y;

            m[x, y] = m[x, y] + m[x + r + boxSize, y];
        }

        public static void _M_2_lines_V(Index size, ArrayView<float> v, ArrayView2D<float> m)
        {
            int x = size.X;
            v[x] = m[x, 0] + m[x, 1];
        }
        public static void _M_2_columns_V(Index size, ArrayView2D<float> v, ArrayView2D<float> m)
        {
            int y = size.X;

            m[0, y] = m[0, y] + m[1, y];
        }

        public static void _V_X_V(Index size, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            int x = size.X;

            output[x] = v0[x] * v1[x];
        }

        public static void _V_X_V_M(Index2 size, ArrayView2D<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            int x = size.X;
            int y = size.Y;

            output[x, y] = v0[x] * v1[y];
        }

        public static void _C_X_M(Index2 size, ArrayView2D<float> m, float c)
        {
            int x = size.X;
            int y = size.Y;

            m[x, y] = m[x, y] * c;
        }

        public static void _M_sum_M(Index2 size, ArrayView2D<float> m0, ArrayView2D<float> m1)
        {
            int x = size.X;
            int y = size.Y;

            m0[x, y] = m0[x, y] + m1[x, y];
        }
        public static void _M_sum_M_sum_M(Index2 size, ArrayView2D<float> m0, ArrayView2D<float> m1, ArrayView2D<float> m2)
        {
            int x = size.X;
            int y = size.Y;

            m0[x, y] = m0[x, y] + m1[x, y] + m2[x, y];
        }

        public static void _V_sum_V(Index size, ArrayView<float> v0, ArrayView<float> v1)
        {
            int x = size.X;

            v0[x] = v0[x] + v1[x];
        }
        public static void _V_sum_V_sum_V(Index size, ArrayView<float> v0, ArrayView<float> v1, ArrayView<float> v2)
        {
            int x = size.X;

            v0[x] = v0[x] + v1[x] + v1[x];
        }

        public static void _V_X_M_column_M(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView<float> v)
        {
            int x = size.X;
            int y = size.Y;

            output[x, y] = v[y] * m[x, y];
        }
        public static void _V_X_M_line_M(Index2 size, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView<float> v)
        {
            int x = size.X;
            int y = size.Y;

            output[x, y] = v[x] * m[x, y];
        }

        public static void _C_X_V(Index size, float c, ArrayView<float> output, ArrayView<float> v)
        {
            int x = size.X;

            output[x] = v[x] * c;
        }
    }
}

using ILGPU;

namespace VI.Maths.Array
{
	public class FloatArrayOperations
	{
		public static void V_mult_V(Index pos, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
		{
			var x     = pos.X;
			output[x] = v0[x] * v1[x];
		}

		public static void V_div_V(Index pos, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
		{
			var x     = pos.X;
			output[x] = v0[x] / v1[x];
		}

		public static void V_sub_V(Index pos, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
		{
			var x     = pos.X;
			output[x] = v0[x] - v1[x];
		}

		public static void V_add_V(Index pos, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
		{
			var x     = pos.X;
			output[x] = v0[x] + v1[x];
		}

		public static void V_add_C(Index pos, ArrayView<float> output, ArrayView<float> v, float c)
		{
			var x     = pos.X;
			output[x] = v[x] + c;
		}

		public static void V_mult_C(Index pos, ArrayView<float> output, ArrayView<float> v, float c)
		{
			var x     = pos.X;
			output[x] = v[x] * c;
		}

		public static void V_sub_C(Index pos, ArrayView<float> output, ArrayView<float> v, float c)
		{
			var x     = pos.X;
			output[x] = v[x] - c;
		}

		public static void C_sub_V(Index pos, ArrayView<float> output, float c, ArrayView<float> v)
		{
			var x     = pos.X;
			output[x] = c - v[x];
		}

		public static void V_div_C(Index pos, ArrayView<float> output, ArrayView<float> v, float c)
		{
			var x     = pos.X;
			output[x] = v[x] / c;
		}

		public static void C_div_V(Index pos, ArrayView<float> output, float c, ArrayView<float> v)
		{
			var x     = pos.X;
			output[x] = c / v[x];
		}

		public static void V_ApplyMask(Index pos, ArrayView<float> output, ArrayView<float> arr, ArrayView<byte> mask)
		{
			var x     = pos.X;
			output[x] = arr[x] * mask[x];
		}

		public static void V_Tanh(Index pos, ArrayView<float> output, ArrayView<float> arr)
		{
			var x     = pos.X;
			output[x] = GPUMath.Tanh(arr[x]);
		}

		public static void V_Sin(Index pos, ArrayView<float> output, ArrayView<float> arr)
		{
			var x     = pos.X;
			output[x] = GPUMath.Sin(arr[x]);
		}

		public static void V_Cos(Index pos, ArrayView<float> output, ArrayView<float> arr)
		{
			var x     = pos.X;
			output[x] = GPUMath.Cos(arr[x]);
		}

		public static void V_Pow(Index pos, ArrayView<float> output, ArrayView<float> arr, float exp)
		{
			var x     = pos.X;
			output[x] = GPUMath.Pow(arr[x], exp);
		}

		public static void V_Exp(Index pos, ArrayView<float> output, ArrayView<float> arr)
		{
			var x     = pos.X;
			output[x] = GPUMath.Exp(arr[x]);
		}

		public static void V_Log(Index pos, ArrayView<float> output, ArrayView<float> arr)
		{
			var x     = pos.X;
			output[x] = GPUMath.Log(arr[x]);
		}

		public static void V_Sqrt(Index pos, ArrayView<float> output, ArrayView<float> arr)
		{
			var x     = pos.X;
			output[x] = GPUMath.Sqrt(arr[x]);
		}

		public static void VT_mult_M(Index2 pos, ArrayView2D<float> output, ArrayView<float> vt, ArrayView2D<float> m)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = vt[y] * m[x, y];
		}

		public static void M_mult_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m0[x, y] * m1[x, y];
		}

		public static void M_div_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m0[x, y] / m1[x, y];
		}

		public static void M_sub_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m0[x, y] - m1[x, y];
		}

		public static void M_add_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m0[x, y] + m1[x, y];
		}

		public static void M_mult_VT(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView<float> vt)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m[x, y] * vt[y];
		}

		public static void M_mult_V(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView<float> v)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m[x, y] * v[x];
		}

		public static void V_mult_M(Index2 pos, ArrayView2D<float> output, ArrayView<float> v, ArrayView2D<float> m)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m[x, y] * v[x];
		}

		public static void M_mult_C(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, float c)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m[x, y] * c;
		}

		public static void M_add_C(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, float c)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m[x, y] + c;
		}

		public static void M_div_C(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, float c)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m[x, y] / c;
		}

		public static void C_div_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, float c)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = c / m[x, y];
		}

		public static void IC_div_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, int c)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = c / m[x, y];
		}

		public static void m_Sqrt(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> arr)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = GPUMath.Sqrt(arr[x, y]);
		}

		public static void M_mult_MT(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView2D<float> mt)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = m[x, y] * mt[y, x];
		}

		public static void MT_mult_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> mt, ArrayView2D<float> m)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = mt[y, x] * m[x, y];
		}

		public static void M_ApplyMask(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> arr, ArrayView2D<byte> mask)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = arr[x, y] * mask[x, y];
		}

		public static void M_SumLine(Index2 pos, ArrayView2D<float> m, int r, int boxSize)
		{
			var x   = pos.X;
			var y   = pos.Y;
			m[x, y] = m[x, y] + m[x, y + r + boxSize];
		}

		public static void M_SumColumn(Index2 pos, ArrayView2D<float> m, int r, int boxSize)
		{
			var x   = pos.X;
			var y   = pos.Y;
			m[x, y] = m[x, y] + m[x + r + boxSize, y];
		}

		public static void M_2_lines_V(Index size, ArrayView<float> v, ArrayView2D<float> m)
		{
			var x = size.X;
			v[x]  = m[x, 0] + m[x, 1];
		}

		public static void M_2_columns_V(Index size, ArrayView<float> v, ArrayView2D<float> m)
		{
			var y = size.X;
			v[y]  = m[0, y] + m[1, y];
		}

		public static void VT_mult_V(Index2 pos, ArrayView2D<float> output, ArrayView<float> vt, ArrayView<float> v)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = vt[y] * v[x];
		}

		public static void V_mult_VT(Index2 pos, ArrayView2D<float> output, ArrayView<float> v, ArrayView<float> vt)
		{
			var x        = pos.X;
			var y        = pos.Y;
			output[x, y] = v[x] * vt[y];
		}
	}
}
using System;
using ILGPU;
using ILGPU.Runtime;
using VI.NumSharp.Drivers.Data.GPU;

namespace VI.NumSharp.Drivers.Executor.GPU
{
	public class ParallelFloatExecutorGPU : IFloatArrayExecutor
	{
		public IFloatData V_mult_V(IFloatData cache, IFloatData v0, IFloatData v1)
		{
			var size = v0.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_mult_V"].Launch(size, cache.View, v0.View, v1.View);
			return cache;
		}

		public IFloatData V_div_V(IFloatData cache, IFloatData v0, IFloatData v1)
		{
			var size = v0.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_div_V"].Launch(size, cache.View, v0.View, v1.View);
			return cache;
		}

		public IFloatData V_sub_V(IFloatData cache, IFloatData v0, IFloatData v1)
		{
			var size = v0.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_sub_V"].Launch(size, cache.View, v0.View, v1.View);
			return cache;
		}

		public IFloatData V_add_V(IFloatData cache, IFloatData v0, IFloatData v1)
		{
			var size = v0.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_add_V"].Launch(size, cache.View, v0.View, v1.View);
			return cache;
		}

		public IFloatData V_add_C(IFloatData cache, IFloatData v, float c)
		{
			var size = v.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_add_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IFloatData V_mult_C(IFloatData cache, IFloatData v, float c)
		{
			var size = v.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_mult_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IFloatData V_sub_C(IFloatData cache, IFloatData v, float c)
		{
			var size = v.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_sub_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IFloatData V_sub_C(IFloatData cache, float c, IFloatData v)
		{
			var size = v.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_sub_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IFloatData V_div_C(IFloatData cache, IFloatData v, float c)
		{
			var size = v.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_div_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IFloatData V_div_C(IFloatData cache, float c, IFloatData v)
		{
			var size = v.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_div_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IFloatData Tanh(IFloatData cache, IFloatData arr)
		{
			var size = arr.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_Tanh"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IFloatData Sin(IFloatData cache, IFloatData arr)
		{
			var size = arr.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_Sin"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IFloatData Cos(IFloatData cache, IFloatData arr)
		{
			var size = arr.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_Cos"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IFloatData Pow(IFloatData cache, IFloatData arr, float exp)
		{
			var size = arr.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_Pow"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IFloatData Exp(IFloatData cache, IFloatData arr)
		{
			var size = arr.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_Exp"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IFloatData Log(IFloatData cache, IFloatData arr)
		{
			var size = arr.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_Log"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IFloatData Sqrt(IFloatData cache, IFloatData arr)
		{
			var size = arr.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_Sqrt"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IFloatData2D VT_mult_M(IFloatData2D cache, IFloatData vt, IFloatData2D m)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["VT_mult_M"].Launch(size, cache.View, vt.View, m.View);
			return cache;
		}

		public IFloatData2D M_mult_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
		{
			var size = new Index2(m0.W, m0.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_M"].Launch(size, cache.View, m0.View, m1.View);
			return cache;
		}

		public IFloatData2D M_div_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
		{
			var size = new Index2(m0.W, m0.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_div_M"].Launch(size, cache.View, m0.View, m1.View);
			return cache;
		}

		public IFloatData2D M_sub_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
		{
			var size = new Index2(m0.W, m0.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_sub_M"].Launch(size, cache.View, m0.View, m1.View);
			return cache;
		}

		public IFloatData2D M_add_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
		{
			var size = new Index2(m0.W, m0.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_add_M"].Launch(size, cache.View, m0.View, m1.View);
			return cache;
		}

		public IFloatData2D M_mult_VT(IFloatData2D cache, IFloatData2D m, IFloatData v)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_VT"].Launch(size, cache.View, m.View, v.View);
			return cache;
		}

		public IFloatData2D M_mult_V(IFloatData2D cache, IFloatData2D m, IFloatData v)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_V"].Launch(size, cache.View, m.View, v.View);
			return cache;
		}

		public IFloatData2D V_mult_M(IFloatData2D cache, IFloatData v, IFloatData2D m)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["V_mult_M"].Launch(size, cache.View, v.View, m.View);
			return cache;
		}

		public IFloatData2D M_mult_C(IFloatData2D cache, IFloatData2D m, float c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_C"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IFloatData2D M_add_C(IFloatData2D cache, IFloatData2D m, float c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_add_C"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IFloatData2D M_div_C(IFloatData2D cache, IFloatData2D m, float c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_div_C"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, float c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["C_div_M"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, int c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.FloatArrayDevice.Executor["IC_div_M"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IFloatData2D Sqrt(IFloatData2D cache, IFloatData2D arr)
		{
			var size = new Index2(arr.W, arr.H);
			ProcessingDevice.FloatArrayDevice.Executor["m_Sqrt"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IFloatData2D VT_mult_V(IFloatData vt, IFloatData v)
		{
			var size   = new Index2(v.Length, vt.Length);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["VT_mult_V"].Launch(size, output.View, vt.View, v.View);
			return new GPU_FloatData2D(output);
		}

		public IFloatData2D V_mult_VT(IFloatData v, IFloatData vt)
		{
			var size   = new Index2(v.Length, vt.Length);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_mult_VT"].Launch(size, output.View, v.View, vt.View);
			return new GPU_FloatData2D(output);
		}

		public IFloatData2D M_mult_MT(IFloatData2D m, IFloatData2D mt)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_MT"].Launch(size, output.View, m.View, mt.View);
			return new GPU_FloatData2D(output);
		}

		public IFloatData2D MT_mult_M(IFloatData2D mt, IFloatData2D m)
		{
			var size   = new Index2(mt.H, mt.W);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["MT_mult_M"].Launch(size, output.View, mt.View, m.View);
			return new GPU_FloatData2D(output);
		}

		public IFloatData ApplyMask(IFloatData arr, IByteData mask)
		{
			var size = arr.Length;
			ProcessingDevice.FloatArrayDevice.Executor["V_ApplyMask"].Launch(size, arr.View, arr.View, mask.View);
			return arr;
		}

		public IFloatData2D ApplyMask(IFloatData2D arr, IByteData2D mask)
		{
			var size = new Index2(arr.W, arr.H);
			ProcessingDevice.FloatArrayDevice.Executor["M_ApplyMask"].Launch(size, arr.View, arr.View, mask.View);
			return arr;
		}

		public IFloatData SumLine(IFloatData2D arr)
		{
			if (arr.H <= 1)
				return new GPU_FloatData(arr.AsArray());

			var s = arr.H / 2d;
			var r = arr.H % 2d;
			while (s > 1)
			{
				var _size = new Index2(arr.W, (int) s);

				_sumLines(_size, (int) Math.Ceiling(r), arr.View);
				r =  s % 2d;
				s /= 2d;
			}

			return new GPU_FloatData(_joinLines(arr.W, arr.View));
		}

		public IFloatData SumColumn(IFloatData2D arr)
		{
			if (arr.W <= 1)
				return new GPU_FloatData(arr.AsArray());

			var s = arr.W / 2d;
			var r = arr.W % 2d;
			while (s > 1)
			{
				var _size = new Index2((int) s, arr.H);

				_sumColumns(_size, (int) Math.Ceiling(r), arr.View);
				r =  s % 2;
				s /= 2;
			}

			return new GPU_FloatData(_joinColumns(arr.H, arr.View));
		}

		private void _sumLines(Index2 size, int r, ArrayView2D<float> m)
		{
			ProcessingDevice.FloatArrayDevice.Executor["M_SumLine"].Launch(size, m, r, size.Y);
		}

		private void _sumColumns(Index2 size, int r, ArrayView2D<float> m)
		{
			ProcessingDevice.FloatArrayDevice.Executor["M_SumColumn"].Launch(size, m, r, size.X);
		}

		private MemoryBuffer<float> _joinLines(Index size, ArrayView2D<float> m)
		{
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_2_lines_V"].Launch(size, output.View, m);
			return output;
		}

		private MemoryBuffer<float> _joinColumns(Index size, ArrayView2D<float> m)
		{
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_2_columns_V"].Launch(size, output.View, m);
			return output;
		}
	}
}
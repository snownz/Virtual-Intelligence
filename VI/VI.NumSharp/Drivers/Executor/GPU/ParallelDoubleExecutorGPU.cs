using System;
using ILGPU;
using ILGPU.Runtime;
using VI.NumSharp.Drivers.Data.GPU;

namespace VI.NumSharp.Drivers.Executor.GPU
{
	public class ParallelDoubleExecutorGPU : IDoubleArrayExecutor
	{
		public IDoubleData V_mult_V(IDoubleData cache, IDoubleData v0, IDoubleData v1)
		{
			var size = v0.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_mult_V"].Launch(size, cache.View, v0.View, v1.View);
			return cache;
		}

		public IDoubleData V_div_V(IDoubleData cache, IDoubleData v0, IDoubleData v1)
		{
			var size = v0.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_div_V"].Launch(size, cache.View, v0.View, v1.View);
			return cache;
		}

		public IDoubleData V_sub_V(IDoubleData cache, IDoubleData v0, IDoubleData v1)
		{
			var size = v0.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_sub_V"].Launch(size, cache.View, v0.View, v1.View);
			return cache;
		}

		public IDoubleData V_add_V(IDoubleData cache, IDoubleData v0, IDoubleData v1)
		{
			var size = v0.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_add_V"].Launch(size, cache.View, v0.View, v1.View);
			return cache;
		}

		public IDoubleData V_add_C(IDoubleData cache, IDoubleData v, double c)
		{
			var size = v.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_add_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IDoubleData V_mult_C(IDoubleData cache, IDoubleData v, double c)
		{
			var size = v.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_mult_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IDoubleData V_sub_C(IDoubleData cache, IDoubleData v, double c)
		{
			var size = v.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_sub_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IDoubleData V_sub_C(IDoubleData cache, double c, IDoubleData v)
		{
			var size = v.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_sub_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IDoubleData V_div_C(IDoubleData cache, IDoubleData v, double c)
		{
			var size = v.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_div_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IDoubleData V_div_C(IDoubleData cache, double c, IDoubleData v)
		{
			var size = v.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_div_C"].Launch(size, cache.View, v.View, c);
			return cache;
		}

		public IDoubleData Tanh(IDoubleData cache, IDoubleData arr)
		{
			var size = arr.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_Tanh"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IDoubleData Sin(IDoubleData cache, IDoubleData arr)
		{
			var size = arr.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_Sin"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IDoubleData Cos(IDoubleData cache, IDoubleData arr)
		{
			var size = arr.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_Cos"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IDoubleData Pow(IDoubleData cache, IDoubleData arr, double exp)
		{
			var size = arr.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_Pow"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IDoubleData Exp(IDoubleData cache, IDoubleData arr)
		{
			var size = arr.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_Exp"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IDoubleData Log(IDoubleData cache, IDoubleData arr)
		{
			var size = arr.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_Log"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IDoubleData Sqrt(IDoubleData cache, IDoubleData arr)
		{
			var size = arr.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_Sqrt"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IDoubleData2D VT_mult_M(IDoubleData2D cache, IDoubleData vt, IDoubleData2D m)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["VT_mult_M"].Launch(size, cache.View, vt.View, m.View);
			return cache;
		}

		public IDoubleData2D M_mult_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1)
		{
			var size = new Index2(m0.W, m0.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_mult_M"].Launch(size, cache.View, m0.View, m1.View);
			return cache;
		}

		public IDoubleData2D M_div_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1)
		{
			var size = new Index2(m0.W, m0.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_div_M"].Launch(size, cache.View, m0.View, m1.View);
			return cache;
		}

		public IDoubleData2D M_sub_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1)
		{
			var size = new Index2(m0.W, m0.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_sub_M"].Launch(size, cache.View, m0.View, m1.View);
			return cache;
		}

		public IDoubleData2D M_add_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1)
		{
			var size = new Index2(m0.W, m0.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_add_M"].Launch(size, cache.View, m0.View, m1.View);
			return cache;
		}

		public IDoubleData2D M_mult_VT(IDoubleData2D cache, IDoubleData2D m, IDoubleData v)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_mult_VT"].Launch(size, cache.View, m.View, v.View);
			return cache;
		}

		public IDoubleData2D M_mult_V(IDoubleData2D cache, IDoubleData2D m, IDoubleData v)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_mult_V"].Launch(size, cache.View, m.View, v.View);
			return cache;
		}

		public IDoubleData2D V_mult_M(IDoubleData2D cache, IDoubleData v, IDoubleData2D m)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["V_mult_M"].Launch(size, cache.View, v.View, m.View);
			return cache;
		}

		public IDoubleData2D M_mult_C(IDoubleData2D cache, IDoubleData2D m, double c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_mult_C"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IDoubleData2D M_add_C(IDoubleData2D cache, IDoubleData2D m, double c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_add_C"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IDoubleData2D M_div_C(IDoubleData2D cache, IDoubleData2D m, double c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_div_C"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IDoubleData2D C_div_M(IDoubleData2D cache, IDoubleData2D m, double c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["C_div_M"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IDoubleData2D C_div_M(IDoubleData2D cache, IDoubleData2D m, int c)
		{
			var size = new Index2(m.W, m.H);
			ProcessingDevice.DoubleArrayDevice.Executor["IC_div_M"].Launch(size, cache.View, m.View, c);
			return cache;
		}

		public IDoubleData2D Sqrt(IDoubleData2D cache, IDoubleData2D arr)
		{
			var size = new Index2(arr.W, arr.H);
			ProcessingDevice.DoubleArrayDevice.Executor["m_Sqrt"].Launch(size, cache.View, arr.View);
			return cache;
		}

		public IDoubleData2D VT_mult_V(IDoubleData vt, IDoubleData v)
		{
			var size   = new Index2(v.Length, vt.Length);
			var output = ILGPUMethods.Allocate<double>(size);
			ProcessingDevice.DoubleArrayDevice.Executor["VT_mult_V"].Launch(size, output.View, vt.View, v.View);
			return new GPU_DoubleData2D(output);
		}

		public IDoubleData2D V_mult_VT(IDoubleData v, IDoubleData vt)
		{
			var size   = new Index2(v.Length, vt.Length);
			var output = ILGPUMethods.Allocate<double>(size);
			ProcessingDevice.DoubleArrayDevice.Executor["V_mult_VT"].Launch(size, output.View, v.View, vt.View);
			return new GPU_DoubleData2D(output);
		}

		public IDoubleData2D M_mult_MT(IDoubleData2D m, IDoubleData2D mt)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<double>(size);
			ProcessingDevice.DoubleArrayDevice.Executor["M_mult_MT"].Launch(size, output.View, m.View, mt.View);
			return new GPU_DoubleData2D(output);
		}

		public IDoubleData2D MT_mult_M(IDoubleData2D mt, IDoubleData2D m)
		{
			var size   = new Index2(mt.H, mt.W);
			var output = ILGPUMethods.Allocate<double>(size);
			ProcessingDevice.DoubleArrayDevice.Executor["MT_mult_M"].Launch(size, output.View, mt.View, m.View);
			return new GPU_DoubleData2D(output);
		}

		public IDoubleData ApplyMask(IDoubleData arr, IByteData mask)
		{
			var size = arr.Length;
			ProcessingDevice.DoubleArrayDevice.Executor["V_ApplyMask"].Launch(size, arr.View, arr.View, mask.View);
			return arr;
		}

		public IDoubleData2D ApplyMask(IDoubleData2D arr, IByteData2D mask)
		{
			var size = new Index2(arr.W, arr.H);
			ProcessingDevice.DoubleArrayDevice.Executor["M_ApplyMask"].Launch(size, arr.View, arr.View, mask.View);
			return arr;
		}

		public IDoubleData SumLine(IDoubleData2D arr)
		{
			if (arr.H <= 1)
				return new GPU_DoubleData(arr.AsArray());

			var s = arr.H / 2d;
			var r = arr.H % 2d;
			while (s > 1)
			{
				var _size = new Index2(arr.W, (int) s);

				_sumLines(_size, (int) Math.Ceiling(r), arr.View);
				r =  s % 2d;
				s /= 2d;
			}

			return new GPU_DoubleData(_joinLines(arr.W, arr.View));
		}

		public IDoubleData SumColumn(IDoubleData2D arr)
		{
			if (arr.W <= 1)
				return new GPU_DoubleData(arr.AsArray());

			var s = arr.W / 2d;
			var r = arr.W % 2d;
			while (s > 1)
			{
				var _size = new Index2((int) s, arr.H);

				_sumColumns(_size, (int) Math.Ceiling(r), arr.View);
				r =  s % 2;
				s /= 2;
			}

			return new GPU_DoubleData(_joinColumns(arr.H, arr.View));
		}

		private void _sumLines(Index2 size, int r, ArrayView2D<double> m)
		{
			ProcessingDevice.DoubleArrayDevice.Executor["M_SumLine"].Launch(size, m, r, size.Y);
			//ProcessingDevice.DoubleArrayDevice.Executor.Wait();
		}

		private void _sumColumns(Index2 size, int r, ArrayView2D<double> m)
		{
			ProcessingDevice.DoubleArrayDevice.Executor["M_SumColumn"].Launch(size, m, r, size.X);
			//ProcessingDevice.DoubleArrayDevice.Executor.Wait();
		}

		private MemoryBuffer<double> _joinLines(Index size, ArrayView2D<double> m)
		{
			var output = ILGPUMethods.Allocate<double>(size);
			ProcessingDevice.DoubleArrayDevice.Executor["M_2_lines_V"].Launch(size, output.View, m);
			//ProcessingDevice.DoubleArrayDevice.Executor.Wait();
			return output;
		}

		private MemoryBuffer<double> _joinColumns(Index size, ArrayView2D<double> m)
		{
			var output = ILGPUMethods.Allocate<double>(size);
			ProcessingDevice.DoubleArrayDevice.Executor["M_2_columns_V"].Launch(size, output.View, m);
			//ProcessingDevice.DoubleArrayDevice.Executor.Wait();
			return output;
		}
	}
}
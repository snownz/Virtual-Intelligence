﻿using ILGPU;
using ILGPU.Runtime;
using VI.NumSharp.Drivers.Data.CPU;
using VI.NumSharp.Drivers.Data.GPU;

namespace VI.NumSharp.Drivers.Executor.GPU
{
	public class ParallelFloatExecutorGPU : IFloatArrayExecutor
	{
		public IFloatData V_mult_V(IFloatData v0, IFloatData v1)
		{
			var size   = v0.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_mult_V"].Launch(size, output.View, v0.View, v1.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_div_V(IFloatData v0, IFloatData v1)
		{
			var size   = v0.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_div_V"].Launch(size, output.View, v0.View, v1.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_sub_V(IFloatData v0, IFloatData v1)
		{
			var size   = v0.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_sub_V"].Launch(size, output.View, v0.View, v1.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_add_V(IFloatData v0, IFloatData v1)
		{
			var size   = v0.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_add_V"].Launch(size, output.View, v0.View, v1.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_add_C(IFloatData v, float c)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_add_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_mult_C(IFloatData v, float c)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_mult_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_sub_C(IFloatData v, float c)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_sub_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_sub_C(float c, IFloatData v)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_sub_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_div_C(IFloatData v, float c)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_div_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData V_div_C(float c, IFloatData v)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_div_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData Tanh(IFloatData arr)
		{
			var size = arr.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_Tanh"].Launch(size, output.View, arr.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData Sin(IFloatData arr)
		{
			var size   = arr.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_Sin"].Launch(size, output.View, arr.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData Cos(IFloatData arr)
		{
			var size   = arr.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_Cos"].Launch(size, output.View, arr.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData Pow(IFloatData arr, float exp)
		{
			var size   = arr.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_Pow"].Launch(size, output.View, arr.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData Exp(IFloatData arr)
		{
			var size   = arr.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_Exp"].Launch(size, output.View, arr.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData Log(IFloatData arr)
		{
			var size   = arr.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_Log"].Launch(size, output.View, arr.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData Sqrt(IFloatData arr)
		{
			var size   = arr.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_Sqrt"].Launch(size, output.View, arr.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData ApplyMask(IFloatData arr, IByteData mask)
		{
			var size   = arr.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_ApplyMask"].Launch(size, output.View, arr.View, mask.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}
		public IFloatData SumLine(IFloatData2D arr)
		{
			if (arr.W <= 1)
				return new GPU_FloatData(arr.AsArray());
			
			var s = arr.W / 2;
			var r = arr.W % 2;
			while (s > 1)
			{
				var _size = new Index2(arr.H, s);

				_sumColumns(_size, r, arr.View);

				r =  s % 2;
				s /= 2;
			}
			return new GPU_FloatData(_joinColumns(arr.W, arr.View));
		}
		public IFloatData SumColumn(IFloatData2D arr)
		{
			if (arr.H <= 1)
				return new GPU_FloatData(arr.AsArray());
			
			var s = arr.H / 2;
			var r = arr.H % 2;
			while (s > 1)
			{
				var _size = new Index2(arr.W, s);

				_sumLines(_size, r, arr.View);

				r =  s % 2;
				s /= 2;
			}
			return new GPU_FloatData(_joinLines(arr.W, arr.View));
		}
		
		private void _sumLines(Index2 size, int r, object m)
		{
			ProcessingDevice.FloatArrayDevice.Executor["M_SumLine"].Launch(size, m, r, size.Y);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
		}
		private void _sumColumns(Index2 size, int r, object m)
		{
			ProcessingDevice.FloatArrayDevice.Executor["M_SumColumn"].Launch(size, r, m, r, size.X);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
		}
		
		private MemoryBuffer<float> _joinLines(Index size, object m)
		{
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_2_lines_V"].Launch(size, output.View, m);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return output;
		}
		private MemoryBuffer<float> _joinColumns(Index size, object m)
		{
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_2_columns_V"].Launch(size, output.View, m);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return output;
		}
		
		public IFloatData2D VT_mult_V(IFloatData vt, IFloatData v)
		{
			var size   = new Index2(v.Length, vt.Length);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["VT_mult_V"].Launch(size, output.View, vt.View, v.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D V_mult_VT(IFloatData v, IFloatData vt)
		{
			var size   = new Index2(v.Length, vt.Length);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_mult_VT"].Launch(size, output.View, v.View, vt.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D VT_mult_M(IFloatData vt, IFloatData2D m)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["VT_mult_M"].Launch(size, output.View, m.View, vt.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_mult_M(IFloatData2D m0, IFloatData2D m1)
		{
			var size   = new Index2(m0.W, m0.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_M"].Launch(size, output.View, m0.View, m1.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_div_M(IFloatData2D m0, IFloatData2D m1)
		{
			var size   = new Index2(m0.W, m0.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_div_M"].Launch(size, output.View, m0.View, m1.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_sub_M(IFloatData2D m0, IFloatData2D m1)
		{
			var size   = new Index2(m0.W, m0.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_sub_M"].Launch(size, output.View, m0.View, m1.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_add_M(IFloatData2D m0, IFloatData2D m1)
		{
			var size   = new Index2(m0.W, m0.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_add_M"].Launch(size, output.View, m0.View, m1.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_mult_VT(IFloatData2D m, IFloatData v)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_VT"].Launch(size, output.View, m.View, v.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_mult_V(IFloatData2D m, IFloatData v)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_V"].Launch(size, output.View, m.View, v.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D V_mult_M(IFloatData v, IFloatData2D m)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["V_mult_M"].Launch(size, output.View, m.View, v.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_mult_C(IFloatData2D m, float c)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_C"].Launch(size, output.View, m.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_add_C(IFloatData2D m, float c)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_add_C"].Launch(size, output.View, m.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_div_C(IFloatData2D m, float c)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_div_C"].Launch(size, output.View, m.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D C_div_M(IFloatData2D m, float c)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["C_div_M"].Launch(size, output.View, m.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D C_div_M(IFloatData2D m, int c)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["IC_div_M"].Launch(size, output.View, m.View, c);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D M_mult_MT(IFloatData2D m, IFloatData2D mt)
		{
			var size   = new Index2(m.W, m.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_mult_MT"].Launch(size, output.View, m.View, mt.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D MT_mult_M(IFloatData2D mt, IFloatData2D m)
		{
			var size   = new Index2(mt.H, mt.W);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["MT_mult_M"].Launch(size, output.View, mt.View, m.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D ApplyMask(IFloatData2D arr, IByteData2D mask)
		{
			var size   = new Index2(arr.W, arr.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["M_ApplyMask"].Launch(size, output.View, arr.View, mask.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
		public IFloatData2D Sqrt(IFloatData2D arr)
		{
			var size   = new Index2(arr.W, arr.H);
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.FloatArrayDevice.Executor["m_Sqrt"].Launch(size, output.View, arr.View);
			ProcessingDevice.FloatArrayDevice.Executor.Wait();
			return new GPU_FloatData2D(output);
		}
	}
}
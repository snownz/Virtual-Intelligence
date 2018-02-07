using System;
using System.Threading.Tasks;
using VI.NumSharp.Drivers.Data.CPU;

namespace VI.NumSharp.Drivers.Executor.CPU
{
	public class ParallelFloatExecutorCPU : IFloatArrayExecutor
	{
		public IFloatData V_mult_V(IFloatData v0, IFloatData v1)
		{
			var output                                    = new float[v0.Length];
			for (var i = 0; i < v0.Length; i++) output[i] = v0[i] * v1[i];
			return new CPU_FloatData(output);
		}

		public IFloatData V_div_V(IFloatData v0, IFloatData v1)
		{
			var output                                    = new float[v0.Length];
			for (var i = 0; i < v0.Length; i++) output[i] = v0[i] / v1[i];
			return new CPU_FloatData(output);
		}

		public IFloatData V_sub_V(IFloatData v0, IFloatData v1)
		{
			var output                                    = new float[v0.Length];
			for (var i = 0; i < v0.Length; i++) output[i] = v0[i] - v1[i];
			return new CPU_FloatData(output);
		}

		public IFloatData V_add_V(IFloatData v0, IFloatData v1)
		{
			var output                                    = new float[v0.Length];
			for (var i = 0; i < v0.Length; i++) output[i] = v0[i] + v1[i];
			return new CPU_FloatData(output);
		}

		public IFloatData V_add_C(IFloatData v, float c)
		{
			var output                                   = new float[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = v[i] + c;
			return new CPU_FloatData(output);
		}

		public IFloatData V_mult_C(IFloatData v, float c)
		{
			var output                                   = new float[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = v[i] * c;
			return new CPU_FloatData(output);
		}

		public IFloatData V_sub_C(IFloatData v, float c)
		{
			var output                                   = new float[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = v[i] - c;
			return new CPU_FloatData(output);
		}

		public IFloatData V_sub_C(float c, IFloatData v)
		{
			var output                                   = new float[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = c - v[i];
			return new CPU_FloatData(output);
		}

		public IFloatData V_div_C(IFloatData v, float c)
		{
			var output                                   = new float[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = v[i] / c;
			return new CPU_FloatData(output);
		}

		public IFloatData V_div_C(float c, IFloatData v)
		{
			var output                                   = new float[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = c / v[i];
			return new CPU_FloatData(output);
		}

		public IFloatData2D VT_mult_V(IFloatData vt, IFloatData v)
		{
			var output                                                                          = new float[vt.Length, v.Length];
			Parallel.ForEach(vt.AxesX, (x) => { Parallel.ForEach(v.AxesX, (y) => { output[x, y] = vt[x] * v[y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D V_mult_VT(IFloatData v, IFloatData vt)
		{
			var output                                                                          = new float[vt.Length, v.Length];
			Parallel.ForEach(vt.AxesX, (x) => { Parallel.ForEach(v.AxesX, (y) => { output[x, y] = v[y] * vt[x]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D VT_mult_M(IFloatData vt, IFloatData2D m)
		{
			var output                                                                         = new float[m.W, m.H];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[x, y] = vt[y] * m[x, y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_mult_M(IFloatData2D m0, IFloatData2D m1)
		{
			var output                                                                           = new float[m0.W, m0.H];
			Parallel.ForEach(m0.AxesX, (x) => { Parallel.ForEach(m0.AxesY, (y) => { output[x, y] = m0[x, y] * m1[x, y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_div_M(IFloatData2D m0, IFloatData2D m1)
		{
			var output                                                                           = new float[m0.W, m0.H];
			Parallel.ForEach(m0.AxesX, (x) => { Parallel.ForEach(m0.AxesY, (y) => { output[x, y] = m0[x, y] / m1[x, y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_sub_M(IFloatData2D m0, IFloatData2D m1)
		{
			var output                                                                           = new float[m0.W, m0.H];
			Parallel.ForEach(m0.AxesX, (x) => { Parallel.ForEach(m0.AxesY, (y) => { output[x, y] = m0[x, y] + m1[x, y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_add_M(IFloatData2D m0, IFloatData2D m1)
		{
			var output                                                                           = new float[m0.W, m0.H];
			Parallel.ForEach(m0.AxesX, (x) => { Parallel.ForEach(m0.AxesY, (y) => { output[x, y] = m0[x, y] - m1[x, y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_mult_VT(IFloatData2D m, IFloatData v)
		{
			var output                                                                         = new float[m.H, m.W];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[y, x] = m[x, y] * v[x]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_mult_V(IFloatData2D m, IFloatData v)
		{
			var output                                                                         = new float[m.W, m.H];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[x, y] = m[x, y] * v[x]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D V_mult_M(IFloatData v, IFloatData2D m)
		{
			var output                                                                         = new float[m.W, m.H];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[x, y] = v[x] * m[x, y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_mult_C(IFloatData2D m, float c)
		{
			var output                                                                         = new float[m.W, m.H];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[x, y] = m[x, y] * c; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_add_C(IFloatData2D m, float c)
		{
			var output                                                                         = new float[m.W, m.H];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[x, y] = m[x, y] + c; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_div_C(IFloatData2D m, float c)
		{
			var output                                                                         = new float[m.W, m.H];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[x, y] = m[x, y] / c; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D C_div_M(IFloatData2D m, float c)
		{
			var output                                                                         = new float[m.W, m.H];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[x, y] = c / m[x, y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D C_div_M(IFloatData2D m, int c)
		{
			var output                                                                         = new float[m.W, m.H];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[x, y] = c / m[x, y]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D M_mult_MT(IFloatData2D mt, IFloatData2D m)
		{
			var output                                                                           = new float[mt.H, mt.W];
			Parallel.ForEach(mt.AxesX, (x) => { Parallel.ForEach(mt.AxesY, (y) => { output[y, x] = mt[x, y] * m[y, x]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData2D MT_mult_M(IFloatData2D m, IFloatData2D mt)
		{
			var output                                                                         = new float[m.H, m.W];
			Parallel.ForEach(m.AxesX, (x) => { Parallel.ForEach(m.AxesY, (y) => { output[y, x] = m[x, y] * mt[y, x]; }); });
			return new CPU_FloatData2D(output);
		}

		public IFloatData ApplyMask(IFloatData arr, IByteData mask)
		{
			var output                                     = new float[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = arr[x] * mask[x];
			return new CPU_FloatData(output);
		}

		public IFloatData2D ApplyMask(IFloatData2D arr, IByteData2D mask)
		{
			var output = new float[arr.W, arr.H];
			for (var x = 0; x < arr.W; x++)
			for (var y = 0; x < arr.H; x++)
				output[x, y] = arr[x, y] * mask[x, y];
			return new CPU_FloatData2D(output);
		}

		public IFloatData Tanh(IFloatData arr)
		{
			var output                                     = new float[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = (float) Math.Tanh(arr[x]);
			return new CPU_FloatData(output);
		}

		public IFloatData Sin(IFloatData arr)
		{
			var output                                     = new float[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = (float) Math.Sin(arr[x]);
			return new CPU_FloatData(output);
		}

		public IFloatData Cos(IFloatData arr)
		{
			var output                                     = new float[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = (float) Math.Cos(arr[x]);
			return new CPU_FloatData(output);
		}

		public IFloatData Pow(IFloatData arr, float exp)
		{
			var output                                     = new float[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = (float) Math.Pow(arr[x], exp);
			return new CPU_FloatData(output);
		}

		public IFloatData Exp(IFloatData arr)
		{
			var output                                     = new float[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = (float) Math.Exp(arr[x]);
			return new CPU_FloatData(output);
		}

		public IFloatData Log(IFloatData arr)
		{
			var output                                     = new float[arr.Length];
			for (var i = 0; i < arr.Length; i++) output[i] = (float) Math.Log(arr[i]);
			return new CPU_FloatData(output);
		}

		public IFloatData2D Sqrt(IFloatData2D arr)
		{
			var output = new float[arr.W, arr.H];
			for (var x = 0; x < arr.W; x++)
			for (var y = 0; y < arr.H; y++)
				output[x, y] = (float) Math.Sqrt(arr[x, y]);
			return new CPU_FloatData2D(output);
		}

		public IFloatData Sqrt(IFloatData arr)
		{
			var output                                     = new float[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = (float) Math.Sqrt(arr[x]);
			return new CPU_FloatData(output);
		}

		public IFloatData SumLine(IFloatData2D arr)
		{
			var output = new float[arr.W];
			for (var x = 0; x < arr.W; x++)
			{
				var sum                             = 0f;
				for (var y = 0; y < arr.H; y++) sum += arr[x, y];

				output[x] = sum;
			}

			return new CPU_FloatData(output);
		}

		public IFloatData SumColumn(IFloatData2D arr)
		{
			var output = new float[arr.H];
			for (var y = 0; y < arr.H; y++)
			{
				var sum                             = 0f;
				for (var x = 0; x < arr.W; x++) sum += arr[x, y];

				output[y] = sum;
			}

			return new CPU_FloatData(output);
		}
	}
}
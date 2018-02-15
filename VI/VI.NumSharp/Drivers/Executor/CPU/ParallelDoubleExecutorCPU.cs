using System;
using System.Threading.Tasks;
using VI.NumSharp.Drivers.Data.CPU;

namespace VI.NumSharp.Drivers.Executor.CPU
{
	public class ParallelDoubleExecutorCPU : IDoubleArrayExecutor
	{
		public IDoubleData V_mult_V(IDoubleData cache, IDoubleData v0, IDoubleData v1)
		{
			var output                                    = new double[v0.Length];
			for (var i = 0; i < v0.Length; i++) output[i] = v0[i] * v1[i];
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_div_V(IDoubleData cache, IDoubleData v0, IDoubleData v1)
		{
			var output                                    = new double[v0.Length];
			for (var i = 0; i < v0.Length; i++) output[i] = v0[i] / v1[i];
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_sub_V(IDoubleData cache, IDoubleData v0, IDoubleData v1)
		{
			var output                                    = new double[v0.Length];
			for (var i = 0; i < v0.Length; i++) output[i] = v0[i] - v1[i];
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_add_V(IDoubleData cache, IDoubleData v0, IDoubleData v1)
		{
			var output                                    = new double[v0.Length];
			for (var i = 0; i < v0.Length; i++) output[i] = v0[i] + v1[i];
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_add_C(IDoubleData cache, IDoubleData v, double c)
		{
			var output                                   = new double[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = v[i] + c;
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_mult_C(IDoubleData cache, IDoubleData v, double c)
		{
			var output                                   = new double[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = v[i] * c;
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_sub_C(IDoubleData cache, IDoubleData v, double c)
		{
			var output                                   = new double[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = v[i] - c;
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_sub_C(IDoubleData cache, double c, IDoubleData v)
		{
			var output                                   = new double[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = c - v[i];
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_div_C(IDoubleData cache, IDoubleData v, double c)
		{
			var output                                   = new double[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = v[i] / c;
			return new CPU_DoubleData(output);
		}

		public IDoubleData V_div_C(IDoubleData cache, double c, IDoubleData v)
		{
			var output                                   = new double[v.Length];
			for (var i = 0; i < v.Length; i++) output[i] = c / v[i];
			return new CPU_DoubleData(output);
		}

		public IDoubleData Tanh(IDoubleData cache, IDoubleData arr)
		{
			var output                                     = new double[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = Math.Tanh(arr[x]);
			return new CPU_DoubleData(output);
		}

		public IDoubleData Sin(IDoubleData cache, IDoubleData arr)
		{
			var output                                     = new double[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = Math.Sin(arr[x]);
			return new CPU_DoubleData(output);
		}

		public IDoubleData Cos(IDoubleData cache, IDoubleData arr)
		{
			var output                                     = new double[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = Math.Cos(arr[x]);
			return new CPU_DoubleData(output);
		}

		public IDoubleData Pow(IDoubleData cache, IDoubleData arr, double exp)
		{
			var output                                     = new double[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = Math.Pow(arr[x], exp);
			return new CPU_DoubleData(output);
		}

		public IDoubleData Exp(IDoubleData cache, IDoubleData arr)
		{
			var output                                     = new double[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = Math.Exp(arr[x]);
			return new CPU_DoubleData(output);
		}

		public IDoubleData Log(IDoubleData cache, IDoubleData arr)
		{
			var output                                     = new double[arr.Length];
			for (var i = 0; i < arr.Length; i++) output[i] = Math.Log(arr[i]);
			return new CPU_DoubleData(output);
		}

		public IDoubleData Sqrt(IDoubleData cache, IDoubleData arr)
		{
			var output                                     = new double[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = Math.Sqrt(arr[x]);
			return new CPU_DoubleData(output);
		}

		public IDoubleData2D VT_mult_M(IDoubleData2D cache, IDoubleData vt, IDoubleData2D m)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[x, y] = vt[y] * m[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_mult_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1)
		{
			var output                                                                       = new double[m0.W, m0.H];
			Parallel.ForEach(m0.AxesX, x => { Parallel.ForEach(m0.AxesY, y => { output[x, y] = m0[x, y] * m1[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_div_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1)
		{
			var output                                                                       = new double[m0.W, m0.H];
			Parallel.ForEach(m0.AxesX, x => { Parallel.ForEach(m0.AxesY, y => { output[x, y] = m0[x, y] / m1[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_sub_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1)
		{
			var output                                                                       = new double[m0.W, m0.H];
			Parallel.ForEach(m0.AxesX, x => { Parallel.ForEach(m0.AxesY, y => { output[x, y] = m0[x, y] - m1[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_add_M(IDoubleData2D cache, IDoubleData2D m0, IDoubleData2D m1)
		{
			var output                                                                       = new double[m0.W, m0.H];
			Parallel.ForEach(m0.AxesX, x => { Parallel.ForEach(m0.AxesY, y => { output[x, y] = m0[x, y] + m1[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_mult_VT(IDoubleData2D cache, IDoubleData2D m, IDoubleData vt)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[y, x] = m[x, y] * vt[y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_mult_V(IDoubleData2D cache, IDoubleData2D m, IDoubleData v)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[x, y] = m[x, y] * v[x]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D V_mult_M(IDoubleData2D cache, IDoubleData v, IDoubleData2D m)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[x, y] = v[x] * m[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_mult_C(IDoubleData2D cache, IDoubleData2D m, double c)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[x, y] = m[x, y] * c; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_add_C(IDoubleData2D cache, IDoubleData2D m, double c)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[x, y] = m[x, y] + c; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_div_C(IDoubleData2D cache, IDoubleData2D m, double c)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[x, y] = m[x, y] / c; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D C_div_M(IDoubleData2D cache, IDoubleData2D m, double c)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[x, y] = c / m[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D C_div_M(IDoubleData2D cache, IDoubleData2D m, int c)
		{
			var output                                                                     = new double[m.W, m.H];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[x, y] = c / m[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D Sqrt(IDoubleData2D cache, IDoubleData2D arr)
		{
			var output = new double[arr.W, arr.H];
			for (var x = 0; x < arr.W; x++)
			for (var y = 0; y < arr.H; y++)
				output[x, y] = Math.Sqrt(arr[x, y]);
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D VT_mult_V(IDoubleData vt, IDoubleData v)
		{
			var output                                                                      = new double[v.Length, vt.Length];
			Parallel.ForEach(v.AxesX, x => { Parallel.ForEach(vt.AxesX, y => { output[x, y] = vt[y] * v[x]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D V_mult_VT(IDoubleData v, IDoubleData vt)
		{
			var output                                                                      = new double[v.Length, vt.Length];
			Parallel.ForEach(v.AxesX, x => { Parallel.ForEach(vt.AxesX, y => { output[x, y] = v[x] * vt[y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D M_mult_MT(IDoubleData2D mt, IDoubleData2D m)
		{
			var output                                                                       = new double[mt.W, mt.H];
			Parallel.ForEach(mt.AxesX, x => { Parallel.ForEach(mt.AxesY, y => { output[y, x] = m[x, y] * mt[y, x]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData2D MT_mult_M(IDoubleData2D m, IDoubleData2D mt)
		{
			var output                                                                     = new double[m.H, m.W];
			Parallel.ForEach(m.AxesX, x => { Parallel.ForEach(m.AxesY, y => { output[y, x] = mt[y, x] * m[x, y]; }); });
			return new CPU_DoubleData2D(output);
		}

		public IDoubleData SumLine(IDoubleData2D arr)
		{
			var output = new double[arr.W];
			for (var x = 0; x < arr.W; x++)
			{
				var sum                             = 0d;
				for (var y = 0; y < arr.H; y++) sum += arr[x, y];

				output[x] = sum;
			}

			return new CPU_DoubleData(output);
		}

		public IDoubleData SumColumn(IDoubleData2D arr)
		{
			var output = new double[arr.H];
			for (var y = 0; y < arr.H; y++)
			{
				var sum                             = 0d;
				for (var x = 0; x < arr.W; x++) sum += arr[x, y];

				output[y] = sum;
			}

			return new CPU_DoubleData(output);
		}

		public IDoubleData ApplyMask(IDoubleData arr, IByteData mask)
		{
			var output                                     = new double[arr.Length];
			for (var x = 0; x < arr.Length; x++) output[x] = arr[x] * mask[x];
			return new CPU_DoubleData(output);
		}

		public IDoubleData2D ApplyMask(IDoubleData2D arr, IByteData2D mask)
		{
			var output = new double[arr.W, arr.H];
			for (var x = 0; x < arr.W; x++)
			for (var y = 0; x < arr.H; x++)
				output[x, y] = arr[x, y] * mask[x, y];
			return new CPU_DoubleData2D(output);
		}
	}
}
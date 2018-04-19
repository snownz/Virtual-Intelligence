using System;
using System.Collections.Generic;
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
	public class DoubleArray : IArray, IDisposable
	{
		public DoubleArray(int size)
		{
			View  = ProcessingDevice.DoubleData.New(size);
			Cache = ProcessingDevice.DoubleData.New(size);
		}

		public DoubleArray(double[] data)
		{
			View  = ProcessingDevice.DoubleData.New(data);
			Cache = ProcessingDevice.DoubleData.New(Length);
		}

		public DoubleArray(IDoubleData data)
		{
			View  = data;
			Cache = ProcessingDevice.DoubleData.New(Length);
		}

		public IDoubleData View { get; }

		public IDoubleData Cache { get; }

		public double this[int x]
		{
			get
			{
				if (x < 0) x = Length - x;
				return View[x];
			}
			set
			{
				if (x < 0) x = Length - x;

				View[x] = value;
			}
		}

		public DoubleArrayT T => new DoubleArrayT(View);

		public int Length => View.Length;

		public void Dispose()
		{
		}

		public static DoubleArray operator *(DoubleArray v0, DoubleArray v1)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_mult_V(v0.Cache, v0.View, v1.View));
		}

		public static DoubleArray operator /(DoubleArray v0, DoubleArray v1)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_div_V(v0.Cache, v0.View, v1.View));
		}

		public static DoubleArray operator +(DoubleArray v0, DoubleArray v1)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_add_V(v0.Cache, v0.View, v1.View));
		}

		public static DoubleArray operator -(DoubleArray v0, DoubleArray v1)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_sub_V(v0.Cache, v0.View, v1.View));
		}

		public static DoubleArray operator *(DoubleArray v0, double c)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_mult_C(v0.Cache, v0.View, c));
		}

		public static DoubleArray operator *(double c, DoubleArray v0)
		{
			return v0 * c;
		}

		public static DoubleArray operator /(DoubleArray v0, double c)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_div_C(v0.Cache, v0.View, c));
		}

		public static DoubleArray operator /(double c, DoubleArray v0)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_div_C(v0.Cache, c, v0.View));
		}

		public static DoubleArray operator +(DoubleArray v0, double c)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_add_C(v0.Cache, v0.View, c));
		}

		public static DoubleArray operator +(double c, DoubleArray v0)
		{
			return v0 + c;
		}

		public static DoubleArray operator -(DoubleArray v0, double c)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_sub_C(v0.Cache, v0.View, c));
		}

		public static DoubleArray operator -(double c, DoubleArray v0)
		{
			return new DoubleArray(ProcessingDevice.DoubleExecutor.V_sub_C(v0.Cache, c, v0.View));
		}

		public static DoubleArray operator >=(DoubleArray v0, double c)
		{
			var output                                    = new double[v0.Length];
			for (var x = 0; x < v0.Length; x++) output[x] = v0[x] >= c ? 1 : 0;
			return new DoubleArray(output);
		}

		public static DoubleArray operator <=(DoubleArray v0, double c)
		{
			throw new NotImplementedException();
		}

		public List<double> ToList()
		{
			var lt = new List<double>();
			for (var x = 0; x < Length; x++) lt.Add(View[x]);
			return lt;
		}

		public double[] ToArray()
		{
			var lt                                 = new double[Length];
			for (var x = 0; x < Length; x++) lt[x] = View[x];
			return lt;
		}

		public DoubleArray Clone()
		{
			return new DoubleArray(View.Clone());
		}

		public override string ToString()
		{
			var str                                   = "[";
			for (var i = 0; i < View.Length; i++) str += $"{View[i].ToString().Replace(",", ".")}, ";
			str                                       =  str.Remove(str.Length - 2);
			str                                       += "]";
			return str;
		}
	}

	public class DoubleArrayT
	{
		public DoubleArrayT(IDoubleData data)
		{
			View = data;
		}

		public IDoubleData View { get; }

		public double this[int x]
		{
			get => View[x];
			set => View[x] = value;
		}

		public int Length => View.Length;

		public static DoubleArray2D operator *(DoubleArrayT vt, DoubleArray v)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.VT_mult_V(vt.View, v.View));
		}

		public static DoubleArray2D operator *(DoubleArray v, DoubleArrayT vt)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.V_mult_VT(v.View, vt.View));
		}

		public static DoubleArray2D operator *(DoubleArrayT vt, DoubleArray2D m)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.VT_mult_M(m.Cache, vt.View, m.View));
		}
	}
}
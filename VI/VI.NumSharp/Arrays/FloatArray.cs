using System;
using System.Collections.Generic;
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
	public class FloatArray : IArray, IDisposable
	{
		public FloatArray(int size)
		{
			View  = ProcessingDevice.FloatData.New(size);
			Cache = ProcessingDevice.FloatData.New(size);
		}

		public FloatArray(float[] data)
		{
			View  = ProcessingDevice.FloatData.New(data);
			Cache = ProcessingDevice.FloatData.New(Length);
		}

		public FloatArray(IFloatData data)
		{
			View  = data;
			Cache = ProcessingDevice.FloatData.New(Length);
		}

		public IFloatData View { get; }

		public IFloatData Cache { get; }

		public float this[int x]
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

		public FloatArrayT T => new FloatArrayT(View);

		public int Length => View.Length;

		public void Dispose()
		{
		}

		public static FloatArray operator *(FloatArray v0, FloatArray v1)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_mult_V(v0.Cache, v0.View, v1.View));
		}

		public static FloatArray operator /(FloatArray v0, FloatArray v1)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_div_V(v0.Cache, v0.View, v1.View));
		}

		public static FloatArray operator +(FloatArray v0, FloatArray v1)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_add_V(v0.Cache, v0.View, v1.View));
		}

		public static FloatArray operator -(FloatArray v0, FloatArray v1)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_sub_V(v0.Cache, v0.View, v1.View));
		}

		public static FloatArray operator *(FloatArray v0, float c)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_mult_C(v0.Cache, v0.View, c));
		}

		public static FloatArray operator *(float c, FloatArray v0)
		{
			return v0 * c;
		}

		public static FloatArray operator /(FloatArray v0, float c)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_div_C(v0.Cache, v0.View, c));
		}

		public static FloatArray operator /(float c, FloatArray v0)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_div_C(v0.Cache, c, v0.View));
		}

		public static FloatArray operator +(FloatArray v0, float c)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_add_C(v0.Cache, v0.View, c));
		}

		public static FloatArray operator +(float c, FloatArray v0)
		{
			return v0 + c;
		}

		public static FloatArray operator -(FloatArray v0, float c)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_sub_C(v0.Cache, v0.View, c));
		}

		public static FloatArray operator -(float c, FloatArray v0)
		{
			return new FloatArray(ProcessingDevice.FloatExecutor.V_sub_C(v0.Cache, c, v0.View));
		}

		public static FloatArray operator >=(FloatArray v0, float c)
		{
			var output                                    = new float[v0.Length];
			for (var x = 0; x < v0.Length; x++) output[x] = v0[x] >= c ? 1 : 0;
			return new FloatArray(output);
		}

		public static FloatArray operator <=(FloatArray v0, float c)
		{
			throw new NotImplementedException();
		}

		public List<float> ToList()
		{
			var lt = new List<float>();
			for (var x = 0; x < Length; x++) lt.Add(View[x]);
			return lt;
		}

		public float[] ToArray()
		{
			var lt                                 = new float[Length];
			for (var x = 0; x < Length; x++) lt[x] = View[x];
			return lt;
		}

		public FloatArray Clone()
		{
			return new FloatArray(View.Clone());
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

	public class FloatArrayT
	{
		public FloatArrayT(IFloatData data)
		{
			View = data;
		}

		public IFloatData View { get; }

		public float this[int x]
		{
			get => View[x];
			set => View[x] = value;
		}

		public int Length => View.Length;

		public static FloatArray2D operator *(FloatArrayT vt, FloatArray v)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.VT_mult_V(vt.View, v.View));
		}

		public static FloatArray2D operator *(FloatArray v, FloatArrayT vt)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.V_mult_VT(v.View, vt.View));
		}

		public static FloatArray2D operator *(FloatArrayT vt, FloatArray2D m)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.VT_mult_M(m.Cache, vt.View, m.View));
		}
	}
}
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
	public class FloatArray2D : IArray2D
	{
		public FloatArray2D(IFloatData2D data)
		{
			View  = data;
			//Cache = data;
			Cache = ProcessingDevice.FloatData.New(W, H);
		}

		public FloatArray2D(int w, int h)
		{
			View  = ProcessingDevice.FloatData.New(w, h);
			Cache = ProcessingDevice.FloatData.New(w, h);
		}

		public FloatArray2D(float[,] data)
		{
			View  = ProcessingDevice.FloatData.New(data);
			Cache = ProcessingDevice.FloatData.New(W, H);
		}
		
		public FloatArray2D Cleanup()
		{
			return new FloatArray2D(View) {View = this.View, Cache = ProcessingDevice.FloatData.New(W, H)};
		}

		public IFloatData2D View { get; private set; }

		public IFloatData2D Cache { get; private set;}

		public float this[int x, int y]
		{
			get => View[x, y];
			set => View[x, y] = value;
		}

		public (int w, int h) Size => (W, H);

		public FloatArray2DT T => new FloatArray2DT(View);

		public int W => View.W;
		public int H => View.H;

		public static FloatArray2D operator *(FloatArray2D m0, FloatArray2D m1)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_M(m0.Cache, m0.View, m1.View));
		}

		public static FloatArray2D operator /(FloatArray2D m0, FloatArray2D m1)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_div_M(m0.Cache, m0.View, m1.View));
		}

		public static FloatArray2D operator -(FloatArray2D m0, FloatArray2D m1)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_sub_M(m0.Cache, m0.View, m1.View));
		}

		public static FloatArray2D operator +(FloatArray2D m0, FloatArray2D m1)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_add_M(m0.Cache, m0.View, m1.View));
		}

		public static FloatArray2D operator *(FloatArray2D m, FloatArrayT v)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_VT(m.Cache, m.View, v.View));
		}

		public static FloatArray2D operator *(FloatArray2D m, FloatArray v)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_V(m.Cache, m.View, v.View));
		}

		public static FloatArray2D operator *(FloatArray v, FloatArray2D m)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.V_mult_M(m.Cache, v.View, m.View));
		}

		public static FloatArray2D operator *(FloatArray2D m, float c)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_C(m.Cache, m.View, c));
		}

		public static FloatArray2D operator *(float c, FloatArray2D m)
		{
			return m * c;
		}

		public static FloatArray2D operator +(FloatArray2D m, float c)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_add_C(m.Cache, m.View, c));
		}

		public static FloatArray2D operator /(FloatArray2D m, int c)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_div_C(m.Cache, m.View, c));
		}

		public static FloatArray2D operator /(int c, FloatArray2D m)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.C_div_M(m.Cache, m.View, c));
		}

		public static FloatArray2D operator /(float c, FloatArray2D m)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.C_div_M(m.Cache, m.View, c));
		}

		public override string ToString()
		{
			var str = "[";
			for (var j = 0; j < H; j++)
			{
				str                             += "[";
				for (var i = 0; i < W; i++) str += $"{View[i, j].ToString().Replace(",", ".")}, ";
				str                             =  str.Remove(str.Length - 2);
				str                             += "],";
			}

			str =  str.Remove(str.Length - 1);
			str += "]";
			return str;
		}
	}

	public class FloatArray2DT
	{
		public FloatArray2DT(IFloatData2D data)
		{
			View = data;
		}

		public IFloatData2D View { get; }

		public float this[int x, int y]
		{
			get => View[x, y];
			set => View[x, y] = value;
		}

		public (int w, int h) Size => (W, H);

		public int W => View.W;
		public int H => View.H;

		public static FloatArray2D operator *(FloatArray2DT mt, FloatArray2D m)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_MT(mt.View, m.View));
		}

		public static FloatArray2D operator *(FloatArray2D m, FloatArray2DT mt)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.MT_mult_M(mt.View, m.View));
		}
	}
}
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
	public class DoubleArray2D : IArray2D
	{
		public DoubleArray2D(IDoubleData2D data)
		{
			View  = data;
			Cache = ProcessingDevice.DoubleData.New(W, H);
		}

		public DoubleArray2D(int w, int h)
		{
			View  = ProcessingDevice.DoubleData.New(w, h);
			Cache = ProcessingDevice.DoubleData.New(w, h);
		}

		public DoubleArray2D(double[,] data)
		{
			View  = ProcessingDevice.DoubleData.New(data);
			Cache = ProcessingDevice.DoubleData.New(W, H);
		}

		public IDoubleData2D View { get; }

		public IDoubleData2D Cache { get; }

		public double this[int x, int y]
		{
			get => View[x, y];
			set => View[x, y] = value;
		}

		public (int w, int h) Size => (W, H);

		public DoubleArray2DT T => new DoubleArray2DT(View);

		public int W => View.W;
		public int H => View.H;

		public static DoubleArray2D operator *(DoubleArray2D m0, DoubleArray2D m1)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_mult_M(m0.Cache, m0.View, m1.View));
		}

		public static DoubleArray2D operator /(DoubleArray2D m0, DoubleArray2D m1)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_div_M(m0.Cache, m0.View, m1.View));
		}

		public static DoubleArray2D operator -(DoubleArray2D m0, DoubleArray2D m1)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_sub_M(m0.Cache, m0.View, m1.View));
		}

		public static DoubleArray2D operator +(DoubleArray2D m0, DoubleArray2D m1)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_add_M(m0.Cache, m0.View, m1.View));
		}

		public static DoubleArray2D operator *(DoubleArray2D m, DoubleArrayT v)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_mult_VT(m.Cache, m.View, v.View));
		}

		public static DoubleArray2D operator *(DoubleArray2D m, DoubleArray v)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_mult_V(m.Cache, m.View, v.View));
		}

		public static DoubleArray2D operator *(DoubleArray v, DoubleArray2D m)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.V_mult_M(m.Cache, v.View, m.View));
		}

		public static DoubleArray2D operator *(DoubleArray2D m, double c)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_mult_C(m.Cache, m.View, c));
		}

		public static DoubleArray2D operator *(double c, DoubleArray2D m)
		{
			return m * c;
		}

		public static DoubleArray2D operator +(DoubleArray2D m, double c)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_add_C(m.Cache, m.View, c));
		}

		public static DoubleArray2D operator /(DoubleArray2D m, int c)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_div_C(m.Cache, m.View, c));
		}

		public static DoubleArray2D operator /(int c, DoubleArray2D m)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.C_div_M(m.Cache, m.View, c));
		}

		public static DoubleArray2D operator /(double c, DoubleArray2D m)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.C_div_M(m.Cache, m.View, c));
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

	public class DoubleArray2DT
	{
		public DoubleArray2DT(IDoubleData2D data)
		{
			View = data;
		}

		public IDoubleData2D View { get; }

		public double this[int x, int y]
		{
			get => View[x, y];
			set => View[x, y] = value;
		}

		public (int w, int h) Size => (W, H);

		public int W => View.W;
		public int H => View.H;

		public static DoubleArray2D operator *(DoubleArray2DT mt, DoubleArray2D m)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.M_mult_MT(mt.View, m.View));
		}

		public static DoubleArray2D operator *(DoubleArray2D m, DoubleArray2DT mt)
		{
			return new DoubleArray2D(ProcessingDevice.DoubleExecutor.MT_mult_M(mt.View, m.View));
		}
	}
}
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
    public class FloatArray2D : IArray2D
	{
		private IFloatData2D _view;
		private readonly IFloatData2D _cache;

        public IFloatData2D View
        {
            get { return _view; }
            set { _view = value; }
        }
        public IFloatData2D Cache => _cache;

        public FloatArray2D()
        {            
        }

        public FloatArray2D(IFloatData2D data)
		{
			_view = data;
            _cache = ProcessingDevice.FloatData.New(W, H); 
        }
		public FloatArray2D(int w, int h)
		{
			_view = ProcessingDevice.FloatData.New(w, h);
            _cache = ProcessingDevice.FloatData.New(w, h);
        }
		public FloatArray2D(float[,] data)
		{
			_view = ProcessingDevice.FloatData.New(data);
            _cache = ProcessingDevice.FloatData.New(W, H);
        }

		public float this[int x, int y]
		{
			get => _view[x, y];
			set => _view[x, y] = value;
		}
		
		public (int w, int h) Size => (W, H);

		public FloatArray2DT T => new FloatArray2DT(_view);

		public int W => _view.W;
		public int H => _view.H;

		public static FloatArray2D operator *(FloatArray2D m0, FloatArray2D m1)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_M(ProcessingDevice.FloatData.New(m0.W, m0.H), m0.View, m1.View));
		}
		public static FloatArray2D operator /(FloatArray2D m0, FloatArray2D m1)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_div_M(ProcessingDevice.FloatData.New(m0.W, m0.H), m0.View, m1.View));
		}
		public static FloatArray2D operator -(FloatArray2D m0, FloatArray2D m1)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_sub_M(ProcessingDevice.FloatData.New(m0.W, m0.H), m0.View, m1.View));
		}
        public static FloatArray2D operator +(FloatArray2D m0, FloatArray2D m1)
        {
            return new FloatArray2D(ProcessingDevice.FloatExecutor.M_add_M(ProcessingDevice.FloatData.New(m0.W, m0.H), m0.View, m1.View));
        }

		public static FloatArray2D operator *(FloatArray2D m, FloatArrayT v)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.VT_mult_M(ProcessingDevice.FloatData.New(m.W, m.H), v.View, m.View));
		}
		public static FloatArray2D operator *(FloatArray2D m, FloatArray v)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_V(ProcessingDevice.FloatData.New(m.W, m.H), m.View, v.View));
		}
		public static FloatArray2D operator *(FloatArray v, FloatArray2D m)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_V(ProcessingDevice.FloatData.New(m.W, m.H), m.View, v.View));
		}
		
        public static FloatArray2D operator *(FloatArray2D m, float c)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_C(ProcessingDevice.FloatData.New(m.W, m.H), m.View, c));
		}
		public static FloatArray2D operator *(float c, FloatArray2D m)
		{
			return m * c;
		}
		public static FloatArray2D operator +(FloatArray2D m, float c)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_add_C(ProcessingDevice.FloatData.New(m.W, m.H), m.View, c));
		}
        public static FloatArray2D operator /(FloatArray2D m, int c)
        {
            return new FloatArray2D(ProcessingDevice.FloatExecutor.M_div_C(ProcessingDevice.FloatData.New(m.W, m.H), m.View, c));
        }
        public static FloatArray2D operator /(int c, FloatArray2D m)
        {
            return new FloatArray2D(ProcessingDevice.FloatExecutor.C_div_M(ProcessingDevice.FloatData.New(m.W, m.H), m.View, c));
        }
        public static FloatArray2D operator /(float c, FloatArray2D m)
        {
            return new FloatArray2D(ProcessingDevice.FloatExecutor.C_div_M(ProcessingDevice.FloatData.New(m.W, m.H), m.View, c));
        }
        
        public override string ToString()
		{
			var str = "[";
			for (var j = 0; j < H; j++)
			{
				str                             += "[";
				for (var i = 0; i < W; i++) str += $"{_view[i, j].ToString().Replace(",", ".")}, ";
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
		private readonly IFloatData2D _view;
		
		public IFloatData2D View => _view;

		public FloatArray2DT(IFloatData2D data)
		{
			_view = data;
		}
		
		public float this[int x, int y]
		{
			get => _view[x, y];
			set => _view[x, y] = value;
		}

		public (int w, int h) Size => (W, H);

		public int W => _view.W;
		public int H => _view.H;
		
		public static FloatArray2D operator *(FloatArray2DT mt, FloatArray2D m)
		{
			return new FloatArray2D(ProcessingDevice.FloatExecutor.M_mult_MT(mt.View, m.View));
		}
		public static FloatArray2D operator *(FloatArray2D m, FloatArray2DT mt)
		{
            return mt * m;
		}
	}
}
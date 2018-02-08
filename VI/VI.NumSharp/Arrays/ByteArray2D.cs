using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
	public class ByteArray2D
	{
		private readonly IByteData2D _view;
		public IByteData2D View => _view;

		public ByteArray2D(IByteData2D data)
		{
			_view = data;
		}
		public ByteArray2D(int w, int h)
		{
			_view = ProcessingDevice.ByteData.New(w, h);
		}
		public ByteArray2D(byte[,] data)
		{
			_view = ProcessingDevice.ByteData.New(data);
		}

		public byte this[int x, int y]
		{
			get => _view[x, y];
			set => _view[x, y] = value;
		}
		
		public (int w, int h) Size => (W, H);

		public int W => _view.W;
		public int H => _view.H;

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
}
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
	public class ByteArray2D
	{
		public ByteArray2D(IByteData2D data)
		{
			View = data;
		}

		public ByteArray2D(int w, int h)
		{
			View = ProcessingDevice.ByteData.New(w, h);
		}

		public ByteArray2D(byte[,] data)
		{
			View = ProcessingDevice.ByteData.New(data);
		}

		public IByteData2D View { get; }

		public byte this[int x, int y]
		{
			get => View[x, y];
			set => View[x, y] = value;
		}

		public (int w, int h) Size => (W, H);

		public int W => View.W;
		public int H => View.H;

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
}
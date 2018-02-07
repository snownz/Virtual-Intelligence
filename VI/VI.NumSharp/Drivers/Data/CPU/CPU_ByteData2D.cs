using System.Collections.Generic;

namespace VI.NumSharp.Drivers.Data.CPU
{
	public class CPU_ByteData2D: IByteData2D
	{
		private byte[,] _view;

		public CPU_ByteData2D()
		{
			
		}
		
		public CPU_ByteData2D(int w, int h)
		{
			_view = new byte[w, h];
		}
		
		public CPU_ByteData2D(byte[,] data)
		{
			_view = data;
		}

		public object View { get; }

		public byte this[int x, int y]
		{
			get => _view[x, y];
			set => _view[x, y] = value;
		}

		public IEnumerable<int> AxesX { get; }
		public IEnumerable<int> AxesY { get; }

		public byte[,] AsArray2D()
		{
			return _view;
		}

		public byte[,] AsArray()
		{
			throw new System.NotImplementedException();
		}

		public int W => _view.GetLength(0);
		public int H => _view.GetLength(1);
		
		public byte[,] Clone()
		{
			return _view.Clone() as byte[,];
		}
	}
}
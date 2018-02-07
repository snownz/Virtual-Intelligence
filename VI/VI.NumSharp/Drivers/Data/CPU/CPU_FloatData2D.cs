using System.Collections.Generic;
using System.Linq;

namespace VI.NumSharp.Drivers.Data.CPU
{
	public class CPU_FloatData2D : IFloatData2D
	{
		private float[,] _view;

		public CPU_FloatData2D()
		{
			
		}
		
		public CPU_FloatData2D(int w, int h)
		{
			_view = new float[w, h];
			AxesX = Enumerable.Range(0, w);
			AxesY = Enumerable.Range(0, h);
		}
		
		public CPU_FloatData2D(float[,] data)
		{
			_view = data;
			AxesX = Enumerable.Range(0, data.GetLength(0));
			AxesY = Enumerable.Range(0, data.GetLength(1));
		}

		public object View { get; }

		public float this[int x, int y]
		{
			get => _view[x, y];
			set => _view[x, y] = value;
		}

		public IEnumerable<int> AxesX { get; }
		public IEnumerable<int> AxesY { get; }

		public float[,] AsArray()
		{
			return _view;
		}

		public int W => _view.GetLength(0);
		public int H => _view.GetLength(1);
		
		public float[,] Clone()
		{
			return _view.Clone() as float[,];
		}
	}
}
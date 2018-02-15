using System;
using System.Collections.Generic;
using System.Linq;
using ILGPU;

namespace VI.NumSharp.Drivers.Data.CPU
{
	public class CPU_DoubleData2D : IDoubleData2D
	{
		private readonly double[,] _view;

		public CPU_DoubleData2D()
		{
		}

		public CPU_DoubleData2D(int w, int h)
		{
			_view = new double[w, h];
			AxesX = Enumerable.Range(0, w);
			AxesY = Enumerable.Range(0, h);
		}

		public CPU_DoubleData2D(double[,] data)
		{
			_view = data;
			AxesX = Enumerable.Range(0, data.GetLength(0));
			AxesY = Enumerable.Range(0, data.GetLength(1));
		}

		public ArrayView2D<double> View { get; }

		public double this[int x, int y]
		{
			get => _view[x, y];
			set => _view[x, y] = value;
		}

		public IEnumerable<int> AxesX { get; }
		public IEnumerable<int> AxesY { get; }

		public double[,] AsArray2D()
		{
			return _view;
		}

		public double[] AsArray()
		{
			throw new NotImplementedException();
		}

		public int W => _view.GetLength(0);
		public int H => _view.GetLength(1);

		public double[,] Clone()
		{
			return _view.Clone() as double[,];
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using ILGPU;

namespace VI.NumSharp.Drivers.Data.CPU
{
	public class CPU_DoubleData : IDoubleData
	{
		private readonly double[] _view;

		public CPU_DoubleData()
		{
		}

		public CPU_DoubleData(int size)
		{
			_view = new double[size];
			AxesX = Enumerable.Range(0, size);
		}

		public CPU_DoubleData(double[] data)
		{
			_view = data;
			AxesX = Enumerable.Range(0, data.Length);
		}

		public ArrayView<double> View { get; }

		public double this[int x]
		{
			get => _view[x];
			set => _view[x] = value;
		}

		public IEnumerable<int> AxesX { get; }

		public double[] AsArray()
		{
			return _view;
		}

		public int Length => _view.Length;

		public double[] Clone()
		{
			return _view.Clone() as double[];
		}
	}
}
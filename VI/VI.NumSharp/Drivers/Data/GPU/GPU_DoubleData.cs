using System.Collections.Generic;
using System.Linq;
using ILGPU;
using ILGPU.Runtime;

namespace VI.NumSharp.Drivers.Data.GPU
{
	public class GPU_DoubleData : IDoubleData
	{
		private readonly MemoryBuffer<double> _view;

		public GPU_DoubleData()
		{
		}

		public GPU_DoubleData(int size)
		{
			_view = ILGPUMethods.Allocate<double>(size);
			AxesX = Enumerable.Range(0, size);
		}

		public GPU_DoubleData(MemoryBuffer<double> data)
		{
			_view = data;
			AxesX = Enumerable.Range(0, data.Length);
		}

		public GPU_DoubleData(double[] data)
		{
			_view = ILGPUMethods.Allocate(data);
			AxesX = Enumerable.Range(0, data.Length);
		}

		public ArrayView<double> View => _view.View;

		public double this[int x]
		{
			get => _view[x];
			set => _view[x] = value;
		}

		public IEnumerable<int> AxesX { get; }

		public double[] AsArray()
		{
			return _view.GetAsArray();
		}

		public int Length => _view.Length;

		public double[] Clone()
		{
			return ILGPUMethods.Clone(_view).GetAsArray();
		}
	}
}
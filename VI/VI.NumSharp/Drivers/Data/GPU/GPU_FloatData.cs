using System.Collections.Generic;
using System.Linq;
using ILGPU.Runtime;

namespace VI.NumSharp.Drivers.Data.GPU
{
	public class GPU_FloatData : IFloatData
	{
		private MemoryBuffer<float> _view;

		public GPU_FloatData()
		{
			
		}
		
		public GPU_FloatData(int size)
		{
			_view = ILGPUMethods.Allocate<float>(size);
			AxesX = Enumerable.Range(0, size);
		}
		
		public GPU_FloatData(MemoryBuffer<float> data)
		{
			_view = data;
			AxesX = Enumerable.Range(0, data.Length);
		}
		
		public GPU_FloatData(float[] data)
		{
			_view = ILGPUMethods.Allocate(data);
			AxesX = Enumerable.Range(0, data.Length);
		}

		public object View => _view.View;

		public float this[int x]
		{
			get => _view[x];
			set => _view[x] = value;
		}

		public IEnumerable<int> AxesX { get; }

		public float[] AsArray()
		{
			return _view.GetAsArray();
		}

		public int Length => _view.Length;
		
		public float[] Clone()
		{
			return ILGPUMethods.Clone(_view).GetAsArray();
		}
	}
}
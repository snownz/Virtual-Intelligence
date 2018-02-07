using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILGPU;
using ILGPU.Runtime;

namespace VI.NumSharp.Drivers.Data.GPU
{
	public class GPU_FloatData2D : IFloatData2D
	{
		private MemoryBuffer2D<float> _view;

		public GPU_FloatData2D()
		{
			
		}
		
		public GPU_FloatData2D(int w, int h)
		{
			_view = ILGPUMethods.Allocate<float>(new Index2(w, h));
			AxesX = Enumerable.Range(0, w);
			AxesY = Enumerable.Range(0, h);
		}
		
		public GPU_FloatData2D(float[,] data)
		{
			_view = ILGPUMethods.Allocate(data);
			AxesX = Enumerable.Range(0, data.GetLength(0));
			AxesY = Enumerable.Range(0, data.GetLength(1));
		}

		public object View => _view.View;

		public float this[int x, int y]
		{
			get => _view[new Index2(x, y)];
			set => _view[new Index2(x, y)] = value;
		}

		public IEnumerable<int> AxesX { get; }
		public IEnumerable<int> AxesY { get; }

		public float[,] AsArray()
		{
			var arr = new float[W, H];
			Parallel.ForEach(AxesX, (x) => { Parallel.ForEach(AxesY, (y) => { arr[y, x] = this[x, y]; }); });
			return arr;
		}

		public int W => _view.Width;
		public int H => _view.Height;
		
		public float[,] Clone()
		{
			return AsArray();
		}
	}
}
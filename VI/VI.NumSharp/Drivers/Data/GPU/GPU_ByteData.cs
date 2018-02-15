using System.Collections.Generic;
using System.Linq;
using ILGPU.Runtime;

namespace VI.NumSharp.Drivers.Data.GPU
{
	public class GPU_ByteData : IByteData
	{
		private readonly MemoryBuffer<byte> _view;

		public GPU_ByteData()
		{
		}

		public GPU_ByteData(int size)
		{
			_view = ILGPUMethods.Allocate<byte>(size);
			AxesX = Enumerable.Range(0, size);
		}

		public GPU_ByteData(MemoryBuffer<byte> data)
		{
			_view = data;
			AxesX = Enumerable.Range(0, data.Length);
		}

		public GPU_ByteData(byte[] data)
		{
			_view = ILGPUMethods.Allocate(data);
			AxesX = Enumerable.Range(0, data.Length);
		}

		public object View => _view.View;

		public byte this[int x]
		{
			get => _view[x];
			set => _view[x] = value;
		}

		public IEnumerable<int> AxesX { get; }

		public byte[] AsArray()
		{
			return _view.GetAsArray();
		}

		public int Length => _view.Length;

		public byte[] Clone()
		{
			return ILGPUMethods.Clone(_view).GetAsArray();
		}
	}
}
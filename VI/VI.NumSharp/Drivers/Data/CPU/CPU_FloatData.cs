using System.Collections.Generic;
using System.Linq;
using ILGPU;

namespace VI.NumSharp.Drivers.Data.CPU
{
	public class CPU_FloatData : IFloatData
	{
		private readonly float[] _view;

		public CPU_FloatData()
		{
		}

		public CPU_FloatData(int size)
		{
			_view = new float[size];
			AxesX = Enumerable.Range(0, size);
		}

		public CPU_FloatData(float[] data)
		{
			_view = data;
			AxesX = Enumerable.Range(0, data.Length);
		}

		public ArrayView<float> View { get; }

		public float this[int x]
		{
			get => _view[x];
			set => _view[x] = value;
		}

		public IEnumerable<int> AxesX { get; }

		public float[] AsArray()
		{
			return _view;
		}

		public int Length => _view.Length;

		public float[] Clone()
		{
			return _view.Clone() as float[];
		}
	}
}
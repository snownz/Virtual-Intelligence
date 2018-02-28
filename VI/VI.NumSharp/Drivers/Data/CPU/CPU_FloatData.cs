using ILGPU;
using System.Collections.Generic;
using System.Linq;

namespace VI.NumSharp.Drivers.Data.CPU
{
	public class CPU_FloatData : IFloatData
	{
		private float[] _view;

		public CPU_FloatData()
		{
			
		}
		
		public CPU_FloatData(int size)
		{
			_view = new float[size];
			AxesX = Enumerable.Range(0, size).ToArray();
		}
		
		public CPU_FloatData(float[] data)
		{
			_view = data;
			AxesX = Enumerable.Range(0, data.Length).ToArray();
		}

		public ArrayView<float> MemoryView { get; }

		public float this[int x]
		{
			get => _view[x];
			set => _view[x] = value;
		}

		public int[] AxesX { get; }

		public float[] AsArray()
		{
			return _view;
		}

		public int Length => _view.Length;

        public float[] View { get => _view; set => _view = value; }

        public float[] Clone()
		{
			return _view.Clone() as float[];
		}
	}
}
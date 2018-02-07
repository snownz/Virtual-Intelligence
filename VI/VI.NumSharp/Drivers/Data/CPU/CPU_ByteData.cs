using System.Collections.Generic;

namespace VI.NumSharp.Drivers.Data.CPU
{
	public class CPU_ByteData: IByteData
	{
		private byte[] _view;

		public CPU_ByteData()
		{
			
		}
		
		public CPU_ByteData(int size)
		{
			_view = new byte[size];
		}
		
		public CPU_ByteData(byte[] data)
		{
			_view = data;
		}
		
		public byte this[int x]
		{
			get => _view[x];
			set => _view[x] = value;
		}

		public IEnumerable<int> AxesX { get; }

		public byte[] AsArray()
		{
			return _view;
		}

		public int Length => _view.Length;
		
		public byte[] Clone()
		{
			return _view.Clone() as byte[];
		}
	}
}
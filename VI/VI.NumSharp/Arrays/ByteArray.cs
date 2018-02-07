using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
	public class ByteArray : IDisposable
	{
		private readonly IByteData _view;

		public IByteData View => _view;
	
		
		public ByteArray(int size)
		{
			_view = ProcessingDevice.ByteData.New(size);
			AxesX = Enumerable.Range(0, size);
		}

		public ByteArray(byte[] data)
		{
			_view = ProcessingDevice.ByteData.New(data);;
			AxesX = Enumerable.Range(0, data.Length);
		}

		public byte this[int x]
		{
			get
			{
				if (x < 0) x = Length - x;
				return _view[x];
			}
			set
			{
				if (x < 0) x = Length - x;

				_view[x] = value;
			}
		}

		public IEnumerable<int> AxesX { get; }

		public int        Length => _view.Length;
		public ByteArrayT T      => new ByteArrayT(_view, AxesX);

		public List<byte> ToList()
		{
			var lt = new List<byte>();
			Parallel.ForEach(AxesX, (i) => { lt.Add(_view[i]); });
			return lt;
		}

		public byte[] ToArray()
		{
			var lt                                 = new byte[Length];
			Parallel.ForEach(AxesX, (i) => { lt[i] = _view[i]; });
			return lt;
		}

		public ByteArray Clone()
		{
			return new ByteArray(_view.Clone() as byte[]);
		}

		public override string ToString()
		{
			var str                                    = "[";
			for (var i = 0; i < _view.Length; i++) str += $"{_view[i].ToString().Replace(",", ".")}, ";
			str                                        =  str.Remove(str.Length - 2);
			str                                        += "]";
			return str;
		}

		public void Dispose()
		{
		}
	}

	public class ByteArrayT
	{
		private readonly IByteData _view;
		
		public IByteData View => _view;

		public IEnumerable<int> AxesX { get; }
		
		public ByteArrayT(IByteData data, IEnumerable<int> ax)
		{
			_view = data;
			AxesX = ax;
		}

		public byte this[int x]
		{
			get => _view[x];
			set => _view[x] = value;
		}

		public int Length => _view.Length;
	}
}
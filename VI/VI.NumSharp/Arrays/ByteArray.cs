using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VI.NumSharp.Drivers;

namespace VI.NumSharp.Arrays
{
	public class ByteArray : IDisposable
	{
		public ByteArray(int size)
		{
			View  = ProcessingDevice.ByteData.New(size);
			AxesX = Enumerable.Range(0, size);
		}

		public ByteArray(byte[] data)
		{
			View = ProcessingDevice.ByteData.New(data);
			;
			AxesX = Enumerable.Range(0, data.Length);
		}

		public IByteData View { get; }

		public byte this[int x]
		{
			get
			{
				if (x < 0) x = Length - x;
				return View[x];
			}
			set
			{
				if (x < 0) x = Length - x;

				View[x] = value;
			}
		}

		public IEnumerable<int> AxesX { get; }

		public int Length => View.Length;
		public ByteArrayT T => new ByteArrayT(View, AxesX);

		public void Dispose()
		{
		}

		public List<byte> ToList()
		{
			var lt = new List<byte>();
			Parallel.ForEach(AxesX, i => { lt.Add(View[i]); });
			return lt;
		}

		public byte[] ToArray()
		{
			var lt                               = new byte[Length];
			Parallel.ForEach(AxesX, i => { lt[i] = View[i]; });
			return lt;
		}

		public ByteArray Clone()
		{
			return new ByteArray(View.Clone());
		}

		public override string ToString()
		{
			var str                                   = "[";
			for (var i = 0; i < View.Length; i++) str += $"{View[i].ToString().Replace(",", ".")}, ";
			str                                       =  str.Remove(str.Length - 2);
			str                                       += "]";
			return str;
		}
	}

	public class ByteArrayT
	{
		public ByteArrayT(IByteData data, IEnumerable<int> ax)
		{
			View  = data;
			AxesX = ax;
		}

		public IByteData View { get; }

		public IEnumerable<int> AxesX { get; }

		public byte this[int x]
		{
			get => View[x];
			set => View[x] = value;
		}

		public int Length => View.Length;
	}
}
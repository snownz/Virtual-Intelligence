using VI.NumSharp.Drivers.Data;
using VI.NumSharp.Drivers.Data.GPU;

namespace VI.NumSharp.Drivers.Executor.GPU
{
	public class ByteDataGPU : IByteDataProcess
	{
		public IByteData New(int size)
		{
			return new GPU_ByteData(size);
		}

		public IByteData New(byte[] data)
		{
			return new GPU_ByteData(data);
		}

		public IByteData2D New(int w, int h)
		{
			return new GPU_ByteData2D(w, h);
		}

		public IByteData2D New(byte[,] data)
		{
			return new GPU_ByteData2D(data);
		}
	}
}
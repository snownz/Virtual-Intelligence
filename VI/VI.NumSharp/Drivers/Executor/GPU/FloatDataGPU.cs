using VI.NumSharp.Drivers.Data;
using VI.NumSharp.Drivers.Data.GPU;

namespace VI.NumSharp.Drivers.Executor.GPU
{
	public class FloatDataGPU : IFloatDataProcess
	{
		public IFloatData New(int size)
		{
			return new GPU_FloatData(size);
		}

		public IFloatData New(float[] data)
		{
			return new GPU_FloatData(data);
		}

		public IFloatData2D New(int w, int h)
		{
			return new GPU_FloatData2D(w, h);
		}

		public IFloatData2D New(float[,] data)
		{
			return new GPU_FloatData2D(data);
		}
	}
}
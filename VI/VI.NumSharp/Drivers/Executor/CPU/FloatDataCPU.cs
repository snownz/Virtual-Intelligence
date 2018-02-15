using VI.NumSharp.Drivers.Data;
using VI.NumSharp.Drivers.Data.CPU;

namespace VI.NumSharp.Drivers.Executor.CPU
{
	public class FloatDataCPU : IFloatDataProcess
	{
		public IFloatData New(int size)
		{
			return new CPU_FloatData(size);
		}

		public IFloatData New(float[] data)
		{
			return new CPU_FloatData(data);
		}

		public IFloatData2D New(int w, int h)
		{
			return new CPU_FloatData2D(w, h);
		}

		public IFloatData2D New(float[,] data)
		{
			return new CPU_FloatData2D(data);
		}
	}
}
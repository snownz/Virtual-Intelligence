using VI.NumSharp.Drivers.Data;
using VI.NumSharp.Drivers.Data.GPU;

namespace VI.NumSharp.Drivers.Executor.GPU
{
	public class DoubleDataGPU : IDoubleDataProcess
	{
		public IDoubleData New(int size)
		{
			return new GPU_DoubleData(size);
		}

		public IDoubleData New(double[] data)
		{
			return new GPU_DoubleData(data);
		}

		public IDoubleData2D New(int w, int h)
		{
			return new GPU_DoubleData2D(w, h);
		}

		public IDoubleData2D New(double[,] data)
		{
			return new GPU_DoubleData2D(data);
		}
	}
}
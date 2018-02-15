using VI.NumSharp.Drivers.Data;
using VI.NumSharp.Drivers.Data.CPU;

namespace VI.NumSharp.Drivers.Executor.CPU
{
	public class DoubleDataCPU : IDoubleDataProcess
	{
		public IDoubleData New(int size)
		{
			return new CPU_DoubleData(size);
		}

		public IDoubleData New(double[] data)
		{
			return new CPU_DoubleData(data);
		}

		public IDoubleData2D New(int w, int h)
		{
			return new CPU_DoubleData2D(w, h);
		}

		public IDoubleData2D New(double[,] data)
		{
			return new CPU_DoubleData2D(data);
		}
	}
}
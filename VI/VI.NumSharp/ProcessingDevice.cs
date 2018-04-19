using VI.Maths.Array;
using VI.NumSharp.Drivers;
using VI.NumSharp.Drivers.Data;
using VI.NumSharp.Drivers.Executor.CPU;
using VI.NumSharp.Drivers.Executor.GPU;
using VI.ParallelComputing;
using VI.ParallelComputing.Drivers;

namespace VI.NumSharp
{
	public class ProcessingDevice
	{
		private static DeviceType _device;

		// Float Cuda Device
		private static IAnnParallelInterface _floatCudaArrayDevice;

		// Double Cuda Device
		private static IAnnParallelInterface _doubleCudaArrayDevice;

		private static IAnnParallelInterface CUDAFloatArrayDevice
			=> _floatCudaArrayDevice ?? (_floatCudaArrayDevice = new CudaAnnInterface<FloatArrayOperations>());

		private static IAnnParallelInterface CUDADoubleArrayDevice
			=> _doubleCudaArrayDevice ?? (_doubleCudaArrayDevice = new CudaAnnInterface<FloatArrayOperations>());


		public static DeviceType Device
		{
			get => _device;
			set
			{
				switch (value)
				{
					case DeviceType.CUDA:

						// Float Cuda Device
						FloatArrayDevice = CUDAFloatArrayDevice;
						FloatExecutor    = new ParallelFloatExecutorGPU();
						FloatData        = new FloatDataGPU();

						// Double Cuda Device
						DoubleArrayDevice = CUDADoubleArrayDevice;
						DoubleExecutor    = new ParallelDoubleExecutorGPU();
						DoubleData        = new DoubleDataGPU();

						// Byte Cuda Device
						ByteData = new ByteDataGPU();

						break;
					case DeviceType.CPU:

						// Float Processing
						FloatExecutor = new ParallelFloatExecutorCPU();
						FloatData     = new FloatDataCPU();

						// Double Processing
						DoubleExecutor = new ParallelDoubleExecutorCPU();
						DoubleData     = new DoubleDataCPU();

						// Byte Processing
						ByteData = new ByteDataCPU();

						break;
				}

				_device = value;
			}
		}

		// Float Processing
		public static IAnnParallelInterface FloatArrayDevice { get; private set; }
		public static IFloatArrayExecutor FloatExecutor { get; private set; }
		public static IFloatDataProcess FloatData { get; private set; }

		// Double Processing
		public static IAnnParallelInterface DoubleArrayDevice { get; private set; }
		public static IDoubleArrayExecutor DoubleExecutor { get; private set; }
		public static IDoubleDataProcess DoubleData { get; private set; }

		// Byte Processing
		public static IByteDataProcess ByteData { get; private set; }
	}
}
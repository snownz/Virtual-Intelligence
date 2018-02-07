using VI.Maths.Array;
using VI.NumSharp.Drivers;
using VI.NumSharp.Drivers.Data;
using VI.NumSharp.Drivers.Data.CPU;
using VI.NumSharp.Drivers.Executor;
using VI.NumSharp.Drivers.Executor.CPU;
using VI.NumSharp.Drivers.Executor.GPU;
using VI.ParallelComputing;
using VI.ParallelComputing.Drivers;

namespace VI.NumSharp
{
	public static class ProcessingDevice
	{
		private static DeviceType            _device;
		private static IAnnParallelInterface _cudaArrayDevice;

		public static DeviceType Device
		{
			get => _device;
			set
			{
				switch (value)
				{
					case DeviceType.CUDA:
						ArrayDevice = CUDAArrayDevice;
						FloatExecutor = new ParallelFloatExecutorGPU();
						FloatData     = new FloatDataGPU();
						ByteData      = new ByteDataGPU();
						break;
					case DeviceType.CPU:
						FloatExecutor = new ParallelFloatExecutorCPU();
						FloatData = new FloatDataCPU();
						ByteData = new ByteDataCPU();
						break;
				}

				_device = value;
			}
		}

		private static IAnnParallelInterface CUDAArrayDevice
			=> _cudaArrayDevice ?? (_cudaArrayDevice = new CudaAnnInterface<ArrayOperations>());

		public static IAnnParallelInterface ArrayDevice { get; private set; }
		public static IFloatArrayExecutor FloatExecutor { get; private set; }
		public static IFloatDataProcess FloatData { get; private set; }
		public static IByteDataProcess ByteData { get; private set; }
	}
}
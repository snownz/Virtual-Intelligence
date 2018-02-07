using VI.ParallelComputing;
using VI.ParallelComputing.Drivers;
using VI.Vision.Array;

namespace VI.Vision
{
	public class ImageProcessingDevice
	{
		private static DeviceType            _device;
		private static IAnnParallelInterface _cpuArrayDevice;
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
						break;
					case DeviceType.CPU:
						break;
				}

				_device = value;
			}
		}

		private static IAnnParallelInterface CUDAArrayDevice
			=> _cudaArrayDevice ?? (_cudaArrayDevice = new CudaAnnInterface<colorArrayOperations>());

		public static IAnnParallelInterface ArrayDevice { get; private set; }
	}
}
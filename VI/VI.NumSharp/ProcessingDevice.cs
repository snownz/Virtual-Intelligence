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
        private static IAnnParallelInterface _cudaArrayDevice;
        private static IAnnParallelInterface CUDAFloatArrayDevice
            => _cudaArrayDevice ?? (_cudaArrayDevice = new CudaAnnInterface<FloatArrayOperations>());

        public static DeviceType Device
        {
            get => _device;
            set
            {
                switch (value)
                {
                    case DeviceType.CUDA:
                        FloatArrayDevice = CUDAFloatArrayDevice;
                        FloatExecutor = new ParallelFloatExecutorGPU();
                        FloatData = new FloatDataGPU();
                        ByteData = new ByteDataGPU();
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

        public static IAnnParallelInterface FloatArrayDevice { get; private set; }
        public static IFloatArrayExecutor FloatExecutor { get; private set; }
        public static IFloatDataProcess FloatData { get; private set; }
        public static IByteDataProcess ByteData { get; private set; }
    }
}
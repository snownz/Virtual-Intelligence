using VI.Maths.Array;
using VI.NumSharp.Provider;
using VI.ParallelComputing;
using VI.ParallelComputing.Drivers;

namespace VI.NumSharp
{
    public static partial class ProcessingDevice
    {
        private static DeviceType _device;
        private static IAnnParallelInterface _cpuArrayDevice;
        private static IAnnParallelInterface _cudaArrayDevice;

        private static IAnnParallelInterface ParallelCPUArrayDevice
            => _cpuArrayDevice ?? (_cpuArrayDevice = new ParallelCpuAnnInterface<ArrayOperations>());

        private static IAnnParallelInterface LinearCPUArrayDevice
            => _cpuArrayDevice ?? (_cpuArrayDevice = new LinearCpuAnnInterface());

        private static IAnnParallelInterface CUDAArrayDevice
            => _cudaArrayDevice ?? (_cudaArrayDevice = new CudaAnnInterface<ArrayOperations>());

        public static DeviceType Device
        {
            get { return _device; }
            set
            {
                switch (value)
                {
                    case DeviceType.CUDA:
                        ArrayDevice = CUDAArrayDevice;
                        ArrayExecutor = new ParallelArrayExecutor();
                        break;
                    case DeviceType.PARALLEL_CPU:
                        ArrayDevice = ParallelCPUArrayDevice;
                        ArrayExecutor = new ParallelArrayExecutor();
                        break;
                    case DeviceType.SINGLE_CPU:
                        ArrayDevice = LinearCPUArrayDevice;
                        ArrayExecutor = new LinearArrayExecutor();
                        break;
                }
                _device = value;
            }
        }        
        public static IAnnParallelInterface ArrayDevice { get; private set; }
        public static IArrayExecutor ArrayExecutor { get; private set; }
    }    
}

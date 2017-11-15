using VI.Maths.Array;
using VI.ParallelComputing.Drivers;

namespace VI.NumSharp
{
    public enum Device
    {
        CUDA,
        CPU
    }
    
    public static partial class ProcessingDevice
    {
        private static Device _device;
        private static IAnnParallelInterface _cpuArrayDevice;
        private static IAnnParallelInterface _cudaArrayDevice;

        public static Device Device
        {
            get { return _device; }
            set
            {
                switch (value)
                {
                    case Device.CUDA:
                        ArrayDevice = CUDAArrayDevice;
                        break;
                    case Device.CPU:
                        ArrayDevice = CPUArrayDevice;
                        break;
                }
                _device = value;
            }
        }

        private static IAnnParallelInterface CPUArrayDevice
            => _cpuArrayDevice ?? (_cpuArrayDevice = new CpuAnnInterface<ArrayOperations>());
        private static IAnnParallelInterface CUDAArrayDevice
            => _cudaArrayDevice ?? (_cudaArrayDevice = new CudaAnnInterface<ArrayOperations>());

        public static IAnnParallelInterface ArrayDevice { get; private set; }
    }    
}

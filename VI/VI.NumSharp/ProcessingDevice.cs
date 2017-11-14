using VI.Maths.Array;
using VI.Maths.LogisticFunctions;
using VI.Maths.LossFunctions;
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

        public static Device Device
        {
            get { return _device; }
            set
            {
                switch (value)
                {
                    case Device.CUDA:
                        ArrayDevice = CUDAArrayDevice;
                        SigmoidDevice = CUDASigmoidDevice;
                        TANHDevice = CUDATANHDevice;
                        LeakReluDevice = CUDALeakReluDevice;
                        SquaredLossDevice = CUDASquaredLossDevice;
                        break;
                    case Device.CPU:
                        ArrayDevice = CPUArrayDevice;
                        SigmoidDevice = CPUSigmoidDevice;
                        TANHDevice = CPUTANHDevice;
                        LeakReluDevice = CPULeakReluDevice;
                        SquaredLossDevice = CPUSquaredLossDevice;
                        break;
                }
                _device = value;
            }
        }
    }

    public static partial class ProcessingDevice
    {
        private static IAnnParallelInterface _cpuArrayDevice;
        private static IAnnParallelInterface _cudaArrayDevice;

        private static IAnnParallelInterface CPUArrayDevice
            => _cpuArrayDevice ?? (_cpuArrayDevice = new CpuAnnInterface<ArrayOperations>());
        private static IAnnParallelInterface CUDAArrayDevice
            => _cudaArrayDevice ?? (_cudaArrayDevice = new CudaAnnInterface<ArrayOperations>());

        public static IAnnParallelInterface ArrayDevice { get; private set; }
    }

    public static partial class ProcessingDevice
    {
        private static IAnnParallelInterface _cpuSigmoidDevice;
        private static IAnnParallelInterface _cudaSigmoidDevice;
        private static IAnnParallelInterface CPUSigmoidDevice
            => _cpuSigmoidDevice ?? (_cpuSigmoidDevice = new CpuAnnInterface<SigmoidFunction>());
        private static IAnnParallelInterface CUDASigmoidDevice
            => _cudaSigmoidDevice ?? (_cudaSigmoidDevice = new CudaAnnInterface<SigmoidFunction>());
        private static IAnnParallelInterface SigmoidDevice { get; set; }
    }

    public static partial class ProcessingDevice
    {
        private static IAnnParallelInterface _cpuTANHDevice;
        private static IAnnParallelInterface _cudaTANHDevice;
        private static IAnnParallelInterface CPUTANHDevice
            => _cpuTANHDevice ?? (_cpuTANHDevice = new CpuAnnInterface<TANHFuncion>());
        private static IAnnParallelInterface CUDATANHDevice
            => _cudaTANHDevice ?? (_cudaTANHDevice = new CudaAnnInterface<TANHFuncion>());
        private static IAnnParallelInterface TANHDevice { get; set; }
    }

    public static partial class ProcessingDevice
    {
        private static IAnnParallelInterface _cpuLeakReluDevice;
        private static IAnnParallelInterface _cudaLeakReluDevice;
        private static IAnnParallelInterface CPULeakReluDevice
            => _cpuLeakReluDevice ?? (_cpuLeakReluDevice = new CpuAnnInterface<LeakyRELUFunction>());
        private static IAnnParallelInterface CUDALeakReluDevice
            => _cudaLeakReluDevice ?? (_cudaLeakReluDevice = new CudaAnnInterface<LeakyRELUFunction>());
        private static IAnnParallelInterface LeakReluDevice { get; set; }
    }

    public static partial class ProcessingDevice
    {
        private static IAnnParallelInterface _cpuSquaredLossDevice;
        private static IAnnParallelInterface _cudaSquaredLossDevice;
        private static IAnnParallelInterface CPUSquaredLossDevice
            => _cpuSquaredLossDevice ?? (_cpuSquaredLossDevice = new CpuAnnInterface<SquaredErrorFunction>());
        private static IAnnParallelInterface CUDASquaredLossDevice
            => _cudaSquaredLossDevice ?? (_cudaSquaredLossDevice = new CudaAnnInterface<SquaredErrorFunction>());
        private static IAnnParallelInterface SquaredLossDevice { get; set; }
    }
}

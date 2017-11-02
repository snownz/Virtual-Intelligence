using VI.Maths.Array;
using VI.Maths.LogisticFunctions;
using VI.Maths.LossFunctions;
using VI.NumSharp.Provider;
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
                        SigmoidDevice = CPUSigmoidDevice;
                        TANHDevice = CPUTANHDevice;
                        LeakReluDevice = CPULeakReluDevice;
                        SquaredLossDevice = CPUSquaredLossDevice;
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

    public  static partial class ProcessingDevice
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
        private static IAnnParallelInterface CPUSigmoidDevice
            => _cpuSigmoidDevice ?? (_cpuSigmoidDevice = new CpuAnnInterface<SigmoidFunction>());
        private static IAnnParallelInterface SigmoidDevice { get; set; }        
    }

    public static partial class ProcessingDevice
    {
        private static IAnnParallelInterface _cpuTANHDevice;
        private static IAnnParallelInterface CPUTANHDevice
            => _cpuTANHDevice ?? (_cpuTANHDevice = new CpuAnnInterface<TANHFuncion>());
        private static IAnnParallelInterface TANHDevice { get; set; }
    }

    public static partial class ProcessingDevice
    {
        private static IAnnParallelInterface _cpuLeakReluDevice;
        private static IAnnParallelInterface CPULeakReluDevice
            => _cpuLeakReluDevice ?? (_cpuLeakReluDevice = new CpuAnnInterface<LeakyRELUFunction>());
        private static IAnnParallelInterface LeakReluDevice { get; set; }
    }

    public  static partial class ProcessingDevice
    {
        private static IAnnParallelInterface _cpuSquaredLossDevice;
        private static IAnnParallelInterface CPUSquaredLossDevice
            => _cpuSquaredLossDevice ?? (_cpuSquaredLossDevice = new CpuAnnInterface<SquaredErrorFunction>());
        private static IAnnParallelInterface SquaredLossDevice { get; set; }
    }

    public static partial class ProcessingDevice
    {
        private static IActivationFunctionProvider _sigmoid;
        private static IActivationFunctionProvider _tanh;
        private static IActivationFunctionProvider _leakRelu;

        public static IActivationFunctionProvider Sigmoid
            => _sigmoid ?? (_sigmoid = new ActivationFunctionProvider(SigmoidDevice));
        public static IActivationFunctionProvider TANH
            => _tanh ?? (_tanh = new ActivationFunctionProvider(TANHDevice));
        public static IActivationFunctionProvider LeakRelu
            => _leakRelu ?? (_leakRelu = new ActivationFunctionProvider(LeakReluDevice));

        private static ILossFunctionProvider _squaredLoss;

        public static ILossFunctionProvider SquaredLoss
           => _squaredLoss ?? (_squaredLoss = new LossFunctionProvider(SquaredLossDevice));
    }
}

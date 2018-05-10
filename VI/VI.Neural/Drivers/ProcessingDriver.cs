using VI.ParallelComputing;

namespace VI.Neural.Drivers.Executors
{
    public static class ProcessingDriver
    {
         private static DeviceType _device;

        public static DeviceType Device
        {
            get => _device;
            set
            {
                NumSharp.ProcessingDevice.Device = value;
                switch (value)
                {
                    case DeviceType.CUDA:                        
                        break;

                    case DeviceType.CPU:
                        Optimization = new CpuOptimizationExecutor();
                        //Activation = new CpuOptimizationExecutor();
                        break;

                    case DeviceType.CPU_Parallel:
                        Optimization = new ParallelCpuOptimizationExecutor();
                        Activation = new ParallelCpuActivation();
                        break;
                }

                _device = value;
            }
        }

        public static IOptimizationExecutor Optimization { get; private set; }
        public static IActivationExecutor Activation { get; private set; }
    }
}
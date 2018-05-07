using VI.ParallelComputing;

namespace VI.Neural.Drivers.Executors
{
    public static class ProcessingDevice
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
                        break;

                    case DeviceType.CPU_Parallel:
                        Optimization = new ParallelCpuOptimizationExecutor();
                        break;
                }

                _device = value;
            }
        }

        public static IOptimizationExecutor Optimization { get; private set; }
    }
}
using System;
using ILGPU.Runtime;

namespace VI.ParallelComputing.Drivers
{
    public class LinearCpuAnnInterface : IAnnParallelInterface
    {
        private readonly Accelerator _accelerator;
        private readonly ParalleExecutorlInterface _interface;

        public ParalleExecutorlInterface Executor => _interface;

        public LinearCpuAnnInterface()
        {
            try
            {
                _accelerator = Device.CPU;
            }
            catch (Exception)
            {
                Console.WriteLine("\n-----------\nCPU is not supported\n-----------\n");
                return;
            }
        }
    }
}

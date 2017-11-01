using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.Maths.Array;
using VI.Maths.LogisticFunctions;
using VI.ParallelComputing.ANN;

namespace VI.ParallelComputing
{
    public enum Device
    {
        CUDA,
        CPU
    }
    public static class ProcessingDevice
    {
        public static Device Device
        {
            set
            {
                switch (value)
                {
                    case Device.CUDA:
                        ArrayDevice = CPUArrayDevice;
                        break;
                    case Device.CPU:
                        ArrayDevice = CPUArrayDevice;
                        break;
                }
            }
        }

        private static IAnnParallelInterface _cpuArrayDevice;
        private static IAnnParallelInterface CPUArrayDevice
        {
            get
            {
                if (_cpuArrayDevice == null)
                {
                    _cpuArrayDevice = new CpuAnnInterface2<ArrayOperations>();
                }
                return _cpuArrayDevice;
            } 
        }
        
        public static IAnnParallelInterface ArrayDevice { get; private set; }

        public static IAnnParallelInterface CPUSigmoidDevice => new CpuAnnInterface2<SigmoidFunction>();
        public static IAnnParallelInterface CPULeakReluDevice => new CpuAnnInterface2<LeakyRELUFunction>();
        public static IAnnParallelInterface CPUTANHDevice => new CpuAnnInterface2<TANHFuncion>();

        public static IAnnParallelInterface CPUSquaredLossDevice => new CpuAnnInterface2<TANHFuncion>();
    }
}

using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using System;

namespace VI.ParallelComputing
{
    public enum DeviceType
    {
        CPU = 0,
        CPU_Parallel = 1,
        [Obsolete("Not finalized")]
        CUDA = 2
    }

    public static class Device
    {
        private static Context _context;
        private static CudaAccelerator _cuda;
        private static Context Context => _context ?? (_context = new Context());
        public static Accelerator CUDA => _cuda ?? (_cuda = new CudaAccelerator(Context));
    }
}
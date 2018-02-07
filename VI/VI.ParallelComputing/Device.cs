using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.CPU;
using ILGPU.Runtime.Cuda;

namespace VI.ParallelComputing
{
	public enum DeviceType
	{
		CPU  = 0,
		CUDA = 1
	}

	public static class Device
	{
		private static Context         _context;
		private static CudaAccelerator _cuda;
		private static CPUAccelerator  _cpu;

		private static Context Context => _context ?? (_context = new Context());

		public static Accelerator CUDA => _cuda ?? (_cuda = new CudaAccelerator(Context));
		public static Accelerator CPU  => _cpu  ?? (_cpu = new CPUAccelerator(Context));
	}
}
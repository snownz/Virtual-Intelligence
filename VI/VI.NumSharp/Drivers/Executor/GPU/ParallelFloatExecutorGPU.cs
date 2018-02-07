using VI.NumSharp.Drivers.Data.CPU;
using VI.NumSharp.Drivers.Data.GPU;

namespace VI.NumSharp.Drivers.Executor.GPU
{
	public class ParallelFloatExecutorGPU : IFloatArrayExecutor
	{
		public IFloatData V_mult_V(IFloatData v0, IFloatData v1)
		{
			var size   = v0.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["_V_X_V"].Launch(size, output.View, v0.View, v1.View);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_div_V(IFloatData v0, IFloatData v1)
		{
			var size   = v0.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["_V_div_V"].Launch(size, output.View, v0.View, v1.View);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_sub_V(IFloatData v0, IFloatData v1)
		{
			var size   = v0.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["_V_sub_V"].Launch(size, output.View, v0.View, v1.View);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_add_V(IFloatData v0, IFloatData v1)
		{
			var size   = v0.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["_V_add_V"].Launch(size, output.View, v0.View, v1.View);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_add_C(IFloatData v, float c)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["V_add_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_mult_C(IFloatData v, float c)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["V_mult_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_sub_C(IFloatData v, float c)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["V_sub_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_sub_C(float c, IFloatData v)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["V_sub_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_div_C(IFloatData v, float c)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["V_div_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData V_div_C(float c, IFloatData v)
		{
			var size   = v.Length;
			var output = ILGPUMethods.Allocate<float>(size);
			ProcessingDevice.ArrayDevice.Executor["V_div_C"].Launch(size, output.View, v.View, c);
			ProcessingDevice.ArrayDevice.Executor.Wait();
			return new GPU_FloatData(output);
		}

		public IFloatData2D VT_mult_V(IFloatData vt, IFloatData v)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D V_mult_VT(IFloatData v, IFloatData vt)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D VT_mult_M(IFloatData vt, IFloatData2D m)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_mult_M(IFloatData2D m0, IFloatData2D m1)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_div_M(IFloatData2D m0, IFloatData2D m1)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_sub_M(IFloatData2D m0, IFloatData2D m1)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_add_M(IFloatData2D m0, IFloatData2D m1)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_mult_VT(IFloatData2D m, IFloatData v)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_mult_V(IFloatData2D m, IFloatData v)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D V_mult_M(IFloatData v, IFloatData2D m)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_mult_C(IFloatData2D m, float c)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_add_C(IFloatData2D m, float c)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_div_C(IFloatData2D m, float c)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D C_div_M(IFloatData2D m, float c)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D C_div_M(IFloatData2D m, int c)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D M_mult_MT(IFloatData2D m0, IFloatData2D m1)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D MT_mult_M(IFloatData2D m0, IFloatData2D m1)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData ApplyMask(IFloatData arr, IByteData mask)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D ApplyMask(IFloatData2D arr, IByteData2D mask)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData Tanh(IFloatData arr)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData Sin(IFloatData arr)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData Cos(IFloatData arr)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData Pow(IFloatData arr, float exp)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData Exp(IFloatData arr)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData Log(IFloatData arr)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData2D Sqrt(IFloatData2D arr)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData Sqrt(IFloatData arr)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData SumLine(IFloatData2D arr)
		{
			throw new System.NotImplementedException();
		}

		public IFloatData SumColumn(IFloatData2D arr)
		{
			throw new System.NotImplementedException();
		}
	}
}
using ILGPU;
using ILGPU.Runtime.Cuda;
using System;
using System.Collections.Generic;
using System.Text;

namespace VI.ParallelComputing
{
    public static class Device
    {
        private static CudaAccelerator _cuda;
        public static CudaAccelerator CUDA
        {
            get
            {
                if(_cuda == null)
                {
                    var context = new Context();
                    _cuda = new CudaAccelerator(context);
                }
                return _cuda;
            }
        }
    }
}

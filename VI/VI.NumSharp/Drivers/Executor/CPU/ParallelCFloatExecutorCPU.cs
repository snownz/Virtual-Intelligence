using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using VI.NumSharp.Drivers.Data.CPU;

namespace VI.NumSharp.Drivers.Executor.CPU
{
    partial class ParallelCFloatExecutorCPU
    {
        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_mult_V(int len, float[] cache, float[] v0, float[] v1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_div_V(int len, float[] cache, float[] v0, float[] v1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_sub_V(int len, float[] cache, float[] v0, float[] v1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_add_V(int len, float[] cache, float[] v0, float[] v1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_add_C(int len, float[] cache, float[] v, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_mult_C(int len,float[] cache, float[] v, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_sub_C(int len, float[] cache, float[] v, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_C_sub_V(int len, float[] cache, float c, float[] v);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_div_C(int len, float[] cache, float[] v, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_C_div_V(int len, float[] cache, float c, float[] v);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Tanh_V(int len, float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void  C_Sin_V(int len, float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Cos_V(int len, float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Pow_V(int len, float[] cache, float[] arr, float exp);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Exp_V(int len, float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Log_V(int len, float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Sqrt_V(int len, float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_VT_mult_M(int w, int h, float[][] cache, float[] vt, float[][] m);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_M(int w, int h, float[,] cache, float[,] m0, float[,] m1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_div_M(int w, int h, float[,] cache, float[,] m0, float[,] m1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_sub_M(int w, int h, float[,] cache, float[,] m0, float[,] m1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_add_M(int w, int h, float[,] cache, float[,] m0, float[,] m1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_VT(int w, int h, float[,] cache, float[,] m, float[] vt);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_V(int w, int h, float[,] cache, float[,] m, float[] v);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_mult_M(int w, int h, float[,] cache, float[] v, float[,] m);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_C(int w, int h, float[,] cache, float[,] m, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_add_C(int w, int h, float[,] cache, float[,] m, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_div_C(int w, int h, float[,] cache, float[,] m, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_C_div_M(int w, int h, float[,] cache, float[,] m, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_C_div_M_int(int w, int h, float[,] cache, float[,] m, int c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Sqrt_M(int w, int h, float[,] cache, float[,] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_VT_mult_V(int w, int h, float[,] cache, float[] vt, float[] v);
        
        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_MT(int w, int h, float[,] cache, float[,] mt, float[,] m);
        
        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_SumLine(int w, int h, float[] cache, float[,] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_SumColumn(int w, int h, float[] cache, float[,] arr);        
    }

    partial class ParallelCFloatExecutorCPU : IFloatArrayExecutor
    {   
        public IFloatData V_mult_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            throw new NotImplementedException();
            C_V_mult_V(cache.Length, cache.View, v0.View, v1.View);
            return cache;
        }
        public IFloatData V_div_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            throw new NotImplementedException();
            C_V_div_V(cache.Length, cache.View, v0.View, v1.View);
            return cache;
        }
        public IFloatData V_sub_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            throw new NotImplementedException();
            C_V_sub_V(cache.Length, cache.View, v0.View, v1.View);
            return cache;
        }
        public IFloatData V_add_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            throw new NotImplementedException();
            C_V_add_V(cache.Length, cache.View, v0.View, v1.View);
            return cache;
        }
        public IFloatData V_add_C(IFloatData cache, IFloatData v, float c)
        {
            throw new NotImplementedException();
            C_V_add_C(cache.Length, cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_mult_C(IFloatData cache, IFloatData v, float c)
        {
            throw new NotImplementedException();
            C_V_mult_C(cache.Length, cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_sub_C(IFloatData cache, IFloatData v, float c)
        {
            throw new NotImplementedException();
            C_V_sub_C(cache.Length, cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_sub_C(IFloatData cache, float c, IFloatData v)
        {
            throw new NotImplementedException();
            C_C_sub_V(cache.Length, cache.View, c, v.View);
            return cache;
        }
        public IFloatData V_div_C(IFloatData cache, IFloatData v, float c)
        {
            throw new NotImplementedException();
            C_V_div_C(cache.Length, cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_div_C(IFloatData cache, float c, IFloatData v)
        {
            throw new NotImplementedException();
            C_C_div_V(cache.Length, cache.View, c, v.View);
            return cache;
        }

        public IFloatData Tanh(IFloatData cache, IFloatData arr)
        {
            throw new NotImplementedException();
            C_Tanh_V(cache.Length, cache.View, arr.View);
            return cache;
        }
        public IFloatData Sin(IFloatData cache, IFloatData arr)
        {
            throw new NotImplementedException();
            C_Sin_V(cache.Length, cache.View, arr.View);
            return cache;
        }
        public IFloatData Cos(IFloatData cache, IFloatData arr)
        {
            throw new NotImplementedException();
            C_Cos_V(cache.Length, cache.View, arr.View);
            return cache;
        }
        public IFloatData Pow(IFloatData cache, IFloatData arr, float exp)
        {
            throw new NotImplementedException();
            C_Pow_V(cache.Length, cache.View, arr.View, exp);
            return cache;
        }
        public IFloatData Exp(IFloatData cache, IFloatData arr)
        {
            throw new NotImplementedException();
            C_Exp_V(cache.Length, cache.View, arr.View);
            return cache;
        }
        public IFloatData Log(IFloatData cache, IFloatData arr)
        {
            throw new NotImplementedException();
            C_Log_V(cache.Length, cache.View, arr.View);
            return cache;
        }
        public IFloatData Sqrt(IFloatData cache, IFloatData arr)
        {
            throw new NotImplementedException();
            C_Sqrt_V(cache.Length, cache.View, arr.View);
            return cache;
        }

        public IFloatData2D VT_mult_M(IFloatData2D cache, IFloatData vt, IFloatData2D m)
        {
            C_VT_mult_M(cache.W, cache.H, null, vt.View, null);
            return cache;
        }
        public IFloatData2D M_mult_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            throw new NotImplementedException();
            C_M_mult_M(cache.W, cache.H, cache.View, m0.View, m1.View);
            return cache;
        }
        public IFloatData2D M_div_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            throw new NotImplementedException();
            C_M_div_M(cache.W, cache.H, cache.View, m0.View, m1.View);
            return cache;
        }
        public IFloatData2D M_sub_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            throw new NotImplementedException();
            C_M_sub_M(cache.W, cache.H, cache.View, m0.View, m1.View);
            return cache;
        }
        public IFloatData2D M_add_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            throw new NotImplementedException();
            C_M_add_M(cache.W, cache.H, cache.View, m0.View, m1.View);
            return cache;
        }
        public IFloatData2D M_mult_V(IFloatData2D cache, IFloatData2D m, IFloatData v)
        {
            throw new NotImplementedException();
            C_M_mult_V(cache.W, cache.H, cache.View, m.View, v.View);
            return cache;
        }
        public IFloatData2D M_mult_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            throw new NotImplementedException();
            C_M_mult_C(cache.W, cache.H, cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D M_add_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            throw new NotImplementedException();
            C_M_add_C(cache.W, cache.H, cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D M_div_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            throw new NotImplementedException();
            C_M_div_C(cache.W, cache.H, cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, float c)
        {
            throw new NotImplementedException();
            C_C_div_M(cache.W, cache.H, cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, int c)
        {
            throw new NotImplementedException();
            C_C_div_M_int(cache.W, cache.H, cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D Sqrt(IFloatData2D cache, IFloatData2D arr)
        {
            throw new NotImplementedException();
            C_Sqrt_M(cache.W, cache.H, cache.View, arr.View);
            return cache;
        }
        public IFloatData2D VT_mult_V(IFloatData vt, IFloatData v)
        {
            throw new NotImplementedException();
            var cache = new CPU_FloatData2D(v.Length, vt.Length);
            C_VT_mult_V(cache.W, cache.H, cache.View, vt.View, v.View);
            return cache;
        }
        public IFloatData2D M_mult_MT(IFloatData2D mt, IFloatData2D m)
        {
            throw new NotImplementedException();
            var cache = new CPU_FloatData2D(m.W, m.H);
            C_M_mult_MT(cache.W, cache.H, cache.View, mt.View, m.View);
            return cache;
        }
        public IFloatData SumLine(IFloatData2D arr)
        {
            throw new NotImplementedException();
            var cache = new CPU_FloatData(arr.W);
            C_SumLine(arr.W, arr.H, cache.View, arr.View);
            return cache;
        }
        public IFloatData SumColumn(IFloatData2D arr)
        {
            throw new NotImplementedException();
            var cache = new CPU_FloatData(arr.H);
            C_SumColumn(arr.W, arr.H, cache.View, arr.View);
            return cache;
        }
        public IFloatData ApplyMask(IFloatData arr, IByteData mask)
        {
            throw new NotImplementedException();
        }
        public IFloatData2D ApplyMask(IFloatData2D arr, IByteData2D mask)
        {
            throw new NotImplementedException();
        }
    }
}

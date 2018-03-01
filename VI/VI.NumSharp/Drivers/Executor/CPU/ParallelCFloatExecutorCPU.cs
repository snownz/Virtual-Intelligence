using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace VI.NumSharp.Drivers.Executor.CPU
{
    partial class ParallelCFloatExecutorCPU
    {
        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_mult_V(float[] cache, float[] v0, float[] v1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_div_V(float[] cache, float[] v0, float[] v1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_sub_V(float[] cache, float[] v0, float[] v1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_add_V(float[] cache, float[] v0, float[] v1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_add_C(float[] cache, float[] v, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_mult_C(int len,float[] cache, float[] v, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_sub_C(float[] cache, float[] v, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_sub_C(float[] cache, float c, float[] v);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_div_C(float[] cache, float[] v, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void _V_div_C(float[] cache, float c, float[] v);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Tanh(float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void  C_Sin(float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Cos(float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Pow(float[] cache, float[] arr, float exp);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Exp(float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Log(float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Sqrt(float[] cache, float[] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_VT_mult_M(float[,] cache, float[] vt, float[,] m);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_M(float[,] cache, float[,] m0, float[,] m1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_div_M(float[,] cache, float[,] m0, float[,] m1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_sub_M(float[,] cache, float[,] m0, float[,] m1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_add_M(float[,] cache, float[,] m0, float[,] m1);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_VT(float[,] cache, float[,] m, float[] vt);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_V(float[,] cache, float[,] m, float[] v);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_mult_M(float[,] cache, float[] v, float[,] m);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_C(float[,] cache, float[,] m, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_add_C(float[,] cache, float[,] m, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_div_C(float[,] cache, float[,] m, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_C_div_M(float[,] cache, float[,] m, float c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_C_div_M(float[,] cache, float[,] m, int c);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_Sqrt(float[,] cache, float[,] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_VT_mult_V(float[] vt, float[] v);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_V_mult_VT(float[] v, float[] vt);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_M_mult_MT(float[,] mt, float[,] m);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_MT_mult_M(float[,] m, float[,] mt);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_SumLine(float[,] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_SumColumn(float[,] arr);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_ApplyMask(float[] arr, IByteData mask);

        [DllImport("VI.Core.Cpu", CallingConvention = CallingConvention.Cdecl)]
        public static extern void C_ApplyMask(float[,] arr, IByteData2D mask);
    }

    partial class ParallelCFloatExecutorCPU : IFloatArrayExecutor
    {   
        public IFloatData V_mult_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            C_V_mult_V(cache.View, v0.View, v1.View);
            return cache;
        }
        public IFloatData V_div_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            C_V_div_V(cache.View, v0.View, v1.View);
            return cache;
        }
        public IFloatData V_sub_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            C_V_sub_V(cache.View, v0.View, v1.View);
            return cache;
        }
        public IFloatData V_add_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            C_V_add_V(cache.View, v0.View, v1.View);
            return cache;
        }
        public IFloatData V_add_C(IFloatData cache, IFloatData v, float c)
        {
            C_V_add_C(cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_mult_C(IFloatData cache, IFloatData v, float c)
        {
            C_V_mult_C(cache.Length, cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_sub_C(IFloatData cache, IFloatData v, float c)
        {
            C_V_sub_C(cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_sub_C(IFloatData cache, float c, IFloatData v)
        {
            C_V_sub_C(cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_div_C(IFloatData cache, IFloatData v, float c)
        {
            C_V_div_C(cache.View, v.View, c);
            return cache;
        }
        public IFloatData V_div_C(IFloatData cache, float c, IFloatData v)
        {
            C_V_div_C(cache.View, v.View, c);
            return cache;
        }

        public IFloatData Tanh(IFloatData cache, IFloatData arr)
        {
            C_Tanh(cache.View, arr.View);
            return cache;
        }
        public IFloatData Sin(IFloatData cache, IFloatData arr)
        {
            C_Sin(cache.View, arr.View);
            return cache;
        }
        public IFloatData Cos(IFloatData cache, IFloatData arr)
        {
            C_Cos(cache.View, arr.View);
            return cache;
        }
        public IFloatData Pow(IFloatData cache, IFloatData arr, float exp)
        {
            C_Pow(cache.View, arr.View, exp);
            return cache;
        }
        public IFloatData Exp(IFloatData cache, IFloatData arr)
        {
            C_Exp(cache.View, arr.View);
            return cache;
        }
        public IFloatData Log(IFloatData cache, IFloatData arr)
        {
            C_Log(cache.View, arr.View);
            return cache;
        }
        public IFloatData Sqrt(IFloatData cache, IFloatData arr)
        {
            C_Sqrt(cache.View, arr.View);
            return cache;
        }

        public IFloatData2D VT_mult_M(IFloatData2D cache, IFloatData vt, IFloatData2D m)
        {
            C_VT_mult_M(cache.View, vt.View, m.View);
            return cache;
        }
        public IFloatData2D M_mult_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            C_M_mult_M(cache.View, m0.View, m1.View);
            return cache;
        }
        public IFloatData2D M_div_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            C_M_div_M(cache.View, m0.View, m1.View);
            return cache;
        }
        public IFloatData2D M_sub_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            C_M_sub_M(cache.View, m0.View, m1.View);
            return cache;
        }
        public IFloatData2D M_add_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            C_M_add_M(cache.View, m0.View, m1.View);
            return cache;
        }
        public IFloatData2D M_mult_VT(IFloatData2D cache, IFloatData2D m, IFloatData vt)
        {
            C_M_mult_VT(cache.View, m.View, vt.View);
            return cache;
        }
        public IFloatData2D M_mult_V(IFloatData2D cache, IFloatData2D m, IFloatData v)
        {
            C_M_mult_V(cache.View, m.View, v.View);
            return cache;
        }
        public IFloatData2D V_mult_M(IFloatData2D cache, IFloatData v, IFloatData2D m)
        {
            C_V_mult_M(cache.View, v.View, m.View);
            return cache;
        }
        public IFloatData2D M_mult_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            C_M_mult_C(cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D M_add_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            C_M_add_C(cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D M_div_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            C_M_div_C(cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, float c)
        {
            C_C_div_M(cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, int c)
        {
            C_C_div_M(cache.View, m.View, c);
            return cache;
        }
        public IFloatData2D Sqrt(IFloatData2D cache, IFloatData2D arr)
        {
            C_Sqrt(cache.View, arr.View);
            return cache;
        }
        public IFloatData2D VT_mult_V(IFloatData vt, IFloatData v)
        {
            return null;
        }
        public IFloatData2D V_mult_VT(IFloatData v, IFloatData vt)
        {
            return null;
        }
        public IFloatData2D M_mult_MT(IFloatData2D mt, IFloatData2D m)
        {
            return null;
        }
        public IFloatData2D MT_mult_M(IFloatData2D m, IFloatData2D mt)
        {
            return null;
        }
        public IFloatData SumLine(IFloatData2D arr)
        {
            return null;
        }
        public IFloatData SumColumn(IFloatData2D arr)
        {
            return null;
        }
        public IFloatData ApplyMask(IFloatData arr, IByteData mask)
        {
            return null;
        }
        public IFloatData2D ApplyMask(IFloatData2D arr, IByteData2D mask)
        {
            return null;
        }
    }
}

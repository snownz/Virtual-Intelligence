using ILGPU;
using ILGPU.Runtime;
using System;
using VI.NumSharp.Drivers.Data.GPU;
using VI.ParallelComputing;

namespace VI.NumSharp.Drivers.Executor.GPU
{
    public partial class ParallelFloatExecutorGPU
    {
        public static void kernel_V_mult_V(Index pos, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            int x = pos.X;
            output[x] = v0[x] * v1[x];
        }

        public static void kernel_V_div_V(Index pos, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            int x = pos.X;
            output[x] = v0[x] / v1[x];
        }

        public static void kernel_V_sub_V(Index pos, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            int x = pos.X;
            output[x] = v0[x] - v1[x];
        }

        public static void kernel_V_add_V(Index pos, ArrayView<float> output, ArrayView<float> v0, ArrayView<float> v1)
        {
            int x = pos.X;
            output[x] = v0[x] + v1[x];
        }

        public static void kernel_V_add_C(Index pos, ArrayView<float> output, ArrayView<float> v, float c)
        {
            int x = pos.X;
            output[x] = v[x] + c;
        }

        public static void kernel_V_mult_C(Index pos, ArrayView<float> output, ArrayView<float> v, float c)
        {
            int x = pos.X;
            output[x] = v[x] * c;
        }

        public static void kernel_V_sub_C(Index pos, ArrayView<float> output, ArrayView<float> v, float c)
        {
            int x = pos.X;
            output[x] = v[x] - c;
        }

        public static void kernel_C_sub_V(Index pos, ArrayView<float> output, float c, ArrayView<float> v)
        {
            int x = pos.X;
            output[x] = c - v[x];
        }

        public static void kernel_V_div_C(Index pos, ArrayView<float> output, ArrayView<float> v, float c)
        {
            int x = pos.X;
            output[x] = v[x] / c;
        }

        public static void kernel_C_div_V(Index pos, ArrayView<float> output, float c, ArrayView<float> v)
        {
            int x = pos.X;
            output[x] = c / v[x];
        }

        public static void kernel_V_ApplyMask(Index pos, ArrayView<float> output, ArrayView<float> arr, ArrayView<byte> mask)
        {
            int x = pos.X;
            output[x] = arr[x] * mask[x];
        }

        public static void kernel_V_Tanh(Index pos, ArrayView<float> output, ArrayView<float> arr)
        {
            int x = pos.X;
            output[x] = GPUMath.Tanh(arr[x]);
        }

        public static void kernel_V_Sin(Index pos, ArrayView<float> output, ArrayView<float> arr)
        {
            int x = pos.X;
            output[x] = GPUMath.Sin(arr[x]);
        }

        public static void kernel_V_Cos(Index pos, ArrayView<float> output, ArrayView<float> arr)
        {
            int x = pos.X;
            output[x] = GPUMath.Cos(arr[x]);
        }

        public static void kernel_V_Pow(Index pos, ArrayView<float> output, ArrayView<float> arr, float exp)
        {
            int x = pos.X;
            output[x] = GPUMath.Pow(arr[x], exp);
        }

        public static void kernel_V_Exp(Index pos, ArrayView<float> output, ArrayView<float> arr)
        {
            int x = pos.X;
            output[x] = GPUMath.Exp(arr[x]);
        }

        public static void kernel_V_Log(Index pos, ArrayView<float> output, ArrayView<float> arr)
        {
            int x = pos.X;
            output[x] = GPUMath.Log(arr[x]);
        }

        public static void kernel_V_Sqrt(Index pos, ArrayView<float> output, ArrayView<float> arr)
        {
            int x = pos.X;
            output[x] = GPUMath.Sqrt(arr[x]);
        }

        public static void kernel_VT_mult_M(Index2 pos, ArrayView2D<float> output, ArrayView<float> vt, ArrayView2D<float> m)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = vt[y] * m[x, y];
        }

        public static void kernel_M_mult_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m0[x, y] * m1[x, y];
        }

        public static void kernel_M_div_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m0[x, y] / m1[x, y];
        }

        public static void kernel_M_sub_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m0[x, y] - m1[x, y];
        }

        public static void kernel_M_add_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m0, ArrayView2D<float> m1)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m0[x, y] + m1[x, y];
        }

        public static void kernel_M_mult_VT(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView<float> vt)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m[x, y] * vt[y];
        }

        public static void kernel_M_mult_V(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView<float> v)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m[x, y] * v[x];
        }

        public static void kernel_V_mult_M(Index2 pos, ArrayView2D<float> output, ArrayView<float> v, ArrayView2D<float> m)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m[x, y] * v[x];
        }

        public static void kernel_M_mult_C(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, float c)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m[x, y] * c;
        }

        public static void kernel_M_add_C(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, float c)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m[x, y] + c;
        }

        public static void kernel_M_div_C(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, float c)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m[x, y] / c;
        }

        public static void kernel_C_div_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, float c)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = c / m[x, y];
        }

        public static void kernel_IC_div_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, int c)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = c / m[x, y];
        }

        public static void kernel_m_Sqrt(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> arr)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = GPUMath.Sqrt(arr[x, y]);
        }

        public static void kernel_M_mult_MT(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> m, ArrayView2D<float> mt)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = m[x, y] * mt[y, x];
        }

        public static void kernel_MT_mult_M(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> mt, ArrayView2D<float> m)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = mt[y, x] * m[x, y];
        }

        public static void kernel_M_ApplyMask(Index2 pos, ArrayView2D<float> output, ArrayView2D<float> arr, ArrayView2D<byte> mask)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = arr[x, y] * mask[x, y];
        }

        public static void kernel_M_SumLine(Index2 pos, ArrayView2D<float> m, int r, int boxSize)
        {
            var x = pos.X;
            var y = pos.Y;
            m[x, y] = m[x, y] + m[x, y + r + boxSize];
        }

        public static void kernel_M_SumColumn(Index2 pos, ArrayView2D<float> m, int r, int boxSize)
        {
            var x = pos.X;
            var y = pos.Y;
            m[x, y] = m[x, y] + m[x + r + boxSize, y];
        }

        public static void kernel_M_2_lines_V(Index size, ArrayView<float> v, ArrayView2D<float> m)
        {
            int x = size.X;
            v[x] = m[x, 0] + m[x, 1];
        }

        public static void kernel_M_2_columns_V(Index size, ArrayView<float> v, ArrayView2D<float> m)
        {
            int y = size.X;
            v[y] = m[0, y] + m[1, y];
        }

        public static void kernel_VT_mult_V(Index2 pos, ArrayView2D<float> output, ArrayView<float> vt, ArrayView<float> v)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = vt[y] * v[x];
        }

        public static void kernel_V_mult_VT(Index2 pos, ArrayView2D<float> output, ArrayView<float> v, ArrayView<float> vt)
        {
            int x = pos.X;
            int y = pos.Y;
            output[x, y] = v[x] * vt[y];
        }
    }

    public partial class ParallelFloatExecutorGPU
    {
        private Action<Index, ArrayView<float>, ArrayView<float>, ArrayView<float>> _V_mult_V;
        private Action<Index, ArrayView<float>, ArrayView<float>, ArrayView<float>> _V_div_V;
        private Action<Index, ArrayView<float>, ArrayView<float>, ArrayView<float>> _V_sub_V;
        private Action<Index, ArrayView<float>, ArrayView<float>, ArrayView<float>> _V_add_V;
        private Action<Index, ArrayView<float>, ArrayView<float>, float> _V_add_C;
        private Action<Index, ArrayView<float>, ArrayView<float>, float> _V_mult_C;
        private Action<Index, ArrayView<float>, ArrayView<float>, float> _V_sub_C;
        private Action<Index, ArrayView<float>, float, ArrayView<float>> _C_sub_V;
        private Action<Index, ArrayView<float>, ArrayView<float>, float> _V_div_C;
        private Action<Index, ArrayView<float>, float, ArrayView<float>> _C_div_V;
        private Action<Index, ArrayView<float>, ArrayView<float>, ArrayView<byte>> _V_ApplyMask;
        private Action<Index, ArrayView<float>, ArrayView<float>> _V_Tanh;
        private Action<Index, ArrayView<float>, ArrayView<float>> _V_Sin;
        private Action<Index, ArrayView<float>, ArrayView<float>> _V_Cos;
        private Action<Index, ArrayView<float>, ArrayView<float>, float> _V_Pow;
        private Action<Index, ArrayView<float>, ArrayView<float>> _V_Exp;
        private Action<Index, ArrayView<float>, ArrayView<float>> _V_Log;
        private Action<Index, ArrayView<float>, ArrayView<float>> _V_Sqrt;
        private Action<Index2, ArrayView2D<float>, ArrayView<float>, ArrayView2D<float>> _VT_mult_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>> _M_mult_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>> _M_div_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>> _M_sub_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>> _M_add_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView<float>> _M_mult_VT;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView<float>> _M_mult_V;
        private Action<Index2, ArrayView2D<float>, ArrayView<float>, ArrayView2D<float>> _V_mult_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, float> _M_mult_C;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, float> _M_add_C;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, float> _M_div_C;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, float> _C_div_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, int> _IC_div_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>> _m_Sqrt;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>> _M_mult_MT;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>> _MT_mult_M;
        private Action<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<byte>> _M_ApplyMask;
        private Action<Index2, ArrayView2D<float>, int, int> _M_SumLine;
        private Action<Index2, ArrayView2D<float>, int, int> _M_SumColumn;
        private Action<Index, ArrayView<float>, ArrayView2D<float>> _M_2_lines_V;
        private Action<Index, ArrayView<float>, ArrayView2D<float>> _M_2_columns_V;
        private Action<Index2, ArrayView2D<float>, ArrayView<float>, ArrayView<float>> _VT_mult_V;
        private Action<Index2, ArrayView2D<float>, ArrayView<float>, ArrayView<float>> _V_mult_VT;

        public ParallelFloatExecutorGPU()
        {
            _V_mult_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, ArrayView<float>>(kernel_V_mult_V);
            _V_div_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, ArrayView<float>>(kernel_V_div_V);
            _V_sub_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, ArrayView<float>>(kernel_V_sub_V);
            _V_add_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, ArrayView<float>>(kernel_V_add_V);
            _V_add_C = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, float>(kernel_V_add_C);
            _V_mult_C = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, float>(kernel_V_mult_C);
            _V_sub_C = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, float>(kernel_V_sub_C);
            _C_sub_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, float, ArrayView<float>>(kernel_C_sub_V);
            _V_div_C = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, float>(kernel_V_div_C);
            _C_div_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, float, ArrayView<float>>(kernel_C_div_V);
            _V_ApplyMask = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, ArrayView<byte>>(kernel_V_ApplyMask);
            _V_Tanh = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>>(kernel_V_Tanh);
            _V_Sin = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>>(kernel_V_Sin);
            _V_Cos = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>>(kernel_V_Cos);
            _V_Pow = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>, float>(kernel_V_Pow);
            _V_Exp = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>>(kernel_V_Exp);
            _V_Log = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>>(kernel_V_Log);
            _V_Sqrt = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView<float>>(kernel_V_Sqrt);
            _VT_mult_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView<float>, ArrayView2D<float>>(kernel_VT_mult_M);
            _M_mult_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>>(kernel_M_mult_M);
            _M_div_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>>(kernel_M_div_M);
            _M_sub_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>>(kernel_M_sub_M);
            _M_add_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>>(kernel_M_add_M);
            _M_mult_VT = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView<float>>(kernel_M_mult_VT);
            _M_mult_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView<float>>(kernel_M_mult_V);
            _V_mult_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView<float>, ArrayView2D<float>>(kernel_V_mult_M);
            _M_mult_C = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, float>(kernel_M_mult_C);
            _M_add_C = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, float>(kernel_M_add_C);
            _M_div_C = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, float>(kernel_M_div_C);
            _C_div_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, float>(kernel_C_div_M);
            _IC_div_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, int>(kernel_IC_div_M);
            _m_Sqrt = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>>(kernel_m_Sqrt);
            _M_mult_MT = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>>(kernel_M_mult_MT);
            _MT_mult_M = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<float>>(kernel_MT_mult_M);
            _M_ApplyMask = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView2D<float>, ArrayView2D<byte>>(kernel_M_ApplyMask);
            _M_SumLine = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, int, int>(kernel_M_SumLine);
            _M_SumColumn = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, int, int>(kernel_M_SumColumn);
            _M_2_lines_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView2D<float>>(kernel_M_2_lines_V);
            _M_2_columns_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index, ArrayView<float>, ArrayView2D<float>>(kernel_M_2_columns_V);
            _VT_mult_V = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView<float>, ArrayView<float>>(kernel_VT_mult_V);
            _V_mult_VT = Device.CUDA.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<float>, ArrayView<float>, ArrayView<float>>(kernel_V_mult_VT);
        }
    }

    public partial class ParallelFloatExecutorGPU : IFloatArrayExecutor
    {
        public IFloatData V_mult_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            var size = v0.Length;
            _V_mult_V(size, cache.MemoryView, v0.MemoryView, v1.MemoryView);
            return cache;
        }

        public IFloatData V_div_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            var size = v0.Length;
            _V_div_V(size, cache.MemoryView, v0.MemoryView, v1.MemoryView);
            return cache;
        }

        public IFloatData V_sub_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            var size = v0.Length;
            _V_sub_V(size, cache.MemoryView, v0.MemoryView, v1.MemoryView);
            return cache;
        }

        public IFloatData V_add_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            var size = v0.Length;
            _V_add_V(size, cache.MemoryView, v0.MemoryView, v1.MemoryView);
            return cache;
        }

        public IFloatData V_add_C(IFloatData cache, IFloatData v, float c)
        {
            var size = v.Length;
            _V_add_C(size, cache.MemoryView, v.MemoryView, c);
            return cache;
        }

        public IFloatData V_mult_C(IFloatData cache, IFloatData v, float c)
        {
            var size = v.Length;
            _V_mult_C(size, cache.MemoryView, v.MemoryView, c);
            return cache;
        }

        public IFloatData V_sub_C(IFloatData cache, IFloatData v, float c)
        {
            var size = v.Length;
            _V_sub_C(size, cache.MemoryView, v.MemoryView, c);
            return cache;
        }

        public IFloatData V_sub_C(IFloatData cache, float c, IFloatData v)
        {
            var size = v.Length;
            _V_sub_C(size, cache.MemoryView, v.MemoryView, c);
            return cache;
        }

        public IFloatData V_div_C(IFloatData cache, IFloatData v, float c)
        {
            var size = v.Length;
            _V_div_C(size, cache.MemoryView, v.MemoryView, c);
            return cache;
        }

        public IFloatData V_div_C(IFloatData cache, float c, IFloatData v)
        {
            var size = v.Length;
            _V_div_C(size, cache.MemoryView, v.MemoryView, c);
            return cache;
        }

        public IFloatData Tanh(IFloatData cache, IFloatData arr)
        {
            var size = arr.Length;
            _V_Tanh(size, cache.MemoryView, arr.MemoryView);
            return cache;
        }

        public IFloatData Sin(IFloatData cache, IFloatData arr)
        {
            var size = arr.Length;
            _V_Sin(size, cache.MemoryView, arr.MemoryView);
            return cache;
        }

        public IFloatData Cos(IFloatData cache, IFloatData arr)
        {
            var size = arr.Length;
            _V_Cos(size, cache.MemoryView, arr.MemoryView);
            return cache;
        }

        public IFloatData Pow(IFloatData cache, IFloatData arr, float exp)
        {
            var size = arr.Length;
            _V_Pow(size, cache.MemoryView, arr.MemoryView, exp);
            return cache;
        }

        public IFloatData Exp(IFloatData cache, IFloatData arr)
        {
            var size = arr.Length;
            _V_Exp(size, cache.MemoryView, arr.MemoryView);
            return cache;
        }

        public IFloatData Log(IFloatData cache, IFloatData arr)
        {
            var size = arr.Length;
            _V_Log(size, cache.MemoryView, arr.MemoryView);
            return cache;
        }

        public IFloatData Sqrt(IFloatData cache, IFloatData arr)
        {
            var size = arr.Length;
            _V_Sqrt(size, cache.MemoryView, arr.MemoryView);
            return cache;
        }

        public IFloatData2D VT_mult_M(IFloatData2D cache, IFloatData vt, IFloatData2D m)
        {
            var size = new Index2(m.W, m.H);
            _VT_mult_M(size, cache.MemoryView, vt.MemoryView, m.MemoryView);
            return cache;
        }

        public IFloatData2D M_mult_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            var size = new Index2(m0.W, m0.H);
            _M_mult_M(size, cache.MemoryView, m0.MemoryView, m1.MemoryView);
            ProcessingDevice.FloatArrayDevice.Executor.Wait();
            return cache;
        }

        public IFloatData2D M_div_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            var size = new Index2(m0.W, m0.H);
            _M_div_M(size, cache.MemoryView, m0.MemoryView, m1.MemoryView);
            return cache;
        }

        public IFloatData2D M_sub_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            var size = new Index2(m0.W, m0.H);
            _M_sub_M(size, cache.MemoryView, m0.MemoryView, m1.MemoryView);
            return cache;
        }

        public IFloatData2D M_add_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            var size = new Index2(m0.W, m0.H);
            _M_add_M(size, cache.MemoryView, m0.MemoryView, m1.MemoryView);
            return cache;
        }

        public IFloatData2D M_mult_VT(IFloatData2D cache, IFloatData2D m, IFloatData v)
        {
            var size = new Index2(m.W, m.H);
            _M_mult_VT(size, cache.MemoryView, m.MemoryView, v.MemoryView);
            return cache;
        }

        public IFloatData2D M_mult_V(IFloatData2D cache, IFloatData2D m, IFloatData v)
        {
            var size = new Index2(m.W, m.H);
            _M_mult_V(size, cache.MemoryView, m.MemoryView, v.MemoryView);
            return cache;
        }

        public IFloatData2D V_mult_M(IFloatData2D cache, IFloatData v, IFloatData2D m)
        {
            var size = new Index2(m.W, m.H);
            _V_mult_M(size, cache.MemoryView, v.MemoryView, m.MemoryView);
            return cache;
        }

        public IFloatData2D M_mult_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            var size = new Index2(m.W, m.H);
            _M_mult_C(size, cache.MemoryView, m.MemoryView, c);
            return cache;
        }

        public IFloatData2D M_add_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            var size = new Index2(m.W, m.H);
            _M_add_C(size, cache.MemoryView, m.MemoryView, c);
            return cache;
        }

        public IFloatData2D M_div_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            var size = new Index2(m.W, m.H);
            _M_div_C(size, cache.MemoryView, m.MemoryView, c);
            return cache;
        }

        public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, float c)
        {
            var size = new Index2(m.W, m.H);
            _C_div_M(size, cache.MemoryView, m.MemoryView, c);
            return cache;
        }

        public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, int c)
        {
            var size = new Index2(m.W, m.H);
            _IC_div_M(size, cache.MemoryView, m.MemoryView, c);
            return cache;
        }

        public IFloatData2D Sqrt(IFloatData2D cache, IFloatData2D arr)
        {
            var size = new Index2(arr.W, arr.H);
            _m_Sqrt(size, cache.MemoryView, arr.MemoryView);
            return cache;
        }

        public IFloatData2D VT_mult_V(IFloatData vt, IFloatData v)
        {
            var size = new Index2(v.Length, vt.Length);
            var output = ILGPUMethods.Allocate<float>(size);
            _VT_mult_V(size, output.View, vt.MemoryView, v.MemoryView);
            return new GPU_FloatData2D(output);
        }

        public IFloatData2D V_mult_VT(IFloatData v, IFloatData vt)
        {
            var size = new Index2(v.Length, vt.Length);
            var output = ILGPUMethods.Allocate<float>(size);
            _V_mult_VT(size, output.View, v.MemoryView, vt.MemoryView);
            return new GPU_FloatData2D(output);
        }

        public IFloatData2D M_mult_MT(IFloatData2D m, IFloatData2D mt)
        {
            var size = new Index2(m.W, m.H);
            var output = ILGPUMethods.Allocate<float>(size);
            _M_mult_MT(size, output.View, m.MemoryView, mt.MemoryView);
            return new GPU_FloatData2D(output);
        }

        public IFloatData2D MT_mult_M(IFloatData2D mt, IFloatData2D m)
        {
            var size = new Index2(mt.H, mt.W);
            var output = ILGPUMethods.Allocate<float>(size);
            _MT_mult_M(size, output.View, mt.MemoryView, m.MemoryView);
            return new GPU_FloatData2D(output);
        }

        public IFloatData SumLine(IFloatData2D arr)
        {
            if (arr.H <= 1)
                return new GPU_FloatData(arr.AsArray());

            var s = arr.H / 2d;
            var r = arr.H % 2d;
            while (s > 1)
            {
                var _size = new Index2(arr.W, (int)s);

                _sumLines(_size, (int)Math.Ceiling(r), arr.MemoryView);
                r = s % 2d;
                s /= 2d;
            }
            return new GPU_FloatData(_joinLines(arr.W, arr.MemoryView));
        }

        public IFloatData SumColumn(IFloatData2D arr)
        {
            if (arr.W <= 1)
                return new GPU_FloatData(arr.AsArray());

            var s = arr.W / 2d;
            var r = arr.W % 2d;
            while (s > 1)
            {
                var _size = new Index2((int)s, arr.H);

                _sumColumns(_size, (int)Math.Ceiling(r), arr.MemoryView);
                r = s % 2;
                s /= 2;
            }
            return new GPU_FloatData(_joinColumns(arr.H, arr.MemoryView));
        }

        private void _sumLines(Index2 size, int r, ArrayView2D<float> m)
        {
            _M_SumLine(size, m, r, size.Y);
        }

        private void _sumColumns(Index2 size, int r, ArrayView2D<float> m)
        {
            _M_SumColumn(size, m, r, size.X);
        }

        private MemoryBuffer<float> _joinLines(Index size, ArrayView2D<float> m)
        {
            var output = ILGPUMethods.Allocate<float>(size);
            _M_2_lines_V(size, output.View, m);
            return output;
        }

        private MemoryBuffer<float> _joinColumns(Index size, ArrayView2D<float> m)
        {
            var output = ILGPUMethods.Allocate<float>(size);
            _M_2_columns_V(size, output.View, m);
            return output;
        }
    }
}
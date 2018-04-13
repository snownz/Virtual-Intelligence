using System;
using System.Threading.Tasks;
using VI.NumSharp.Drivers.Data.CPU;

namespace VI.NumSharp.Drivers.Executor.CPU
{
    public class FloatExecutorCPU : IFloatArrayExecutor
    {
        private float SafeDouble(double d)
        {
            float result = (float)d;
            if (float.IsPositiveInfinity(result)) return float.MaxValue;
            if (float.IsNegativeInfinity(result)) return float.MinValue;
            return result;
        }

        public IFloatData V_mult_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = v0[i] * v1[i];
            return cache;
        }

        public IFloatData V_div_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = v0[i] / v1[i];
            return cache;
        }

        public IFloatData V_sub_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = v0[i] - v1[i];
            return cache;
        }

        public IFloatData V_add_V(IFloatData cache, IFloatData v0, IFloatData v1)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = v0[i] + v1[i];
            return cache;
        }

        public IFloatData V_add_C(IFloatData cache, IFloatData v, float c)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = v[i] + c;
            return cache;
        }

        public IFloatData V_mult_C(IFloatData cache, IFloatData v, float c)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = v[i] * c;
            return cache;
        }

        public IFloatData V_sub_C(IFloatData cache, IFloatData v, float c)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = v[i] - c;
            return cache;
        }

        public IFloatData V_sub_C(IFloatData cache, float c, IFloatData v)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = c - v[i];
            return cache;
        }

        public IFloatData V_div_C(IFloatData cache, IFloatData v, float c)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = v[i] / c;
            return cache;
        }

        public IFloatData V_div_C(IFloatData cache, float c, IFloatData v)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = c / v[i];
            return cache;
        }

        public IFloatData Tanh(IFloatData cache, IFloatData arr)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = SafeDouble(Math.Tanh(arr[i]));
            return cache;
        }

        public IFloatData Sin(IFloatData cache, IFloatData arr)
        {
             for(int i = 0; i < cache.Length; i++)  cache[i] = SafeDouble(Math.Sin(arr[i]));
            return cache;
        }

        public IFloatData Cos(IFloatData cache, IFloatData arr)
        {
            for(int i = 0; i < cache.Length; i++)  cache[i] = SafeDouble(Math.Cos(arr[i]));
            return cache;
        }

        public IFloatData Pow(IFloatData cache, IFloatData arr, float exp)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = SafeDouble(Math.Pow(arr[i], exp));
            return cache;
        }

        public IFloatData Exp(IFloatData cache, IFloatData arr)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = SafeDouble(Math.Exp(arr[i]));
            return cache;
        }

        public IFloatData Log(IFloatData cache, IFloatData arr)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = SafeDouble(Math.Log(arr[i]));
            return cache;
        }

        public IFloatData Sqrt(IFloatData cache, IFloatData arr)
        {
            for(int i = 0; i < cache.Length; i++) cache[i] = SafeDouble(Math.Sqrt(arr[i]));
            return cache;
        }

        public IFloatData2D VT_mult_M(IFloatData2D cache, IFloatData vt, IFloatData2D m)
        {
            for(int x = 0; x < m.W; x++)for(int y = 0; y < m.H; y++) cache[x, y] = vt[y] * m[x, y];
            return cache;
        }

        public IFloatData2D M_mult_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            for(int x = 0; x < m0.W; x++)for(int y = 0; y < m0.H; y++)cache[x, y] = m0[x, y] * m1[x, y]; 
            return cache;
        }

        public IFloatData2D M_div_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            for(int x = 0; x < m0.W; x++)for(int y = 0; y < m0.H; y++)cache[x, y] = m0[x, y] / m1[x, y];
            return cache;
        }

        public IFloatData2D M_sub_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            for(int x = 0; x < m0.W; x++)for(int y = 0; y <m0.H; y++)cache[x, y] = m0[x, y] - m1[x, y];
            return cache;
        }

        public IFloatData2D M_add_M(IFloatData2D cache, IFloatData2D m0, IFloatData2D m1)
        {
            for(int x = 0; x < m0.W; x++)for(int y = 0; y < m0.H; y++)cache[x, y] = m0[x, y] + m1[x, y];
            return cache;
        }

        public IFloatData2D M_mult_V(IFloatData2D cache, IFloatData2D m, IFloatData v)
        {
            for(int x = 0; x < m.W; x++)for(int y = 0; y < m.H; y++) cache[x, y] = m[x, y] * v[x];
            return cache;
        }

        public IFloatData2D M_mult_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            for(int x = 0; x < m.W; x++)for(int y = 0; y < m.H; y++) cache[x, y] = m[x, y] * c;
            return cache;
        }

        public IFloatData2D M_add_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            for(int x = 0; x < m.W; x++)for(int y = 0; y < m.H; y++) cache[x, y] = m[x, y] + c;
            return cache;
        }

        public IFloatData2D M_div_C(IFloatData2D cache, IFloatData2D m, float c)
        {
            for(int x = 0; x < m.W; x++)for(int y = 0; y < m.H; y++) cache[x, y] = m[x, y] / c;
            return cache;
        }

        public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, float c)
        {
            for(int x = 0; x < m.W; x++)for(int y = 0; y < m.H; y++) cache[x, y] = c / m[x, y];
            return cache;
        }

        public IFloatData2D C_div_M(IFloatData2D cache, IFloatData2D m, int c)
        {
            for(int x = 0; x < m.W; x++)for(int y = 0; y < m.H; y++) cache[x, y] = c / m[x, y];
            return cache;
        }

        public IFloatData2D Sqrt(IFloatData2D cache, IFloatData2D arr)
        {
            for(int x = 0; x < arr.W; x++) for(int y = 0; y < arr.H; y++) cache[x, y] = SafeDouble(Math.Sqrt(arr[x, y]));
            return cache;
        }

        public IFloatData2D VT_mult_V(IFloatData vt, IFloatData v)
        {
            var output = new float[v.Length, vt.Length];
            for(int x = 0; x < v.Length; x++) for(int y = 0; y < vt.Length; y++ )  output[x, y] = vt[y] * v[x];
            return new CPU_FloatData2D(output);
        }

        public IFloatData2D M_mult_MT(IFloatData2D mt, IFloatData2D m)
        {
            var output = new float[mt.W, mt.H];
            for(int x = 0; x < mt.W; x++ ) for(int y = 0; y < mt.H;  y ++ ) output[y, x] = m[x, y] * mt[y, x]; 
            return new CPU_FloatData2D(output);
        }

        public IFloatData SumLine(IFloatData2D arr)
        {
            var output = new float[arr.W];
            for(int x = 0; x < arr.W; x++ )
            {
                var sum = 0f;
                for (var y = 0; y < arr.H; y++) sum += arr[x, y];

                output[x] = sum;
            };
            return new CPU_FloatData(output);
        }

        public IFloatData SumColumn(IFloatData2D arr)
        {
            var output = new float[arr.H];
            for(int y = 0; y < arr.H; y++ )
            {
                var sum = 0f;
                for (var x = 0; x < arr.W; x++) sum += arr[x, y];

                output[y] = sum;
            };
            return new CPU_FloatData(output);
        }

        public IFloatData ApplyMask(IFloatData arr, IByteData mask)
        {
            var output = new float[arr.Length];
            for (var x = 0; x < arr.Length; x++) output[x] = arr[x] * mask[x];
            return new CPU_FloatData(output);
        }

        public IFloatData2D ApplyMask(IFloatData2D arr, IByteData2D mask)
        {
            var output = new float[arr.W, arr.H];
            for (var x = 0; x < arr.W; x++)
                for (var y = 0; x < arr.H; x++)
                    output[x, y] = arr[x, y] * mask[x, y];
            return new CPU_FloatData2D(output);
        }
    }
}
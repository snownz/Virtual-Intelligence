using System;
using System.Threading.Tasks;
using VI.NumSharp.Arrays;

namespace VI.Neural.Drivers.Executors
{
    public class ParallelCpuActivation : IActivationExecutor
    {
        public FloatArray ArcTANH(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => result[x] = (float)Math.Pow( Math.Tanh( v[x] ), -1 ) );
            return result;
        }   
        public FloatArray DArcTANH(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => result[x] = 1 / ( v[x] * v[x] + 1) );
            return result;
        }

        public FloatArray Elu(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => 
            {
                var mask = ( v[x] <= 0 ? 1 : 0 ) * 1f;
                var less_zero = v[x] * mask;
                var safe =  ( v[x]  > 0 ? 1 : 0 ) * 1f;
                var greater_zero = v[x] * safe;
                var final = 3f * ( (float)Math.Exp(less_zero) - 1f ) * less_zero;
                result[x] = greater_zero + final;
            } );
            return result;
        }
        public FloatArray DElu(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => 
            {
                var safe  = ( v[x] > 0 ? 1 : 0) * 1f;
                var mask2 = ( v[x] <= 0 ? 1 : 0) * 1f;
                var temp  = v[x] * mask2;
                var final = ( 3f * (float)Math.Exp(temp) ) * mask2;
                result[x] = ( v[x] * safe ) + final;
            } );
            return result;
        }

        public FloatArray LeakRelu(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => result[x] =  Math.Max( .001f * v[x], v[x] ) );
            return result;
        }
        public FloatArray DLeakRelu(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => result[x] = (v[x] >= .001f ? 1 : 0) + .001f );
            return result;
        }

        public FloatArray Sigmoid(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => result[x] =  1f / ( 1f + (float) Math.Exp( -2f * v[x] ) ) );
            return result;
        }
        public FloatArray DSigmoid(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => result[x] = 2f * v[x] * ( 1 - v[x] ) );
            return result;
        }

        public FloatArray DTANH(FloatArray v)
        {
            var result = new FloatArray(v.Length);
            Parallel.For(0, v.Length, x => result[x] = 1f- ( v[x] * v[x] ) );
            return result;
        }
    }
}
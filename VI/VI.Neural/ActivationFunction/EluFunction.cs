using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class EluFunction : IActivationFunction
    {
        public FloatArray Activate(FloatArray sum)
        {
            var mask = ( sum <= 0 ) * 1f;
            var less_zero = sum * mask;
            var safe =  ( sum > 0 ) * 1f;
            var greater_zero = sum * safe;
            var final = 3f * ( less_zero.Exp() - 1f ) * less_zero;
            return greater_zero + final;
        }

        public FloatArray Derivate(FloatArray sum, FloatArray act)
        {
            var safe  = ( sum > 0  ) * 1f;
            var mask2 = ( sum <= 0 ) * 1f;
            var temp  = sum * mask2;
            var final = ( 3f * temp.Exp() ) * mask2;
            return ( sum * safe ) + final;
        }
    }
}
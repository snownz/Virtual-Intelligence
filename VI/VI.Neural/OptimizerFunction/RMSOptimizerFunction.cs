using VI.Neural.Consts;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class RMSOptimizerFunction : IOptimizerFunction
    {
        private FloatArray2D gW;
        private FloatArray bW;

        private float e;
        private float v;
        private float m;

        public RMSOptimizerFunction()
        {
            e = OptimizationFunctionsConsts.Epsilon;
            v = 0.999f;
            m = 0.001f;
        }

        public void CalculateParams(ILayer target)
        {
            gW = NumMath.Array(target.Size, target.ConectionsSize);
            bW = NumMath.Array(target.Size);
        }

        public FloatArray Error(FloatArray target, FloatArray output)
        {
            return output - target;
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {           
            gW = ( ( v * gW ) + ( m * ( dW * dW ) ) );
            target.KnowlodgeMatrix -= ( ( target.LearningRate / ( gW + e ).Sqrt() ) * dW ); 
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            bW = ( ( v * bW ) + ( m * ( dB * dB ) ) );
            target.BiasVector -= ( ( target.LearningRate / (bW + e ).Sqrt() ) * dB );
        }
    }
}
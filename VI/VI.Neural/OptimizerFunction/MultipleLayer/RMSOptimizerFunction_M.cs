using VI.Neural.Consts;
using VI.Neural.Layer;
using VI.Neural.OptimizerFunction.MultipleLayer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class RMSOptimizerFunction_M : IOptimizerMultipleLayerFunction
    {
        private Array<FloatArray2D> gW;
        private FloatArray bW;

        private float e;
        private float v;
        private float m;

        public RMSOptimizerFunction_M()
        {
            e = OptimizationFunctionsConsts.Epsilon;
            v = 0.999f;
            m = 0.001f;
        }

        public void CalculateParams(IMultipleLayer target)
        {
            gW = NumMath.Array(target.Size, target.ConectionsSize);
            bW = NumMath.Array(target.Size);
        }

        public FloatArray Error(FloatArray target, FloatArray output)
        {
            return output - target;
        }

        public void UpdateWeight(IMultipleLayer target, Array<FloatArray2D> dW)
        {   for(int i = 0; i < target.ConectionsSize.Length; i++)
            {      
                gW[i] = ( ( v * gW[i] ) + ( m * ( dW[i] * dW[i] ) ) );
                target.KnowlodgeMatrix[i] -= ( ( target.LearningRate / ( gW[i] + e ).Sqrt() ) * dW[i] );
            } 
        }

        public void UpdateBias(IMultipleLayer target, FloatArray dB)
        {
            bW = ( ( v * bW ) + ( m * ( dB * dB ) ) );
            target.BiasVector -= ( ( target.LearningRate / (bW + e ).Sqrt() ) * dB );
        }
    }
}
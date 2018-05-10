using VI.Neural.Layer;
using VI.Neural.OptimizerFunction.MultipleLayer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class AdadeltaOptimizerFunction_M : IOptimizerMultipleLayerFunction
    {
        private Array<FloatArray2D> AdaDeltaW;
        private Array<FloatArray2D> AdaDeltaW_v;
        private FloatArray AdaDeltaB;
        private FloatArray AdaDeltaB_v;

        private float e;
        private float v;
        private float m;

        public AdadeltaOptimizerFunction_M()
        {
            e =  1e-8f;
            v = 0.001f;
            m = 0.999f;
        }

        public void CalculateParams(IMultipleLayer target)
        {
            AdaDeltaW   = NumMath.Array(target.Size, target.ConectionsSize);
            AdaDeltaW_v = NumMath.Array(target.Size, target.ConectionsSize);
            AdaDeltaB   = NumMath.Array(target.Size);
            AdaDeltaB_v = NumMath.Array(target.Size);
        }

        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }

        public void UpdateBias(IMultipleLayer target, FloatArray dB)
        {
            AdaDeltaB   = ( v * AdaDeltaB ) + ( m * ( dB * dB ) );
            var mid     = -1 * ( ( ( AdaDeltaB_v + e ).Sqrt() / ( AdaDeltaB + e ).Sqrt() ) * dB );
            AdaDeltaB_v = ( v * AdaDeltaB_v ) + ( m * ( mid * mid ) );
            target.BiasVector -= mid;
        }

        public void UpdateWeight(IMultipleLayer target, Array<FloatArray2D> dW)
        {
            for(int i = 0; i < target.ConectionsSize.Length; i++)
            {
                AdaDeltaW[i]   = ( v * AdaDeltaW[i] ) + ( m * ( dW[i] * dW[i] ) );
                var mid     = -1 * ( ( ( AdaDeltaW_v[i] + e ).Sqrt() / ( AdaDeltaW[i] + e ).Sqrt() ) * dW[i] );
                AdaDeltaW_v[i] = ( v * AdaDeltaW_v[i] ) + ( m * ( mid * mid ) );
                target.KnowlodgeMatrix[i] -= mid;
            }
        }
    }
}
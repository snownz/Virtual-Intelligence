using VI.Neural.Layer;
using VI.Neural.OptimizerFunction.MultipleLayer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class AdamOptimizerFunction_M : IOptimizerMultipleLayerFunction
    {
        private Array<FloatArray2D> mW;
        private Array<FloatArray2D> vW;
        private FloatArray mB;
        private FloatArray vB;

        private float e;
        private float b1;
        private float b2;

        public AdamOptimizerFunction_M()
        {
            e =  1e-8f;
            b1 = 0.9f;
            b2 = 0.999f;
        }

        public void CalculateParams(IMultipleLayer target)
        {
            mW = NumMath.Array(target.Size, target.ConectionsSize);
            vW = NumMath.Array(target.Size, target.ConectionsSize);
            mB = NumMath.Array(target.Size);
            vB = NumMath.Array(target.Size);
        }

        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }

        public void UpdateBias(IMultipleLayer target, FloatArray dB)
        {
            mB  = ( b1 * mB )  + ( ( 1f - b1 ) *  dB );
            vB  = ( b2 * vB )  + ( ( 1f - b2 ) * ( dB * dB ) );
            var Adam_m_b_hat  = mB / ( 1f - b1 );
            var Adam_v_b_hat  = vB / ( 1f - b2 );
            target.BiasVector -= ( target.LearningRate / ( ( Adam_v_b_hat  ).Sqrt() + e ) ) * Adam_m_b_hat;
        }

        public void UpdateWeight(IMultipleLayer target, Array<FloatArray2D> dW)
        {
            for(int i = 0; i < target.ConectionsSize.Length; i++)
            {
                mW[i]  = ( b1 * mW[i] )  + ( ( 1f - b1 ) * dW[i] );
                vW[i]  = ( b2 * vW[i] )  + ( ( 1f - b2 ) * ( dW[i] * dW[i] ) );
                var Adam_m_ws_hat  = mW[i]  / ( 1f - b1 );
                var Adam_v_ws_hat  = vW[i]  / ( 1f - b2 );
                target.KnowlodgeMatrix[i] -= ( target.LearningRate / ( ( Adam_v_ws_hat ).Sqrt() + e ) ) * Adam_m_ws_hat;
            }
        }
    }
}
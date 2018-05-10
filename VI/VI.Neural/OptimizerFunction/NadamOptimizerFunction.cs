using VI.Neural.Drivers.Executors;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class NadamOptimizerFunction : IOptimizerFunction
    {
        private FloatArray2D mW;
        private FloatArray2D vW;
        private FloatArray mB;
        private FloatArray vB;

        private float e;
        private float b1;
        private float b2;

        public NadamOptimizerFunction()
        {
            e = 1e-8f;
            b1 = 0.9f;
            b2 = 0.999f;
        }

        public void CalculateParams(ILayer target)
        {
            mW = NumMath.Array(target.Size, target.ConectionsSize);
            vW = NumMath.Array(target.Size, target.ConectionsSize);
            mB = NumMath.Array(target.Size);
            vB = NumMath.Array(target.Size);
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            //mB  = ( b1 * mB )  + ( ( 1f - b1 ) *  dB );
            //vB  = ( b2 * vB )  + ( ( 1f - b2 ) * ( dB * dB ) );
            //var Adam_m_b_hat  = mB / ( 1f - b1 );
            //var Adam_v_b_hat  = vB / ( 1f - b2 );
            //target.BiasVector -= ( target.LearningRate / ( Adam_v_b_hat.Sqrt() + e ) ) * ( b1 * Adam_m_b_hat + ( ( ( 1 - b1 ) * dB ) / ( 1 - b1 ) ) );
            ProcessingDriver.Optimization.Nadam(target.BiasVector, dB, mB, vB, target.LearningRate);
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {
            //mW  = ( b1 * mW )  + ( ( 1f - b1 ) * dW );
            //vW  = ( b2 * vW )  + ( ( 1f - b2 ) * ( dW * dW ) );
            //var Adam_m_ws_hat  = mW  / ( 1f - b1 );
            //var Adam_v_ws_hat  = vW  / ( 1f - b2 );
            //target.KnowlodgeMatrix -= ( target.LearningRate / ( Adam_v_ws_hat.Sqrt() + e ) ) * ( b1 * Adam_m_ws_hat + ( ( ( 1 - b1 ) * dW ) / ( 1 - b1 ) ) );
            ProcessingDriver.Optimization.Nadam(target.KnowlodgeMatrix, dW, mW, vW, target.LearningRate);
        }
    }
}
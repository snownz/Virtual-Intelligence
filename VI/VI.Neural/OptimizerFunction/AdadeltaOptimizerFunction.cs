using VI.Neural.Drivers.Executors;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class AdadeltaOptimizerFunction : IOptimizerFunction
    {
        private FloatArray2D AdaDeltaW;
        private FloatArray2D AdaDeltaW_v;
        private FloatArray AdaDeltaB;
        private FloatArray AdaDeltaB_v;

        private float e;
        private float v;
        private float m;

        public AdadeltaOptimizerFunction()
        {
            e = 1e-8f;
            v = 0.001f;
            m = 0.999f;
        }

        public void CalculateParams(ILayer target)
        {
            AdaDeltaW   = NumMath.Array(target.Size, target.ConectionsSize);
            AdaDeltaW_v = NumMath.Array(target.Size, target.ConectionsSize);
            AdaDeltaB   = NumMath.Array(target.Size);
            AdaDeltaB_v = NumMath.Array(target.Size);
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            //AdaDeltaB   = ( v * AdaDeltaB ) + ( m * ( dB * dB ) );
            //var mid     = -1 * ( ( ( AdaDeltaB_v + e ).Sqrt() / ( AdaDeltaB + e ).Sqrt() ) * dB );
            //AdaDeltaB_v = ( v * AdaDeltaB_v ) + ( m * ( mid * mid ) );
            //target.BiasVector -= mid;
            ProcessingDriver.Optimization.Adadelta(target.BiasVector, dB, AdaDeltaB, AdaDeltaB_v, target.LearningRate);
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {
            //AdaDeltaW   = ( v * AdaDeltaW ) + ( m * ( dW * dW ) );
            //var mid     = -1 * ( ( ( AdaDeltaW_v + e ).Sqrt() / ( AdaDeltaW + e ).Sqrt() ) * dW );
            //AdaDeltaW_v = ( v * AdaDeltaW_v ) + ( m * ( mid * mid ) );
            //target.KnowlodgeMatrix -= mid;
            ProcessingDriver.Optimization.Adadelta(target.KnowlodgeMatrix, dW, AdaDeltaW, AdaDeltaW_v, target.LearningRate);
        }
    }
}
using VI.Neural.Drivers.Executors;
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
            e =  1e-8f;
            v = 0.999f;
            m = 0.001f;
        }

        public void CalculateParams(ILayer target)
        {
            gW = NumMath.Array(target.Size, target.ConectionsSize);
            bW = NumMath.Array(target.Size);
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {           
            //gW = ( ( v * gW ) + ( m * ( dW * dW ) ) );
            //target.KnowlodgeMatrix -= ( ( target.LearningRate / ( gW + e ).Sqrt() ) * dW ); 
            ProcessingDriver.Optimization.RMSProp(target.KnowlodgeMatrix, dW, gW, target.LearningRate);
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            //bW = ( ( v * bW ) + ( m * ( dB * dB ) ) );
            //target.BiasVector -= ( ( target.LearningRate / (bW + e ).Sqrt() ) * dB );
            ProcessingDriver.Optimization.RMSProp(target.BiasVector, dB, bW, target.LearningRate);
        }
    }
}
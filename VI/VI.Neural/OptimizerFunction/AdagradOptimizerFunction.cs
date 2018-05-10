using VI.Neural.Drivers.Executors;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class AdagradOptimizerFunction : IOptimizerFunction
    {
        private FloatArray2D mW;
        private FloatArray mB;

        private float e;

        public AdagradOptimizerFunction()
        {
           e = 1e-8f;
        }

        public void CalculateParams(ILayer target)
        {
            mW = NumMath.Array(target.Size, target.ConectionsSize);
            mB = NumMath.Array(target.Size);
        }

        public void UpdateBias(ILayer target, FloatArray dB)
        {
            //mB += ( dB * dB );
            //target.BiasVector -= ( ( target.LearningRate / ( mB + e ).Sqrt() ) * dB );
            ProcessingDriver.Optimization.AdaGrad(target.BiasVector, dB, mB, target.LearningRate);
        }

        public void UpdateWeight(ILayer target, FloatArray2D dW)
        {
            //mW += ( dW * dW );
            //target.KnowlodgeMatrix -= ( ( target.LearningRate / ( mW + e ).Sqrt() ) * dW );
            ProcessingDriver.Optimization.AdaGrad(target.KnowlodgeMatrix , dW, mW, target.LearningRate);
            
        }
    }
}
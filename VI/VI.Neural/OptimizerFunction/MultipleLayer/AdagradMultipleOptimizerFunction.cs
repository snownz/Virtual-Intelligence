using System.Threading.Tasks;
using VI.Neural.Layer;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction.MultipleLayer
{
    public class AdagradMultipleOptimizerFunction : IOptimizerMultipleLayerFunction
    {
        private Array<FloatArray2D> mW;
        private FloatArray mB;

        public void CalculateParams(IMultipleLayer target)
        {
            mW = NumMath.Array(target.Size, target.ConectionsSize);
            mB = NumMath.Array(target.Size);
        }

        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }

        public void UpdateBias(IMultipleLayer target, FloatArray dB)
        {
            mB += dB * dB;
            target.BiasVector -= ((target.LearningRate / (mB + 1e-8f).Sqrt()) * dB);
        }

        public void UpdateWeight(IMultipleLayer target, Array<FloatArray2D> dW)
        {
            Parallel.For(0, target.ConectionsSize.Length, i =>
            {
                mW[i] += dW[i] * dW[i];
                target.KnowlodgeMatrix[i] -= ((target.LearningRate / (mW[i] + 1e-8f).Sqrt()) * dW[i]);
            });
        }
    }
}
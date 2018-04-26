using VI.Neural.Layer;
using VI.Neural.OptimizerFunction.MultipleLayer;
using VI.NumSharp.Arrays;

namespace VI.Neural.OptimizerFunction
{
    public class SGDOptimizerFunction_M : IOptimizerMultipleLayerFunction
    {
        public void CalculateParams(IMultipleLayer target)
        {
        }

        public FloatArray Error(FloatArray targetOutputVector, FloatArray values)
        {
            return values - targetOutputVector;
        }

        public void UpdateWeight(IMultipleLayer target, Array<FloatArray2D> dW)
        {
            for(int i = 0; i < target.ConectionsSize.Length; i++)
            {
                target.KnowlodgeMatrix[i] -=  dW[i] * target.LearningRate;
            }
        }

        public void UpdateBias(IMultipleLayer target, FloatArray dB)
        {
            target.BiasVector -=  dB * target.LearningRate;
        }
    }
}
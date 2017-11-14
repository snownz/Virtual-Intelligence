using VI.Neural.Layer;
using VI.NumSharp.Arrays;

namespace VI.Neural.LearningImproveMethods
{
    public interface IAnnMomentumOperations
    {
        Array<float> ComputeBiasMomentum();
        Array2D<float> ComputeWeightMomentum(ActivationLayer target, float momentum);
    }
}
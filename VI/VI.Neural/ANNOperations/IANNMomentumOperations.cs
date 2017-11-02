using VI.Neural.Layer;
using VI.NumSharp.Array;

namespace VI.Neural.ANNOperations
{
    public interface IANNMomentumOperations
    {
        Array<float> ComputeBiasMomentum();
        Array2D<float> ComputeWeightMomentum(ActivationLayer2 target, float momentum);
    }
}
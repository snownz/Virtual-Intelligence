using VI.Cognitive.Layer;
using VI.NumSharp.Array;

namespace VI.Cognitive.ANNOperations
{
    public interface IANNMomentumOperations
    {
        Array<float> ComputeBiasMomentum();
        Array2D<float> ComputeWeightMomentum(ActivationLayer2 target, float momentum);
    }
}
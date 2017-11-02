using VI.Neural.Layer;
using VI.NumSharp.Array;

namespace VI.Neural.ANNOperations
{
    public sealed class ANNMomentumOperations : IANNMomentumOperations
    {
        public Array<float> ComputeBiasMomentum()
        {
            throw new System.NotImplementedException();
        }

        public Array2D<float> ComputeWeightMomentum(ActivationLayer2 target, float momentum)
        {
            return null;
        }
    }
}

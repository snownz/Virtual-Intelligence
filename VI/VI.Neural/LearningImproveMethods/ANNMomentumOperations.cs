using VI.Neural.Layer;
using VI.NumSharp.Array;

namespace VI.Neural.LearningImproveMethods
{
    public sealed class AnnMomentumOperations : IAnnMomentumOperations
    {
        public Array<float> ComputeBiasMomentum()
        {
            throw new System.NotImplementedException();
        }

        public Array2D<float> ComputeWeightMomentum(ActivationLayer target, float momentum)
        {
            return null;
        }
    }
}

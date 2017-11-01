using ILGPU.Runtime;
using VI.Cognitive.Layer;
using VI.Cognitive.Provider;

namespace VI.Cognitive.ANNOperations
{
    public sealed class ANNMomentumOperations : IANNMomentumOperations
    {
        private readonly IAnnArrayProvider _operationsProvider;

        public void Momentum(ActivationLayer target, float momentum)
        {
            target.Momentum = momentum;
            target.CachedMomentum = target.LearningRate * target.Momentum;
            target.CachedLearningRate = target.LearningRate * (1 - target.Momentum);
        }

        public MemoryBuffer2D<float> ComputeWeightMomentum()
        {
            return null;
        }

        public MemoryBuffer<float> ComputeBiasMomentum()
        {
            return null;
        }
    }
}

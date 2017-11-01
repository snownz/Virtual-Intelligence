using ILGPU.Runtime;
using VI.Cognitive.Layer;

namespace VI.Cognitive.ANNOperations
{
    public interface IANNMomentumOperations
    {
        MemoryBuffer<float> ComputeBiasMomentum();
        MemoryBuffer2D<float> ComputeWeightMomentum();
        void Momentum(ActivationLayer target, float momentum);
    }
}
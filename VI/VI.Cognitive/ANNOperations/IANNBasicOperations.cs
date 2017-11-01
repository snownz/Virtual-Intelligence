using ILGPU.Runtime;
using VI.Cognitive.Layer;

namespace VI.Cognitive.ANNOperations
{
    public interface IANNBasicOperations
    {
        void BackWard(ActivationLayer target, MemoryBuffer<float> de_dOut);
        void BackWardDesired(ActivationLayer target, MemoryBuffer<float> desired);
        void BackWardError(ActivationLayer target, MemoryBuffer<float> error);
        void ErrorGradient(ActivationLayer target, MemoryBuffer<float> error, MemoryBuffer<float> inputs);
        void FeedForward(MemoryBuffer<float> feed, ActivationLayer forWard);
        void UpdateBias(ActivationLayer target);
        void UpdateBias(ActivationLayer target, MemoryBuffer<float> u);
        void UpdateWeight(ActivationLayer target);
        void UpdateWeight(ActivationLayer target, MemoryBuffer2D<float> u);
    }
}
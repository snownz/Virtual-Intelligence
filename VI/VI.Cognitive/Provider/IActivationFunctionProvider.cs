using ILGPU.Runtime;
using VI.NumSharp.Array;

namespace VI.Cognitive.Provider
{
    public interface IActivationFunctionProvider
    {
        void Activation(MemoryBuffer<float> vSource, MemoryBuffer<float> vTarget);
        MemoryBuffer<float> Derivated(MemoryBuffer<float> vSource);
        void Activation(Array<float> vSource, Array<float> vTarget);
        Array<float> Derivated(Array<float> vSource);
    }
}
using ILGPU.Runtime;
using VI.NumSharp.Array;

namespace VI.Cognitive.Provider
{
    public interface ILossFunctionProvider
    {
        MemoryBuffer<float> Error(MemoryBuffer<float> v0, MemoryBuffer<float> v1);
        Array<float> Error(Array<float> v0, Array<float> v1);
        float Loss();
    }
}
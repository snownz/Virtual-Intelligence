using ILGPU.Runtime;
using VI.NumSharp.Array;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Provider
{
    public class LossFunctionProvider : ILossFunctionProvider
    {
        private IAnnParallelInterface _function;
        public LossFunctionProvider(IAnnParallelInterface function)
        {
            _function = function;
        }


        public Array<float> Error(Array<float> v0, Array<float> v1)
        {
            throw new System.NotImplementedException();
        }

        public float Loss()
        {
            return 0f;
        }

        public MemoryBuffer<float> Error(MemoryBuffer<float> v0, MemoryBuffer<float> v1)
        {
            return null;
        }
    }
}

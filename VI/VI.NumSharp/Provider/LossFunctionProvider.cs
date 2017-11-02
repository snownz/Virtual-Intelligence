using VI.NumSharp.Array;
using VI.ParallelComputing.Drivers;

namespace VI.NumSharp.Provider
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
            var size = v0.View.Length;
            var vTarget = _function.Executor.CreateBuffer<float>(size);
            _function.Executor["Error"].Launch(size, vTarget.View, v0.View.View, v1.View.View);
            _function.Executor.Wait();
            return new Array<float>(vTarget);
        }

        public float Loss()
        {
            return 0f;
        }
    }
}

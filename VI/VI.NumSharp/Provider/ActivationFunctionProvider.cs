using VI.NumSharp.Array;
using VI.ParallelComputing.Drivers;

namespace VI.NumSharp.Provider
{
    public class ActivationFunctionProvider : IActivationFunctionProvider
    {
        private IAnnParallelInterface _function;
        public ActivationFunctionProvider(IAnnParallelInterface function)
        {
            _function = function;
        }       

        public void Activation(Array<float> vSource, Array<float> vTarget)
        {
            var size = vSource.View.Length;
            _function.Executor["Function"].Launch(size, vTarget.View.View, vSource.View.View);
            _function.Executor.Wait();
        }

        public Array<float> Derivated(Array<float> vSource)
        {
            var size = vSource.View.Length;
            var vTarget = _function.Executor.CreateBuffer<float>(size);
            _function.Executor["Derivative"].Launch(size, vTarget.View, vSource.View.View);
            _function.Executor.Wait();
            return  new Array<float>(vTarget);
        }
    }
}

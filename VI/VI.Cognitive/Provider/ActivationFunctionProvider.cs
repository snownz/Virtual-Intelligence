using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.NumSharp.Array;
using VI.ParallelComputing;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Provider
{
    public class ActivationFunctionProvider : IActivationFunctionProvider
    {
        private IAnnParallelInterface _function;
        public ActivationFunctionProvider(IAnnParallelInterface function)
        {
            _function = function;
        }

        public void Activation(MemoryBuffer<float> vSource, MemoryBuffer<float> vTarget)
        {

        }

        public MemoryBuffer<float> Derivated(MemoryBuffer<float> vSource)
        {
            return null;
        }

        public void Activation(Array<float> vSource, Array<float> vTarget)
        {
            var size = vSource.View.Length;
            _function.Executor["Function"].Launch(size, vTarget.View.View, vSource.View.View);
            _function.Executor.Wait();
        }

        public Array<float> Derivated(Array<float> vSource)
        {
            throw new NotImplementedException();
        }
    }
}

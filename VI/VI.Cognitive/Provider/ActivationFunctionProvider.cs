using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VI.NumSharp.Array;
using VI.ParallelComputing.ANN;

namespace VI.Cognitive.Provider
{
    public class ActivationFunctionProvider : IActivationFunctionProvider
    {
        public ActivationFunctionProvider()
        {

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
            throw new NotImplementedException();
        }

        public Array<float> Derivated(Array<float> vSource)
        {
            throw new NotImplementedException();
        }
    }
}

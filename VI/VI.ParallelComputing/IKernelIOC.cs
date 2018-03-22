using System;
using System.Collections.Generic;
using System.Text;

namespace VI.ParallelComputing
{
    interface IKernelIOC
    {
        Dictionary<string, Action> LoadKernels();
    }
}

using System;
using System.Collections.Generic;

namespace VI.ParallelComputing
{
    internal interface IKernelIOC
    {
        Dictionary<string, Action> LoadKernels();
    }
}
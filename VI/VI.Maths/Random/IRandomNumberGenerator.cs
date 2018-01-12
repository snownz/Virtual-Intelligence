using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Maths.Random
{
    public interface IRandomNumberGenerator
    {
        float Mean { get; }
        float Variance { get; }
        float Next();
        void SetSeed(int seed);
    }
}

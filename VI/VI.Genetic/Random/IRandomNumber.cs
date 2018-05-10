using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Genetic.Random
{
    public interface IRandomNumber
    {
        float Mean { get; }
        float Variance { get; }
        float Next();
        void SetSeed(int seed);
    }
}

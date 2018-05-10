using VI.Genetic.Chromosomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Genetic.Fitness
{
    public interface IFitnessValue
    {
        bool MoreThan(IFitnessValue value);
        bool LessThan(IFitnessValue value);
        IFitnessValue NewInstance();
    }
}

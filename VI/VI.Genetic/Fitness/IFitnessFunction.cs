using VI.Genetic.Chromosomes;
using System;
using System.Collections.Generic;
using System.Text;
using VI.Genetic.Data;

namespace VI.Genetic.Fitness
{
    public interface IFitnessFunction
    {
        IFitnessValue Evaluate(IChromosome chromosome);        
    }

    public interface IFitnessFunctionGeneric<T> : IFitnessFunction where T : class
    {
        void SetDataToTest(List<T> dt);
        void SetDataToValidate(List<T> dt);
    }
}

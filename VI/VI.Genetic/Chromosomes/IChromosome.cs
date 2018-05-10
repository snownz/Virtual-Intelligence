using VI.Genetic.Fitness;
using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Genetic.Chromosomes
{
    public interface IChromosome
    {
        IFitnessValue Fitness { get; }
        void Generate();
        IChromosome CreateNew();
        IChromosome Clone();
        void Mutate();
        void Crossover(IChromosome pair);
        void Evaluate(IFitnessFunction function);
        object Decode();        
    }
}

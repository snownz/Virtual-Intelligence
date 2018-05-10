using VI.Genetic.Chromosomes;
using System;
using System.Collections.Generic;
using System.Text;
using VI.Genetic.Fitness;

namespace VI.Genetic.Selection
{
    public interface ISelectionBest
    {
        (IFitnessValue avg, IFitnessValue max, IChromosome best) Find(List<IChromosome> chromosomes);
    }
}

using VI.Genetic.Chromosomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Genetic.Selection
{
    public interface ISelectionMethod
    {
        void ApplySelection(List<IChromosome> chromosomes, int size);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VI.Genetic.Chromosomes;

namespace VI.Genetic.Selection
{
    public interface ISelectionMethod
    {
        void ApplySelection(IList<IChromosome> chromosomes, int size);
    }
}

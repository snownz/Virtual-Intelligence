using VI.Genetic.Chromosomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VI.Genetic.Selection
{
    public class EliteSelectionMethod : ISelectionMethod
    {
        public EliteSelectionMethod()
        {
        }

        public void ApplySelection(List<IChromosome> chromosomes, int size)
        {
            //chromosomes.Sort();
            chromosomes
                .OrderByDescending(x=>x.Fitness)
                .ToList()
                .RemoveRange(size, chromosomes.Count - size);
        }
    }
}

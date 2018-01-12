using System.Collections.Generic;
using VI.Genetic.Chromosomes;

namespace VI.Genetic.Selection
{
    public class EliteSelectionMethod : ISelectionMethod
    {
        public EliteSelectionMethod()
        {
        }

        public void ApplySelection(List<IChromosome> chromosomes, int size)
        {
            chromosomes.Sort();
            chromosomes.RemoveRange(size, chromosomes.Count - size);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using VI.Genetic.Fitness;

namespace VI.Genetic.Chromosomes
{
    public abstract class GeneBase : IChromosome
    {
        protected float fitness = 0;

        public float Fitness => fitness;
                
        public abstract void Generate();        
        public abstract IChromosome CreateNew();
        public abstract IChromosome Clone();
        public abstract void Mutate();
        public abstract void Crossover(IChromosome pair);

        public void Evaluate(IFitnessFunction function)
        {
            fitness = function.Evaluate(this);
        }

        public int CompareTo(object o)
        {
            double f = ((GeneBase)o).fitness;

            return (fitness == f) ? 0 : (fitness < f) ? 1 : -1;
        }
    }
}

using VI.Genetic.Fitness;
using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Genetic.Chromosomes
{
    public abstract class GeneBase : IChromosome
    {
        protected IFitnessValue fitness;

        public IFitnessValue Fitness => fitness;

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
            IFitnessValue f = ((GeneBase)o).fitness;

            return (fitness.Equals(f)) ? 0 : (fitness.LessThan(f)) ? 1 : -1;
        }

        public virtual object Decode()
        {
            throw new NotImplementedException();
        }
    }
}

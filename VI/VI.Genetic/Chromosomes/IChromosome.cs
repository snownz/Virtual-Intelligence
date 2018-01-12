using VI.Genetic.Fitness;

namespace VI.Genetic.Chromosomes
{
    public interface IChromosome
    {        
        float Fitness { get; }
        void Generate();
        IChromosome CreateNew();
        IChromosome Clone();
        void Mutate();
        void Crossover(IChromosome pair);
        void Evaluate(IFitnessFunction function);
    }
}

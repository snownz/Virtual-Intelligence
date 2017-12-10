using VI.Genetic.Fitness;

namespace VI.Genetic.Chromosomes
{
    public interface IChromosome
    {        
        object FitnessValue { get; }
        float Fitness { get; set; }
        void Generate();
        IChromosome CreateNew();
        IChromosome Clone();
        void Mutate();
        void Crossover(IChromosome pair);
        void Evaluate(IFitnessFunction function);
    }
}

using VI.Genetic.Chromosomes;

namespace VI.Genetic.Fitness
{
    public interface IFitnessFunction
    {
        float Evaluate(IChromosome chromosome);
    }
}

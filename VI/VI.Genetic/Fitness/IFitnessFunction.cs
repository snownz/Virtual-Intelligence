using VI.Genetic.Chromosomes;

namespace VI.Genetic.Fitness
{
    public interface IFitnessFunction
    {
        double Evaluate(IChromosome chromosome);
    }
}

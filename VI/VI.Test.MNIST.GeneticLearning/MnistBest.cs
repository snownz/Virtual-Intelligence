using System.Collections.Generic;
using System.Linq;
using VI.Data.Array;
using VI.Data.MNIST;
using VI.Genetic.Chromosomes;
using VI.Genetic.Data;
using VI.Genetic.Fitness;
using VI.Genetic.Random;
using VI.Genetic.Selection;
using VI.ML.Tools.ModelsFramework;
using VI.Neural.Models;
using VI.NumSharp.Arrays;

namespace VI.Test.MNIST.GeneticLearning
{
    public sealed class MnistBest : ISelectionBest
    {
        public (IFitnessValue avg, IFitnessValue max, IChromosome best) Find(List<IChromosome> chromosomes)
        {
            var bestChromosome = chromosomes[0];

            var fitnessMin = (bestChromosome.Fitness as MnistFitnessValue).Loss;
            float sum = 0;

            for (int i = 1; i < chromosomes.Count; i++)
            {
                float fitness = (chromosomes[i].Fitness as MnistFitnessValue).Loss;
                sum += fitness;
                if (fitness < fitnessMin)
                {
                    fitnessMin = fitness;
                    bestChromosome = chromosomes[i];
                }
            }
        
            var fitnessAvg = sum / chromosomes.Count;

            return ( new MnistFitnessValue(10), new MnistFitnessValue(10), bestChromosome );
        }
    }
}
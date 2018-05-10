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
    public sealed class DataSelection : IDataSelection<DigitImage>
    {
        public List<DigitImage> Select(List<DigitImage> dt)
        {
            return dt.GroupBy( x => x.label ).Select( x => x.First() ).ToList();
        }
    }
}
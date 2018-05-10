using System;
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
    public sealed class MNISTFitnessFunction : IFitnessFunctionGeneric<DigitImage>
    {
        private List<DigitImage> train; 

        public IFitnessValue Evaluate(IChromosome chromosome)
        {
            var c = chromosome as MnistChromossome;
            var model = c.Decode() as DenseModel;

            var t = Console.Title;

            var e = 0f;
            for(int j = 0; j < 10; j++)
            {
                for ( int index = 0; index < train.Count; index++ )
                {
                    var inputs = new FloatArray( ArrayMethods.ByteToArray( train[index].pixels, 28, 28 ) );
                    var target = new FloatArray( ArrayMethods.ByteToArray( train[index].label,  10     ) );

                    //  Learning
                    e += model.Learn( inputs, target );      

                    Console.Title = t + $" - Training Epoch {j} of {10}, Smaple {index} of {train.Count} - Model Size: {model.Len.Length}";                
                }
            }

            return new MnistFitnessValue(e);
        }

        public void SetDataToTest(List<DigitImage> dt)
        {
           train = dt;
        }

        public void SetDataToValidate(List<DigitImage> dt)
        {
            
        }
    }
}
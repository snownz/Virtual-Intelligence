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
    public sealed class MnistFitnessValue : IFitnessValue, IComparable
    {
        private float loss;

        public MnistFitnessValue(float init)
        {
            loss = init;
        }

        public float Loss { get => loss; set => loss = value; }

        public int CompareTo(object obj)
        {
            var o = obj as MnistFitnessValue;

            return loss == o.Loss ? 0 : (loss < o.Loss ? 1 : -1);
        }

        public bool LessThan(IFitnessValue value)
        {
            if(value is MnistFitnessValue )
            {
                return loss < ( value as MnistFitnessValue ).Loss;
            }
            throw new System.NotImplementedException();
        }

        public bool MoreThan(IFitnessValue value)
        {
            if(value is MnistFitnessValue )
            {
                return loss > ( value as MnistFitnessValue ).Loss;
            }
            throw new System.NotImplementedException();
        }

        public IFitnessValue NewInstance()
        {
            return new MnistFitnessValue(100);
        }
    }    
}
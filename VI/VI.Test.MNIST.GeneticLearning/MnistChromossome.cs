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
using VI.Maths.Regression;
using VI.ML.Tools.ModelsFramework;
using VI.Neural.Factory;
using VI.Neural.LossFunction;
using VI.Neural.Models;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;

namespace VI.Test.MNIST.GeneticLearning
{
    public sealed class MnistChromossome : GeneBase
    {
        private IRandomNumber chromosomeGenerator;
        private IRandomNumber mutationMultiplierGenerator;
        private IRandomNumber mutationAdditionGenerator;

        private BinaryChromosome value;

        private int size = 4;

        public BinaryChromosome Value { get => value; set => this.value = value; }

        public MnistChromossome(
            IRandomNumber chromosomeGenerator,
            IRandomNumber mutationMultiplierGenerator,
            IRandomNumber mutationAdditionGenerator          
        )
        {
            this.chromosomeGenerator = chromosomeGenerator;
            this.mutationMultiplierGenerator = mutationMultiplierGenerator;
            this.mutationAdditionGenerator = mutationAdditionGenerator;

            Generate();
        }

        private MnistChromossome()
        {
            Generate();
        }

        private MnistChromossome( MnistChromossome source )
        {
            Value = source.Value.Clone() as BinaryChromosome;
        }

        public override void Generate()
        {
            value = new BinaryChromosome( size );
        }
        public override IChromosome CreateNew()
        {
            return new MnistChromossome();
        }
        
        public override IChromosome Clone( )
        {
            return new MnistChromossome( this );
        }

        public override void Crossover(IChromosome pair)
        {
            value.Crossover((pair as MnistChromossome).Value);
        }

        public override void Mutate()
        {
            value.Mutate();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override object Decode()
        {
            var model = new DenseModel();

            var size = Convert.ToInt32(value.ToString(), 2);

            // Linear Regression
            var linear = new Linear();
            linear.InterData(784, 0);
            linear.InterData(10, size);
            linear.CreateRegression();

            // Creating Layers
            int prior = 784;
            for(int i = 1; i < size; i++)
            {
                var current = (int)linear.Calculate(i);
                model.AddLayer( BuildedModels.DenseLeakRelu ( prior, current, 1e-1f, 1e-1f, EnumOptimizerFunction.SGD ) );
                prior = current;
            }            
            model.AddLayer( BuildedModels.DenseSoftMax  ( prior,  10,  1e-1f, 1e-1f, EnumOptimizerFunction.SGD ) );

            // Setting loss function
            model.SetLossFunction( new CrossEntropyLossFunction() );  

            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using VI.Genetic;
using VI.Genetic.Chromosomes;
using VI.Genetic.Data;
using VI.Genetic.Fitness;
using VI.Genetic.Random;
using VI.Genetic.Selection;

namespace VI.ML.Tools.ModelsFramework
{
    public class AnnModelFramework<T> 
        where T : class
    {
        // Data mining
        private readonly IDataSelection<T> _dataTestSelector;
        private readonly IDataSelection<T> _dataValidationSelector;

        // Genetic
        private readonly IChromosome _base;
        private readonly IFitnessFunctionGeneric<T> _ff;
        private readonly ISelectionMethod _selectionMethod;
        private readonly GenePool _geneticPool;

        // Selection
        private readonly ISelectionBest _selectionWinner;


        public AnnModelFramework(
                                    int searchSpaceSize,
                                    IDataSelection<T> testSelector,
                                    IDataSelection<T> validateSelector,
                                    IChromosome geneBase,
                                    IFitnessFunctionGeneric<T> ff,
                                    ISelectionMethod selectionMethod,
                                    ISelectionBest selectionBest                                    
                                )
        {
            _base                   = geneBase;
            _ff                     = ff;
            _selectionMethod        = selectionMethod;
            _selectionWinner        = selectionBest;
            _dataTestSelector       = testSelector;
            _dataValidationSelector = validateSelector;
            
            _geneticPool     = new GenePool( searchSpaceSize, geneBase, ff, selectionMethod, selectionBest ) { AutoShuffling = true, RandomSelectionPortion = .3d};            
        }

        public void CreateDatabase(IList<T> dt, float validationSize)
        {
            _ff    .SetDataToTest( _dataTestSelector      .Select( dt.Take( (int)( ( 1 - validationSize ) * dt.Count) ).ToList() ) );
            _ff.SetDataToValidate( _dataValidationSelector.Select( dt.Skip( (int)( ( 1 - validationSize ) * dt.Count) ).ToList() ) );
        }
        
        public ( int epoch, IFitnessValue fitness ) RunMinimize( int maxEpoch, IFitnessValue minFitness )
        {
            var currentF = minFitness.NewInstance();
            var currentE = 0;

            while ( currentF.MoreThan( minFitness ) &&  currentE < maxEpoch )
            {
                System.Console.WriteLine($"\nEpoch: {currentE} of {maxEpoch}");
                _geneticPool.RunGeneration();
                currentF = _geneticPool.BestChromosome.Fitness;

                currentE++;
            }

            return ( currentE, currentF );
        }

        public (int epoch, IFitnessValue fitness ) RunMaximize( int maxEpoch, IFitnessValue minFitness )
        {
            var currentF = minFitness.NewInstance();
            var currentE = 0;

            while ( currentF.LessThan( minFitness ) &&  currentE < maxEpoch )
            {
                System.Console.WriteLine($"\nEpoch: {currentE} of {maxEpoch}");
                _geneticPool.RunGeneration();
                currentF = _geneticPool.BestChromosome.Fitness;

                currentE++;
            }

            return ( currentE, currentF );
        }

        public object GetModel()
        {
            var winner =  _geneticPool.BestChromosome;
            return winner.Decode();
        }
    }
}
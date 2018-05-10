using System;
using System.Diagnostics;
using VI.Data.Array;
using VI.Data.MNIST;
using VI.Neural.Factory;
using VI.Neural.LossFunction;
using VI.Neural.Models;
using VI.Neural.OptimizerFunction;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;
using RoslynTools.Console;
using VI.Neural.Drivers.Executors;
using VI.ML.Tools.ModelsFramework;
using System.Collections.Generic;
using VI.Genetic.Data;
using VI.Genetic.Selection;
using VI.Genetic.Random;

namespace VI.Test.MNIST.GeneticLearning
{
    internal class Program : Cw
    {
        #if DEBUG
        private static string path = "../VI.Test.MNIST/Data";
#else
        private static string path = "VI.Test.MNIST/Data";
#endif
        private static void Main(string[] args)
        {
            Clear();

            var watch = Stopwatch.StartNew();

            ProcessingDriver.Device = DeviceType.CPU_Parallel;

            watch.Stop();
            Console.WriteLine($"Device Time: {watch.ElapsedMilliseconds}ms");

            // load MNIST data
            MnistLoader.DataPath = path;
            var trainingValues = MnistLoader.OpenMnist();

            // create ancestral
            var gen = new UniformRandom();
            var ancestral = new MnistChromossome( gen, gen, gen );

            // initialize genetic variables
            var dtSelect = new DataSelection();
            var ff = new MNISTFitnessFunction();
            var selectionGenes = new EliteSelectionMethod();
            var selectionBest =  new MnistBest();

            // Search Params
            var minimalFitness = new MnistFitnessValue(0.5f);
            var maximalEpoch = 1000;

            // create framework
            var framework = new AnnModelFramework<DigitImage>( 3, dtSelect, dtSelect, ancestral, ff, selectionGenes, selectionBest );            
            
            // build database
            framework.CreateDatabase( trainingValues, .3f );
            
            // run framework
            framework.RunMinimize( maximalEpoch, minimalFitness );

            // get best model
            var model = framework.GetModel() as DenseModel;

            watch = Stopwatch.StartNew();
            watch.Stop();
            Console.WriteLine($"Search Time: {watch.ElapsedMilliseconds} ms");

            int cont = 0;
            int sizeTrain = trainingValues.Count;

            var e = 0f;
            while ( true )
            {
                watch = Stopwatch.StartNew();
                e = 0f;
                int ct = 0;
                for ( int i = 0; i < sizeTrain; i++ )
                {
                    var index = i;

                    var inputs = new FloatArray( ArrayMethods.ByteToArray( trainingValues[index].pixels, 28, 28 ) );
                    var target = new FloatArray( ArrayMethods.ByteToArray( trainingValues[index].label,  10     ) );

                    //  Learning
                    e += model.Learn( inputs, target );

                    // Sample
                    if ( ct % 1000 == 0 )
                    {
                        Write( trainingValues[index].ToString() );
                        Write( ArrayMethods.PrintArray( model.Output(inputs), 10 ) );
                        Write( "\n" );
                    }       

                    ct++;                    
                }
                cont++;
                watch.Stop();
                var time = watch.ElapsedMilliseconds;               
                Console.Title =
                    $"Error: {e} --- TSPS (Training Sample per Second): {Math.Ceiling(1000d / ((double)time / (double)sizeTrain))}";
            }
        }
    }
}
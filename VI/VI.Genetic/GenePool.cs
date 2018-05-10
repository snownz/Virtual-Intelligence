using VI.Genetic.Chromosomes;
using VI.Genetic.Fitness;
using VI.Genetic.Random;
using VI.Genetic.Selection;
using System;
using System.Collections.Generic;

namespace VI.Genetic
{
    public class GenePool
    {
        private static ThreadSafeRandom rand = new ThreadSafeRandom();

        private IFitnessFunction _fitnessFuntion;
        private ISelectionMethod _selectionMethod;
        private ISelectionBest _selectionBest;
        private List<IChromosome> _genes;
        private int _size;
        private double _randomSelectionPortion = .05;
        private bool _autoShuffling = false;

        private double _crossoverRate = .63;
        private double _mutationRate = .08;

        private IFitnessValue _fitnessMax;
        private IFitnessValue _fitnessAvg;

        private IChromosome _bestChromosome = null;

        public double CrossoverRate
        {
            get { return _crossoverRate; }
            set
            {
                _crossoverRate = Math.Max(0.1, Math.Min(1.0, value));
            }
        }
        public double MutationRate
        {
            get { return _mutationRate; }
            set
            {
                _mutationRate = Math.Max(0.1, Math.Min(1.0, value));
            }
        }
        public double RandomSelectionPortion
        {
            get { return _randomSelectionPortion; }
            set
            {
                _randomSelectionPortion = Math.Max(0, Math.Min(0.9, value));
            }
        }
        public bool AutoShuffling
        {
            get { return _autoShuffling; }
            set { _autoShuffling = value; }
        }

        public ISelectionMethod SelectionMethod
        {
            get { return _selectionMethod; }
            set { _selectionMethod = value; }
        }

        public ISelectionBest SelectionBest
        {
            get { return _selectionBest; }
            set { _selectionBest = value; }
        }

        public IFitnessFunction FitnessFunction
        {
            get { return _fitnessFuntion; }
            set
            {
                _fitnessFuntion = value;

                foreach (IChromosome member in _genes)
                {
                    member.Evaluate(_fitnessFuntion);
                }

                FindBestChromosome();
            }
        }

        public IFitnessValue FitnessMax
        {
            get { return _fitnessMax; }
        }
        public IFitnessValue FitnessAvg
        {
            get { return _fitnessAvg; }
        }

        public IChromosome BestChromosome
        {
            get { return _bestChromosome; }
        }

        public int Size
        {
            get { return _size; }
        }

        public IChromosome this[int index]
        {
            get { return _genes[index]; }
        }
        
        public GenePool(int size,
                          IChromosome ancestor,
                          IFitnessFunction fitnessFunction,
                          ISelectionMethod selectionMethod,
                          ISelectionBest selectionBest
                          )
        {
            if (size < 2)
                throw new ArgumentException("Too small population's size was specified.");

            this._fitnessFuntion = fitnessFunction;
            this._selectionMethod = selectionMethod;
            this._selectionBest = selectionBest;
            this._size = size;
            
            _genes = new List<IChromosome>();

            for (int i = 1; i < size; i++)
            {
                IChromosome c = ancestor.CreateNew();
                _genes.Add(c);
            }
        }

        public void Regenerate()
        {
            IChromosome ancestor = _genes[0];
            _genes.Clear();
            for (int i = 0; i < _size; i++)
            {
                IChromosome c = ancestor.CreateNew();
                c.Evaluate(_fitnessFuntion);
                _genes.Add(c);
            }
        }

        public virtual void Crossover()
        {
            for (int i = 1; i < _size; i += 2)
            {
                Console.Title = $"Crossover - {i} of {_size}";
                if (rand.NextDouble() <= _crossoverRate)
                {
                    IChromosome c1 = _genes[i - 1].Clone();
                    IChromosome c2 = _genes[i].Clone();

                    c1.Crossover(c2);

                    c1.Evaluate(_fitnessFuntion);
                    c2.Evaluate(_fitnessFuntion);

                    _genes.Add(c1);
                    _genes.Add(c2);
                }
            }
        }
        public virtual void Mutate()
        {
            for (int i = 0; i < _size; i++)
            {
                Console.Title = $"Mutate - {i} of {_size}";
                if (rand.NextDouble() <= _mutationRate)
                {
                    IChromosome c = _genes[i].Clone();
                    c.Mutate();
                    c.Evaluate(_fitnessFuntion);
                    _genes.Add(c);
                }
            }
        }
        public virtual void Selection()
        {
            int randomAmount = (int)(_randomSelectionPortion * _size);

            _selectionMethod.ApplySelection(_genes, _size - randomAmount);

            if (randomAmount > 0)
            {
                IChromosome ancestor = _genes[0];

                for (int i = 0; i < randomAmount; i++)
                {
                    IChromosome c = ancestor.CreateNew();
                    c.Evaluate(_fitnessFuntion);
                    _genes.Add(c);
                }
            }

            FindBestChromosome();
        }

        public void RunGeneration()
        {
            Console.WriteLine("Running Crossver: ");
            Crossover();
            Console.WriteLine("Running Mutate: ");
            Mutate();
            Console.WriteLine("Running Selection: ");
            Selection();

            if (_autoShuffling)
                RandomPool();
        }

        public void RandomPool()
        {
            int size = _genes.Count;
            List<IChromosome> tempPopulation = _genes.GetRange(0, size);
            _genes.Clear();

            while (size > 0)
            {
                int i = rand.Next(size);

                _genes.Add(tempPopulation[i]);
                tempPopulation.RemoveAt(i);

                size--;
            }
        }

        public void AddChromosome(IChromosome chromosome)
        {
            chromosome.Evaluate(_fitnessFuntion);
            _genes.Add(chromosome);
        }

        private void FindBestChromosome()
        {
            ( _fitnessMax, _fitnessAvg, _bestChromosome ) = _selectionBest.Find(_genes);
        }
    }
}
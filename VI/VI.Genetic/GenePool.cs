using System;
using System.Collections.Generic;
using VI.Genetic.Chromosomes;
using VI.Genetic.Fitness;
using VI.Genetic.Selection;
using VI.Maths.Random;

namespace VI.Genetic
{
    public class GenePool
    {
        private static ThreadSafeRandom rand = new ThreadSafeRandom();

        private IFitnessFunction ff;
        private ISelectionMethod sm;
        private List<IChromosome> members = new List<IChromosome>();
        private int size;
        private double randomSelectionPortion = 0.0;
        private bool autoShuffling = false;

        private double crossoverRate = 0.75;
        private double mutationRate = 0.10;

        private double fitnessMax = 0;
        private double fitnessSum = 0;
        private double fitnessAvg = 0;
        private IChromosome bestChromosome = null;

        public double CrossoverRate
        {
            get { return crossoverRate; }
            set
            {
                crossoverRate = Math.Max(0.1, Math.Min(1.0, value));
            }
        }
        public double MutationRate
        {
            get { return mutationRate; }
            set
            {
                mutationRate = Math.Max(0.1, Math.Min(1.0, value));
            }
        }
        public double RandomSelectionPortion
        {
            get { return randomSelectionPortion; }
            set
            {
                randomSelectionPortion = Math.Max(0, Math.Min(0.9, value));
            }
        }
        public bool AutoShuffling
        {
            get { return autoShuffling; }
            set { autoShuffling = value; }
        }

        public ISelectionMethod SelectionMethod
        {
            get { return sm; }
            set { sm = value; }
        }
        public IFitnessFunction FitnessFunction
        {
            get { return ff; }
            set
            {
                ff = value;

                foreach (IChromosome member in members)
                {
                    member.Evaluate(ff);
                }

                FindBestChromosome();
            }
        }

        public double FitnessMax
        {
            get { return fitnessMax; }
        }
        public double FitnessSum
        {
            get { return fitnessSum; }
        }
        public double FitnessAvg
        {
            get { return fitnessAvg; }
        }

        public IChromosome BestChromosome
        {
            get { return bestChromosome; }
        }        

        public int Size
        {
            get { return size; }
        }

        public IChromosome this[int index]
        {
            get { return members[index]; }
        }

        public GenePool()
        {

        }

        public void Regenerate()
        {
            IChromosome ancestor = members[0];
            members.Clear();
            for (int i = 0; i < size; i++)
            {
                IChromosome c = ancestor.CreateNew();
                c.Evaluate(ff);
                members.Add(c);
            }
        }

        public virtual void Crossover()
        {
            for (int i = 1; i < size; i += 2)
            {
                if (rand.NextDouble() <= crossoverRate)
                {
                    IChromosome c1 = members[i - 1].Clone();
                    IChromosome c2 = members[i].Clone();
                    
                    c1.Crossover(c2);
                    
                    c1.Evaluate(ff);
                    c2.Evaluate(ff);
                    
                    members.Add(c1);
                    members.Add(c2);
                }
            }
        }
        public virtual void Mutate()
        {
            for (int i = 0; i < size; i++)
            {
                if (rand.NextDouble() <= mutationRate)
                {
                    IChromosome c = members[i].Clone();
                    c.Mutate();
                    c.Evaluate(ff);
                    members.Add(c);
                }
            }
        }
        public virtual void Selection()
        {
            int randomAmount = (int)(randomSelectionPortion * size);

            sm.ApplySelection(members, size - randomAmount);

            if (randomAmount > 0)
            {
                IChromosome ancestor = members[0];

                for (int i = 0; i < randomAmount; i++)
                {
                    IChromosome c = ancestor.CreateNew();
                    c.Evaluate(ff);
                    members.Add(c);
                }
            }

            FindBestChromosome();
        }

        public void RunEpoch()
        {
            Crossover();
            Mutate();
            Selection();

            if (autoShuffling)
                Shuffle();
        }

        public void Shuffle()
        {
            // current population size
            int size = members.Count;
            // create temporary copy of the population
            List<IChromosome> tempPopulation = members.GetRange(0, size);
            // clear current population and refill it randomly
            members.Clear();

            while (size > 0)
            {
                int i = rand.Next(size);

                members.Add(tempPopulation[i]);
                tempPopulation.RemoveAt(i);

                size--;
            }
        }

        public void AddChromosome(IChromosome chromosome)
        {
            chromosome.Evaluate(ff);
            members.Add(chromosome);
        }

        private void FindBestChromosome()
        {
            bestChromosome = members[0];
            fitnessMax = bestChromosome.Fitness;
            fitnessSum = fitnessMax;

            for (int i = 1; i < size; i++)
            {
                double fitness = members[i].Fitness;
                fitnessSum += fitness;

                if (fitness > fitnessMax)
                {
                    fitnessMax = fitness;
                    bestChromosome = members[i];
                }
            }
            fitnessAvg = fitnessSum / size;
        }
    }
}

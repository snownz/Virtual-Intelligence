using VI.Genetic.Random;
using System;
using System.Collections.Generic;
using System.Text;
using VI.NumSharp.Arrays;

namespace VI.Genetic.Chromosomes
{
    public class FloatArrayChromosome : GeneBase
    {
        protected IRandomNumber chromosomeGenerator;

        protected IRandomNumber mutationMultiplierGenerator;

        protected IRandomNumber mutationAdditionGenerator;

        protected static ThreadSafeRandom rand = new ThreadSafeRandom();

        public const int MaxLength = 65536;

        private int length;

        protected float[] val = null;

        private float mutationBalancer = 5e-2f;
        private float crossoverBalancer = 5e-1f;

        public int Length
        {
            get { return length; }
        }

        public float[] Value
        {
            get { return val; }
        }

        public float MutationBalancer
        {
            get { return mutationBalancer; }
            set { mutationBalancer = Math.Max(0.0f, Math.Min(1.0f, value)); }
        }

        public float CrossoverBalancer
        {
            get { return crossoverBalancer; }
            set { crossoverBalancer = Math.Max(0.0f, Math.Min(1.0f, value)); }
        }

        public FloatArrayChromosome(
            IRandomNumber chromosomeGenerator,
            IRandomNumber mutationMultiplierGenerator,
            IRandomNumber mutationAdditionGenerator,
            int length)
        {
            this.chromosomeGenerator = chromosomeGenerator;
            this.mutationMultiplierGenerator = mutationMultiplierGenerator;
            this.mutationAdditionGenerator = mutationAdditionGenerator;
            this.length = Math.Max(2, Math.Min(MaxLength, length)); ;

            val = new float[length];

            Generate();
        }

        public FloatArrayChromosome(FloatArrayChromosome source)
        {
            this.chromosomeGenerator = source.chromosomeGenerator;
            this.mutationMultiplierGenerator = source.mutationMultiplierGenerator;
            this.mutationAdditionGenerator = source.mutationAdditionGenerator;
            this.length = source.length;
            this.fitness = source.fitness;
            this.mutationBalancer = source.mutationBalancer;
            this.crossoverBalancer = source.crossoverBalancer;

            val = (float[])source.val.Clone();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(val[0]);
            for (int i = 1; i < length; i++)
            {
                sb.Append(' ');
                sb.Append(val[i]);
            }

            return sb.ToString();
        }

        public override void Generate()
        {
            val = new float[length];
            for (int i = 0; i < length; i++)
            {
                val[i] = chromosomeGenerator.Next();
            }
        }

        public override IChromosome CreateNew()
        {
            return new FloatArrayChromosome(chromosomeGenerator, mutationMultiplierGenerator, mutationAdditionGenerator, length);
        }

        public override IChromosome Clone()
        {
            return new FloatArrayChromosome(this);
        }

        public override void Mutate()
        {
            int mutationGene = rand.Next(length);

            if (rand.NextDouble() < mutationBalancer)
            {
                val[mutationGene] *= mutationMultiplierGenerator.Next();
            }
            else
            {
                val[mutationGene] += mutationAdditionGenerator.Next();
            }
        }

        public override void Crossover(IChromosome pair)
        {
            FloatArrayChromosome p = (FloatArrayChromosome)pair;

            if ((p != null) && (p.length == length))
            {
                if (rand.NextDouble() < crossoverBalancer)
                {
                    int crossOverPoint = rand.Next(length - 1) + 1;
                    int crossOverLength = length - crossOverPoint;
                    float[] temp = new float[crossOverLength];

                    Array.Copy(val, crossOverPoint, temp, 0, crossOverLength);
                    Array.Copy(p.val, crossOverPoint, val, crossOverPoint, crossOverLength);
                    Array.Copy(temp, 0, p.val, crossOverPoint, crossOverLength);
                }
                else
                {
                    float[] pairVal = p.val;

                    float factor = rand.NextFloat();
                    if (rand.Next(2) == 0)
                        factor = -factor;

                    for (int i = 0; i < length; i++)
                    {
                        float portion = (val[i] - pairVal[i]) * factor;

                        val[i] -= portion;
                        pairVal[i] += portion;
                    }
                }
            }
        }

        public FloatArray AsLinear()
        {
            return new FloatArray(val);
        }
    }
}

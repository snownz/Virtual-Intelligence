using VI.Genetic.Random;
using System;
using VI.NumSharp.Arrays;

namespace VI.Genetic.Chromosomes
{
    public class FloatMatrixChromosome : GeneBase
    {
        protected IRandomNumber chromosomeGenerator;

        protected IRandomNumber mutationMultiplierGenerator;

        protected IRandomNumber mutationAdditionGenerator;
                
        private int length;
        private int subLength;

        protected FloatArrayChromosome[] val = null;
        
        public int Length
        {
            get { return length; }
        }

        public FloatArrayChromosome[] Value
        {
            get { return val; }
        }
        
        public FloatMatrixChromosome(
            IRandomNumber chromosomeGenerator,
            IRandomNumber mutationMultiplierGenerator,
            IRandomNumber mutationAdditionGenerator,
            int len, int  subLen)
        {
            this.chromosomeGenerator = chromosomeGenerator;
            this.mutationMultiplierGenerator = mutationMultiplierGenerator;
            this.mutationAdditionGenerator = mutationAdditionGenerator;

            length = len;
            subLength = subLen;

            Generate();
        }

        public FloatMatrixChromosome(FloatMatrixChromosome source)
        {
            this.chromosomeGenerator = source.chromosomeGenerator;
            this.mutationMultiplierGenerator = source.mutationMultiplierGenerator;
            this.mutationAdditionGenerator = source.mutationAdditionGenerator;
            this.length = source.length;
            this.subLength = source.subLength;
            this.fitness = source.fitness;

            val = (FloatArrayChromosome[])source.val.Clone();
        }
               
        public override void Generate()
        {
            val = new FloatArrayChromosome[length];
            for (int i = 0; i < length; i++)
            {
                val[i] = new FloatArrayChromosome(chromosomeGenerator, mutationMultiplierGenerator, mutationAdditionGenerator, subLength);
            }
        }

        public override IChromosome CreateNew()
        {
            return new FloatMatrixChromosome(chromosomeGenerator, mutationMultiplierGenerator, mutationAdditionGenerator, length, subLength);
        }

        public override IChromosome Clone()
        {
            return new FloatMatrixChromosome(this);
        }

        public override void Mutate()
        {
            for (int i = 0; i < length; i++)
            {
                val[i].Mutate();
            }
        }

        public override void Crossover(IChromosome pair)
        {
            for (int i = 0; i < length; i++)
            {
                var p = pair as FloatMatrixChromosome;
                val[i].Crossover(p.Value[i]);
            }
        }

        public FloatArray2D As2D()
        {
            var f = new FloatArray2D(length, subLength);
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < subLength; y++)
                {
                    f[x, y] = val[x].Value[y];
                }
            }

            return f;
        }
    }
}

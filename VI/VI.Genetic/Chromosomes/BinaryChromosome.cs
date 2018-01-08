using System;
using VI.Maths.Random;

namespace VI.Genetic.Chromosomes
{
    public class BinaryChromosome : GeneBase
    {
        protected static ThreadSafeRandom rand = new ThreadSafeRandom();
        
        protected int length;
        protected ulong val = 0;
        public const int MaxLength = 64;

        public int Length => length;
        public ulong Value => val & (0xFFFFFFFFFFFFFFFF >> (64 - length));
        public ulong MaxValue => 0xFFFFFFFFFFFFFFFF >> (64 - length);

        public BinaryChromosome(int length)
        {
            this.length = Math.Max(2, Math.Min(MaxLength, length));
            Generate();
        }
        protected BinaryChromosome(BinaryChromosome source)
        {
            length = source.length;
            val = source.val;
            fitness = source.fitness;
        }

        public override void Generate()
        {
            byte[] bytes = new byte[8];
            
            rand.NextBytes(bytes);
            val = BitConverter.ToUInt64(bytes, 0);
        }
        public override IChromosome CreateNew()
        {
            return new BinaryChromosome(length);
        }
        public override IChromosome Clone()
        {
            return new BinaryChromosome(this);
        }


        public override void Mutate()
        {
            val ^= ((ulong)1 << rand.Next(length));
        }
        public override void Crossover(IChromosome pair)
        {
            BinaryChromosome p = (BinaryChromosome)pair;

            if ((p != null) && (p.length == length))
            {
                int crossOverPoint = 63 - rand.Next(length - 1);
                ulong mask1 = 0xFFFFFFFFFFFFFFFF >> crossOverPoint;
                ulong mask2 = ~mask1;

                ulong v1 = val;
                ulong v2 = p.val;

                val = (v1 & mask1) | (v2 & mask2);
                p.val = (v2 & mask1) | (v1 & mask2);
            }
        }
    }
}

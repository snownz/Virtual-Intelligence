using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Genetic.Random
{
    public class UniformRandom : IRandomNumber
    {
        private ThreadSafeRandom rand = null;

        public float Mean
        {
            get { return 0.5f; }
        }

        public float Variance
        {
            get { return 1f / 12; }
        }

        public UniformRandom()
        {
            rand = new ThreadSafeRandom(0);
        }

        public UniformRandom(int seed)
        {
            rand = new ThreadSafeRandom(seed);
        }

        public float Next()
        {
            return rand.NextFloat();
        }

        public void SetSeed(int seed)
        {
            rand = new ThreadSafeRandom(seed);
        }
    }
}

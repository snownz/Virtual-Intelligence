using System;
using System.Collections.Generic;
using System.Text;

namespace VI.Maths.Random
{
    public class UniformOneGenerator : IRandomNumberGenerator
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
        
        public UniformOneGenerator()
        {
            rand = new ThreadSafeRandom(0);
        }
        
        public UniformOneGenerator(int seed)
        {
            rand = new ThreadSafeRandom(seed);
        }
        
        public float Next()
        {
            return (float)rand.NextDouble();
        }
        
        public void SetSeed(int seed)
        {
            rand = new ThreadSafeRandom(seed);
        }
    }
}

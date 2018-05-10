using VI.Genetic.Random;
using System;
using System.Collections.Generic;
using System.Text;
using VI.NumSharp.Arrays;

namespace VI.Genetic.Chromosomes
{
    public class BinaryChromosome : GeneBase
    {
        protected int length;

        protected ulong val = 0;

        protected static ThreadSafeRandom rand = new ThreadSafeRandom( );
        
        public const int MaxLength = 64;

        public int Length
        {
            get { return length; }
        }

        public ulong Value
        {
            get { return val & ( 0xFFFFFFFFFFFFFFFF >> ( 64 - length ) ); }
        }

        public ulong MaxValue
        {
            get { return 0xFFFFFFFFFFFFFFFF >> ( 64 - length ); }
        }

        public BinaryChromosome( int length )
        {
            this.length = Math.Max( 2, Math.Min( MaxLength, length ) );
            Generate( );
        }

        protected BinaryChromosome( BinaryChromosome source )
        {
            length  = source.length;
            val     = source.val;
            fitness = source.fitness;
        }
        
        public override string ToString( )
        {
            ulong	tval = val;
            char[]	chars = new char[length];

            for ( int i = length - 1; i >= 0; i-- )
            {
                chars[i] = (char) ( ( tval & 1 ) + '0' );
                tval >>= 1;
            }
            return new string( chars );
        }

        public override void Generate( )
        {
            byte[] bytes = new byte[8];

            // generate value
            rand.NextBytes( bytes );
            val = BitConverter.ToUInt64( bytes, 0 );
        }
        public override IChromosome CreateNew( )
        {
            return new BinaryChromosome( length );
        }
        
        public override IChromosome Clone( )
        {
            return new BinaryChromosome( this );
        }
        
        public override void Mutate( )
        {
            val ^= ( (ulong) 1 << rand.Next( length ) );
        }
        
        public override void Crossover( IChromosome pair )
        {
            BinaryChromosome p = (BinaryChromosome) pair;
            
            if ( ( p != null ) && ( p.length == length ) )
            {
                int		crossOverPoint = 63 - rand.Next( length - 1 );
                ulong	mask1 = 0xFFFFFFFFFFFFFFFF >> crossOverPoint;
                ulong	mask2 = ~mask1;

                ulong	v1 = val;
                ulong	v2 = p.val;

                val   = ( v1 & mask1 ) | ( v2 & mask2 );
                p.val = ( v2 & mask1 ) | ( v1 & mask2 );
            }
        }
    }
}

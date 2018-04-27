using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VI.Neural.Network;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;

namespace VI.Test.Recurrent.TextWriter
{
    internal class Program
    {
#if DEBUG
        private static string path = "../VI.Test.Recurrent.TextWriter/Data/text.txt";
#else
        private static string path = "VI.Test.Recurrent.TextWriter/Data/text.txt";
#endif

        private static int vocab_size;
        private static int hidden_size;
        private static int seq_length;
        private static int data_size;
        private static float learning_rate;
        private static float std;
        private static int recurrentUnits;
        private static string txt;
        private static Dictionary<char, int> char_to_ix;
        private static Dictionary<int, char> ix_to_char;

        private static ClassifierRecurrentNeuralNetwork net;

        private static void OpenText()
        {
            txt = File.ReadAllText( path );

            var chars = new String( txt.Distinct().ToArray() );

            vocab_size = chars.Length;
            data_size = txt.Length;

            Console.WriteLine( $"data has {data_size} chars, {vocab_size} unique" );

            char_to_ix = new Dictionary<char, int>();
            ix_to_char = new Dictionary<int, char>();

            for ( int i = 0; i < vocab_size; i++ )
            {
                char_to_ix.Add( chars[i], i );
                ix_to_char.Add( i, chars[i] );
            }
        }

        private static void Main(string[] args)
        {
            Console.Clear();

            ProcessingDevice.Device = DeviceType.CPU_Parallel;

            OpenText();

            hidden_size    = 50;
            seq_length     = 25;
            recurrentUnits = 1; 
            learning_rate  = 1e-1f;
            std            = 1e-1f;

            net = new ClassifierRecurrentNeuralNetwork( vocab_size, vocab_size, hidden_size, recurrentUnits, learning_rate, std, EnumOptimizerFunction.SGD );

            var hprev = new Array<FloatArray>( recurrentUnits ).Fill( hidden_size );
            var smooth_loss = -Math.Log( 1.0 / vocab_size ) * seq_length;

            int n = 0;
            int p = 0;

            while ( 1 == 1 )
            {
                if ( p + seq_length + 1 >= data_size || n == 0 )
                {
                    hprev = new Array<FloatArray>( recurrentUnits ).Fill( hidden_size );
                    p = 0;
                }

                var inputs = new int[seq_length];
                var targets = new int[seq_length];

                for ( int i = 0; i < seq_length; i++ ) inputs[i]  = char_to_ix[txt[p + i    ]];
                for ( int i = 0; i < seq_length; i++ ) targets[i] = char_to_ix[txt[p + 1 + i]];

                ( var loss, var dwy, var dby, var dwh, var dbh, var hs ) = net.BPTT( inputs, targets, hprev );

                net.UpdateParams( dwy, dby, dwh, dbh );

                smooth_loss = smooth_loss * 0.999 + loss * 0.001;
                
                if ( n % 100 == 0 )
                {
                    Sample( hprev, inputs[0], 200 );
                    Console.WriteLine( $"iter {n}, loss: {smooth_loss}" );
                }                

                hprev = hs;
               
                p += seq_length;
                n += 1;
            }
        }

        private static void Sample(Array<FloatArray> hprev, int seed_ix, int size)
        {
            var x = new FloatArray( vocab_size );
            x[seed_ix] = 1;

            var ixes = new List<int>();

            for ( int t = 0; t < size; t++ )
            {
                var data = net.Output( x, hprev );

                var ix = NumMath.Choice( Enumerable.Range( 0, vocab_size ).ToArray(), 1, data.ps.ToArray() ).First();

                x = new FloatArray( vocab_size ) { [ix] = 1 };
                ixes.Add(ix);

                hprev = data.hs;
            }

            var str = string.Join( "", ixes.Select(c => ix_to_char[c]) );
            Console.WriteLine( $"----\n {str} \n----" );
        }
    }
}
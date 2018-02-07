using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using VI.Maths.Random;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;

namespace ConsoleApp1
{
    class Program
    {
        static ThreadSafeRandom tr = new ThreadSafeRandom();

        private static int vocab_size;
        private static int hidden_size;
        private static int seq_length;
        private static int data_size;
        private static float learning_rate;

        private static string txt;

        private static Dictionary<char, int> char_to_ix;
        private static Dictionary<int, char> ix_to_char;

        private static Array2D<float> Wxh;
        private static Array2D<float> Whh;
        private static Array2D<float> Why;
        private static Array<float> bh;
        private static Array<float> by;

        static void Main(string[] args)
        {
            ProcessingDevice.Device = DeviceType.PARALLEL_CPU;

            OpenText();

            hidden_size = 100;
            seq_length = 25;
            learning_rate = 1e-1f;

            Wxh = NumMath.Random(hidden_size, vocab_size, .01f);  // input -> hidden
            Whh = NumMath.Random(hidden_size, hidden_size, .01f); // memory -> hidden
            Why = NumMath.Random(vocab_size, hidden_size, .01f);  // hidden -> output
            bh = NumMath.Repeat(hidden_size, 1);                  // bias hidden
            by = NumMath.Repeat(vocab_size, 1);                   // bias output

            int n = 0;
            int p = 0;

            var hprev = new Array<float>(hidden_size);

            var smooth_loss = -Math.Log(1.0 / vocab_size) * seq_length;

            var mWxh = new Array2D<float>(Wxh.Size);
            var mWhh = new Array2D<float>(Whh.Size);
            var mWhy = new Array2D<float>(Why.Size);

            var mbh = new Array<float>(bh.Length);
            var mby = new Array<float>(by.Length);

            while (n <= 1000 * 100)
            {
                if (p + seq_length + 1 >= data_size || n == 0)
                {
                    hprev = new Array<float>(hidden_size);
                    p = 0;
                }

                var inputs = new int[seq_length];
                var targets = new int[seq_length + 1];

                for (int i = 0; i < seq_length; i++)
                {
                    inputs[i] = char_to_ix[txt[p + i]];
                }

                for (int i = 0; i < seq_length + 1; i++)
                {
                    targets[i] = char_to_ix[txt[p + 1 + i]];
                }

                var loss_data = BPTT(inputs, targets, hprev);

                smooth_loss = smooth_loss * 0.999 + loss_data.loss * 0.001;

                Console.WriteLine($"iter {n}, loss: {smooth_loss}");
                Sample(hprev, inputs[0], 200);

                // memoria Adagrad
                mWxh += loss_data.dWxh * loss_data.dWxh;
                mWhh += loss_data.dWhh * loss_data.dWhh;
                mWhy += loss_data.dWhy * loss_data.dWhy;
                mbh += loss_data.dbh * loss_data.dbh;
                mby += loss_data.dby * loss_data.dby;

                // update params
                Wxh += -learning_rate * loss_data.dWxh / (mWxh + 1e-8f).Sqrt();
                Whh += -learning_rate * loss_data.dWhh / (mWhh + 1e-8f).Sqrt();
                Why += -learning_rate * loss_data.dWhy / (mWhy + 1e-8f).Sqrt();
                bh += -learning_rate * loss_data.dbh / (mbh + 1e-8f).Sqrt();
                by += -learning_rate * loss_data.dby / (mby + 1e-8f).Sqrt();

                p += seq_length;
                n += 1;
            }


        }

        static void OpenText()
        {
            txt = File.ReadAllText(@"C:\Projects\GitHub\Virtual-Intelligence\kafka.txt");
            var chars = new String(txt.Distinct().ToArray());

            vocab_size = chars.Length;
            data_size = txt.Length;

            Console.WriteLine($"data has {data_size} chars, {vocab_size} unique");

            char_to_ix = new Dictionary<char, int>();
            ix_to_char = new Dictionary<int, char>();

            for (int i = 0; i < vocab_size; i++)
            {
                char_to_ix.Add(chars[i], i);
                ix_to_char.Add(i, chars[i]);
            }
        }

        static void Sample(Array<float> hprev, int seed_ix, int size)
        {
            var x = new Array<float>(vocab_size);
            x[seed_ix] = 1;

            var ixes = new List<int>();

            for (int t = 0; t < size; t++)
            {
                var data = FeedForward(x, hprev);

                //var ix = data.ps.FindMax().x; 
                var ix = NumMath.Choice(Enumerable.Range(0, vocab_size).ToArray(), 1, data.ps.ToArray()).First();

                x = new Array<float>(vocab_size) { [ix] = 1 };
                ixes.Add(ix);

                hprev = data.hs;
            }

            var str = string.Join("", ixes.Select(c => ix_to_char[c]));
            Console.WriteLine($"----\n {str} \n----");
        }

        static (Array<float> ps, Array<float> hs, Array<float> ys) FeedForward(Array<float> xs, Array<float> hprev)
        {
            var hs = ((xs.T * Wxh).SumColumn() + (hprev.T * Whh).SumColumn() + bh).Tanh();

            var ys = (hs.T * Why).SumColumn() + by;

            var ps = ys.Exp() / ys.Exp().Sum();

            return (ps, hs, ys);
        }

        static (float loss, Array2D<float> dWxh, Array2D<float> dWhh, Array2D<float> dWhy, Array<float> dbh, Array<float> dby, Array<float> hs) BPTT(int[] inputs, int[] targets, Array<float> hprev)
        {
            float loss = 0f;

            var xs = new Array<float>[inputs.Length + 1]; // Inputs
            var hs = new Array<float>[inputs.Length + 1]; // Hidden Result
            var ys = new Array<float>[inputs.Length + 1]; // Output Result
            var ps = new Array<float>[inputs.Length + 1]; // Softmax probabilit
            var tg = new Array<float>[inputs.Length + 1]; // Targets

            hs[0] = hprev;
            for (int t = 1; t <= inputs.Length; t++)
            {
                xs[t] = new Array<float>(vocab_size);
                xs[t][inputs[t - 1]] = 1;

                tg[t] = new Array<float>(vocab_size);
                tg[t][targets[t - 1]] = 1;

                var data = FeedForward(xs[t], hs[t - 1]);

                ys[t] = data.ys;
                ps[t] = data.ps;
                hs[t] = data.hs;

                loss += -(tg[t] * ps[t].Log()).Sum();
            }

            var dWxh = new Array2D<float>(Wxh.Size);
            var dWhh = new Array2D<float>(Whh.Size);
            var dWhy = new Array2D<float>(Why.Size);

            var dhnext = new Array<float>(hs[0].Length);

            var dbh = new Array<float>(bh.Length);
            var dby = new Array<float>(by.Length);

            for (int t = inputs.Length; t > 0; t--)
            {
                var dy = ps[t].Clone(); // 81

                dy[targets[t - 1]] -= 1;

                dWhy += dy.T * hs[t]; // 81 * 100

                dby += dy; // 81

                var dh = (dy * Why).SumLine() + dhnext; // 100

                var dhraw = (1 - hs[t] * hs[t]) * dh; // 100

                dbh += dhraw; // 100

                dWxh += xs[t] * dhraw.T; // 100 * 81

                dWhh += hs[t - 1] * dhraw.T; // 100 * 100 (hs[t] é o t-1)

                dhnext = (dhraw.T * Whh).SumLine(); // 100

                NumMath.Normalize(-5, 5, dWxh);
                NumMath.Normalize(-5, 5, dWhh);
                NumMath.Normalize(-5, 5, dWhy);
                NumMath.Normalize(-5, 5, dbh);
                NumMath.Normalize(-5, 5, dby);
            }

            return (loss, dWxh, dWhh, dWhy, dbh, dby, hs[inputs.Length - 1]);
        }
    }
}
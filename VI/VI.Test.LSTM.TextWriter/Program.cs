using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.NumSharp.Prototypes.ANN;
using VI.ParallelComputing;

namespace VI.Test.LSTM.TextWriter
{
    internal class Program
    {
#if DEBUG
        private static string path = "../VI.Test.LSTM.TextWriter/Data/text.txt";
#else
        private static string path = "VI.Test.LSTM.TextWriter/Data/text.txt";
#endif

        private static int vocab_size;
        private static int hidden_size;
        private static int seq_length;
        private static int data_size;
        private static float learning_rate;

        private static string txt;

        private static Dictionary<char, int> char_to_ix;
        private static Dictionary<int, char> ix_to_char;

        private static LSTMNetwork net;

        private static void OpenText()
        {
            txt = File.ReadAllText(path);

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

        private static void Main(string[] args)
        {
            Console.Clear();

            ProcessingDevice.Device = DeviceType.CPU;

            OpenText();

            hidden_size = 100;
            seq_length = 25;
            learning_rate = 1e-1f;

            net = new LSTMNetwork(vocab_size, vocab_size, hidden_size, learning_rate, 1e-1f);

            var hprev = new FloatArray(hidden_size);
            var cprev = new FloatArray(hidden_size);
            var smooth_loss = -Math.Log(1.0 / vocab_size) * seq_length;

            int n = 0;
            int p = 0;

            while (n <= 1000 * 100)
            {
                if (p + seq_length + 1 >= data_size || n == 0)
                {
                    hprev = new FloatArray(hidden_size);
                    cprev = new FloatArray(hidden_size);
                    p = 0;
                }

                var inputs = new int[seq_length];
                var targets = new int[seq_length];

                for (int i = 0; i < seq_length; i++) inputs[i] = char_to_ix[txt[p + i]];
                for (int i = 0; i < seq_length; i++) targets[i] = char_to_ix[txt[p + 1 + i]];

                (var loss, var dWf, var dWi, var dWc, var dWo, var dWv,
                 var dBf, var dBi, var dBc, var dBo, var dBv,
                 var hs, var cs) = net.BPTT(inputs, targets, hprev, cprev);

                net.UpdateParams(dWf, dWi, dWc, dWo, dWv, dBf, dBi, dBc, dBo, dBv);

                if (n % 100 == 0)
                {
                    Sample(hprev, cprev, inputs[0], 200);
                    Console.WriteLine($"iter {n}, loss: {smooth_loss}");
                }

                hprev = hs;
                cprev = cs;

                smooth_loss = smooth_loss * 0.999 + loss * 0.001;

                p += seq_length;
                n += 1;
            }
        }

        private static void Sample(FloatArray hprev, FloatArray cprev, int seed_ix, int size)
        {
            var x = new FloatArray(vocab_size);
            x[seed_ix] = 1;

            var ixes = new List<int>();

            for (int t = 0; t < size; t++)
            {
                var data = net.FeedForward(x, hprev, cprev);

                var ix = NumMath.Choice(Enumerable.Range(0, vocab_size).ToArray(), 1, data.y.ToArray()).First();

                x = new FloatArray(vocab_size) { [ix] = 1 };
                ixes.Add(ix);

                hprev = data.h;
                cprev = data.c;
            }

            var str = string.Join("", ixes.Select(c => ix_to_char[c]));
            Console.WriteLine($"----\n {str} \n----");
        }
    }
}
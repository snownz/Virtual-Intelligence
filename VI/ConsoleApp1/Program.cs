using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VI.Maths.Random;
using VI.Neural.Prototype;
using VI.NumSharp;
using VI.NumSharp.Arrays;
using VI.ParallelComputing;

namespace ConsoleApp1
{
	class Program
	{
		private static ThreadSafeRandom tr = new ThreadSafeRandom();

		private static int vocab_size;
		private static int hidden_size;
		private static int seq_length;
		private static int data_size;
		private static float learning_rate;

		private static string txt;

		private static Dictionary<char, int> char_to_ix;
		private static Dictionary<int, char> ix_to_char;

		private static RecurrentNeuralNetwork RNN;

		private static void ExecuteRecurrent()
		{
			ProcessingDevice.Device = DeviceType.CPU;

			OpenText();

			hidden_size = 50;
			seq_length  = 25;

			RNN = new RecurrentNeuralNetwork(vocab_size, vocab_size, hidden_size, 1e-1f, 1e-2f);

			var n = 0;
			var p = 0;

			var hprev       = new FloatArray(hidden_size);
			RNN.Smooth_loss = -Math.Log(1.0 / vocab_size) * seq_length;

			var loss = 0d;

			while (n <= 1000 * 1000)
			{
				// reset memory
				if (p + seq_length + 1 >= data_size || n == 0)
				{
					hprev = new FloatArray(hidden_size);
					p     = 0;
				}

				// create inputs and outputs
				var inputs                                      = new int[seq_length];
				var targets                                     = new int[seq_length];
				for (var i = 0; i < seq_length; i++) inputs[i]  = char_to_ix[txt[p + i]];
				for (var i = 0; i < seq_length; i++) targets[i] = char_to_ix[txt[p + 1 + i]];

				// execute bptt
				(var _loss, var _wtt, var wht, var _why, var _bh, var _by, var _hprev) =
					RNN.BPTT(inputs, targets, hprev);

				// move to next time
				hprev = _hprev;

				// calculate error
				loss = RNN.SmoothLoss(_loss);

				if (n % 100 == 0)
				{
					Sample(hprev, inputs[0], 200);
					Console.WriteLine($"iter {n}, loss: {loss}");
				}

				// update wwights
				RNN.UpdateParams(_wtt, wht, _why, _bh, _by);

				p += seq_length;
				n += 1;
			}

			Console.ReadKey();
		}

		private static float[] inttofloatarray(int i, int max)
		{
			var arr = new float[max];
			arr[i]  = 1;
			return arr;
		}

		private static void Main(string[] args)
		{
			ExecuteRecurrent();
		}

		private static void OpenText()
		{
			txt = new StreamReader(
				@"ConsoleApp1/kafka.txt",
				Encoding.UTF7).ReadToEnd();
			var chars = txt.ToArray().Distinct().OrderBy(x => x).ToArray();

			vocab_size = chars.Length;
			data_size  = txt.Length;

			Console.WriteLine($"data has {data_size} chars, {vocab_size} unique");

			char_to_ix = new Dictionary<char, int>();
			ix_to_char = new Dictionary<int, char>();

			for (var i = 0; i < vocab_size; i++)
			{
				char_to_ix.Add(chars[i], i);
				ix_to_char.Add(i, chars[i]);
			}
		}

		private static void Sample(FloatArray hprev, int seed_ix, int size)
		{
			var x      = new FloatArray(vocab_size);
			var ps     = new FloatArray(vocab_size);
			x[seed_ix] = 1;

			var ixes = new List<int>();

			for (var t = 0; t < size; t++)
			{
				(ps, hprev, _, _) = RNN.FeedForward(x, hprev);
				var ix               = NumMath.Choice(Enumerable.Range(0, vocab_size).ToArray(), 1, ps.ToArray()).First();
				x                    = new FloatArray(vocab_size) {[ix] = 1};
				ixes.Add(ix);
			}

			var str = string.Join("", ixes.Select(c => ix_to_char[c]));
			Console.WriteLine($"----\n {str} \n----");
		}
	}
}
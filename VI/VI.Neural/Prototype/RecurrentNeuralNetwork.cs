using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Prototype
{
	public class RecurrentNeuralNetwork
	{
		private FloatArray bt;
		private FloatArray by;
		public int input_size;
		public float learning_rate;
		private FloatArray mbt;
		private FloatArray mby;
		private FloatArray2D mWhy;
		private FloatArray2D mWtt;

		private FloatArray2D mWxt;
		public int output_size;
		public int temporal_size;
		private FloatArray2D Why;
		private FloatArray2D Wtt;

		private FloatArray2D Wxt;

		public RecurrentNeuralNetwork(int input, int output, int temporal, float learning_rate, float std)
		{
			input_size         = input;
			output_size        = output;
			temporal_size      = temporal;
			this.learning_rate = learning_rate;

			Wxt = NumMath.Random(temporal_size, input_size, std);
			Wtt = NumMath.Random(temporal_size, temporal_size, std);
			Why = NumMath.Random(output_size, temporal_size, std);
			bt  = NumMath.Repeat(temporal_size, 0);
			by  = NumMath.Repeat(output_size, 0);

			ResetAdagradParams();
		}

		public (FloatArray2D Wxt, FloatArray2D Wtt, FloatArray2D Why, FloatArray bh, FloatArray by) Params =>
			(Wxt, Wtt, Why, bt, by);

		public double Smooth_loss { get; set; }

		public void LoadParams(float[,] Wxh, float[,] Whh, float[,] Why, float[] bh, float[] by)
		{
			Wxt      = NumMath.Array(Wxh);
			Wtt      = NumMath.Array(Whh);
			this.Why = NumMath.Array(Why);
			bt       = NumMath.Array(bh);
			this.by  = NumMath.Array(by);
		}

		public void ResetAdagradParams()
		{
			mWxt = new FloatArray2D(Wxt.W, Wxt.H);
			mWtt = new FloatArray2D(Wtt.W, Wtt.H);
			mWhy = new FloatArray2D(Why.W, Why.H);

			mbt = new FloatArray(bt.Length);
			mby = new FloatArray(by.Length);
		}

		public (FloatArray ps, FloatArray ht, FloatArray ys, FloatArray xs) FeedForward(FloatArray xs, FloatArray hprev)
		{
			var ht = ((xs.T * Wxt).SumLine() + (hprev.T * Wtt).SumLine() + bt).Tanh();

			var ys = (ht.T * Why).SumLine() + by;

			var exp = ys.Exp();

			var ps = exp / exp.Sum();

			return (ps, ht, ys, xs);
		}

		public (float loss, FloatArray2D dWxt, FloatArray2D dWtt, FloatArray2D dWhy, FloatArray dbh, FloatArray dby,
			FloatArray hs)
			BPTT(float[][] inputs, int[] targets, FloatArray hprev)
		{
			var loss = 0f;

			var xs = new FloatArray[inputs.Length];
			var ht = new Array<FloatArray>(inputs.Length + 1);
			var ys = new FloatArray[inputs.Length];
			var ps = new Array<FloatArray>(inputs.Length);
			var tg = new FloatArray[inputs.Length];

			ht[-1] = hprev;
			for (var t = 0; t < inputs.Length; t++)
			{
				xs[t] = new FloatArray(inputs[t]);

				tg[t]             = new FloatArray(output_size);
				tg[t][targets[t]] = 1;

				(ps[t], ht[t], ys[t], xs[t]) = FeedForward(xs[t], ht[t - 1]);

				loss += -(tg[t] * ps[t].Log()).Sum();
			}

			var dWxt = new FloatArray2D(Wxt.W, Wxt.H);
			var dWtt = new FloatArray2D(Wtt.W, Wtt.H);
			var dWhy = new FloatArray2D(Why.W, Why.H);

			var dhnext = new FloatArray(ht[0].Length);

			var dbt = new FloatArray(bt.Length);
			var dby = new FloatArray(by.Length);

			for (var t = inputs.Length - 1; t >= 0; t--)
			{
				// output probabilities
				var dy = ps[t].Clone();
				// derive our first gradient
				dy[targets[t]] -= 1;
				// derivative of output bias
				dby += dy;
				// backpropagate to h1
				// backpropagate to T
				var dt = (Why * dy).SumColumn() + dhnext;
				// Compute gradient of T (Derivate of Tanh)
				var dtraw = (1 - ht[t] * ht[t]) * dt;
				// derivative of T bias
				dbt += dtraw;

				// Compute Derivates
				dWhy += ht[t].T     * dy;    // Output W
				dWtt += ht[t - 1].T * dtraw; // Temporal
				dWxt += xs[t].T     * dtraw; // Input

				// Acc to next Time
				dhnext = (dtraw.T * Wtt).SumColumn();

				NumMath.Normalize(-5, 5, ref dWxt);
				NumMath.Normalize(-5, 5, ref dWtt);
				NumMath.Normalize(-5, 5, ref dWhy);
				NumMath.Normalize(-5, 5, ref dbt);
				NumMath.Normalize(-5, 5, ref dby);
			}

			return (loss, dWxt, dWtt, dWhy, dbt, dby, ht[inputs.Length - 1]);
		}

		public (float loss, FloatArray2D dWxt, FloatArray2D dWtt, FloatArray2D dWhy, FloatArray dbh, FloatArray dby,
			FloatArray hs)
			BPTT(int[] inputs, int[] targets, FloatArray hprev)
		{
			var loss = 0f;

			var xs = new FloatArray[inputs.Length];
			var ht = new Array<FloatArray>(inputs.Length + 1);
			var ys = new FloatArray[inputs.Length];
			var ps = new Array<FloatArray>(inputs.Length);
			var tg = new FloatArray[inputs.Length];

			ht[-1] = hprev;
			for (var t = 0; t < inputs.Length; t++)
			{
				xs[t]            = new FloatArray(input_size);
				xs[t][inputs[t]] = 1;

				tg[t]             = new FloatArray(output_size);
				tg[t][targets[t]] = 1;

				(ps[t], ht[t], ys[t], xs[t]) = FeedForward(xs[t], ht[t - 1]);

				loss += -(tg[t] * ps[t].Log()).Sum();
			}

			var dWxt = new FloatArray2D(Wxt.W, Wxt.H);
			var dWtt = new FloatArray2D(Wtt.W, Wtt.H);
			var dWhy = new FloatArray2D(Why.W, Why.H);

			var dhnext = new FloatArray(ht[0].Length);

			var dbt = new FloatArray(bt.Length);
			var dby = new FloatArray(by.Length);

			for (var t = inputs.Length - 1; t >= 0; t--)
			{
				// output probabilities
				var dy = ps[t].Clone();
				// derive our first gradient
				dy[targets[t]] -= 1;
				// derivative of output bias
				dby += dy;
				// backpropagate to h1
				// backpropagate to T
				var dt = (Why * dy).SumColumn() + dhnext;
				// Compute gradient of T (Derivate of Tanh)
				var dtraw = (1 - ht[t] * ht[t]) * dt;
				// derivative of T bias
				dbt += dtraw;

				// Compute Derivates
				dWhy += ht[t].T     * dy;    // Output W
				dWtt += ht[t - 1].T * dtraw; // Temporal
				dWxt += xs[t].T     * dtraw; // Input

				// Acc to next Time
				dhnext = (dtraw.T * Wtt).SumColumn();

				NumMath.Normalize(-5, 5, ref dWxt);
				NumMath.Normalize(-5, 5, ref dWtt);
				NumMath.Normalize(-5, 5, ref dWhy);
				NumMath.Normalize(-5, 5, ref dbt);
				NumMath.Normalize(-5, 5, ref dby);
			}

			return (loss, dWxt, dWtt, dWhy, dbt, dby, ht[inputs.Length - 1]);
		}

		public (float loss, FloatArray2D dWxt, FloatArray2D dWtt, FloatArray2D dWhy, FloatArray dbh, FloatArray dby,
			FloatArray hs)
			BPTT_many_2_one(int[] inputs, int[] targets, int position, FloatArray hprev)
		{
			var loss = 0f;

			var xs = new FloatArray[inputs.Length];
			var ht = new Array<FloatArray>(inputs.Length + 1);
			var ys = new FloatArray[inputs.Length];
			var ps = new Array<FloatArray>(inputs.Length);
			var tg = new FloatArray[inputs.Length];

			ht[-1] = hprev;
			for (var t = 0; t < inputs.Length; t++)
			{
				xs[t]            = new FloatArray(input_size);
				xs[t][inputs[t]] = 1;

				(ps[t], ht[t], ys[t], xs[t]) = FeedForward(xs[t], ht[t - 1]);
			}

			tg[position]                    = new FloatArray(output_size);
			tg[position][targets[position]] = 1;

			loss = -(tg[position] * ps[position].Log()).Sum();

			var dWxt = new FloatArray2D(Wxt.W, Wxt.H);
			var dWtt = new FloatArray2D(Wtt.W, Wtt.H);
			var dWhy = new FloatArray2D(Why.W, Why.H);

			var dhnext = new FloatArray(ht[0].Length);

			var dbt = new FloatArray(bt.Length);
			var dby = new FloatArray(by.Length);

			// output probabilities
			var dy = ps[position].Clone();
			// derive our first gradient
			dy[targets[position]] -= 1;
			// derivative of output bias
			dby += dy;
			// backpropagate to h
			var dt = (Why * dy).SumColumn();

			for (var t = inputs.Length - 1; t >= 0; t--)
			{
				// Compute gradient of T (Derivate of Tanh)
				var dtraw = (1 - ht[t] * ht[t]) * dt;
				// derivative of T bias
				dbt += dtraw;

				// Compute Derivates
				dWhy += ht[t].T     * dy;    // Output W
				dWtt += ht[t - 1].T * dtraw; // Temporal
				dWxt += xs[t].T     * dtraw; // Input

				// Acc to next Time
				dhnext = (dtraw.T * Wtt).SumColumn();
				dt     = dhnext;

				NumMath.Normalize(-5, 5, ref dWxt);
				NumMath.Normalize(-5, 5, ref dWtt);
				NumMath.Normalize(-5, 5, ref dWhy);
				NumMath.Normalize(-5, 5, ref dbt);
				NumMath.Normalize(-5, 5, ref dby);
			}

			return (loss, dWxt, dWtt, dWhy, dbt, dby, ht[inputs.Length - 1]);
		}

		public (float loss, FloatArray2D dWxt, FloatArray2D dWtt, FloatArray2D dWhy, FloatArray dbh, FloatArray dby,
			FloatArray hs)
			BPTT_many_2_one(float[][] inputs, int targets, int position, FloatArray hprev)
		{
			var loss = 0f;

			var xs = new FloatArray[inputs.Length];
			var ht = new Array<FloatArray>(inputs.Length + 1);
			var ys = new FloatArray[inputs.Length];
			var ps = new Array<FloatArray>(inputs.Length);
			var tg = new FloatArray[inputs.Length];

			ht[-1] = hprev;
			for (var t = 0; t < inputs.Length; t++)
			{
				xs[t]                        = new FloatArray(inputs[t]);
				(ps[t], ht[t], ys[t], xs[t]) = FeedForward(xs[t], ht[t - 1]);
			}

			tg[position]          = new FloatArray(output_size);
			tg[position][targets] = 1;

			loss = -(tg[position] * ps[position].Log()).Sum();

			var dWxt = new FloatArray2D(Wxt.W, Wxt.H);
			var dWtt = new FloatArray2D(Wtt.W, Wtt.H);
			var dWhy = new FloatArray2D(Why.W, Why.H);

			var dhnext = new FloatArray(ht[0].Length);

			var dbt = new FloatArray(bt.Length);
			var dby = new FloatArray(by.Length);

			// output probabilities
			var dy = ps[position].Clone();
			// derive our first gradient
			dy[targets] -= 1;
			// derivative of output bias
			dby += dy;
			// backpropagate to h
			var dt = (Why * dy).SumColumn();

			for (var t = inputs.Length - 1; t >= 0; t--)
			{
				// Compute gradient of T (Derivate of Tanh)
				var dtraw = (1 - ht[t] * ht[t]) * dt;
				// derivative of T bias
				dbt += dtraw;

				// Compute Derivates
				dWtt += ht[t - 1].T * dtraw; // Temporal
				dWxt += xs[t].T     * dtraw; // Input

				// Acc to next Time
				dhnext = (dtraw.T * Wtt).SumColumn();
				dt     = dhnext;

				NumMath.Normalize(-5, 5, ref dWxt);
				NumMath.Normalize(-5, 5, ref dWtt);
				NumMath.Normalize(-5, 5, ref dbt);
			}

			dWhy += ht[inputs.Length - 1].T * dy; // Output W
			NumMath.Normalize(-5, 5, ref dWhy);
			NumMath.Normalize(-5, 5, ref dby);

			return (loss, dWxt, dWtt, dWhy, dbt, dby, ht[inputs.Length - 1]);
		}

		public void UpdateParams(FloatArray2D dWxt, FloatArray2D dWtt, FloatArray2D dWhy, FloatArray dbt, FloatArray dby)
		{
			// memoria Adagrad
			mWxt += dWxt * dWxt;
			mWtt += dWtt * dWtt;
			mWhy += dWhy * dWhy;
			mbt  += dbt  * dbt;
			mby  += dby  * dby;

			// update params
			Wxt += -learning_rate * dWxt / (mWxt + 1e-8f).Sqrt();
			Wtt += -learning_rate * dWtt / (mWtt + 1e-8f).Sqrt();
			Why += -learning_rate * dWhy / (mWhy + 1e-8f).Sqrt();
			bt  += -learning_rate * dbt  / (mbt  + 1e-8f).Sqrt();
			by  += -learning_rate * dby  / (mby  + 1e-8f).Sqrt();
		}

		public double SmoothLoss(double loss)
		{
			Smooth_loss = Smooth_loss * 0.999 + loss * 0.001;
			return Smooth_loss;
		}
	}
}
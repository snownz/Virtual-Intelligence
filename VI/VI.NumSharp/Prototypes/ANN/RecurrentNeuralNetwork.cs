using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.ANN
{
    public class RecurrentNeuralNetwork
    {
        public int input_size;
        public int hidden_size;
        public int output_size;
        public float learning_rate;

        private FloatArray2D Wxt;
        private FloatArray2D Wtt;
        private FloatArray2D Why;
        private FloatArray bt;
        private FloatArray by;

        private FloatArray2D mWxt;
        private FloatArray2D mWtt;
        private FloatArray2D mWhy;
        private FloatArray mbt;
        private FloatArray mby;

        public RecurrentNeuralNetwork(int input, int output, int hidden, float learning_rate, float std)
        {
            input_size = input;
            output_size = output;
            hidden_size = hidden;
            this.learning_rate = learning_rate;

            Wxt = NumMath.Random(hidden_size, input_size, std);
            Wtt = NumMath.Random(hidden_size, hidden_size, std);
            Why = NumMath.Random(output_size, hidden_size, std);
            bt = NumMath.Repeat(hidden_size, 0);
            by = NumMath.Repeat(output_size, 0);

            ResetAdagradParams();
        }

        public void ResetAdagradParams()
        {
            mWxt = new FloatArray2D(Wxt.W, Wxt.H);
            mWtt = new FloatArray2D(Wtt.W, Wtt.H);
            mWhy = new FloatArray2D(Why.W, Why.H);

            mbt = new FloatArray(bt.Length);
            mby = new FloatArray(by.Length);
        }

        public (FloatArray ps, FloatArray hs) FeedForward(FloatArray xs, FloatArray hprev)
        {
            var ht = ((xs.T * Wxt).SumLine() + (hprev.T * Wtt).SumLine() + bt).Tanh();

            var ys = (ht.T * Why).SumLine() + by;

            var exp = ys.Exp();
            var ps = exp / exp.Sum();

            return (ps, ht);
        }

        public (float loss, FloatArray2D dWxt, FloatArray2D dWtt, FloatArray2D dWhy, FloatArray dbh, FloatArray dby,
            FloatArray hs)
            BPTT(int[] inputs, int[] targets, FloatArray hprev)
        {
            var loss = 0f;
            var xs = new Array<FloatArray>(inputs.Length);
            var ht = new Array<FloatArray>(inputs.Length);
            var ps = new Array<FloatArray>(inputs.Length);
            var tg = new Array<FloatArray>(inputs.Length);
            ht[-1] = hprev;

            var dWxt = new FloatArray2D(Wxt.W, Wxt.H);
            var dWtt = new FloatArray2D(Wtt.W, Wtt.H);
            var dWhy = new FloatArray2D(Why.W, Why.H);
            var dhnext = new FloatArray(hidden_size);
            var dbt = new FloatArray(bt.Length);
            var dby = new FloatArray(by.Length);

            for (var t = 0; t < inputs.Length; t++)
            {
                xs[t] = new FloatArray(input_size);
                xs[t][inputs[t]] = 1;

                tg[t] = new FloatArray(output_size);
                tg[t][targets[t]] = 1;

                (ps[t], ht[t]) = FeedForward(xs[t], ht[t - 1]);
                loss += -(tg[t] * ps[t].Log()).Sum();
            }

            for (var t = inputs.Length - 1; t >= 0; t--)
            {
                // output probabilities
                var dy = ps[t].Clone();
                // derive our first gradient
                dy[targets[t]] -= 1;
                // backpropagate to
                var dt = (Why * dy).SumColumn() + dhnext;

                // Compute gradient of T (Derivate of Tanh)
                var dtraw = (1 - ht[t] * ht[t]) * dt;

                dWtt += ht[t - 1].T * dtraw; // Temporal
                dWxt += xs[t].T * dtraw; // Input
                dbt += dtraw;

                // Acc to next Time
                dhnext = (dtraw * Wtt).SumColumn();

                // Compute Derivates
                dWhy += ht[t].T * dy;
                dby += dy;
            }

            // Normalize
            dWxt = NumMath.Normalize(-5, 5, dWxt);
            dWtt = NumMath.Normalize(-5, 5, dWtt);
            dWhy = NumMath.Normalize(-5, 5, dWhy);
            dbt = NumMath.Normalize(-5, 5, dbt);
            dby = NumMath.Normalize(-5, 5, dby);

            return (loss, dWxt, dWtt, dWhy, dbt, dby, ht[inputs.Length - 1]);
        }

        public void UpdateParams(FloatArray2D dWxt, FloatArray2D dWtt, FloatArray2D dWhy, FloatArray dbt, FloatArray dby)
        {
            // Adagrad Params
            mWxt += dWxt * dWxt;
            mWtt += dWtt * dWtt;
            mWhy += dWhy * dWhy;
            mbt += dbt * dbt;
            mby += dby * dby;

            // update params
            Wxt -= ((learning_rate / (mWxt + 1e-8f).Sqrt()) * dWxt);
            Wtt -= ((learning_rate / (mWtt + 1e-8f).Sqrt()) * dWtt);
            Why -= ((learning_rate / (mWhy + 1e-8f).Sqrt()) * dWhy);
            bt -= ((learning_rate / (mbt + 1e-8f).Sqrt()) * dbt);
            by -= ((learning_rate / (mby + 1e-8f).Sqrt()) * dby);
        }
    }
}
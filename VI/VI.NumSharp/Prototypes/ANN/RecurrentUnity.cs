using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.ANN
{
    public class RecurrentUnity
    {
        public int input_size;
        public int hidden_size;
        public float learning_rate;

        private FloatArray2D Wxt;
        private FloatArray2D Wtt;
        private FloatArray bt;

        private FloatArray2D mWxt;
        private FloatArray2D mWtt;
        private FloatArray mbt;

        public RecurrentUnity(int input, int hidden, float learning_rate, float std)
        {
            input_size = input;
            hidden_size = hidden;
            this.learning_rate = learning_rate;

            Wxt = NumMath.Random(hidden_size, input_size, std);
            Wtt = NumMath.Random(hidden_size, hidden_size, std);
            bt = NumMath.Repeat(hidden_size, 0);

            ResetAdagradParams();
        }

        public void ResetAdagradParams()
        {
            mWxt = new FloatArray2D(Wxt.W, Wxt.H);
            mWtt = new FloatArray2D(Wtt.W, Wtt.H);

            mbt = new FloatArray(bt.Length);
        }

        public FloatArray FeedForward(FloatArray xs, FloatArray hprev)
        {
            var ht = ((xs.T * Wxt).SumLine() + (hprev.T * Wtt).SumLine() + bt).Tanh();

            return ht;
        }

        public (FloatArray2D dWxt, FloatArray2D dWtt, FloatArray dbh, FloatArray hs) BPTT (Array<FloatArray> inputs, FloatArray hprev, FloatArray error)
        {
            var xs = new Array<FloatArray>(inputs.Length);
            var ht = new Array<FloatArray>(inputs.Length);
            ht[-1] = hprev;

            var dWxt = new FloatArray2D(Wxt.W, Wxt.H);
            var dWtt = new FloatArray2D(Wtt.W, Wtt.H);
            var dhnext = new FloatArray(hidden_size);
            var dbt = new FloatArray(bt.Length);

            for (var t = 0; t < inputs.Length; t++)
            {
                xs[t] = inputs[t];
                ht[t] = FeedForward(xs[t], ht[t - 1]);
            }

            dhnext = error;

            for (var t = inputs.Length - 1; t >= 0; t--)
            {
                var dt = dhnext;

                // Compute gradient of T (Derivate of Tanh)
                var dtraw = (1 - ht[t] * ht[t]) * dt;

                dWtt += ht[t - 1].T * dtraw; // Temporal
                dWxt += xs[t].T * dtraw; // Input
                dbt += dtraw;

                // Acc to next Time
                dhnext = (dtraw * Wtt).SumColumn();
            }

            // Normalize
            dWxt = NumMath.Normalize(-5, 5, dWxt);
            dWtt = NumMath.Normalize(-5, 5, dWtt);
            dbt = NumMath.Normalize(-5, 5, dbt);

            return (dWxt, dWtt, dbt, ht[inputs.Length - 1]);
        }

        public void UpdateParams(FloatArray2D dWxt, FloatArray2D dWtt, FloatArray dbt)
        {
            // Adagrad Params
            mWxt += dWxt * dWxt;
            mWtt += dWtt * dWtt;
            mbt += dbt * dbt;

            // update params
            Wxt -= ((learning_rate / (mWxt + 1e-8f).Sqrt()) * dWxt);
            Wtt -= ((learning_rate / (mWtt + 1e-8f).Sqrt()) * dWtt);
            bt -= ((learning_rate / (mbt + 1e-8f).Sqrt()) * dbt);
        }
    }
}
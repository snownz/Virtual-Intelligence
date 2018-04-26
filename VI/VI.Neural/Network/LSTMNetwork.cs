using VI.Neural.LossFunction;
using VI.Neural.Models;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Network
{
    /*
    public class LSTMNetwork
    {
        public int input_size;
        public int hidden_size;
        public int output_size;
        public float learning_rate;

        private LSTMCellModel encoder;
        private DenseModel decoder;
        private ILossFunction lossFun = new CrossEntropyLossFunction();

        /// <summary>
        /// Execute Feedforward N Get All layer Results
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>All layer Results</returns>
        public (FloatArray z, FloatArray f, FloatArray i, FloatArray cell, FloatArray c, FloatArray o,
           FloatArray h, Array<FloatArray> y)
           FeedForward(FloatArray x, FloatArray hprev, FloatArray cprev)
        {
            (var z, var fGate, var iGate, var cGate, var cellState, var oGate, var hState)
                = encoder.FeedForward(x, hprev, cprev);

            var ps = decoder.FeedForward(hState);

            return (z, fGate, iGate, cGate, cellState, oGate, hState, ps);
        }

        /// <summary>
        /// Execute Feedforward N Get Output Layer Result
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>Output Layer Result</returns>
        public FloatArray Output(FloatArray xs, FloatArray hprev, FloatArray cprev)
        {
            (_, _, _, _, _, _, _, var ps) = FeedForward(xs, hprev, cprev);
            return ps[0];
        }

        /// <summary>
        /// Execute Backpropagation through time
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="targets"></param>
        /// <param name="hprev"></param>
        /// <returns></returns>
        public (float loss, Array<FloatArray2D> dwy, Array<FloatArray> dby, Array<FloatArray2D> dwh, Array<FloatArray> dbh, FloatArray h, FloatArray c)
            BPTT(int[] inputs, int[] targets, FloatArray hprev, FloatArray cprev)
        {
            var x = new Array<FloatArray>(inputs.Length);
            var z = new Array<FloatArray>(inputs.Length);
            var f = new Array<FloatArray>(inputs.Length);
            var i = new Array<FloatArray>(inputs.Length);
            var c = new Array<FloatArray>(inputs.Length);
            var o = new Array<FloatArray>(inputs.Length);
            var hs = new Array<FloatArray>(inputs.Length);
            var cs = new Array<FloatArray>(inputs.Length);
            var tg = new Array<FloatArray>(inputs.Length);
            var ps = new Array<Array<FloatArray>>(inputs.Length);

            // loss
            var loss = 0f;

            // init timing
            hs[-1] = hprev.Clone();
            cs[-1] = cprev.Clone();

            // forward
            for (var t = 0; t < inputs.Length; t++)
            {
                x[t] = new FloatArray(input_size);
                x[t][inputs[t]] = 1;

                tg[t] = new FloatArray(output_size);
                tg[t][targets[t]] = 1;

                (z[t], f[t], i[t], c[t], cs[t], o[t], hs[t], ps[t]) =
                    FeedForward(x[t], hs[t - 1], cs[t - 1]);

                loss += lossFun.Loss(tg[t], ps[t][0]);
            }

            var dcnext = NumMath.Array(hidden_size);
            var dhnext = NumMath.Array(hidden_size);

            //Backward
            var dwy = new Array<FloatArray2D>(new[] { NumMath.Array(output_size, hidden_size) });
            var dby = new Array<FloatArray>(new[] { NumMath.Array(output_size) });
            var dwh = new Array<FloatArray2D>(new[] { NumMath.Array(hidden_size, input_size + hidden_size), NumMath.Array(hidden_size, input_size + hidden_size),
                                                      NumMath.Array(hidden_size, input_size + hidden_size), NumMath.Array(hidden_size, input_size + hidden_size) });
            var dbh = new Array<FloatArray>(new[] { NumMath.Array(hidden_size), NumMath.Array(hidden_size), NumMath.Array(hidden_size), NumMath.Array(hidden_size) });

            for (var t = inputs.Length - 1; t >= 0; t--)
            {
                // Sequencial
                // Decoder Backward
                var dy = decoder.ComputeErrorNBackward(tg[t], ps[t]) + dhnext;
                // Encoder Backward
                (dhnext, dcnext) = encoder.Backward(dy, dcnext, cs[t - 1], f[t], i[t], c[t], cs[t], o[t]);

                (var wy, var by) = decoder.ComputeGradient(hs[t]);
                (var wh, var bh) = encoder.ComputeGradient(z[t]);

                dwy = dwy.Sum(wy);
                dby = dby.Sum(by);
                dwh = dwh.Sum(wh);
                dbh = dbh.Sum(bh);
            }
            // Parallel
            dwy = NumMath.Normalize(-5, 5, dwy);
            dby = NumMath.Normalize(-5, 5, dby);
            dwh = NumMath.Normalize(-5, 5, dwh);
            dbh = NumMath.Normalize(-5, 5, dbh);

            return (loss, dwy, dby, dwh, dbh, hs[inputs.Length - 1], cs[inputs.Length - 1]);
        }

        /// <summary>
        /// Update Params from network
        /// </summary>
        /// <param name="dwy"></param>
        /// <param name="dby"></param>
        /// <param name="dwh"></param>
        /// <param name="dbh"></param>
        public void UpdateParams(Array<FloatArray2D> dwy, Array<FloatArray> dby, Array<FloatArray2D> dwh, Array<FloatArray> dbh)
        {
            // Parallel
            decoder.UpdateParams(dwy, dby);
            encoder.UpdateParams(dwh, dbh);
        }
    }
    */
}
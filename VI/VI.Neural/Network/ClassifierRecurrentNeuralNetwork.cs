using VI.Neural.Factory;
using VI.Neural.LossFunction;
using VI.Neural.Models;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Network
{
    public class ClassifierRecurrentNeuralNetwork
    {
        private int input_size;
        private int hidden_size;
        private int output_size;
        private float learning_rate;
        private int recurrentUnits;
        private RecurrentCellModel encoder;
        private DenseModel decoder;

        public ClassifierRecurrentNeuralNetwork(int input, int output, int hidden, int units, float learningRate, float std, EnumOptimizerFunction opt)
        {
            encoder = new RecurrentCellModel();
            decoder = new DenseModel();

            int x = input;
            for(int i = 0; i < units; i++)
            {
                encoder.AddLayer( BuildedModels.RecurrentTanh( x, hidden, learningRate, std, opt ) );
                x = hidden;
            }
            
            decoder.AddLayer( BuildedModels.DenseSoftMax ( hidden, output, learningRate, std, opt ) );
            decoder.SetLossFunction( new CrossEntropyLossFunction() );

            input_size     = input;
            hidden_size    = hidden;
            output_size    = output;
            recurrentUnits = units;
            learning_rate  = learningRate;
        }

        /// <summary>
        /// Execute Feedforward N Get All layer Results
        /// </summary>
        /// <returns>All layer Results</returns>
        public (Array<FloatArray> hs, Array<FloatArray> ps) FeedForward(FloatArray xs, Array<FloatArray> hprev)
        {
            var hs = encoder.FeedForward(xs, hprev);
            var ps = decoder.FeedForward(hs[-1]);
            return (hs, ps);
        }

        /// <summary>
        /// Execute Feedforward N Get Output Layer Result
        /// </summary>
        /// <returns>Output Layer Result</returns>
        public (Array<FloatArray> hs, FloatArray ps) Output(FloatArray xs, Array<FloatArray> hprev)
        {
            var hs = encoder.FeedForward(xs, hprev);
            var ps = decoder.Output(hs[-1]);
            return (hs, ps);
        }

        /// <summary>
        /// Execute Backpropagation through time
        /// </summary>
        /// <returns></returns>
        public (float loss, Array<FloatArray2D> dwy, Array<FloatArray> dby, Array<Array<FloatArray2D>> dwh, Array<FloatArray> dbh, Array<FloatArray> hs) BPTT(int[] inputs, int[] targets, Array<FloatArray> hprev)
        {
            float loss = 0f;

            //Feedforward
            var xs = new Array<FloatArray>(inputs.Length); // Inputs
            var hs = new Array<Array<FloatArray>>(inputs.Length); // Hidden Result
            var ps = new Array<Array<FloatArray>>(inputs.Length); // Softmax probabilit
            var tg = new Array<FloatArray>(inputs.Length); // Targets
            hs[-1] = hprev;

            //Backward
            var dwy = new Array<FloatArray2D>( 1 );
            var dby = new Array<FloatArray>( 1 );

            var dwh = new Array<Array<FloatArray2D>>( recurrentUnits );
            var dbh = new Array<FloatArray>( recurrentUnits );
            var dhnext = new Array<FloatArray>( recurrentUnits );

            for (int t = 0; t < inputs.Length; t++)
            {
                xs[t] = new FloatArray(input_size);
                xs[t][inputs[t]] = 1;

                tg[t] = new FloatArray(output_size);
                tg[t][targets[t]] = 1;

                (hs[t], ps[t]) = FeedForward(xs[t], hs[t - 1]);
            }

            for (int t = inputs.Length - 1; t >= 0; t--)
            {
                // Sequencial
                ( var l, var dy ) = decoder.ComputeErrorNBackward( tg[t], ps[t] );
                (dhnext, _) = encoder.Backward( dy, dhnext, hs[t] );

                // Parallel
                ( var wy, var by ) = decoder.ComputeGradient( hs[t][-1] );
                ( var wh, var bh ) = encoder.ComputeGradient( xs[t], hs[t - 1] );

                // Parallel
                dwy = dwy.Sum( wy );
                dby = dby.Sum( by );
                dwh = dwh.Sum( wh );
                dbh = dbh.Sum( bh );

                loss += l;
            }

            // Parallel
            dwy = NumMath.Normalize(-5, 5, dwy);
            dby = NumMath.Normalize(-5, 5, dby);
            dwh = NumMath.Normalize(-5, 5, dwh);
            dbh = NumMath.Normalize(-5, 5, dbh);

            return (loss, dwy, dby, dwh, dbh, hs[-1]);
        }

        /// <summary>
        /// Update Params from network
        /// </summary>
        public void UpdateParams(Array<FloatArray2D> dwy, Array<FloatArray> dby, Array<Array<FloatArray2D>> dwh, Array<FloatArray> dbh)
        {
            // Parallel
            decoder.UpdateParams(dwy, dby);
            encoder.UpdateParams(dwh, dbh);
        }
    }
}
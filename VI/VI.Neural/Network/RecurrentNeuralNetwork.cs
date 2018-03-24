using VI.Neural.Factory;
using VI.Neural.LossFunction;
using VI.Neural.Models;
using VI.Neural.OptimizerFunction;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Network
{
    public class RecurrentNeuralNetwork
    {
        public int input_size;
        public int hidden_size;
        public int output_size;
        public float learning_rate;

        private RecurrentCellModel encoder;
        private DenseModel decoder;
        private ILossFunction lossFunction = new CrossEntropyLossFunction();

        public RecurrentNeuralNetwork(int input, int output, int hidden, float learningRate, float std)
        {
            var encoderLayer = BuildedModels.RecurrentTanh(input, hidden, learningRate, std, OptimizerFunctionEnum.Adagrad);
            var decoderLayer = BuildedModels.DenseSoftMax(hidden, output, learningRate, std, OptimizerFunctionEnum.Adagrad);

            encoder = new RecurrentCellModel(encoderLayer);
            decoder = new DenseModel();
            decoder.AddLayer(decoderLayer);

            input_size = input;
            hidden_size = hidden;
            output_size = output;
            learning_rate = learningRate;
        }

        /// <summary>
        /// Execute Feedforward N Get All layer Results
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>All layer Results</returns>
        public (FloatArray hs, Array<FloatArray> ps) FeedForward(FloatArray xs, FloatArray hprev)
        {
            var hs = encoder.FeedForward(xs, hprev);
            var ps = decoder.FeedForward(hs);
            return (hs, ps);
        }

        /// <summary>
        /// Execute Feedforward N Get Output Layer Result
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>Output Layer Result</returns>
        public (FloatArray hs, FloatArray ps) Output(FloatArray xs, FloatArray hprev)
        {
            var hs = encoder.FeedForward(xs, hprev);
            var ps = decoder.Output(hs);
            return (hs, ps);
        }

        /// <summary>
        /// Execute Backpropagation through time
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="targets"></param>
        /// <param name="hprev"></param>
        /// <returns></returns>
        public (float loss, Array<FloatArray2D> dwy, Array<FloatArray> dby, Array<FloatArray2D> dwh, FloatArray dbh, FloatArray hs)
            BPTT(int[] inputs, int[] targets, FloatArray hprev)
        {
            float loss = 0f;
            
            //Feedforward
            var xs = new Array<FloatArray>(inputs.Length); // Inputs
            var hs = new Array<FloatArray>(inputs.Length); // Hidden Result
            var ps = new Array<Array<FloatArray>>(inputs.Length); // Softmax probabilit
            var tg = new Array<FloatArray>(inputs.Length); // Targets
            hs[-1] = hprev;
            
            //Backward
            var dwy = new Array<FloatArray2D>(new[] { NumMath.Array(output_size, hidden_size) });
            var dby = new Array<FloatArray>(new[] { NumMath.Array(output_size) });
            var dwh = new Array<FloatArray2D>(new[] { NumMath.Array(hidden_size, input_size), NumMath.Array(hidden_size, hidden_size) });
            var dbh = new FloatArray(hidden_size);
            var dhnext = new FloatArray(hidden_size);
            
            for (int t = 0; t < inputs.Length; t++)
            {
                xs[t] = new FloatArray(input_size);
                xs[t][inputs[t]] = 1;
            
                tg[t] = new FloatArray(output_size);
                tg[t][targets[t]] = 1;
            
                (hs[t], ps[t]) = FeedForward(xs[t], hs[t - 1]);
                
                loss += lossFunction.Loss(tg[t], ps[t][0]);
            }
            
            for (int t = inputs.Length - 1; t >= 0; t--)
            {
                // Sequencial
                var dy = decoder.ComputeErrorNBackward(tg[t], ps[t]);
                dhnext = encoder.Backward(dy, dhnext, hs[t]);
            
                // Parallel
                (var wy, var by) = decoder.ComputeGradient(hs[t]);
                (var wh, var bh) = encoder.ComputeGradient(xs[t], hs[t - 1]);
            
                // Parallel
                dwy = dwy.Sum(wy);
                dby = dby.Sum(by);
                dwh = dwh.Sum(wh);
                dbh += bh;
            }
            
            // Parallel
            dwy = NumMath.Normalize(-5, 5, dwy);
            dby = NumMath.Normalize(-5, 5, dby);
            dwh = NumMath.Normalize(-5, 5, dwh);
            dbh = NumMath.Normalize(-5, 5, dbh);
            
            return (loss, dwy, dby, dwh, dbh, hs[inputs.Length - 1]);
        }
        
        /// <summary>
        /// Update Params from network
        /// </summary>
        /// <param name="dwy"></param>
        /// <param name="dby"></param>
        /// <param name="dwh"></param>
        /// <param name="dbh"></param>
        public void UpdateParams(Array<FloatArray2D> dwy, Array<FloatArray> dby, Array<FloatArray2D> dwh, FloatArray dbh)
        {
            // Parallel
            decoder.UpdateParams(dwy, dby);
            encoder.UpdateParams(dwh, dbh);
        }              
    }
}

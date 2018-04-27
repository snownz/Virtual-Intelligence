using System.Threading.Tasks;
using VI.Neural.extension;
using VI.Neural.LossFunction;
using VI.Neural.Node;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Models
{
    public class DenseModel
    {
        private Array<INeuron> w;

        private ILossFunction _lf;

        public int[] Len => w.Len();

        public DenseModel(Array<INeuron> layers)
        {
            w = layers;
        }

        public float Learn(FloatArray inputs, FloatArray target)
        {
            ( var error, _ ) =  ComputeErrorNBackward( inputs, target );
            ( var dw, var db ) = ComputeGradient( inputs );
            UpdateParams( dw, db );

            return error;
        }

        public DenseModel()
        {
            w = new Array<INeuron>(0);
        }

        public void SetLossFunction(ILossFunction lf)
        {
            _lf = lf;
        }

        /// <summary>
        /// Include a new Layer on the model
        /// </summary>
        /// <param name="layer"></param>
        public void AddLayer(INeuron layer)
        {
            w = w.Join(layer);
        }

        /// <summary>
        /// Execute Feedforward N Get All layer Results
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>All layer Results</returns>
        public Array<FloatArray> FeedForward(FloatArray inputs)
        {
            var result = new Array<FloatArray>(w.Length);
            result[-1] = inputs;

            for (int i = 0; i < w.Length; i++)
            {
                result[i] = w[i].FeedForward(result[i - 1]);
            }

            return result;
        }

        /// <summary>
        /// Execute Feedforward N Get Output Layer Result
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>Output Layer Result</returns>
        public FloatArray Output(FloatArray inputs)
        {
            return FeedForward(inputs)[- 1];
        }

        /// <summary>
        /// Compute error for linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public (float loss, FloatArray dw) ComputeErrorNBackward(FloatArray inputs, FloatArray target)
        {
            var result = FeedForward(inputs);

            var dw = w[w.Length - 1].ComputeErrorNBackWard(target);

            for (int i = w.Length - 2; i >= 0; i--)
            {
                dw = w[i].BackWard(dw);
            }

            return ( _lf.Loss(target, result[-1]),  dw );
        }

        
        /// <summary>
        /// Compute error for linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public (float loss, FloatArray dw) ComputeErrorNBackward(FloatArray target, Array<FloatArray> output)
        {
            w.SetOutputs(output);
            var dw = w[-1].ComputeErrorNBackWard(target);

            for (int i = w.Length - 2; i >= 0; i--)
            {
                dw = w[i].BackWard(dw);
            }

           return ( _lf.Loss(target, output[-1]),  dw );
        }

        /// <summary>
        /// Backward for linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public FloatArray Backward(FloatArray inputs, FloatArray error)
        {
            FeedForward(inputs);

            var dw = new FloatArray(0);

            for (int i = w.Length - 1; i >= 0; i--)
            {
                dw = w[i].BackWard(dw);
            }

            return dw;
        }

        /// <summary>
        /// Backward for non-linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public FloatArray Backward(FloatArray error, Array<FloatArray> output)
        {
            w.SetOutputs(output);
            var dw = new FloatArray(0);

            for (int i = w.Length - 1; i >= 0; i--)
            {
                dw = w[i].BackWard(dw);
            }

            return dw;
        }

        /// <summary>
        /// Compute All layer Gradients
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public (Array<FloatArray2D> dw, Array<FloatArray> db) ComputeGradient(FloatArray input)
        {
            w[0].ComputeGradient(input);

            for (int i = 1; i <  w.Length ; i++)
            {
                w[i].ComputeGradient(w[i - 1].Nodes.OutputVector);
            }
             
            return (w.GetWeightsGradient(), w.GetBiasGradient());
        }

        /// <summary>
        /// Update Params from all layers
        /// </summary>
        /// <param name="dw"></param>
        /// <param name="db"></param>
        public void UpdateParams(Array<FloatArray2D> dw, Array<FloatArray> db)
        {
            for (int i = 0; i <  w.Length ; i++)
            {
                w[i].UpdateParams(dw[i], db[i]);
            }
        }
    }
}
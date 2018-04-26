using VI.Neural.extension;
using VI.Neural.Node;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Models
{
    public class RecurrentCellModel
    {
        private Array<IMultipleNeuron> w;

        public RecurrentCellModel()
        {
             w = new Array<IMultipleNeuron>(0);
        }

        
        /// <summary>
        /// Include a new Layer on the model
        /// </summary>
        /// <param name="layer"></param>
        public void AddLayer(IMultipleNeuron layer)
        {
            w = w.Join(layer);
        }

        public Array<FloatArray> FeedForward(FloatArray inputs, Array<FloatArray> hprev)
        {
            var result = new Array<FloatArray>(w.Length);
            result[-1] = inputs;

            for (int i = 0; i < w.Length; i++)
            {
                result[i] = w[i].FeedForward( result[i - 1].Join( hprev[i - 1] ) );
            }

            return result;
        }

        /// <summary>
        /// Backward for non-linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Array<FloatArray> Backward(FloatArray error, Array<FloatArray> hnext, Array<FloatArray> output)
        {
            w.SetOutputs(output);
            
            var dw = new Array<FloatArray>(w.Length) {[-1] = error + hnext[-1] };             

            for (int i = w.Length - 1; i >= 0; i--)
            {
                dw[i - 1] = w[i].BackWard(dw[i])[1] + hnext[i];
            }

            return dw;
        }

        /// <summary>
        /// Compute Cell gradient
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="hprev"></param>
        /// <returns></returns>
        public (Array<Array<FloatArray2D>> dw, Array<FloatArray> db) ComputeGradient(FloatArray inputs, Array<FloatArray> hprev)
        {
            w[0].ComputeGradient(inputs.Join(hprev[0]));

            for (int i = 1; i <  w.Length ; i++)
            {
                w[i].ComputeGradient( w[i - 1].Nodes.OutputVector.Join( hprev[i] ) );
            }
             
            return (w.GetWeightsGradient(), w.GetBiasGradient());
        }

        /// <summary>
        /// Update Cell Params
        /// </summary>
        /// <param name="dw"></param>
        /// <param name="db"></param>
        public void UpdateParams(Array<Array<FloatArray2D>> dw, Array<FloatArray> db)
        {
            for (int i = 0; i < w.Length; i++)
            {
                w[i].UpdateParams(dw[i], db[i]);
            }
        }
    }
}
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
                result[i] = w[i].FeedForward( result[i - 1].Join( hprev[i] ) );
            }

            return result;
        }

        /// <summary>
        /// Backward for non-linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public (Array<FloatArray> hprev, FloatArray backward) Backward(FloatArray error, Array<FloatArray> hprev, Array<FloatArray> output)
        {
            w.SetOutputs(output);

            var bHprev = new Array<FloatArray>(output.Length);

            for (int i = w.Length - 1; i >= 0; i--)
            {
                //TODO error + hprev[i] inside BackWard
                var backward = w[i].BackWard( error + hprev[i] );                
                ( error, bHprev[i] ) = ( backward[0], backward[1] );
                
                //System.Console.WriteLine($"B1: {backward[0].Length} -- B2: {backward[1].Length}");
                //throw new System.Exception();      
            }

            return (bHprev, error);
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
using VI.Neural.Node;
using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.Models
{
    public class RecurrentCellModel
    {
        private IMultipleNeuron w;
        
        public RecurrentCellModel(IMultipleNeuron layer)
        {
            w = layer;
        }

        public FloatArray FeedForward(FloatArray inputs, FloatArray hprev)
        {
            return w.FeedForward(inputs.Join(hprev));
        }
        
        /// <summary>
        /// Backward for non-linear timing
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public FloatArray Backward(FloatArray error, FloatArray hnext, FloatArray output)
        {
            w.Output = output;
            var dt = error + hnext;
            return w.BackWard(dt)[1];
        }

        /// <summary>
        /// Compute Cell gradient
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="hprev"></param>
        /// <returns></returns>
        public (Array<FloatArray2D> dw, FloatArray db) ComputeGradient(FloatArray inputs, FloatArray hprev)
        {
            w.ComputeGradient(inputs.Join(hprev));
            return (w.WGradients, w.BGradients);
        }

        /// <summary>
        /// Update Cell Params
        /// </summary>
        /// <param name="dw"></param>
        /// <param name="db"></param>
        public void UpdateParams(Array<FloatArray2D> dw, FloatArray db)
        {
            w.UpdateParams(dw, db);
        }
    }
}

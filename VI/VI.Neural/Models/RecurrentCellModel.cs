using VI.Neural.Node;
using VI.NumSharp.Arrays;

namespace VI.Neural.Models
{
    public class RecurrentCellModel
    {
        private INeuron W;

        public RecurrentCellModel(INeuron layer)
        {
            W = layer;
        }

        public FloatArray FeedForward(FloatArray inputs, FloatArray hprev)
        {
            return W.FeedForward(inputs.Join(hprev));
        }

        public FloatArray Backward(FloatArray error, FloatArray hnext)
        {
            var dt = error + hnext;
            return W.BackWard(dt);
        }

        public (FloatArray2D dw, FloatArray db) ComputeGradient(FloatArray inputs, FloatArray hprev)
        {
            W.ComputeGradient(inputs.Join(hprev));
            return (W.WGradients, W.BGradients);
        }

        public void UpdateParams(FloatArray2D dw, FloatArray db)
        {
            W.UpdateParams(dw, db);
        }
    }
}

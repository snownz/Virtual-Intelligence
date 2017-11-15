using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
    public class SquareLossFunction : ILossFunction
    {
        
        public float Loss(Array2D<float> targets, Array2D<float> prediction)
        {
            var sum = 0.0f;
            var dif = targets - prediction;
            throw new System.NotImplementedException();
        }
    }
}
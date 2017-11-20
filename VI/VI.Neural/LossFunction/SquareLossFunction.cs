using VI.NumSharp;
using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
    public class SquareLossFunction : ILossFunction
    {
        
        public float Loss(Array<float> targets, Array<float> prediction)
        {
            var dif = targets - prediction;
            var sum = NumMath.Sum(dif);
            return .5f * (sum / targets.View.Length);
        }

        public float Loss(Array<float> targets, float[] prediction)
        {
            using (var p = new Array<float>(prediction))
            {
                var dif = targets - p;
                var sum = NumMath.Sum(dif);
                return .5f * (sum / targets.View.Length);
            }
        }
    }
}
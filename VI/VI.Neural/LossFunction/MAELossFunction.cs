using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
    public class MAELossFunction : ILossFunction
    {
        public float Loss(FloatArray targets, FloatArray prediction)
        {
            return 1f / targets.Length * (prediction - targets).Sum();
        }

        public float Loss(float[] targets, FloatArray prediction)
        {
            using (var t = new FloatArray(targets))
            {
                return Loss(t, prediction);
            }
        }
    }
}
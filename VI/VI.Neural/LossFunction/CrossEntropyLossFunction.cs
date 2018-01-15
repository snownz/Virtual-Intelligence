using VI.NumSharp.Arrays;

namespace VI.Neural.LossFunction
{
    /// <summary>
    /// https://rdipietro.github.io/friendly-intro-to-cross-entropy-loss/
    /// </summary>
    public class CrossEntropyLossFunction : ILossFunction
    {
        public float Loss(Array<float> targets, Array<float> prediction)
        {
            return - (targets * prediction.Log()).Sum();
        }

        public float Loss(float[] targets, Array<float> prediction)
        {
            using (var t = new Array<float>(targets))
            {
                return Loss(t, prediction);
            }
        }
    }
}

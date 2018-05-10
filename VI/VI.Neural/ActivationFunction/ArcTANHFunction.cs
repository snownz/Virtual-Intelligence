using VI.Neural.Drivers.Executors;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class ArcTANHFunction : IActivationFunction
    {
        public FloatArray Activate(FloatArray sum)
        {
            return sum.Tanh().Pow(-1);
            //return ProcessingDriver.Activation.ArcTANH(sum);
        }

        public FloatArray Derivate(FloatArray sum, FloatArray act)
        {
            return 1 / (sum * sum + 1f);
            //return ProcessingDriver.Activation.DArcTANH(sum);
        }
    }
}
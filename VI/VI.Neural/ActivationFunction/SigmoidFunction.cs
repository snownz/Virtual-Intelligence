using VI.Neural.Drivers.Executors;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class SigmoidFunction : IActivationFunction
    {
        private const float Alpha = 2f;

        public FloatArray Activate(FloatArray sum)
        {
            return 1f / (1f + (-Alpha * sum).Exp());
            //return ProcessingDriver.Activation.Sigmoid(sum);
        }

        public FloatArray Derivate(FloatArray sum, FloatArray act)
        {
            return Alpha * act * (1f - act);
            //return ProcessingDriver.Activation.DSigmoid(act);
        }
    }
}
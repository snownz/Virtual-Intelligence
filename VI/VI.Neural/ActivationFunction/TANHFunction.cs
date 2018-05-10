using VI.Neural.Drivers.Executors;
using VI.NumSharp.Arrays;

namespace VI.Neural.ActivationFunction
{
    public class TANHFunction : IActivationFunction
    {
        public FloatArray Activate(FloatArray sum)
        {
            return sum.Tanh();
        }

        public FloatArray Derivate(FloatArray sum, FloatArray act)
        {
            return (1f - act * act);
            //return ProcessingDriver.Activation.DTANH(act);
        }
    }
}
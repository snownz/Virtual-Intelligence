using VI.NumSharp.Arrays;

namespace VI.NumSharp.Prototypes.ANN
{
    public static class ActivationFunctions
    {
        public static FloatArray Sigmoid(FloatArray x)
        {
            return 1 / (1 + (-1 * x).Exp());
        }
        public static FloatArray Dsigmoid(FloatArray y)
        {
            return y * (1 - y);
        }
        public static FloatArray Tanh(FloatArray x)
        {
            return x.Tanh();
        }
        public static FloatArray Dtanh(FloatArray y)
        {
            return 1 - y * y;
        }
        public static FloatArray Relu(FloatArray x)
        {
            return ((x >= 0) * x) + .001f;
        }
        public static FloatArray Drelu(FloatArray y)
        {
            return ((y >= 0) + .001f) * y;
        }
    }
}

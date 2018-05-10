using VI.NumSharp.Arrays;

namespace VI.Neural.Drivers.Executors
{
    public interface IActivationExecutor
    {
        FloatArray Sigmoid(FloatArray v);
        FloatArray DSigmoid(FloatArray v);

        FloatArray ArcTANH(FloatArray v);
        FloatArray DArcTANH(FloatArray v);

        FloatArray DTANH(FloatArray v);

        FloatArray LeakRelu(FloatArray v);
        FloatArray DLeakRelu(FloatArray v);

        FloatArray Elu(FloatArray v);
        FloatArray DElu(FloatArray v);
    } 
}
using VI.NumSharp.Arrays;

namespace VI.Neural.Error
{
    public class DenseErrorFunction : IErrorFunction
    {
        public Array<float> Error(Array<float> targetOutputVector, Array<float> values)
        {
            return values;
        }
    }
}
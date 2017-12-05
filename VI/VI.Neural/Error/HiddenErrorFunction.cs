using VI.NumSharp.Arrays;

namespace VI.Neural.Error
{
    public class HiddenErrorFunction : IErrorFunction
    {
        public Array<float> Error(Array<float> targetOutputVector, Array<float> values)
        {
            return values;
        }
    }
}
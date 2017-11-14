using VI.NumSharp.Arrays;

namespace VI.Neural.Error
{
    public interface IErrorFunction
    {
        Array<float> Error(Array<float> targetOutputVector, Array<float> values);
    }
}
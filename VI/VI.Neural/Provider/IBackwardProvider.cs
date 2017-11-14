using VI.NumSharp.Arrays;

namespace VI.Neural.Provider
{
    public interface IBackwardProvider
    {
        Array<float> Error(Array<float> targetOutputVector, Array<float> values);
    }
}
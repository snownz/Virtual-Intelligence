using VI.NumSharp.Arrays;

namespace VI.Neural.Provider
{
    public interface ILossFunctionProvider
    {
        float Loss(Array<float> desired);
    }
}
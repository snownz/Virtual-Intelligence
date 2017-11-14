using VI.NumSharp.Arrays;

namespace VI.Neural.Provider
{
    public interface IActivationFunctionProvider
    {
        void Activation(Array<float> vSource, Array<float> vTarget);
        Array<float> Derivated(Array<float> vSource);
    }
}
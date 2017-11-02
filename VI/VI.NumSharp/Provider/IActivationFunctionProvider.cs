using VI.NumSharp.Array;

namespace VI.NumSharp.Provider
{
    public interface IActivationFunctionProvider
    {
        void Activation(Array<float> vSource, Array<float> vTarget);
        Array<float> Derivated(Array<float> vSource);
    }
}
using VI.NumSharp.Array;

namespace VI.NumSharp.Provider
{
    public interface ILossFunctionProvider
    {
        Array<float> Error(Array<float> v0, Array<float> v1);
        float Loss();
    }
}
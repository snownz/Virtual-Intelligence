using VI.NumSharp.Arrays;

namespace VI.Neural.Node
{
    public interface ISupervisedLearning
    {
        Array<float> Learn(float[] inputs, Array<float> error);
        Array<float> Learn(Array<float> inputs, float[] error);
        Array<float> Learn(Array<float> inputs, Array<float> error);
    }
}
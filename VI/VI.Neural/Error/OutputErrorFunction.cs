using VI.NumSharp.Arrays;

namespace VI.Neural.Error
{
    public class OutputErrorFunction: IErrorFunction
    {
        public Array<float> Error(Array<float> targetOutputVector, Array<float> values)
        {
            return values - targetOutputVector;
        }
    }
}
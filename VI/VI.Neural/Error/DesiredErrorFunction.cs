using VI.NumSharp.Arrays;

namespace VI.Neural.Error
{
    public class DesiredErrorFunction: IErrorFunction
    {
        public Array<float> Error(Array<float> targetOutputVector, Array<float> values)
        {
            return values - targetOutputVector;
        }
    }
}
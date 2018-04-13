using System.Collections.Generic;

namespace VI.NumSharp.Arrays
{
    public interface IArray
    {
        int Length { get; }
        FloatArrayT T { get; }

        List<float> ToList();

        float[] ToArray();

        FloatArray Clone();
    }
}
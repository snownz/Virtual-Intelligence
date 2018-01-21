using ILGPU;
using VI.NumSharp.Arrays;
using VI.Vision.Array;
using VI.Vision.SpaceColor;

namespace VI.Vision
{
    public static class CollorMath
    {
        public static ArrayColor2D<HSV> ReversedHeatMap(Index2 size, int w, Array<float> data, float maxValue)
        {
            var mem = Allocate<HSV>(size);
            ImageProcessingDevice
                .ArrayDevice
                .Executor["reversed_heat_map"]
                .Launch(size, w, mem.View, data.View, maxValue);
            ImageProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static ArrayColor2D<T> Allocate<T>(Index2 size)
            where T : struct
        {
            var mem = ImageProcessingDevice.ArrayDevice.Executor.CreateBuffer<T>(size);
            return new ArrayColor2D<T>(mem);
        }
    }
}

using ILGPU;
using VI.NumSharp.Arrays;
using VI.Vision.Array;
using VI.Vision.SpaceColor;

namespace VI.Vision
{
    public static class ColorMath
    {
        public static ArrayColor2D<HSV> ReversedHeatMap(int w, int h, Array2D<float> data, float maxValue)
        {
            var size = new Index2(w, h);
            var mem = Allocate<HSV>(size);
            ImageProcessingDevice
                .ArrayDevice
                .Executor["reversed_heat_map"]
                .Launch(size, mem.View, data.View, maxValue);
            ImageProcessingDevice.ArrayDevice.Executor.Wait();
            return mem;
        }

        public static ArrayColor2D<RGB> HSV2RGB(ArrayColor2D<HSV> data)
        {
            var size = new Index2(data.View.Width, data.View.Height);
            var mem = Allocate<RGB>(size);
            ImageProcessingDevice
                .ArrayDevice
                .Executor["hsv_2_rgb"]
                .Launch(size, mem.View, data.View);
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

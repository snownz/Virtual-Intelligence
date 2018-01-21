using ILGPU;
using VI.Vision.SpaceColor;

namespace VI.Vision.Array
{
    public class CollorArrayOperations
    {
        public static void reversed_heat_map(Index2 size, int w, ArrayView2D<HSV> output, ArrayView<float> data, float max)
        {
            var x = size.X;
            var y = size.Y;

            var point_projected = y * w + x;

            var c = (270f * data[point_projected]) / max;
            var c1 = 1f - (data[point_projected] / max);

            var color = new HSV();
            color.Hue = c;
            color.Saturation = c1;
            color.Value = c1;

            output[x, y] = color;
        }
    }
}

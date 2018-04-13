using ILGPU;

namespace VI.NumSharp.Drivers
{
    public interface IFloatData
    {
        ArrayView<float> MemoryView { get; }
        float this[int x] { get; set; }
        float[] View { get; set; }
        int[] AxesX { get; }

        float[] AsArray();

        int Length { get; }

        float[] Clone();
    }
}
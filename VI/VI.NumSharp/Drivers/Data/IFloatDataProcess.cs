namespace VI.NumSharp.Drivers.Data
{
    public interface IFloatDataProcess
    {
        IFloatData New(int size);

        IFloatData New(float[] data);

        IFloatData2D New(int w, int h);

        IFloatData2D New(float[,] data);
    }
}
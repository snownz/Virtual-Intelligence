namespace VI.NumSharp.Drivers.Data
{
	public interface IDoubleDataProcess
	{
		IDoubleData New(int size);
		IDoubleData New(double[] data);
		IDoubleData2D New(int w, int h);
		IDoubleData2D New(double[,] data);
	}
}
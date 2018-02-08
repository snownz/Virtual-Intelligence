namespace VI.NumSharp.Drivers.Data
{
	public interface IByteDataProcess
	{
		IByteData   New(int      size);
		IByteData New(byte[]  data);
		IByteData2D New(int      w, int h);
		IByteData2D New(byte[,] data);
	}
}